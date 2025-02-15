﻿using System;
using System.Data;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;

namespace Client
{
    [Serializable]
    static class ClientObject
    {
        static private int port = 4020;
        static private string address = "127.0.0.1";

        static private DataTable GetDataTable(byte[] dtData)
        {
            DataTable dt;
            BinaryFormatter bFormat = new BinaryFormatter();
            using (MemoryStream ms = new MemoryStream(dtData))
            {
                dt = bFormat.Deserialize(ms) as DataTable;
            }
            return dt;
        }

        static public string SendRequestToServer(string message, string command = "")
        {
            IPEndPoint ipPoint = new IPEndPoint(IPAddress.Parse(address), port);

            Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            socket.Connect(ipPoint);
            byte[] data = Encoding.Unicode.GetBytes(message);
            socket.Send(data);

            data = new byte[1000];
            StringBuilder builder = new StringBuilder();
            int bytes = 0;

            do
            {
                bytes = socket.Receive(data, data.Length, 0);
                builder.Append(Encoding.Unicode.GetString(data, 0, bytes));
            }
            while (socket.Available > 0);

            socket.Shutdown(SocketShutdown.Both);
            socket.Close();
            return builder.ToString();
        }

        static public DataTable SendSelectRequestToServer(string message)
        {
            IPEndPoint ipPoint = new IPEndPoint(IPAddress.Parse(address), port);

            Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            socket.Connect(ipPoint);
            byte[] data = Encoding.Unicode.GetBytes(message);
            socket.Send(data);

            data = new byte[500000];
            StringBuilder builder = new StringBuilder();
            int bytes = 0;

            do
            {
                bytes = socket.Receive(data, data.Length, 0);
                builder.Append(Encoding.Unicode.GetString(data, 0, bytes));
            }
            while (socket.Available > 0);
            DataTable dataTable = GetDataTable(data);

            socket.Shutdown(SocketShutdown.Both);
            socket.Close();

            return dataTable;

        }
    }
}
