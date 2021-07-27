using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.IE;
using OpenQA.Selenium.Edge;
using System.IO;
using System.Threading;
using System.Collections.Generic;

namespace CoreTools
{
    public class CoreTools
    {

        //  ---------------------------------------------------------------
        //  GENERAL
        //  ---------------------------------------------------------------
        //  ---------------------------------------------------------------


        string LogMsg;
        private IWebDriver Driver { get; set; }
        private string BrowserName { get; set; }
        private string DriverFileName { get; set; }
        private string DriverFilePath { get; set; }

        public IWebElement Element { get; set; }
        public List<IWebElement> Elements { get; set; }



        //  ---------------------------------------------------------------
        //  CREATING A SESSION
        //  ---------------------------------------------------------------
        //  ---------------------------------------------------------------

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
                    string LogMsg = "The Browser Provided does not match an acceptable value.";
                    Logger.Write(LogMsg);
                    throw new Exception(LogMsg);

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
                    Logger.Write($"Driver Path Found:\t{DriverFilePath}");
                    fullDriverFilePath = driverDirPathItem + "/" + DriverFileName;

                    if (File.Exists(fullDriverFilePath))
                    {
                        Logger.Write($"Driver File Found:\t{fullDriverFilePath}");
                        DriverFilePath = driverDirPathItem + "/";
                        return;
                    }
                }
            }
            if (!isFoundDriverDirPath) throw new Exception("EXCEPTION:\n\tDriver Directory Cannot Be Found");

            throw new Exception("EXCEPTION:\n\tDriver File Cannot Be Found");
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
                    string LogMsg = "Unable to Locate WebDriver";
                    Logger.Write(LogMsg);
                    throw new Exception(LogMsg);
            }

        }

        /// <summary>
        /// Opens the browser session. 
        /// <para><br>browserName = the name of the browser to open</br>
        /// <br>-Options:</br>
        /// <br>---FireFox, FF</br>
        /// <br>---Google, Chrome</br>
        /// <br>---IE, IExplore</br>
        /// <br>---Edge, MSEdge</br></para>
        /// </summary>
        /// <param name="browserName"></param>
        public void OpenBrowser(string browserName)
        {
            Logger.Write("Opening Browser");
            BrowserName = browserName;

            SetDriverFileName();

            SetDriverFilePath();

            Driver = CreateSession();


        }


        //  ---------------------------------------------------------------
        //  CLOSING A SESSION
        //  ---------------------------------------------------------------
        //  ---------------------------------------------------------------


        /// <summary>
        /// Closes the Browser.
        /// </summary>
        public void CloseBrowser()
        {

            Logger.Write("Closing Browser Session");
            try
            {
                Driver.Close();
            }
            catch (Exception e)
            {
                string exceptionMsg = $"EXCEPTION:\n\tBrowser has failed to close.\n{e}";
                Logger.Write(exceptionMsg);
                throw new Exception(exceptionMsg);

            }

        }

        //  ---------------------------------------------------------------
        //  Navigating To pages
        //  ---------------------------------------------------------------
        //  ---------------------------------------------------------------


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
                        Logger.Write(ReadyState);
                        if (ReadyState.ToLower() == "complete") break;
                    }
                    catch (Exception e)
                    {
                        string LogMsg = $"\nEXCEPTION HAS OCCURRRED\n\t{e}";
                        Logger.Write(LogMsg);
                        throw new Exception(LogMsg);
                    }

                }
                if (ReadyState.ToLower() == "complete") break;
            }

            if (ReadyState.ToLower() != "complete") throw new Exception($"\nEXCEPTION:\n\tFailed to Load Page {goToURL}");
            Logger.Write($"Success - Navigated to:\t{Driver.Url.ToString()}");


        }


        //  ---------------------------------------------------------------
        //  Find Elements (FE)
        //  ---------------------------------------------------------------
        //  ---------------------------------------------------------------


        By Locator;

        /// <summary>
        /// Sets the Locator and strategy to use.
        /// <para><br>-- elementLocator = The locator for the element (IE: #id, "//a", etc)</br>
        /// <br>-- locatorStrategy = The Strategy to use to locate the element </br>
        /// <br>-- -- -- xpath (default)</br>
        /// <br>-- -- -- css / cssselector</br>
        /// <br>-- -- -- name</br>
        /// <br>-- -- -- id</br>
        /// </para>
        /// </summary>
        /// <param name="elementLocator"></param>
        /// <param name="locatorStrategy"></param>

        public void SetLocator(string elementLocator, string locatorStrategy = "xpath")
        {
            switch (locatorStrategy.ToLower())
            {
                case "xpath":
                    Locator = By.XPath(elementLocator);
                    break;
                case "css":
                case "cssselector":
                    Locator = By.CssSelector(elementLocator);
                    break;
                case "id":
                    Locator = By.Id(elementLocator);
                    break;
                case "name":
                    Locator = By.Name(elementLocator);
                    break;
                default:
                    LogMsg = $"EXCEPTION\tLOCATOR ERROR\nError:\tFE00001\n\tThe Locator Stragety Provided does not match a recognized strategy.\n\tLocator Strategy Provided:\t{locatorStrategy}";
                    Logger.Write(LogMsg);
                    throw new Exception(LogMsg);

            }


        }

        /// <summary>
        /// Locates a single Element on the page that matches the locator and returns an IWebElement object
        /// <para><br>-- elementLocator = The locator for the element (IE: #id, "//a", etc)</br>
        /// <br>-- locatorStrategy = The Strategy to use to locate the element </br>
        /// <br>-- -- -- xpath (default)</br>
        /// <br>-- -- -- css / cssselector</br>
        /// <br>-- -- -- name</br>
        /// <br>-- -- -- id</br>
        /// <br>-- waitForElement = Wait for element to appear on page (true {default}/false)</br>
        /// <br>-- waitTimeSec = Number of seconds to wait for the element (default = 20)</br>
        /// </para>
        /// </summary>
        /// <param name="elementLocator"></param>
        /// <param name="locatorStrategy"></param>
        /// <param name="waitForElement"></param>
        /// <param name="waitTimeSec"></param>
        /// <returns></returns>


        public void FindElement(string elementLocator, string locatorStrategy = "xpath", bool waitForElement = true, int waitTimeSec = 20)
        {
            Element  = null;

            SetLocator(elementLocator, locatorStrategy);

            int waitLoopCounter = (waitTimeSec < 1) ? 1 : waitTimeSec;
            for (int waitCount = waitLoopCounter; waitCount > 0; waitCount--)
            {
                Thread.Sleep(1000);
                try
                {
                    Element = Driver.FindElement(Locator);
                    break;
                }
                catch
                {

                    Logger.Write("Locator Not Found");

                }
            }

            if (Element == null)
            {
                LogMsg = $"Unable to Locate Element:\t{Locator} using strategy {locatorStrategy}.";
                Logger.Write(LogMsg);
                throw new Exception(LogMsg);
            }

        }


        /*
        public IWebElement FindElement(string elementLocator,string locatorStrategy = "xpath",bool waitForElement = true,int waitTimeSec = 20)
        {
            IWebElement returnElement = null;

            SetLocator(elementLocator, locatorStrategy);

            int waitLoopCounter = (waitTimeSec < 1) ? 1 : waitTimeSec;
            for (int waitCount = waitLoopCounter; waitCount > 0; waitCount--)
            {
                Thread.Sleep(1000);
                try
                {
                    returnElement = Driver.FindElement(Locator);
                    break;
                }
                catch
                {

                    Logger.Write("Locator Not Found");

                }
            }

            if (returnElement == null)
            {
                LogMsg = $"Unable to Locate Element:\t{Locator} using strategy {locatorStrategy}.";
                Logger.Write(LogMsg);
                throw new Exception(LogMsg);
            }
            else { 
                return returnElement;
            }
        }

        */


        /// <summary>
        /// Locates all Elements on the page that match the locator and returns a List of IWebElement objects
        /// <para><br>-- elementLocator = The locator for the element (IE: #id, "//a", etc)</br>
        /// <br>-- locatorStrategy = The Strategy to use to locate the element </br>
        /// <br>-- -- -- xpath (default)</br>
        /// <br>-- -- -- css / cssselector</br>
        /// <br>-- -- -- name</br>
        /// <br>-- -- -- id</br>
        /// <br>-- waitForElement = Wait for element to appear on page (true {default}/false)</br>
        /// <br>-- waitTimeSec = Number of seconds to wait for the element (default = 20)</br>
        /// </para>
        /// </summary>
        /// <param name="elementLocator"></param>
        /// <param name="locatorStrategy"></param>
        /// <param name="waitForElement"></param>
        /// <param name="waitTimeSec"></param>
        /// <returns></returns>


        public void FindElements(string elementLocator, string locatorStrategy = "xpath", bool waitForElement = true, int waitTimeSec = 20)
        {
            string LogMsg;
            Elements = new List<IWebElement>();


            SetLocator(elementLocator, locatorStrategy);

            int waitLoopCounter = (waitTimeSec < 1) ? 1 : waitTimeSec;
            for (int waitCount = waitLoopCounter; waitCount > 0; waitCount--)
            {
                Thread.Sleep(1000);
                try
                {
                    Elements = new List<IWebElement>(Driver.FindElements(Locator));

                    break;
                }
                catch
                {

                    Logger.Write("Locator Not Found");

                }
            }

            if (Elements.Count == 0)
            {
                LogMsg = $"Unable to Locate Element:\t{Locator} using strategy {locatorStrategy}.";
                Logger.Write(LogMsg);
                throw new Exception(LogMsg);
            }

        }


        /*
        public List<IWebElement> FindElements(string elementLocator, string locatorStrategy = "xpath", bool waitForElement = true, int waitTimeSec = 20)
        {
            string LogMsg;
            List<IWebElement> returnElements = new List<IWebElement>();


            SetLocator(elementLocator, locatorStrategy);

            int waitLoopCounter = (waitTimeSec < 1) ? 1 : waitTimeSec;
            for (int waitCount = waitLoopCounter; waitCount > 0; waitCount--)
            {
                Thread.Sleep(1000);
                try
                {
                    returnElements = new List<IWebElement>(Driver.FindElements(Locator));
                    
                    break;
                }
                catch
                {

                    Logger.Write("Locator Not Found");

                }
            }

            if (returnElements.Count == 0)
            {
                LogMsg = $"Unable to Locate Element:\t{Locator} using strategy {locatorStrategy}.";
                Logger.Write(LogMsg);
                throw new Exception(LogMsg);
            }
            else
            {
                return returnElements;
            }

        }

        */

        //  ---------------------------------------------------------------
        //  Element Interactions (FE)
        //  ---------------------------------------------------------------
        //  ---------------------------------------------------------------



        /// <summary>
        /// Clicks on an element using the Selenium WebDriver
        /// </summary>
        public void Click()
        {
            string outMsg;
            try
            {
                Logger.Write("Clicking on Element.");
                Element.Click();
            }
            catch (Exception e)
            {
                outMsg = $"ERROR:\tCannot Click with Selenium.\n{e}";
                Logger.Write(outMsg);
                throw new Exception(outMsg);

            }

        }

        public string GetElementAttribute(string attribute2Get = "innerText")
        {
            string outMsg;
            string outValue = "";

            switch (attribute2Get.ToLower())
            {
                case "innertext":
                case "text":
                    outValue = Element.GetAttribute("innerText");
                    break;
                case "innerhtml":
                case "html":
                    outValue = Element.GetAttribute("innerHTML");
                    break;
                case "value":
                    outValue = Element.GetProperty("value");
                    break;

                default:
                    outMsg = $"ERROR:\tThe attribute provided [{attribute2Get}] does not match a valid type. ";
                    Logger.Write(outMsg);
                    throw new Exception(outMsg);
            }

            return outValue;



        }




    }
}
