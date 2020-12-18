using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace ServerApp.Objects.Mediator
{
    class Collegue

    {
        private Mediator _chatroom;
        private string _name;
        private Socket _clientSocket;

        public Collegue(string name, Socket clientSocket)
        {
            this._name = name;
            this._clientSocket = clientSocket;
        }

        public string Name
        {
            get { return _name; }
        }

        public Mediator Chatroom
        {
            set { _chatroom = value; }
            get { return _chatroom; }
        }

        public void Send(string to, string message)
        {
            _chatroom.Send(_name, to, message);
        }

        public virtual void Receive(
          string from, string message)
        {
            message = String.Format("{0} to {1}: '{2}'",
              from, Name, message);
            JObject sayCommand = new JObject();
            sayCommand["sayCommand"] = message;
            SendClientMessage(_clientSocket, sayCommand.ToString());
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
