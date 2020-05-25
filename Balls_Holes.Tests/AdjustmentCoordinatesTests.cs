using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using NUnit.Framework;
using Tilt_Game.Managers;
using Tilt_Game.Models;

namespace Tilt_Game.Tests
{
    //TODO Consider using one test and different test cases
    public class AdjustmentCoordinatesTests
    {

        private readonly TiltManager _tiltManager;

        public AdjustmentCoordinatesTests()
        {
            _tiltManager = new TiltManager();
        }

        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void AdjustmentCoordinatesTiltWest()
        {
            var f = InitializeFieldAdjustmentX();
            _tiltManager.Tilt(f, TiltDirection.West);
            Assert.AreEqual(f.Balls[0].Coordinates, new Point(0, 4));
            Assert.AreEqual(f.Balls[1].Coordinates, new Point(1, 4));
            Assert.AreEqual(f.Balls[2].Coordinates, new Point(2, 4));

        }

        [Test]
        public void AdjustmentCoordinatesTiltEast()
        {
            var f = InitializeFieldAdjustmentX();
            _tiltManager.Tilt(f, TiltDirection.East);
            Assert.AreEqual(f.Balls[0].Coordinates, new Point(7, 4));
            Assert.AreEqual(f.Balls[1].Coordinates, new Point(8, 4));
            Assert.AreEqual(f.Balls[2].Coordinates, new Point(9, 4));

        }

        [Test]
        public void AdjustmentCoordinatesTiltNorth()
        {
            var f = InitializeFieldAdjustmentY();
            _tiltManager.Tilt(f, TiltDirection.North);
            Assert.AreEqual(f.Balls[0].Coordinates, new Point(4, 7));
            Assert.AreEqual(f.Balls[1].Coordinates, new Point(4, 8));
            Assert.AreEqual(f.Balls[2].Coordinates, new Point(4, 9));

        }

        [Test]
        public void AdjustmentCoordinatesTiltSouth()
        {
            var f = InitializeFieldAdjustmentY();
            _tiltManager.Tilt(f, TiltDirection.South);
            Assert.AreEqual(f.Balls[0].Coordinates, new Point(4, 0));
            Assert.AreEqual(f.Balls[1].Coordinates, new Point(4, 1));
            Assert.AreEqual(f.Balls[2].Coordinates, new Point(4, 2));
        }

        [Test]
        public void AdjustmentBallsHolesTiltWest()
        {
            var f = InitializeFieldAdjustmentBallsHoles();
            _tiltManager.Tilt(f, TiltDirection.West);
            Assert.AreEqual(f.Balls[0].Coordinates, f.Holes[0].Coordinates);
            Assert.AreEqual(f.Balls[0].Coordinates, f.Holes[0].Coordinates);
            Assert.AreEqual(f.Balls[0].Coordinates, f.Holes[0].Coordinates);
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