using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.IE;
using OpenQA.Selenium.Edge;
using System.IO;
using System.Threading;
using System.Collections.Generic;
using System.Text.RegularExpressions;


namespace CoreTools
{
    public class CoreTools
    {

        //  ---------------------------------------------------------------
        //  GENERAL
        //  ---------------------------------------------------------------
        //  ---------------------------------------------------------------


        private Logger logger = new Logger(CTConstants.LOG_DEBUG);

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
            string funcName = "SetDriverFileName";

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
                    logger.Write(LogMsg,funcName,CTConstants.LOG_CRITICAL);
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
            string funcName = "SetDriverFilePath";
            string msg = "";
            bool isFoundDriverDirPath = false;
            string fullDriverFilePath;

            foreach (string driverDirPathItem in CTConstants.DEFAULT_DRIVER_DIRECTORIES)
            {
                if (Directory.Exists(driverDirPathItem))
                {
                    isFoundDriverDirPath = true;
                    logger.Write($"Driver Path Found:\t{DriverFilePath}",funcName,CTConstants.LOG_INFO);
                    fullDriverFilePath = driverDirPathItem + "/" + DriverFileName;

                    if (File.Exists(fullDriverFilePath))
                    {
                        logger.Write($"Driver File Found:\t{fullDriverFilePath}",funcName,CTConstants.LOG_INFO) ;
                        DriverFilePath = driverDirPathItem + "/";
                        return;
                    }
                }
            }
            if (!isFoundDriverDirPath)
            {
                msg = "EXCEPTION:\n\tDriver Directory Cannot Be Found";
                logger.Write(msg, funcName, CTConstants.LOG_CRITICAL);
                throw new Exception(msg);
            }

