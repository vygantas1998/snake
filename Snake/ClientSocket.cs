// A C# program for Client 
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using ServerApp.Objects;
using Snake.Objects;
using Snake.Objects.PowerUps;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Windows.Forms;

namespace Snake
{
    public class ClientSocket
    {
        Socket sender;
        List<Thread> threads;
        Map map;
        RichTextBox console;
        Form1 form;
        public ClientSocket(string ip, string port, RichTextBox con, Form1 Form)
        {
            console = con;
            map = Map.GetInstance;
            threads = new List<Thread>();
            form = Form;
            try
            {
                IPHostEntry ipHost = Dns.GetHostEntry(Dns.GetHostName());
                IPAddress ipAddr = IPAddress.Parse(ip);
                IPEndPoint localEndPoint = new IPEndPoint(ipAddr, int.Parse(port));

                sender = new Socket(ipAddr.AddressFamily,
                           SocketType.Stream, ProtocolType.Tcp);
                try
                {
                    sender.Connect(localEndPoint);
                    Thread t = new Thread(new ThreadStart(() => WaitForMessage()));
                    threads.Add(t);
                    t.Start();
                }
                catch (ArgumentNullException ane)
                {
                    Console.WriteLine("ArgumentNullException : {0}", ane.ToString());
                }
                catch (SocketException se)
                {
                    Console.WriteLine("SocketException : {0}", se.ToString());
                }
                catch (Exception e)
                {
                    Console.WriteLine("Unexpected exception : {0}", e.ToString());
                }
            }

            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }
        public void SendMessage(string message)
        {
            message = message.Replace("\n", "").Replace("\r", "");
            NetworkStream ns = new NetworkStream(sender);
            StreamWriter sw = new StreamWriter(ns);
            sw.WriteLine(message);
            sw.Flush();
        }

        public void WaitForMessage()
        {
            byte[] messageReceived = new byte[1024];
            while (true)
            {
                try
                {
                    NetworkStream ns = new NetworkStream(sender);
                    StreamReader sw = new StreamReader(ns);
                    string message = sw.ReadLine();
                    //AddMessage(message);

                    JObject data = JObject.Parse(message);

                    ProccessData(data);
                } catch (Exception e)
                {
                    AddMessage(e.ToString());
                    break;
                }
            }
        }

        public void AddMessage(string item)
        {
            if (console.InvokeRequired)
            {
                console.Invoke(new MethodInvoker(delegate
                {
                    console.Text += item + "\n";
                }));
            }
            else
            {
                console.Text += item + "\n";
            }
        }

        public void ProccessData(JObject dataPairs)
        {
            if (dataPairs.ContainsKey("changeDirection"))
            {
                map.changeSnakeDirection(int.Parse(dataPairs["snakeId"].ToString()), Map.getDirection(dataPairs["changeDirection"].ToString()));
            }
            else if (dataPairs.ContainsKey("serverAddress"))
            {
                AddMessage( $"Connected to server {dataPairs["serverAddress"]}");
                map.snakeId = int.Parse(dataPairs["snakeId"].ToString());
            }
            else if (dataPairs.ContainsKey("powerUp"))
            {
                map.addFood(int.Parse(dataPairs["powerUp"]["x"].ToString()), 
                    int.Parse(dataPairs["powerUp"]["y"].ToString()), 
                    bool.Parse(dataPairs["powerUp"]["isBuff"].ToString()), 
                    (Objects.PowerUps.PowerUpType)int.Parse(dataPairs["powerUp"]["powerUpType"].ToString()));
            }
            else if (dataPairs.ContainsKey("message"))
            {
                AddMessage(dataPairs["message"].ToString());
            }
            else if (dataPairs.ContainsKey("gameStart"))
            {
                form.gameStart();
            } else if (dataPairs.ContainsKey("syncSnakes"))
            {
                map.Snakes = System.Text.Json.JsonSerializer.Deserialize<List<SnakeBody>>(dataPairs["syncSnakes"].ToString());
                map.Scores = new List<Score>();
                for(int i = 0; i < map.Snakes.Count; i++)
                {
                    map.Scores.Add(new Score(i, 0));
                }
                
            }
        }
        public void AddSnake(SnakeBody snake)
        {
            JObject addSnake = new JObject();
            addSnake["addSnake"] = JObject.Parse("{\"snake\":" + System.Text.Json.JsonSerializer.Serialize(snake) + "}")["snake"];
            SendMessage(addSnake.ToString());
        }
        public void ChangeDirection(Direction direction)
        {
            JObject changeDirection = new JObject();
            changeDirection["changeDirection"] = direction.ToString();
            changeDirection["snakeId"] = map.snakeId;
            SendMessage(changeDirection.ToString());
        }
        public void GameStart()
        {
            JObject gameStart = new JObject();
            gameStart["gameStart"] = true;
            SendMessage(gameStart.ToString());
        }
        public void AddPowerUp()
        {
            int maxXPos = 1000 / map.Width;
            int maxYPos = 969 / map.Height;

            Random random = new Random();
            int x = random.Next(0, maxXPos);
            int y = random.Next(0, maxYPos);
            PowerUp powerUp = new SizeUp(x, y);

            JObject powerUpObj = new JObject();
            JObject addPowerUp = new JObject();
            addPowerUp["x"] = powerUp.X;
            addPowerUp["y"] = powerUp.Y;
            addPowerUp["isBuff"] = (powerUp is SpeedUp || powerUp is SizeUp) ? true : false;
            addPowerUp["powerUpType"] = powerUp is Objects.PowerUps.Size ? 0 : 1;
            powerUpObj["powerUp"] = addPowerUp;
            SendMessage(powerUpObj.ToString());
        }
    }
}