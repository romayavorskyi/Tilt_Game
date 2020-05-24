using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using Balls_Holes;

namespace Tilt_Game.Models
{
    public class Field
    {
        public int Size { get; }
        public List<Ball> Balls { get; }
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
            //TODO Think about faster validation way
            foreach (var hole in Holes)
            {
                var ball = Balls.FirstOrDefault(x => x.Coordinates == hole.Coordinates);
                if (ball != null)
                {
                    PutBallInHole(ball, hole);
                }
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
            foreach (var ball in Balls)
            {
                sb.AppendLine(ball.ToString());
            }
            foreach (var hole in Holes)
            {
                sb.AppendLine(hole.ToString());
            }
            return sb.ToString();
        }
    }
}
