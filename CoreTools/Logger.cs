using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Threading;

namespace CoreTools
{
    public static class Logger
    {

        // Declaring Logging Levels

        private static readonly LogLevel CRITICAL = CTConstants.LOG_CRITICAL;
        private static readonly LogLevel CRIT = CTConstants.LOG_CRIT;
        private static readonly LogLevel ERROR = CTConstants.LOG_ERROR;
        private static readonly LogLevel ERR = CTConstants.LOG_ERR;
        private static readonly LogLevel WARNING = CTConstants.LOG_WARNING;
        private static readonly LogLevel WARN = CTConstants.LOG_WARN;
        private static readonly LogLevel INFO = CTConstants.LOG_INFO;
        private static readonly LogLevel DEBUG = CTConstants.LOG_DEBUG;
        private static readonly LogLevel TRACE = CTConstants.LOG_TRACE;

        public static void Write(string message,LogLevel severityLevel = DEBUG)
        {




            string LogMessage = $"{GetTimeStamp()}\t{severityLevel}\t{message}";
            string targetFile = $"{GetWorkingDir()}/{CTConstants.LOGFILE_FOLDER_NAME}/{CTConstants.LOGFILE_NAME}";
            string logFileFolder = $"{ GetWorkingDir() }/{ CTConstants.LOGFILE_FOLDER_NAME}";

            //Find or Create Logfile Directory

            if (!Directory.Exists(logFileFolder)) { 
                Directory.CreateDirectory(logFileFolder);
                Thread.Sleep(3000);
            }


            // Set Logfile
            FileInfo logFile = new FileInfo(targetFile);


            // Backup Logfile & Delete Original If Too Big
            if (logFile.Length > CTConstants.MAX_LOGFILE_SIZE) {
                string targetNewFile = $"{logFileFolder}/{GetTimeStamp(false)}_{CTConstants.LOGFILE_NAME}";
                if (File.Exists(targetNewFile)) { }
                logFile.CopyTo(targetNewFile);
                Thread.Sleep(2000);
                logFile.Delete();
                Thread.Sleep(5000);
            }

            //Create New Logfile if Does Not Exist
            //ALSO, Write to Logfile
            if (!logFile.Exists)
            {
                using (StreamWriter streamWriter = logFile.CreateText())
                {
                    streamWriter.Write($"{LogMessage}\n");
                    streamWriter.Flush();
                    streamWriter.Close();
                }

            }
            else
            {
                using (StreamWriter streamWriter = logFile.AppendText())
                {
                    streamWriter.Write($"{LogMessage}\n");
                    streamWriter.Flush();
                    streamWriter.Close();
                }
            }

            Console.WriteLine(LogMessage);
            

        }

        private static string GetTimeStamp(bool returnFullTimeStamp = true)
        {
            string outDate = "";
            string outTime = "";
            DateTime now = DateTime.Now;

            string Year = "0000" + now.Year.ToString();
            Year = Year.Substring(Year.Length - 4, 4);

            string Month = "00" + now.Month.ToString();
            Month = Month.Substring(Month.Length - 2, 2);

            string Day = "00" + now.Day.ToString();
            Day = Day.Substring(Day.Length - 2, 2);

            string Hour = "00" + now.Hour.ToString();
            Hour = Hour.Substring(Hour.Length - 2, 2);

            string Minute = "00" + now.Minute.ToString();
            Minute = Minute.Substring(Minute.Length - 2, 2);

            string Second = "00" + now.Second.ToString();
            Second = Second.Substring(Second.Length - 2, 2);

            string Millisecond = "0000" + now.Millisecond.ToString();
            Millisecond = Millisecond.Substring(Millisecond.Length - 4, 4);

            outDate = $"{Year}{Month}{Day}";
            outTime = (returnFullTimeStamp)? $"-{Hour}:{Minute}:{Second}.{Millisecond}":"";
            return outDate + outTime;

        }

        private static string GetWorkingDir()
        {
            string outString = System.IO.Directory.GetCurrentDirectory();
            return outString;
        }

    }
}
