using System;
using System.Collections.Generic;
using System.Text;

namespace CoreTools
{

    // Yes, some are Statics. This was just a good place to put values that shouldn't change
    public class CTConstants
    {

        // ----------------------------------------------------------------------
        // DRIVER CONSTANTS
        // ----------------------------------------------------------------------
        public const string FIREFOX_DRIVER_NAME = "geckodriver.exe";
        public const string CHROME_DRIVER_NAME = "chromedriver.exe";
        public const string IE_DRIVER_NAME = "IEDriverServer.exe";
        public const string MSEDGE_DRIVER_NAME = "msedgedriver.exe";
        public static readonly string[] DEFAULT_DRIVER_DIRECTORIES =  { "C:/ProgramData/SeleniumDrivers",
                                                                        "./Drivers",
                                                                        "../../../Drivers",
                                                                        "../../Drivers",
                                                                        "../Drivers"
        };

        // ----------------------------------------------------------------------
        // LOGGING CONSTANTS
        // ----------------------------------------------------------------------
        public const string LOGFILE_NAME = "_LogFile.log";
        public const string LOGFILE_FOLDER_NAME = "LogFiles";
        public const int MAX_LOGFILE_SIZE = 50000000;   //50MB -> 50,000,000
        public static readonly LogLevel LOG_CRITICAL = new LogLevel("Critical", 1);
        public static readonly LogLevel LOG_CRIT = LOG_CRITICAL; 
        public static readonly LogLevel LOG_ERROR = new LogLevel("Error",2);
        public static readonly LogLevel LOG_ERR = LOG_ERROR;
        public static readonly LogLevel LOG_WARNING = new LogLevel("Warning",3);
        public static readonly LogLevel LOG_WARN = LOG_WARNING;
        public static readonly LogLevel LOG_INFO = new LogLevel("Info",4);
        public static readonly LogLevel LOG_DEBUG = new LogLevel("Debug",5);
        public static readonly LogLevel LOG_TRACE = new LogLevel("Trace",7);





    }
}
