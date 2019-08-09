using System;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;

namespace csasync
{

  class Program
    {
        const int waitTime = 100;

        static void Main(string[] args)
        {
            var t = new Stopwatch();
            t.Start();
            RunAsync(args[0], args[1]).Wait();
            t.Stop();
            Console.WriteLine($"Run time: {t.ElapsedMilliseconds}");
        }
        
        static Task RunAsync(string runArg, string waitArg) 
        {
            switch(runArg)
            {
                case "1": return Run1Async(waitArg);
                case "2": return Run2Async(waitArg);
            }
            throw new Exception($"Unknown run arg: {runArg}");
        }

        static async Task Run1Async(string waitArg) 
        {
            for (int i = 0; i < 100; i++)
            {
                await WaitAsync(waitArg);
            }
        }

        static async Task Run2Async(string waitArg)
        {
            var tasks = new Task[100];
            for (int i = 0; i < 100; i++)
            {
                tasks[i] = WaitAsync(waitArg);
            }

            for (int i = 0; i < 100; i++)
            {
                await tasks[i];
            }
        }
        
        static Task WaitAsync(string arg)
        {
            switch(arg) {
                case "1": return Wait1Async();
                case "2": return Wait2Async();
            }
            throw new Exception($"Unexpected wait arg: {arg}");
        }

        static Task Wait1Async() => Task.Delay(waitTime);
        static Task Wait2Async() => Task.Run(() => Thread.Sleep(waitTime));
    }
}
