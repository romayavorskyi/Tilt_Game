using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using Newtonsoft.Json;
using Tilt_Game.Extensions;
using Tilt_Game.Helpers;
using Tilt_Game.Interfaces;
using Tilt_Game.Managers;
using Tilt_Game.Models;

namespace Balls_Holes
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                IFieldProvider fp = new FieldProvider();
                var field = fp.GetField();
                //TODO use logger interface instead of direct Console.WriteLine
                Console.WriteLine(field.ToString());
                TiltPlayer player = new TiltPlayer();
                var result = player.GetWinningStrategy(field);
                Console.WriteLine(result.ToFormattedString());
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message); 
            }
        }
    }
}
