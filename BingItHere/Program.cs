using System;
using CoreTools;
using OpenQA.Selenium;
using System.Threading;
using System.Collections.Generic;

namespace BingItHere
{
    class Program
    {



        static void Main(string[] args)
        {

            
            IWebElement element;
            List<IWebElement> Elements = new List<IWebElement>();

            Console.WriteLine("Hello World!");
            CoreTools.CoreTools coreTools = new CoreTools.CoreTools();

            coreTools.OpenBrowser("ff");
            coreTools.NavTo("https://www.seleniumeasy.com/test/basic-checkbox-demo.html");
            coreTools.FindElement("isAgeSelected", "id");
            Console.WriteLine("Get Value:\t" + coreTools.GetElementAttribute("value"));
            coreTools.Click();
            Console.WriteLine("Get Value:\t" + coreTools.GetElementAttribute("value"));

            Thread.Sleep(5000);

            coreTools.NavTo("http://www.ltaat.com");

            Console.WriteLine("Find Element");
            //            element = coreTools.FindElement("//a[text()='About the Founder']");
            coreTools.FindElement("//a[text()='About the Founder']");
            element = coreTools.Element;
            Console.WriteLine(element.GetAttribute("innerText"));
            Console.WriteLine(element.GetAttribute("innerHTML"));
            Console.WriteLine("Find Elements");
            //            Elements = coreTools.FindElements("//a");
            //            Console.WriteLine($"Elements Found:\t{Elements.Count}");

            coreTools.FindElements("//a");
            Console.WriteLine($"Elements Found:\t{coreTools.Elements.Count.ToString()}");
            Console.WriteLine("Click Element");
            coreTools.Click();

            Thread.Sleep(10000);
            coreTools.CloseBrowser();


        }
    }
}
