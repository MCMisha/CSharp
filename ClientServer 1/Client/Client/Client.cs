using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace Client
{
    class Client
    {
        //порты
        int portForChecking = 8000;
        int portForRegister = 8001;
        int portForAuthorization = 8002;
        int portForSendingMessage = 8003;
        //адрес
        string ipAddress = "127.0.0.1";
        public Client() { }
        public string requestForChecking()
        {
            try
            {
                string answer;
                IPEndPoint ipPoint = new IPEndPoint(IPAddress.Parse(ipAddress), portForChecking);
                Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                socket.Connect(ipPoint);
                string message = "check";
                byte[] data = Encoding.Unicode.GetBytes(message);
                socket.Send(data);
                data = new byte[256];
                StringBuilder builder = new StringBuilder();
                int bytes;
                do
                {
                    bytes = socket.Receive(data, data.Length, 0);
                    builder.Append(Encoding.Unicode.GetString(data, 0, bytes));
                } while (socket.Available > 0);
                socket.Shutdown(SocketShutdown.Both);
                socket.Close();
                answer = builder.ToString();
                return answer;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
        public string requestForRegistration(string login, string password)
        {
            try
            {
                string answer;
                IPEndPoint ipPoint = new IPEndPoint(IPAddress.Parse(ipAddress), portForRegister);
                Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                socket.Connect(ipPoint);
                string message = login + " " + password;
                byte[] data = Encoding.Unicode.GetBytes(message);
                socket.Send(data);
                data = new byte[256];
                StringBuilder builder = new StringBuilder();
                int bytes;
                do
                {
                    bytes = socket.Receive(data, data.Length, 0);
                    builder.Append(Encoding.Unicode.GetString(data, 0, bytes));
                } while (socket.Available > 0);
                socket.Shutdown(SocketShutdown.Both);
                socket.Close();
                answer = builder.ToString();
                return answer;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
        public string requestForAutorization(string login, string password)
        {
            try
            {
                string answer;
                IPEndPoint ipPoint = new IPEndPoint(IPAddress.Parse(ipAddress), portForAuthorization);
                Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                socket.Connect(ipPoint);
                string message = login + " " + password;
                byte[] data = Encoding.Unicode.GetBytes(message);
                socket.Send(data);
                data = new byte[256];
                StringBuilder builder = new StringBuilder();
                int bytes;
                do
                {
                    bytes = socket.Receive(data, data.Length, 0);
                    builder.Append(Encoding.Unicode.GetString(data, 0, bytes));
                } while (socket.Available > 0);
                socket.Shutdown(SocketShutdown.Both);
                socket.Close();
                answer = builder.ToString();
                return answer;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
        public string requestForSendingMessage(string message)
        {
            try
            {
                string answer;
                IPEndPoint ipPoint = new IPEndPoint(IPAddress.Parse(ipAddress), portForSendingMessage);
                Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                socket.Connect(ipPoint);
                byte[] data = Encoding.Unicode.GetBytes(message);
                socket.Send(data);
                data = new byte[256];
                StringBuilder builder = new StringBuilder();
                int bytes;
                do
                {
                    bytes = socket.Receive(data, data.Length, 0);
                    builder.Append(Encoding.Unicode.GetString(data, 0, bytes));
                } while (socket.Available > 0);
                socket.Shutdown(SocketShutdown.Both);
                socket.Close();
                answer = builder.ToString();
                return answer;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
    }
}
