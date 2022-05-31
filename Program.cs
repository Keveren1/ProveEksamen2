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
            string[] Directions = { "N", "NE", "E", "SE", "S", "SW", "W", "NW" };
            int _speed = 6;
            int _direction = 0;
            Random _ran = new Random();
            string vindhastighed;
            int vindretning;

            int NextSpeed()
            {
                _speed += _ran.Next(-1, 2);
                if (_speed < 0) _speed = 0;
                return _speed;
            }

            string NextDirection()
            {
                _direction += _ran.Next(-1, 2);
                if (_direction == -1) _direction = 7;
                if (_direction == 8) _direction = 0;
                return Directions[_direction];
            }


            int nextTest = 1;
            Console.WriteLine("UDP Client");


            using (UdpClient socket = new UdpClient())
            {
                while (true)
                {
                    string sensorName = "UDPTest" + nextTest++;

                    WindDataGenerator sensorData = new WindDataGenerator()
                    {

                        VindHastighed = NextSpeed(),
                        VindRetning = NextDirection()
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
