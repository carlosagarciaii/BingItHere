using System;
using CoreTools;

namespace BingItHere
{
    class Program
    {


        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            CoreTools.CoreTools coreTools = new CoreTools.CoreTools();

            coreTools.OpenBrowser("ff");
            coreTools.NavTo("http://www.ltaat.com");

        }
    }
}
