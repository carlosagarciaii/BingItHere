using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using CoreToolSet;

namespace BingItHere.Tests
{
    class TableTest
    {

        public void Run(CoreTools coreTools)
        {
            // Table Tests
            coreTools.NavTo("https://en.wikipedia.org/wiki/List_of_Nintendo_Entertainment_System_games");
            coreTools.FindElement("//table[@id='softwarelist']", "xpath");
            coreTools.Table2Array();

        }



    }
}
