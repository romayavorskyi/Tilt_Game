using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using NUnit.Framework;
using Tilt_Game.Managers;
using Tilt_Game.Models;

namespace Tilt_Game.Tests
{
    public class DifferentCoordinatesTests
    {

        private readonly TiltManager _tiltManager;
        private readonly TiltPlayer _tiltPlayer;

        public DifferentCoordinatesTests()
        {
            _tiltManager = new TiltManager();
            _tiltPlayer = new TiltPlayer(_tiltManager);
        }

        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void DifferentCoordinatesTiltWest()
        {
            var f = InitializeFieldDifferentCoordinates();
            _tiltManager.Tilt(f, TiltDirection.West);
            Assert.AreEqual(f.Balls[0].Coordinates, new Point(0, 4));
            Assert.AreEqual(f.Balls[1].Coordinates, new Point(0, 5));
            Assert.AreEqual(f.Balls[2].Coordinates, new Point(0, 6));

        }

        [Test]
        public void DifferentCoordinatesTiltEast()
        {
            var f = InitializeFieldDifferentCoordinates();
            _tiltManager.Tilt(f, TiltDirection.East);
            Assert.AreEqual(f.Balls[0].Coordinates, new Point(9, 4));
            Assert.AreEqual(f.Balls[1].Coordinates, new Point(9, 5));
            Assert.AreEqual(f.Balls[2].Coordinates, new Point(9, 6));

        }

        [Test]
        public void DifferentCoordinatesTiltNorth()
        {
            var f = InitializeFieldDifferentCoordinates();
            _tiltManager.Tilt(f, TiltDirection.North);
            Assert.AreEqual(f.Balls[0].Coordinates, new Point(4, 9));
            Assert.AreEqual(f.Balls[1].Coordinates, new Point(5, 9));
            Assert.AreEqual(f.Balls[2].Coordinates, new Point(6, 9));

        }

        [Test]
        public void DifferentCoordinatesTiltSouth()
        {
            var f = InitializeFieldDifferentCoordinates();
            _tiltManager.Tilt(f, TiltDirection.South);
            Assert.AreEqual(f.Balls[0].Coordinates, new Point(4, 0));
            Assert.AreEqual(f.Balls[1].Coordinates, new Point(5, 0));
            Assert.AreEqual(f.Balls[2].Coordinates, new Point(6, 0));
        }

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
    }
}