using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using Balls_Holes;
using Newtonsoft.Json;
using Tilt_Game.Interfaces;

namespace Tilt_Game.Models
{
    public class Field
    {
        [JsonProperty]
        public int Size { get; }
        [JsonProperty]
        public List<Ball> Balls { get; }
        [JsonProperty]
        public List<Hole> Holes { get; }
        public Field(int size, List<Ball> balls, List<Hole> holes)
        {
            Size = size;
            Balls = balls;
            Holes = holes;
            ValidateCoordinates();
        }

        private void ValidateCoordinates()
        {
            ValidateSize(Holes);
            ValidateSize(Balls);
            ValidateCollisions(Holes);
            ValidateCollisions(Balls);
            //TODO Bad performance. Think how to do faster.
            foreach (var hole in Holes)
            {
                var ball = Balls.FirstOrDefault(x => x.Coordinates == hole.Coordinates);
                if (ball != null)
                {
                    PutBallInHole(ball, hole);
                }
            }
        }

        private void ValidateSize(IEnumerable<ICoordinatable> elements)
        {
            if (elements.Any(x => x.Coordinates.X < 0
                                  || x.Coordinates.Y > Size - 1
                                  || x.Coordinates.Y < 0
                                  || x.Coordinates.Y > Size - 1))
            {
                throw new ArgumentException("One of the elements coordinate for field creation is out of the field range. " +
                                            "Please, check input arguments.");
            }
        }

        private void ValidateCollisions(IEnumerable<ICoordinatable> elements)
        {
            if (elements.
                GroupBy(x => x.Coordinates).
                Any(g => Enumerable.Count(g) > 1))
            {
                throw new ArgumentException("Some of the elements for field creation have same coordinates." +
                                            " Please, check input arguments.");
            }
        }

        internal void PutBallInHole(Ball ball, Hole hole)
        {
            hole.BallId = ball.Id;
            ball.HoleId = hole.Id;
            ball.Coordinates = hole.Coordinates;
        }

        public string GetBallsState()
        {
            StringBuilder sb = new StringBuilder();
            foreach (var ball in Balls)
            {
                sb.Append(ball.GetState());
            }
            return sb.ToString();
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine($"Field size - {Size}");
            sb.AppendLine();
            foreach (var ball in Balls)
            {
                sb.AppendLine(ball.ToString());
            }
            sb.AppendLine();
            foreach (var hole in Holes)
            {
                sb.AppendLine(hole.ToString());
            }
            return sb.ToString();
        }

        public static Field CreateDefaultField()
        {
            var balls = new List<Ball>
            {
                new Ball(1, new Point(7,4)),
                new Ball(2, new Point(8,4)),
                new Ball(3, new Point(9,4)),
            };
            var holes = new List<Hole>
            {
                new Hole(1, new Point(5,4)),
                new Hole(2, new Point(4,4)),
                new Hole(3, new Point(3,4)),
            };
            return new Field(10, balls, holes);
        }
    }
}
