using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Linq;
using System.Diagnostics.CodeAnalysis;

namespace Balls_Holes
{

    public enum TiltDirection
    {
        North,
        South,
        West,
        East,
    }

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
        }

        public void Tilt(TiltDirection direction)
        {
            // To check ball-hole collision in correct order for balls in same row\column
            var sortedBalls = GetBallsOrder(direction);
            foreach (var ball in sortedBalls)
            {
                var hole = GetHoleForBall(direction, ball);
                if (hole != null)
                {
                    PutBallInHole(ball, hole);
                }
                else
                {
                    var action = GetTiltAction(direction, GetBallsBeforeCount(direction, ball));
                    action?.Invoke(ball);
                }
            }
            Console.WriteLine(ToString());
        }

        private void PutBallInHole(Ball ball, Hole hole)
        {
            hole.BallId = ball.Id;
            ball.HoleId = hole.Id;
            ball.Coordinates = hole.Coordinates;
        }

        private Action<Ball> GetTiltAction(TiltDirection direction, int ballsBefore)
        {
            switch (direction)
            {
                case TiltDirection.North:
                    return (ball) => ball.Coordinates = new Point(ball.Coordinates.X, Size - 1 - ballsBefore);
                case TiltDirection.South:
                    return (ball) => ball.Coordinates = new Point(ball.Coordinates.X, ballsBefore);
                case TiltDirection.West:
                    return (ball) => ball.Coordinates = new Point(ballsBefore, ball.Coordinates.Y);
                case TiltDirection.East:
                default:
                    return (ball) => ball.Coordinates = new Point(Size - 1 - ballsBefore, ball.Coordinates.Y);
            }
        }

        private int GetBallsBeforeCount(TiltDirection direction, Ball ball)
        {
            switch (direction)
            {
                case TiltDirection.North:
                    return Balls.Where(x => !x.HoleId.HasValue &&
                    x.Coordinates.X == ball.Coordinates.X &&
                    x.Coordinates.Y > ball.Coordinates.Y).Count();
                case TiltDirection.South:
                    return Balls.Where(x => !x.HoleId.HasValue &&
                    x.Coordinates.X == ball.Coordinates.X &&
                    x.Coordinates.Y < ball.Coordinates.Y).Count();
                case TiltDirection.West:
                    return Balls.Where(x => !x.HoleId.HasValue &&
                    x.Coordinates.Y == ball.Coordinates.Y &&
                    x.Coordinates.X < ball.Coordinates.X).Count();
                case TiltDirection.East:
                default:
                    return Balls.Where(x => !x.HoleId.HasValue &&
                    x.Coordinates.Y == ball.Coordinates.Y
                    && x.Coordinates.X > ball.Coordinates.X).Count();
            }
        }

        private List<Ball> GetBallsOrder(TiltDirection direction)
        {
            switch (direction)
            {
                case TiltDirection.North:
                    return Balls.Where(x => !x.HoleId.HasValue).OrderByDescending(x => x.Coordinates.Y).ToList();
                case TiltDirection.South:
                    return Balls.Where(x => !x.HoleId.HasValue).OrderBy(x => x.Coordinates.Y).ToList();
                case TiltDirection.West:
                    return Balls.Where(x => !x.HoleId.HasValue).OrderBy(x => x.Coordinates.X).ToList();
                case TiltDirection.East:
                default:
                    return Balls.Where(x => !x.HoleId.HasValue).OrderByDescending(x => x.Coordinates.X).ToList();
            }
        }

        private Hole GetHoleForBall(TiltDirection direction, Ball ball)
        {
            switch (direction)
            {
                case TiltDirection.North:
                    return Holes.Where(x => !x.BallId.HasValue &&
                    x.Coordinates.X == ball.Coordinates.X &&
                    x.Coordinates.Y > ball.Coordinates.Y).
                        OrderBy(x => x.Coordinates.Y).FirstOrDefault();
                case TiltDirection.South:
                    return Holes.Where(x => !x.BallId.HasValue &&
                    x.Coordinates.X == ball.Coordinates.X &&
                    x.Coordinates.Y < ball.Coordinates.Y).
                    OrderByDescending(x => x.Coordinates.Y).FirstOrDefault();
                case TiltDirection.West:
                    return Holes.Where(x => !x.BallId.HasValue && 
                    x.Coordinates.Y == ball.Coordinates.Y &&
                    x.Coordinates.X < ball.Coordinates.X).
                        OrderByDescending(x => x.Coordinates.X).FirstOrDefault();
                case TiltDirection.East:
                default:
                    return Holes.Where(x => !x.BallId.HasValue &&
                    x.Coordinates.Y == ball.Coordinates.Y 
                    && x.Coordinates.X > ball.Coordinates.X).
                        OrderBy(x => x.Coordinates.Y).FirstOrDefault();
            }
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

        public string GetBallsState()
        {
            StringBuilder sb = new StringBuilder();
            foreach (var ball in Balls)
            {
                sb.Append(ball.GetState());
            }
            return sb.ToString();
        }
    }
}
