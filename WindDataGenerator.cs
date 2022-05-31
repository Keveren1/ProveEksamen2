using System;

namespace UDP_broadcastWind
{
    public class WindDataGenerator
    {
        private static readonly string[] Directions = { "N", "NE", "E", "SE", "S", "SW", "W", "NW" };
        private int _speed = 6;
        private int _direction = 0;
        private readonly Random _ran = new Random();



        public int NextSpeed()
        {
            _speed += _ran.Next(-1, 2);
            if (_speed < 0) _speed = 0;
            return _speed;
        }

        public string NextDirection()
        {
            _direction += _ran.Next(-1, 2);
            if (_direction == -1) _direction = 7;
            if (_direction == 8) _direction = 0;
            return Directions[_direction];
        }

        public string VindRetning { get; set; }
        public int VindHastighed { get; set; }
    }
}