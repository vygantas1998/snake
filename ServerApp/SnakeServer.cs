using Newtonsoft.Json.Linq;
using Snake;
using Snake.Objects;
using Snake.Objects.Levels;
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
        Map Map;

        public MapObserver(Socket client, Map map)
        {
            Client = client;
            Map = map;
        }
        public void Update(JObject data)
        {
            if (data.ContainsKey("generatePowerUp"))
            {
                PowerUp powerUp = Map.PowerUps[Map.PowerUps.Count - 1];
                SendClientMessage(powerUp.toJSON().ToString());
            }
            else if (data.ContainsKey("addSnake"))
            {
                JObject snak = new JObject();
                snak["syncSnakes"] = JObject.Parse("{\"snakes\":" + JsonSerializer.Serialize(Map.Snakes) + "}")["snakes"];
                SendClientMessage(snak.ToString());
            } else
            {
                SendClientMessage(data.ToString());
            }
        }
        public void SendClientMessage(string msg)
        {
            msg = SnakeServer.RemoveLineSymbols(msg);
            NetworkStream ns = new NetworkStream(Client);
            StreamWriter sw = new StreamWriter(ns);
            sw.WriteLine(msg);
            sw.Flush();
        }
        
    }
    class SnakeServer
    {
        List<Socket> clients = new List<Socket>();
        IPAddress[] allIps = Array.FindAll(Dns.GetHostEntry(string.Empty).AddressList, a => a.AddressFamily == AddressFamily.InterNetwork);
        int Port = 3000;
        Socket listener = new Socket(AddressFamily.InterNetwork,
                SocketType.Stream, ProtocolType.Tcp);
        int snakeId = 0;
        List<Thread> threads = new List<Thread>();
        Map map = new Map();

        Observable<JObject> mapObservable = new Observable<JObject>();

        public SnakeServer()
        {
        }

        public void ExecuteServer()
        {
            IPAddress ipAddr = allIps[allIps.Length-1];
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
            MapObserver mapObserver = new MapObserver(clientSocket, map);
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
                        map.PowerUps.Add(map.Level.generatePowerUp(map.Height, map.Width));
                    }
                    if (dataPairs.ContainsKey("addSnake"))
                    {
                        map.Snakes.Add(JsonSerializer.Deserialize<SnakeBody>(dataPairs["addSnake"].ToString()));
                    }
                    if (dataPairs.ContainsKey("gameStart"))
                    {
                        map.Level = LevelFactory.CreateLevel(dataPairs["level"].ToString());
                        map.Width = map.Width > int.Parse(dataPairs["mapWidth"].ToString()) ? int.Parse(dataPairs["mapWidth"].ToString()) : map.Width;
                        map.Height = map.Height > int.Parse(dataPairs["mapHeight"].ToString()) ? int.Parse(dataPairs["mapHeight"].ToString()) : map.Height;
                    }
                    if (dataPairs.ContainsKey("sayCommand"))
                    {
                        Console.WriteLine(dataPairs["sayCommand"]);
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
        public void SendClientMessage(Socket clientSocket, string msg)
        {
            msg = RemoveLineSymbols(msg);
            NetworkStream ns = new NetworkStream(clientSocket);
            StreamWriter sw = new StreamWriter(ns);
            sw.WriteLine(msg);
            sw.Flush();
        }
        public static string RemoveLineSymbols(string str)
        {
            return str.Replace("\n", "").Replace("\r", "");
        }
    }
}
