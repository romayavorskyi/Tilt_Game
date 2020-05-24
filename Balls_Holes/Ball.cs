﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace Balls_Holes
{
    public class Ball
    {

        public int Id { get; }
        public Point Coordinates { get; internal set; }
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
