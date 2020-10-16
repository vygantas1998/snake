using Newtonsoft.Json.Linq;
using Snake.Objects;
using Snake.Objects.PowerUps;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Text.Json;
using System.Threading;

namespace ServerApp
{
    public class MapObserver : IObserver<JObject>
    {
        Socket Client;
        List<SnakeBody> Snakes;
        List<PowerUp> PowerUps;

        public MapObserver(Socket client, List<SnakeBody> snakes, List<PowerUp> powerUps)
        {
            Client = client;
            Snakes = snakes;
            PowerUps = powerUps;
        }
        public void Update(JObject data)
        {
            if (data.ContainsKey("generatePowerUp"))
            {
                PowerUp powerUp = PowerUps[PowerUps.Count - 1];

                JObject powerUpObj = new JObject();
                JObject addPowerUp = new JObject();
                addPowerUp["x"] = powerUp.X;
                addPowerUp["y"] = powerUp.Y;
                addPowerUp["isBuff"] = (powerUp is SpeedUp || powerUp is SizeUp) ? true : false;
                addPowerUp["powerUpType"] = powerUp is Snake.Objects.PowerUps.Size ? 0 : 1;
                powerUpObj["powerUp"] = addPowerUp;
                SendClientMessage(powerUpObj.ToString());
            }
            if (data.ContainsKey("addSnake"))
            {
                JObject snak = new JObject();
                snak["syncSnakes"] = JObject.Parse("{\"snakes\":" + JsonSerializer.Serialize(Snakes) + "}")["snakes"];
                SendClientMessage(snak.ToString());
            } else
            {
                SendClientMessage(data.ToString());
            }
        }
        public void SendClientMessage(string msg)
        {
            msg = msg.Replace("\n", "").Replace("\r", "");
            NetworkStream ns = new NetworkStream(Client);
            StreamWriter sw = new StreamWriter(ns);
            sw.WriteLine(msg);
            sw.Flush();
        }
    }
    class SnakeServer
    {
        List<Socket> clients = new List<Socket>();
        IPAddress ipAddr = Array.FindAll(Dns.GetHostEntry(string.Empty).AddressList, a => a.AddressFamily == AddressFamily.InterNetwork)[0];
        int Port = 3000;
        Socket listener = new Socket(AddressFamily.InterNetwork,
                SocketType.Stream, ProtocolType.Tcp);
        int snakeId = 0;
        List<Thread> threads = new List<Thread>();
        List<SnakeBody> snakes = new List<SnakeBody>();
        List<PowerUp> powerUps = new List<PowerUp>();

        Observable<JObject> mapObservable = new Observable<JObject>();

        public SnakeServer()
        {
        }

        public void ExecuteServer()
        {
            IPEndPoint localEndPoint = new IPEndPoint(ipAddr, Port);
            Socket listener = new Socket(ipAddr.AddressFamily,
                         SocketType.Stream, ProtocolType.Tcp);
            try
            {
                listener.Bind(localEndPoint);
                listener.Listen(10);
                Console.WriteLine("Waiting connection ... ");
                Console.WriteLine(String.Format("Server ip: {0}:{1}", ipAddr, Port));
                Thread t = new Thread(new ThreadStart(() => WaitForMessage(listener)));
                threads.Add(t);
                t.Start();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }
        public void WaitForMessage(Socket listener)
        {
            Socket clientSocket = listener.Accept();
            MapObserver mapObserver = new MapObserver(clientSocket, snakes,powerUps);
            mapObservable.Register(mapObserver);

            clients.Add(clientSocket);

            Thread t = new Thread(new ThreadStart(() => WaitForMessage(listener)));
            threads.Add(t);
            t.Start();
            JObject server = new JObject();
            server["serverAddress"] = clientSocket.LocalEndPoint.ToString();
            server["snakeId"] = snakeId++;
            SendClientMessage(clientSocket, server.ToString());
            Console.WriteLine(String.Format("ClientConnected -> {0}", clientSocket.RemoteEndPoint));

            byte[] bytes = new Byte[1024];

            while (true)
            {
                try
                {
                    NetworkStream ns = new NetworkStream(clientSocket);
                    StreamReader sw = new StreamReader(ns);
                    string dat = sw.ReadLine();

                    JObject dataPairs = JObject.Parse(dat);

                    if (dataPairs.ContainsKey("generatePowerUp"))
                    {
                        Random rnd = new Random();
                        powerUps.Add(new SizeUp(rnd.Next(0, 1000/16), rnd.Next(0, 969 / 16)));
                    }
                   if (dataPairs.ContainsKey("addSnake"))
                    {
                        snakes.Add(JsonSerializer.Deserialize<SnakeBody>(dataPairs["addSnake"].ToString()));
                    }
                    ProcessData(dataPairs);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.ToString());
                    break;
                }
            }
        }
        public void ProcessData(JObject dataPairs)
        {
            mapObservable.Subject = dataPairs;
        }
        public void ShutDownServer()
        {
            foreach (Socket client in clients)
            {
                client.Shutdown(SocketShutdown.Both);
                client.Close();
            }
            listener.Close();
        }
        public void SendClientMessage(Socket clientSocket, string msg)
        {
            msg = msg.Replace("\n", "").Replace("\r", "");
            NetworkStream ns = new NetworkStream(clientSocket);
            StreamWriter sw = new StreamWriter(ns);
            sw.WriteLine(msg);
            sw.Flush();
        }
    }
}
