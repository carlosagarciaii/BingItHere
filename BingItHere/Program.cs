using System;
using CoreToolSet;
using OpenQA.Selenium;
using System.Threading;
using System.Collections.Generic;
using BingItHere.Tests;

namespace BingItHere
{
    class Program
    {



        static void Main(string[] args)
        {
            

            CoreToolSet.CoreTools coreTools = new CoreToolSet.CoreTools("ff",CoreToolSet.CTConstants.LOG_DEBUG);




            FormTest formTest = new FormTest();
            formTest.Run(coreTools);

            Thread.Sleep(5000);


            FindElementTests fet = new FindElementTests();
            fet.Run(coreTools);

            Thread.Sleep(10000);
            coreTools.CloseBrowser();


        }
    }
}
