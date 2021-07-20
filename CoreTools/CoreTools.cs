using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.IE;
using OpenQA.Selenium.Edge;
using System.IO;


namespace CoreTools
{
    public class CoreTools
    {

        private IWebDriver MyWebDriver { get; set; }
        private string MyBrowserName { get; set; }
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

            switch (MyBrowserName.ToLower())
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
        /// <para>driverFileName = The name of the Driver File (IE: Geckodriver.exe)</para>
        /// </summary>
        /// <param name="driverFileName"></param>
        /// <returns></returns>
        private string SetDriverFilePath()
        {

            bool isFoundDriverFilePath = false;
            string fullDriverFilePath;

            foreach (string DriverFilePath in CTConstants.DEFAULT_DRIVER_DIRECTORIES)
            {
                if (Directory.Exists(DriverFilePath))
                {
                    isFoundDriverFilePath = true;
                    Console.WriteLine($"Driver Path Found:\t{DriverFilePath}");
                    fullDriverFilePath = DriverFilePath + "/" + DriverFileName;

                    if (File.Exists(fullDriverFilePath))
                    {
                        Console.WriteLine($"Driver File Found:\t{fullDriverFilePath}");
                        return DriverFilePath;
                    }
                }
            }
            if (!isFoundDriverFilePath) throw new Exception("Driver Path Cannot Be Found");

            throw new Exception("Driver File Cannot Be Found");

        }

        public void OpenBrowser(string browserName)
        {
            MyBrowserName = browserName;

            SetDriverFileName();

            SetDriverFilePath();




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
        private IWebDriver CreateSession(string browserName)
        {


            switch (MyBrowserName.ToLower())
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
        /// <para>goToURL = The URL to navigate to.</para>
        /// </summary>
        /// <param name="goToURL"></param>
        public void NavTo(string goToURL)
        {
            throw new Exception("Not Implemented");
        }


    }
}
