using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Tilt_Game.Helpers;
using Tilt_Game.Interfaces;

namespace Tilt_Game.Models
{
    public class FieldProvider : IFieldProvider
    {
        public const string FileWithFieldName = "SetFieldHere.json";

        public Field GetField()
        {
            if (File.Exists(Path.Combine(Directory.GetCurrentDirectory(), FileWithFieldName)))
            {
                return SerializeHelper.Deserialize<Field>(FileWithFieldName);
            }

            var t = Field.CreateDefaultField();
            SerializeHelper.Serialize(t, FileWithFieldName);
            //TODO use interface for logger, do not use Console.WriteLine directly
            Console.WriteLine($"File {FileWithFieldName} was created with default field in application folder. " +
                              $"Use it to set field for application");
            Console.WriteLine();
            return t;
        }
    }
}
