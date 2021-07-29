using System;
using System.Collections.Generic;
using System.Text;

namespace CoreTools
{
    public class LogLevel
    {
        public string LevelName { get; set; }
        public int LevelValue { get; set; }

        public LogLevel(string name,int value)
        {
            LevelName = name;
            LevelValue = value;
        }

    }
}
