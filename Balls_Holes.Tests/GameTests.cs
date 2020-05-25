using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using NUnit.Framework;
using Tilt_Game.Managers;
using Tilt_Game.Models;

namespace Tilt_Game.Tests
{
    //TODO Consider using one test and different test cases
    class GameTests
    {
        private readonly TiltPlayer _tiltPlayer;

        public GameTests()
        {
            _tiltPlayer = new TiltPlayer();
        }

        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void OneMoveWinTest()
        {
            var balls = new List<Ball>
            {
                new Ball(1, new Point(2,0)),
                new Ball(2, new Point(2,1)),
                new Ball(3, new Point(2,2)),
            };
            var holes = new List<Hole>
            {
                new Hole(1, new Point(0,0)),
                new Hole(2, new Point(0,1)),
                new Hole(3, new Point(0,2)),
            };
            var f = new Field(3, balls, holes);
            var result = _tiltPlayer.GetWinningStrategy(f);
            Assert.AreEqual(GameState.Win, result.state);
            Assert.AreEqual(1, result.directions.Count);
            Assert.AreEqual(result.directions.FirstOrDefault(), TiltDirection.West);
        }

        [Test]
        public void TwoMoveWinTest()
        {
            var balls = new List<Ball>
            {
                new Ball(1, new Point(2,0)),
                new Ball(2, new Point(2,2)),
            };
            var holes = new List<Hole>
            {
                new Hole(1, new Point(0,0)),
                new Hole(2, new Point(0,1)),
            };
            var f = new Field(3, balls, holes);
            var result = _tiltPlayer.GetWinningStrategy(f);
            Assert.AreEqual(GameState.Win, result.state);
            Assert.AreEqual(2, result.directions.Count);
            Assert.AreEqual(result.directions.FirstOrDefault(), TiltDirection.South);
            Assert.AreEqual(result.directions.Skip(1).FirstOrDefault(), TiltDirection.West);
        }

        [Test]
        public void CycleTest()
        {
            var balls = new List<Ball>
            {
                new Ball(1, new Point(0,0)),
            };
            var holes = new List<Hole>
            {
                new Hole(1, new Point(1,1)),
            };
            var f = new Field(3, balls, holes);
            var result = _tiltPlayer.GetWinningStrategy(f);
            Assert.AreEqual(GameState.Fail, result.state);
            Assert.AreEqual(0, result.directions.Count);
        }

        [Test]
        public void AutoWinTest()
        {
            var balls = new List<Ball>
            {
                new Ball(1, new Point(0,0)),
            };
            var holes = new List<Hole>
            {
                new Hole(1, new Point(0,0)),
            };
            var f = new Field(3, balls, holes);
            var result = _tiltPlayer.GetWinningStrategy(f);
            Assert.AreEqual(GameState.Win, result.state);
            Assert.AreEqual(0, result.directions.Count);
        }


    }
}
