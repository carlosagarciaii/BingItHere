using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.IE;
using OpenQA.Selenium.Edge;
using System.IO;
using CoreTools;


namespace CoreTools
{
    public class CoreTools
    {
        private IWebDriver driver { get; set; }

        /// <summary>
        /// <para>Returns a String for the filename of the WebDriver</para>
        /// <para>browserName = The name of the browser
        /// <br>---Gecko = FF or FireFox</br>
        /// <br>---Chrome = Google or Chrome</br>
        /// <br>---IE = IE or IExplore</br>
        /// <br>---MSEdge = Edge or MSEdge</br></para>
        /// </summary>
        /// <returns>String value for the driver's file name</returns>
        private string FindDriver(string browserName)
        {
            string driverFileName ;

            switch (browserName.ToLower())
            {
                case "ff":
                    driverFileName = CTConstants.FIREFOX_DRIVER_NAME;
                    break;
                case "firefox":
                    driverFileName = CTConstants.FIREFOX_DRIVER_NAME;
                    break;
                case "chrome":
                    driverFileName = CTConstants.CHROME_DRIVER_NAME;
                    break;
                case "google":
                    driverFileName = CTConstants.CHROME_DRIVER_NAME;
                    break;
                case "ie":
                    driverFileName = CTConstants.IE_DRIVER_NAME;
                    break;
                case "iexplore":
                    driverFileName = CTConstants.IE_DRIVER_NAME;
                    break;
                case "edge":
                    driverFileName = CTConstants.MSEDGE_DRIVER_NAME;
                    break;
                case "msedge":
                    driverFileName = CTConstants.MSEDGE_DRIVER_NAME;
                    break;
                default:
                    throw new Exception("The Browser Provided does not match an acceptable value.");

            }

            return driverFileName;
        }

        private string FindDriverPath(string driverFileName)
        {
            return "Not Implemented";

        }
        public IWebDriver CreateSession(string browserName)
        {
            string driverFileName = FindDriver(browserName);
            string driverFilePath = FindDriverPath(driverFileName);

            switch (browserName.ToLower())
            {
                case "ff":
                    return new FirefoxDriver();
                    
                case "firefox":
                    return new FirefoxDriver();
                case "chrome":
                    return new ChromeDriver();
                case "google":
                    return new ChromeDriver();
                case "ie":
                    return new InternetExplorerDriver();
                case "iexplore":
                    return new InternetExplorerDriver();
                case "edge":
                    return new EdgeDriver();
                case "msedge":
                    return new EdgeDriver();
                default:
                    throw new Exception("Unable to Locate WebDriver");
            }



        }

    }
}
