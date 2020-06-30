using System;
using System.Net;
using System.Net.Sockets;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
namespace Server
{
    class Server
    {
        readonly int portForChecking = 8000;
        readonly int portForRegister = 8001;
        readonly int portForAuthorization = 8002;
        readonly int portForSendingMessage = 8003;
        readonly string ipAddress = "127.0.0.1";
        readonly DataBase dataBase = new DataBase();
        public Server(){}
        private void RequestForCheck()
        {
            Console.WriteLine("Порт для проверки запущен.");
            IPEndPoint ipPoint = new IPEndPoint(IPAddress.Parse(ipAddress), portForChecking);
            Socket listenSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            try
            {
                listenSocket.Bind(ipPoint);
                listenSocket.Listen(10);
                while (true)
                {
                    Socket handler = listenSocket.Accept();
                    StringBuilder builder = new StringBuilder();
                    string message;
                    int bytes = 0;
                    byte[] data = new byte[256];
                    do
                    {
                        bytes = handler.Receive(data);
                        builder.Append(Encoding.Unicode.GetString(data, 0, bytes));
                    }
                    while (handler.Available > 0);
                    Console.WriteLine(DateTime.Now.ToShortTimeString() + ": проверка сервера");
                    string info = builder.ToString();
                    if(info == "check")
                    {
                        message = "OK";
                    }
                    else
                    {
                        message = "Error";
                    }
                    data = Encoding.Unicode.GetBytes(message);
                    handler.Send(data);
                    handler.Shutdown(SocketShutdown.Both);
                    handler.Close();
                }
            }
            catch (Exception ex)
            {
               Console.WriteLine(ex.Message);
            }
        }
        private void RequestForRegisty()
        {
            Console.WriteLine("Порт для регистрации запущен.");
            IPEndPoint ipPoint = new IPEndPoint(IPAddress.Parse(ipAddress), portForRegister);
            Socket listenSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            try { 
            listenSocket.Bind(ipPoint);
            listenSocket.Listen(10);

                while (true)
                {
                    Socket handler = listenSocket.Accept();
                    StringBuilder builder = new StringBuilder();
                    string message;
                    int bytes = 0;
                    byte[] data = new byte[256];
                    do
                    {
                        bytes = handler.Receive(data);
                        builder.Append(Encoding.Unicode.GetString(data, 0, bytes));
                    }
                    while (handler.Available > 0);
                    Console.WriteLine(DateTime.Now.ToShortTimeString() + ": регистрация нового пользователя");
                    string[] info = builder.ToString().Split(' ');
                    if (dataBase.FindData(info[0]) == true)
                    {
                        message = "Такой пользователь уже есть!";
                        Console.WriteLine(DateTime.Now.ToShortTimeString() + ": ошибка регистрации");
                    }
                    else
                    {
                        message = "Регистрация прошла успешна";
                        Console.WriteLine(DateTime.Now.ToShortTimeString() + ": регистрация прошла успешно");
                        dataBase.InsertDataInUsers(info[0], info[1]);
                    }
                    data = Encoding.Unicode.GetBytes(message);
                    handler.Send(data);
                    handler.Shutdown(SocketShutdown.Both);
                    handler.Close(); 
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        private void RequestForAuthorization()
        {
            Console.WriteLine("Порт для авторизации запущен.");
            IPEndPoint ipPoint = new IPEndPoint(IPAddress.Parse(ipAddress), portForAuthorization);
            Socket listenSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            try
            {
                listenSocket.Bind(ipPoint);
                listenSocket.Listen(10);
                while (true)
                {
                    Socket handler = listenSocket.Accept();
                    StringBuilder builder = new StringBuilder();
                    string message;
                    int bytes = 0;
                    byte[] data = new byte[256];
                    do
                    {
                        bytes = handler.Receive(data);
                        builder.Append(Encoding.Unicode.GetString(data, 0, bytes));
                    }
                    while (handler.Available > 0);
                    Console.WriteLine(DateTime.Now.ToShortTimeString() + ": авторизация пользователя");
                    string[] info = builder.ToString().Split(' ');
                    if (dataBase.FindData(info[0], info[1]) == true)
                    {
                        message = "Авторизация прошла успешна";
                        Console.WriteLine(DateTime.Now.ToShortTimeString() + ": авторизация прошла успешно");
                    }
                    else
                    {
                        message = "Ошибка авторизации!";
                        Console.WriteLine(DateTime.Now.ToShortTimeString() + ": ошибка авторизации");
                    }
                    data = Encoding.Unicode.GetBytes(message);
                    handler.Send(data);
                    handler.Shutdown(SocketShutdown.Both);
                    handler.Close();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        private void RequestForSendingMessage()
        {
            Console.WriteLine("Порт для отправки сообщений запущен.");
            IPEndPoint ipPoint = new IPEndPoint(IPAddress.Parse(ipAddress), portForSendingMessage);
            Socket listenSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            try
            {
                listenSocket.Bind(ipPoint);
                listenSocket.Listen(10);
                while (true)
                {
                    SHA256 sha256Hash = SHA256.Create();
                    Socket handler = listenSocket.Accept();
                    StringBuilder builder = new StringBuilder();
                    int bytes = 0;
                    byte[] data = new byte[256];
                    do
                    {
                        bytes = handler.Receive(data);
                        builder.Append(Encoding.Unicode.GetString(data, 0, bytes));
                    }
                    while (handler.Available > 0);
                    Console.WriteLine(DateTime.Now.ToShortTimeString() + ": " + builder.ToString());
                    string answer = GetHash(sha256Hash, builder.ToString());
                    data = Encoding.Unicode.GetBytes(answer);
                    handler.Send(data);
                    handler.Shutdown(SocketShutdown.Both);
                    handler.Close();
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        private string GetHash(HashAlgorithm hashAlgorithm, string input)
        {
            byte[] data = hashAlgorithm.ComputeHash(Encoding.UTF8.GetBytes(input));
            var sBuilder = new StringBuilder();
            for (int i = 0; i < data.Length; i++)
            {
                sBuilder.Append(data[i].ToString("x2"));
            }
            return sBuilder.ToString();
        }
        public void Start()
        {
            Thread t0 = new Thread(RequestForCheck);
            Thread t1 = new Thread(RequestForRegisty);
            Thread t2 = new Thread(RequestForAuthorization);
            Thread t3 = new Thread(RequestForSendingMessage);
            t0.Start();
            t1.Start();
            t2.Start();
            t3.Start();
        }
    }
}