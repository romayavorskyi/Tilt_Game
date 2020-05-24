using NUnit.Framework;
using System.Drawing;
using System.Collections.Generic;

namespace Balls_Holes.Tests
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        #region DifferentCoordinatesTests

        [Test]
        public void DifferentCoordinatesTiltWest()
        {
            var f = InitializeFieldDifferentCoordinates();
            f.Tilt(TiltDirection.West);
            Assert.AreEqual(f.Balls[0].Coordinates, new Point(0, 4));
            Assert.AreEqual(f.Balls[1].Coordinates, new Point(0, 5));
            Assert.AreEqual(f.Balls[2].Coordinates, new Point(0, 6));

        }

        [Test]
        public void DifferentCoordinatesTiltEast()
        {
            var f = InitializeFieldDifferentCoordinates();
            f.Tilt(TiltDirection.East);
            Assert.AreEqual(f.Balls[0].Coordinates, new Point(9, 4));
            Assert.AreEqual(f.Balls[1].Coordinates, new Point(9, 5));
            Assert.AreEqual(f.Balls[2].Coordinates, new Point(9, 6));

        }

        [Test]
        public void DifferentCoordinatesTiltNorth()
        {
            var f = InitializeFieldDifferentCoordinates();
            f.Tilt(TiltDirection.North);
            Assert.AreEqual(f.Balls[0].Coordinates, new Point(4, 9));
            Assert.AreEqual(f.Balls[1].Coordinates, new Point(5, 9));
            Assert.AreEqual(f.Balls[2].Coordinates, new Point(6, 9));

        }

        [Test]
        public void DifferentCoordinatesTiltSouth()
        {
            var f = InitializeFieldDifferentCoordinates();
            f.Tilt(TiltDirection.South);
            Assert.AreEqual(f.Balls[0].Coordinates, new Point(4, 0));
            Assert.AreEqual(f.Balls[1].Coordinates, new Point(5, 0));
            Assert.AreEqual(f.Balls[2].Coordinates, new Point(6, 0));
        }

        #endregion

        #region AdjustmentCoordinatesTests
        [Test]
        public void AdjustmentCoordinatesTiltWest()
        {
            var f = InitializeFieldAdjustmentX();
            f.Tilt(TiltDirection.West);
            Assert.AreEqual(f.Balls[0].Coordinates, new Point(0, 4));
            Assert.AreEqual(f.Balls[1].Coordinates, new Point(1, 4));
            Assert.AreEqual(f.Balls[2].Coordinates, new Point(2, 4));

        }

        [Test]
        public void AdjustmentCoordinatesTiltEast()
        {
            var f = InitializeFieldAdjustmentX();
            f.Tilt(TiltDirection.East);
            Assert.AreEqual(f.Balls[0].Coordinates, new Point(7, 4));
            Assert.AreEqual(f.Balls[1].Coordinates, new Point(8, 4));
            Assert.AreEqual(f.Balls[2].Coordinates, new Point(9, 4));

        }

        [Test]
        public void AdjustmentCoordinatesTiltNorth()
        {
            var f = InitializeFieldAdjustmentY();
            f.Tilt(TiltDirection.North);
            Assert.AreEqual(f.Balls[0].Coordinates, new Point(4, 7));
            Assert.AreEqual(f.Balls[1].Coordinates, new Point(4, 8));
            Assert.AreEqual(f.Balls[2].Coordinates, new Point(4, 9));

        }

        [Test]
        public void AdjustmentCoordinatesTiltSouth()
        {
            var f = InitializeFieldAdjustmentY();
            f.Tilt(TiltDirection.South);
            Assert.AreEqual(f.Balls[0].Coordinates, new Point(4, 0));
            Assert.AreEqual(f.Balls[1].Coordinates, new Point(4, 1));
            Assert.AreEqual(f.Balls[2].Coordinates, new Point(4, 2));
        }
        #endregion

        #region BallsInHolesTests
        [Test]
        public void AdjustmentBallsHolesTiltWest()
        {
            var f = InitializeFieldAdjustmentBallsHoles();
            f.Tilt(TiltDirection.West);
            Assert.AreEqual(f.Balls[0].Coordinates, f.Holes[0].Coordinates);
            Assert.AreEqual(f.Balls[0].Coordinates, f.Holes[0].Coordinates);
            Assert.AreEqual(f.Balls[0].Coordinates, f.Holes[0].Coordinates);
        }
        #endregion

        private Field InitializeFieldDifferentCoordinates()
        {
            var balls = new List<Ball>
            {
                new Ball(1, new Point(4,4)),
                new Ball(2, new Point(5,5)),
                new Ball(3, new Point(6,6)),
            };
            return new Field(10, balls, new List<Hole>());
        }

        private Field InitializeFieldAdjustmentX()
        {
            var balls = new List<Ball>
            {
                new Ball(1, new Point(4,4)),
                new Ball(2, new Point(5,4)),
                new Ball(3, new Point(6,4)),
            };
            return new Field(10, balls, new List<Hole>());
        }

        private Field InitializeFieldAdjustmentY()
        {
            var balls = new List<Ball>
            {
                new Ball(1, new Point(4,4)),
                new Ball(2, new Point(4,5)),
                new Ball(3, new Point(4,6)),
            };
            return new Field(10, balls, new List<Hole>());
        }

        private Field InitializeFieldAdjustmentBallsHoles()
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