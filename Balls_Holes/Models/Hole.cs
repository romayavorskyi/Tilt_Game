using System;
using System.Drawing;
using Newtonsoft.Json;
using Tilt_Game.Interfaces;

namespace Tilt_Game.Models
{
    public class Hole: ICoordinatable
    {
        [JsonProperty]
        public int Id { get; }
        [JsonProperty]
        public int? BallId { get; internal set; }
        [JsonProperty]
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
