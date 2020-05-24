using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using Balls_Holes;
using Tilt_Game.Models;

namespace Tilt_Game.Managers
{
    public class TiltManager
    {
        public void Tilt(Field field, TiltDirection direction)
        {
            // To check ball-hole collision in correct order for balls in same row\column
            var sortedBalls = GetBallsOrder(field, direction);
            foreach (var ball in sortedBalls)
            {
                var hole = GetHoleForBall(field, direction, ball);
                if (hole != null)
                {
                    field.PutBallInHole(ball, hole);
                }
                else
                {
                    var action = GetTiltAction(direction, field.Size, GetBallsBeforeCount(field, direction, ball));
                    action?.Invoke(ball);
                }
            }
            Console.WriteLine(field.ToString());
        }

        private Action<Ball> GetTiltAction(TiltDirection direction, int  fieldSize, int ballsBefore)
        {
            switch (direction)
            {
                case TiltDirection.North:
                    return (ball) => ball.Coordinates = new Point(ball.Coordinates.X, fieldSize - 1 - ballsBefore);
                case TiltDirection.South:
                    return (ball) => ball.Coordinates = new Point(ball.Coordinates.X, ballsBefore);
                case TiltDirection.West:
                    return (ball) => ball.Coordinates = new Point(ballsBefore, ball.Coordinates.Y);
                case TiltDirection.East:
                default:
                    return (ball) => ball.Coordinates = new Point(fieldSize - 1 - ballsBefore, ball.Coordinates.Y);
            }
        }

        private int GetBallsBeforeCount(Field field, TiltDirection direction, Ball ball)
        {
            switch (direction)
            {
                case TiltDirection.North:
                    return field.Balls.Count(x => !x.HoleId.HasValue &&
                                                  x.Coordinates.X == ball.Coordinates.X &&
                                                  x.Coordinates.Y > ball.Coordinates.Y);
                case TiltDirection.South:
                    return field.Balls.Count(x => !x.HoleId.HasValue &&
                                                  x.Coordinates.X == ball.Coordinates.X &&
                                                  x.Coordinates.Y < ball.Coordinates.Y);
                case TiltDirection.West:
                    return field.Balls.Count(x => !x.HoleId.HasValue &&
                                                  x.Coordinates.Y == ball.Coordinates.Y &&
                                                  x.Coordinates.X < ball.Coordinates.X);
                case TiltDirection.East:
                default:
                    return field.Balls.Count(x => !x.HoleId.HasValue &&
                                                  x.Coordinates.Y == ball.Coordinates.Y
                                                  && x.Coordinates.X > ball.Coordinates.X);
            }
        }

        private List<Ball> GetBallsOrder(Field field, TiltDirection direction)
        {
            switch (direction)
            {
                case TiltDirection.North:
                    return field.Balls.Where(x => !x.HoleId.HasValue).OrderByDescending(x => x.Coordinates.Y).ToList();
                case TiltDirection.South:
                    return field.Balls.Where(x => !x.HoleId.HasValue).OrderBy(x => x.Coordinates.Y).ToList();
                case TiltDirection.West:
                    return field.Balls.Where(x => !x.HoleId.HasValue).OrderBy(x => x.Coordinates.X).ToList();
                case TiltDirection.East:
                default:
                    return field.Balls.Where(x => !x.HoleId.HasValue).OrderByDescending(x => x.Coordinates.X).ToList();
            }
        }

        private Hole GetHoleForBall(Field field, TiltDirection direction, Ball ball)
        {
            switch (direction)
            {
                case TiltDirection.North:
                    return field.Holes.Where(x => !x.BallId.HasValue &&
                                                 x.Coordinates.X == ball.Coordinates.X &&
                                                 x.Coordinates.Y > ball.Coordinates.Y).
                        OrderBy(x => x.Coordinates.Y).FirstOrDefault();
                case TiltDirection.South:
                    return field.Holes.Where(x => !x.BallId.HasValue &&
                                                  x.Coordinates.X == ball.Coordinates.X &&
                                                  x.Coordinates.Y < ball.Coordinates.Y).
                    OrderByDescending(x => x.Coordinates.Y).FirstOrDefault();
                case TiltDirection.West:
                    return field.Holes.Where(x => !x.BallId.HasValue &&
                                                  x.Coordinates.Y == ball.Coordinates.Y &&
                                                  x.Coordinates.X < ball.Coordinates.X).
                        OrderByDescending(x => x.Coordinates.X).FirstOrDefault();
                case TiltDirection.East:
                default:
                    return field.Holes.Where(x => !x.BallId.HasValue &&
                                                  x.Coordinates.Y == ball.Coordinates.Y
                                                  && x.Coordinates.X > ball.Coordinates.X).
                        OrderBy(x => x.Coordinates.Y).FirstOrDefault();
            }
        }

    }
}