            msg = "EXCEPTION:\n\tDriver File Cannot Be Found";
            logger.Write(msg, funcName, CTConstants.LOG_CRITICAL);
            throw new Exception(msg);
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
            string funcName = "CreateSession";

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
                    logger.Write(LogMsg,funcName);
                    throw new Exception(LogMsg);
            }

        }


        /*
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
            string funcName = "OpenBrowser";
            logger.Write("Opening Browser",funcName,CTConstants.LOG_INFO);
            BrowserName = browserName;

            SetDriverFileName();

            SetDriverFilePath();

            Driver = CreateSession();


        }
        */


        /// <summary>
        /// Instantiates the class and Opens the browser session 
        /// <para><br>browserName = the name of the browser to open</br>
        /// <br>-Options:</br>
        /// <br>---FireFox, FF</br>
        /// <br>---Google, Chrome</br>
        /// <br>---IE, IExplore</br>
        /// <br>---Edge, MSEdge</br>
        /// <br>loggingLevel = The highest level to log</br>
        /// </para>
        /// </summary>
        /// <param name="browserName"></param>
        public CoreTools(string browserName)
        {
        
            string funcName = "OpenBrowser";
            logger.Write("Opening Browser", funcName, CTConstants.LOG_INFO);
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
            string funcName = "CloseBrowser";

            logger.Write("Closing Browser Session",funcName,CTConstants.LOG_INFO);
            try
            {
                Driver.Close();
            }
            catch (Exception e)
            {
                string exceptionMsg = $"EXCEPTION:\n\tBrowser has failed to close.\n{e}";
                logger.Write(exceptionMsg,funcName,CTConstants.LOG_WARNING);
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
            string funcName = "NavTo";
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
                        logger.Write($"Ready State:\t{ReadyState}",funcName,CTConstants.LOG_INFO);
                        if (ReadyState.ToLower() == "complete") break;
                    }
                    catch (Exception e)
                    {
                        string LogMsg = $"\nEXCEPTION HAS OCCURRRED\n\t{e}";
                        logger.Write(LogMsg,funcName,CTConstants.LOG_ERROR);
                        throw new Exception(LogMsg);
                    }

                }
                if (ReadyState.ToLower() == "complete") break;
            }

            if (ReadyState.ToLower() != "complete") throw new Exception($"\nEXCEPTION:\n\tFailed to Load Page {goToURL}");
            logger.Write($"Success - Navigated to:\t{Driver.Url.ToString()}",funcName,CTConstants.LOG_INFO);


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
            string funcName = "SetLocator";
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
                    LogMsg = $"EXCEPTION\tLOCATOR ERROR\nError:\tFE00001\n\tThe Locator Stragety Provided does not match a recognized strategy.\n\tLocator Strategy Provided:\t{locatorStrategy}\n\tLocator:\t{elementLocator}";
                    logger.Write(LogMsg,funcName,CTConstants.LOG_CRITICAL);
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
        /// <br>-- isRequired = Whether element is required. (Default: true)</br>
        /// <br>-- waitForElement = Wait for element to appear on page (true {default}/false)</br>
        /// <br>-- waitTimeSec = Number of seconds to wait for the element (default = 20)</br>
        /// </para>
        /// </summary>
        /// <param name="elementLocator"></param>
        /// <param name="locatorStrategy"></param>
        /// <param name="isRequired"></param>
        /// <param name="waitForElement"></param>
        /// <param name="waitTimeSec"></param>
        /// <returns></returns>


        public void FindElement(string elementLocator, string locatorStrategy = "xpath",bool isRequired = true,bool waitForElement = true, int waitTimeSec = 20)
        {
            string funcName = "FindElement";
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
                    LogMsg = $"Locator not found:\t{elementLocator}\t|\t{locatorStrategy}";

                    logger.Write(LogMsg,funcName,CTConstants.LOG_WARNING);

                }
            }

            if (Element == null)
            {
                LogMsg = $"Unable to Locate Element:\t{Locator} using strategy {locatorStrategy}.";
                if (isRequired)
                {
                    logger.Write(LogMsg, funcName, CTConstants.LOG_CRITICAL);
                    throw new Exception(LogMsg);

                }
                else
                {
                    logger.Write(LogMsg, funcName, CTConstants.LOG_WARNING);
                }
            }

        }



        /// <summary>
        /// Locates all Elements on the page that match the locator and returns a List of IWebElement objects
        /// <para><br>-- elementLocator = The locator for the element (IE: #id, "//a", etc)</br>
        /// <br>-- locatorStrategy = The Strategy to use to locate the element </br>
        /// <br>-- -- -- xpath (default)</br>
        /// <br>-- -- -- css / cssselector</br>
        /// <br>-- -- -- name</br>
        /// <br>-- -- -- id</br>
        /// <br>-- isRequired = If element is required (Default true)</br>
        /// <br>-- waitForElement = Wait for element to appear on page (true {default}/false)</br>
        /// <br>-- waitTimeSec = Number of seconds to wait for the element (default = 20)</br>
        /// </para>
        /// </summary>
        /// <param name="elementLocator"></param>
        /// <param name="locatorStrategy"></param>
        /// <param name="isRequired"></param>
        /// <param name="waitForElement"></param>
        /// <param name="waitTimeSec"></param>
        /// <returns></returns>


        public void FindElements(string elementLocator, string locatorStrategy = "xpath", bool isRequired = true, bool waitForElement = true, int waitTimeSec = 20)
        {

            string funcName = "FindElements";
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
                    LogMsg = $"Locator not found:\t{elementLocator}\t|\t{locatorStrategy}";

                    logger.Write(LogMsg, funcName, CTConstants.LOG_WARNING);


                }
            }

            if (Elements.Count == 0)
            {
                LogMsg = $"Unable to Locate Element:\t{Locator} using strategy {locatorStrategy}.";
                if (isRequired) {
                    logger.Write(LogMsg, funcName, CTConstants.LOG_ERROR);
                    throw new Exception(LogMsg); 
                }
                else
                {

                    logger.Write(LogMsg, funcName, CTConstants.LOG_WARNING);
                }
            }

        }

     

        //  ---------------------------------------------------------------
        //  Element Interactions (FE)
        //  ---------------------------------------------------------------
        //  ---------------------------------------------------------------



        /// <summary>
        /// Clicks on an element using the Selenium WebDriver
        /// </summary>
        public void Click()
        {
            string funcName = "Click";
            string outMsg;
            try
            {
                logger.Write("Clicking on Element.",funcName,CTConstants.LOG_WARNING);
                Element.Click();
            }
            catch (Exception e)
            {
                outMsg = $"ERROR:\tCannot Click with Selenium.\n{e}";
                logger.Write(outMsg,funcName,CTConstants.LOG_ERROR);
                throw new Exception(outMsg);

            }

        }


        /// <summary>
        /// Gets Element Attributes (IE: InnerText, InnerHTML)
        /// <para><br>--- attribute2Get = the attribute to get.</br>
        /// <br>--- --- innerHTML</br>
        /// <br>--- --- innerText</br></para>
        /// </summary>
        /// <param name="attribute2Get"></param>
        /// <returns></returns>
        public string GetElementAttribute(string attribute2Get = "innerText")
        {
            string funcName = "GetElementAttribute";
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
                    logger.Write(outMsg,funcName,CTConstants.LOG_ERROR);
                    throw new Exception(outMsg);
            }

            return outValue;



        }


        /// <summary>
        /// Sends keys to the element
        /// <para>sendValue = the text to send
        /// <br>doTrim = Trim all beginning and ending white space as well as removing double spaces</br></para>
        /// </summary>
        /// <param name="sendValue"></param>
        /// <param name="doTrim"></param>
        public void SendKeys(string sendValue,bool doTrim = true)
        {
            string funcName = "SendKeys";

            if (doTrim)
            {
                // Remove Double Spacing
                sendValue = Regex.Replace(sendValue,"\\s+"," ");
                // Left Trim/Right Trim
                sendValue = sendValue.Trim();
            }

            try
            {
                Element.SendKeys(sendValue);
            }
            catch(Exception e)
            {
                LogMsg = $"Unable to Send text [{sendValue}] to Element [{Element}]\n{e}";
                logger.Write(LogMsg, funcName, CTConstants.LOG_ERROR);
                throw new Exception(LogMsg);
            }

        }



    }
}
