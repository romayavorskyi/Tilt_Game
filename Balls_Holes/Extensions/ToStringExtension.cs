using System;
using System.Collections.Generic;
using System.Text;

namespace Tilt_Game.Extensions
{
    public static class ToStringExtension
    {

        public static string ToFormattedString(this (GameState state, List<TiltDirection> directions) tuple)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine(tuple.state.ToString());
            sb.Append("Directions: ");
            foreach (var direction in tuple.directions)
            {
                sb.AppendLine($"{direction.ToString()}; ");
            }
            return sb.ToString();
        }

    }
}
