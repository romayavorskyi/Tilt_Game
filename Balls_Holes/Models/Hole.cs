using System;
using System.Drawing;

namespace Tilt_Game.Models
{
    public class Hole
    {
        public int Id { get; }
        public int? BallId { get; internal set; }
        public Point Coordinates { get; }

        public Hole(int id, Point coordinates)
        {
            Id = id;
            Coordinates = coordinates;
        }

        public override string ToString()
        {
            return $"Hole {Id} with coordinates {Coordinates.ToString()}";
        }
    }
}
