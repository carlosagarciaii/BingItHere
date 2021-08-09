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
            

            CoreTools.CoreTools coreTools = new CoreTools.CoreTools("ff",CoreTools.CTConstants.LOG_DEBUG);

            // Table Tests
                coreTools.NavTo("https://en.wikipedia.org/wiki/List_of_Nintendo_Entertainment_System_games");
                coreTools.FindElement("//table[@id='softwarelist']", "xpath");
                coreTools.Table2Array();

/*
            //Form Tests
                coreTools.NavTo("https://www.seleniumeasy.com/test/basic-checkbox-demo.html");
                coreTools.FindElement("isAgeSelected", "id");
                Console.WriteLine("Get Value:\t" + coreTools.GetProperty("value"));
                coreTools.Click();
                Console.WriteLine("Get Value:\t" + coreTools.GetProperty("value"));


                coreTools.NavTo("https://www.seleniumeasy.com/test/basic-first-form-demo.html");
                coreTools.FindElement("at-cv-lightbox-close", "id", false);
                coreTools.Click();
                Thread.Sleep(1000);
                coreTools.FindElement("user-message", "id",true);

                Thread.Sleep(3000);
                coreTools.SendKeys("    This is my test   I Test   ");
                Thread.Sleep(5000);


                Console.WriteLine("Get Value:\t" + coreTools.GetProperty("value"));
                Console.WriteLine("Get Value:\t" + coreTools.GetAttribute("placeholder"));
                Thread.Sleep(5000);
//            */


/*
            // Find Element Tests
            coreTools.NavTo("http://www.ltaat.com");


            coreTools.FindElement("//*[contains(text(),'LTAAT')]");
            coreTools.SetAttribute("class", "");
            coreTools.SetAttribute("style", "text-align:right,font-size:200");

            Console.WriteLine("Attribute:\t" + coreTools.GetAttribute("style"));

            
            coreTools.FindElement("//a[text()='About the Founder']");
            //element = coreTools.Element;
            
            coreTools.FindElements("//a");
            coreTools.Click();

//  */

            Thread.Sleep(10000);
            coreTools.CloseBrowser();


        }
    }
}
