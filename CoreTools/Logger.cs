using System;
using System.Collections.Generic;
using System.Text;
using System.IO;


namespace CoreTools
{
    public class Logger
    {

        public static void Write(string message,string severityLevel = "INFO")
        {
            string LogMessage = $"{GetTimeStamp()}\t{severityLevel}\t{message}";
            string targetFile = $"{GetWorkingDir()}/{CTConstants.LOGFILE_NAME}";

            FileInfo logFile = new FileInfo(targetFile);
            
            if (!logFile.Exists) { logFile.Create(); }
            using (StreamWriter streamWriter = logFile.AppendText())
            {
                streamWriter.Write(LogMessage);
                streamWriter.Flush();
                streamWriter.Close();
            }

            Console.WriteLine(LogMessage);


        }

        private static string GetTimeStamp()
        {
            string outString = "";
            DateTime now = DateTime.Now;

            string Year = "0000" + now.Year.ToString();
            Year = Year.Substring(Year.Length - 4, 4);

            string Month = "00" + now.Month.ToString();
            Month = Month.Substring(Month.Length - 2, 2);

            string Day = "00" + now.Day.ToString();
            Day = Day.Substring(Day.Length - 4, 4);

            string Hour = "00" + now.Hour.ToString();
            Hour = Hour.Substring(Hour.Length - 2, 2);

            string Minute = "00" + now.Minute.ToString();
            Minute = Minute.Substring(Minute.Length - 2, 2);

            string Second = "00" + now.Second.ToString();
            Second = Second.Substring(Second.Length - 2, 2);

            string Millisecond = "0000" + now.Millisecond.ToString();
            Millisecond = Millisecond.Substring(Millisecond.Length - 4, 4);

            outString = $"{Year}{Month}{Day}-{Hour}:{Minute}:{Second}.{Millisecond}";
            return outString;

        }

        private static string GetWorkingDir()
        {
            string outString = System.IO.Directory.GetCurrentDirectory();
            return outString;
        }

    }
}
