using System;
using System.Net;
using System.Net.Sockets;
using System.IO;
using System.Text;

class SimpleTcpSocketClient
{
    public static void Main()
    {
        Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        IPEndPoint remoteEP = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 9050);
        try
        {
            socket.Connect(remoteEP);
        }
        catch (SocketException e)
        {
            Console.WriteLine("Unable to connect to server. ");
            Console.WriteLine(e);
            return;
        }

        byte[] data = new byte[1024];
        int dataSize = socket.Receive(data);
        Console.WriteLine(Encoding.ASCII.GetString(data, 0, dataSize));

        String input = null;
        while (true)
        {
            Console.Write("Enter Message for Server <Enter to Stop>: ");
            input = Console.ReadLine();
            if (input.Length == 0)
                break;
            socket.Send(Encoding.ASCII.GetBytes(input));
            data = new byte[1024];
            dataSize = socket.Receive(data);

            Console.WriteLine("Echo: " + Encoding.ASCII.GetString(data, 0, dataSize));
        }
        Console.WriteLine("Disconnecting from Server..");
        socket.Shutdown(SocketShutdown.Both);
        socket.Close();
    }
}