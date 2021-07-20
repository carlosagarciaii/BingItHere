using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.IE;
using OpenQA.Selenium.Edge;
using System.IO;
using System.Threading;


namespace CoreTools
{
    public class CoreTools
    {

        private IWebDriver Driver { get; set; }
        private string BrowserName { get; set; }
        private string DriverFileName { get; set; }
        private string DriverFilePath { get; set; }


        /// <summary>
        /// <para>Returns a String for the filename of the WebDriver</para>
        /// <para>browserName = The name of the browser
        /// <br>---Gecko = FF or FireFox</br>
        /// <br>---Chrome = Google or Chrome</br>
        /// <br>---IE = IE or IExplore</br>
        /// <br>---MSEdge = Edge or MSEdge</br></para>
        /// </summary>
        /// <returns>String value for the driver's file name</returns>
        private void SetDriverFileName()
        {

            switch (BrowserName.ToLower())
            {
                case "ff":
                    DriverFileName = CTConstants.FIREFOX_DRIVER_NAME;
                    break;
                case "firefox":
                    DriverFileName = CTConstants.FIREFOX_DRIVER_NAME;
                    break;
                case "chrome":
                    DriverFileName = CTConstants.CHROME_DRIVER_NAME;
                    break;
                case "google":
                    DriverFileName = CTConstants.CHROME_DRIVER_NAME;
                    break;
                case "ie":
                    DriverFileName = CTConstants.IE_DRIVER_NAME;
                    break;
                case "iexplore":
                    DriverFileName = CTConstants.IE_DRIVER_NAME;
                    break;
                case "edge":
                    DriverFileName = CTConstants.MSEDGE_DRIVER_NAME;
                    break;
                case "msedge":
                    DriverFileName = CTConstants.MSEDGE_DRIVER_NAME;
                    break;
                default:
                    throw new Exception("The Browser Provided does not match an acceptable value.");

            }

        }


        /// <summary>
        /// Returns a String for the full path to the Driver File
        /// <para>DriverFileName = The name of the Driver File (IE: Geckodriver.exe)</para>
        /// </summary>
        /// <param name="driverFileName"></param>
        /// <returns></returns>
        private void SetDriverFilePath()
        {

            bool isFoundDriverDirPath = false;
            string fullDriverFilePath;

            foreach (string driverDirPathItem in CTConstants.DEFAULT_DRIVER_DIRECTORIES)
            {
                if (Directory.Exists(driverDirPathItem))
                {
                    isFoundDriverDirPath = true;
                    Console.WriteLine($"Driver Path Found:\t{DriverFilePath}");
                    fullDriverFilePath = driverDirPathItem + "/" + DriverFileName;

                    if (File.Exists(fullDriverFilePath))
                    {
                        Console.WriteLine($"Driver File Found:\t{fullDriverFilePath}");
                        DriverFilePath = driverDirPathItem + "/";
                        return;
                    }
                }
            }
            if (!isFoundDriverDirPath) throw new Exception("EXCEPTION:\n\tDriver Directory Cannot Be Found");

            throw new Exception("EXCEPTION:\n\tDriver File Cannot Be Found");
        }

        public void OpenBrowser(string browserName)
        {
            BrowserName = browserName;

            SetDriverFileName();

            SetDriverFilePath();

            Driver = CreateSession();


        }

        /// <summary>
        /// <para>Creates the IWebDriver session based on the browser selected</para>
        /// <para>browserName = The name of the browser
        /// <br>---Gecko = FF or FireFox</br>
        /// <br>---Chrome = Google or Chrome</br>
        /// <br>---IE = IE or IExplore</br>
        /// <br>---MSEdge = Edge or MSEdge</br></para>
        /// </summary>
        /// <param name="browserName"></param>
        /// <returns></returns>
        private IWebDriver CreateSession()
        {


            switch (BrowserName.ToLower())
            {
                case "ff":
                    return new FirefoxDriver(DriverFilePath);
                    
                case "firefox":
                    return new FirefoxDriver(DriverFilePath);
                case "chrome":
                    return new ChromeDriver(DriverFilePath);
                case "google":
                    return new ChromeDriver(DriverFilePath);
                case "ie":
                    return new InternetExplorerDriver(DriverFilePath);
                case "iexplore":
                    return new InternetExplorerDriver(DriverFilePath);
                case "edge":
                    return new EdgeDriver(DriverFilePath);
                case "msedge":
                    return new EdgeDriver(DriverFilePath);
                default:
                    throw new Exception("Unable to Locate WebDriver");
            }

        }


        /// <summary>
        /// Navigates to the specified URL
        /// <para><br>goToURL = The URL to navigate to.</br>
        /// <br>retryNumber = Number of Times to Retry loading the page (Default 0)</br>
        /// <br>waitInSec = The number of seconds to wait before reloading the page.(Default 20) </br></para>
        /// </summary>
        /// <param name="goToURL"></param>
        /// <param name="retryNumbers"></param>
        /// <param name="waitInSec"></param>
        public void NavTo(string goToURL,int retryNumbers = 0,int waitInSec = 20)
        {
            string ReadyState = "";
            Driver.Url = goToURL;
            for (int retryCount = retryNumbers + 1; retryCount >= 0; retryCount--)
            {
                Driver.Navigate();

                for (int i = 0; i < waitInSec; i++)
                {
                    try
                    {
                        Thread.Sleep(1000);
                        ReadyState = (string)((IJavaScriptExecutor)Driver).ExecuteScript("return document.readyState;");
                        Console.WriteLine(ReadyState);
                        if (ReadyState.ToLower() == "complete") break;
                    }
                    catch (Exception e)
                    {
                        throw new Exception($"\nEXCEPTION HAS OCCURRRED\n\t{e}");
                    }

                }
                if (ReadyState.ToLower() == "complete") break;
            }

            if (ReadyState.ToLower() != "complete") throw new Exception($"\nEXCEPTION:\n\tFailed to Load Page {goToURL}");
            Console.WriteLine($"Success - Navigated to:\t{Driver.Url.ToString()}");


        }


    }
}
