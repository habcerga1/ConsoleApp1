using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Holoxomo.Network;
using PcapDotNet.Core;
using System.Reactive.Linq;
using System.Threading.Tasks.Dataflow;
using Newtonsoft.Json;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            var st = Stopwatch.StartNew();
            Parallel.For(0, 1000, i =>
            {
                new Request().GetHttpWebRequestAsync(i).ConfigureAwait(false);
            });
            Console.WriteLine($" ****** ST:{st.Elapsed} ******");
            Console.ReadKey();
        }
    }
    
}
         
        
