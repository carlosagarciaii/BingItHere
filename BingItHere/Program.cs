using System;
using CoreTools;
using OpenQA.Selenium;
using System.Threading;
namespace BingItHere
{
    class Program
    {


        static void Main(string[] args)
        {

            IWebElement element;
            Console.WriteLine("Hello World!");
            CoreTools.CoreTools coreTools = new CoreTools.CoreTools();

            coreTools.OpenBrowser("ff");
            coreTools.NavTo("http://www.ltaat.com");
            Console.WriteLine("Find Element");
            element = coreTools.FindElement("//a[text()='About the Founder']");
            Console.WriteLine("Click Element");
            element.Click();


        }
    }
}
