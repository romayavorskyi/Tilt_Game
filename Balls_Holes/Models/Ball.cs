using System;
using System.Drawing;
using Newtonsoft.Json;
using Tilt_Game.Interfaces;

namespace Tilt_Game.Models
{
    public class Ball: ICoordinatable
    {
        [JsonProperty]
        public int Id { get; }
        [JsonProperty]
        public Point Coordinates { get; internal set; }
        [JsonProperty]
        public int? HoleId { get; internal set; }

        public Ball(int id, Point initialPosition)
        {
            Id = id;
            Coordinates = initialPosition;
        }

        public override string ToString()
        {
            return $"Ball {Id} with coordinates {Coordinates.ToString()}";
        }

        public string GetState()
        {
            return $"{Id}{Coordinates.X}{Coordinates.Y}";
        }

    }
}
