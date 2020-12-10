// A C# program for Client 
using Newtonsoft.Json.Linq;
using Snake.Objects;
using Snake.Objects.PowerUps;
using Snake.Objects.State;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Sockets;
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
                (Objects.PowerUps.PowerUpType)int.Parse(dataPairs["powerUp"]["powerUpType"].ToString()), int.Parse(dataPairs["powerUp"]["random"].ToString()));
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
            else if (dataPairs.ContainsKey("pauseGame"))
            {
                Command command = new CommandPause(map);
                Invoker invoker = new Invoker();
                invoker.SetCommand(command);
                if (map.gameState is Started)
                {
                    invoker.ExecuteCommand();
                }
                else
                {
                    invoker.Undo();
                }
            }
            else if (dataPairs.ContainsKey("saveState"))
            {
                map.SaveSnakesState();
            }
            else if (dataPairs.ContainsKey("restoreState"))
            {
                map.RestoreSnakesState();
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
        public void GameStart(string level, int mapWidth, int mapHeight)
        {
            JObject gameStart = new JObject();
            gameStart["gameStart"] = true;
            gameStart["level"] = level;
            gameStart["mapWidth"] = mapWidth;
            gameStart["mapHeight"] = mapHeight;
            SendMessage(gameStart.ToString());
        }
        public void AddPowerUp()
        {
            JObject generatePowerUp = new JObject();
            generatePowerUp["generatePowerUp"] = true;
            SendMessage(generatePowerUp.ToString());
        }
        public void PauseGame()
        {
            JObject pauseGame = new JObject();
            pauseGame["pauseGame"] = true;
            SendMessage(pauseGame.ToString());
        }
        public void SaveState()
        {
            JObject saveState = new JObject();
            saveState["saveState"] = true;
            SendMessage(saveState.ToString());
        }
        public void RestoreState()
        {
            JObject restoreState = new JObject();
            restoreState["restoreState"] = true;
            SendMessage(restoreState.ToString());
        }
    }
}