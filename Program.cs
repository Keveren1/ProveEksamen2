using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Text.Json;
using System.Threading;


namespace UDP_broadcastWind
{
    class Program
    {
        static void Main(string[] args)
        {
            int nextTest = 1;
            Console.WriteLine("UDP Client");


            using (UdpClient socket = new UdpClient())
            {
                WindDataGenerator dataGenerator = new WindDataGenerator();
                dataGenerator.VindHastighed = dataGenerator.NextSpeed();
                dataGenerator.VindRetning = dataGenerator.NextDirection();
                while (true)
                {
                    string sensorName = "UDPTest" + nextTest++;

                    WindDataGenerator sensorData = new WindDataGenerator()
                    {

                        VindHastighed = dataGenerator.NextSpeed(),
                        VindRetning = dataGenerator.NextDirection()
                    };

                    string message = JsonSerializer.Serialize(sensorData);
                    byte[] data = Encoding.UTF8.GetBytes(message);
                    Console.WriteLine("Broadcaster sent " + message);

                    socket.Send(data, data.Length, "127.0.0.1", 10100);
                    Thread.Sleep(3000);
                }
            }
        }
    }
}