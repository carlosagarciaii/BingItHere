﻿using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Threading;

namespace CoreTools
{
    public static class Logger
    {

        // Declaring Logging Levels



        public static void Write(string message, string functionName,  LogLevel severityLevel = null)
        {
            if (severityLevel == null)
            {
                severityLevel = CTConstants.LOG_INFO;
            }



            string LogMessage = $"{GetTimeStamp()}  |  {severityLevel.Name}  |  {functionName}  |  {message}";
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
            if (logFile.Exists && logFile.Length > CTConstants.MAX_LOGFILE_SIZE) {
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

            if (severityLevel == CTConstants.LOG_CRITICAL)
            {

                throw new Exception(CTConstants.LOG_CRITICAL.Name + LogMessage);
            }            

        }


        /// <summary>
        /// Gets the Current Date/Time and Creates a TimeStamp
        /// </summary>
        /// <param name="returnFullTimeStamp"></param>
        /// <returns></returns>
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


        /// <summary>
        /// Returns the Current Working Directory
        /// <para>May be redundant.... not sure</para>
        /// </summary>
        /// <returns></returns>
        private static string GetWorkingDir()
        {
            string outString = System.IO.Directory.GetCurrentDirectory();
            return outString;
        }

    }
}
