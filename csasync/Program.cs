using System;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;

namespace csasync
{

  class Program
    {
        private Task WaitAsync(int ms)
        {
            return Task.Delay(ms);
        }

        public Task Wait2Async(int ms)
        {
            return Task.Run(() => Thread.Sleep(ms));
        }

        static void Main(string[] args)
        {
            var t = new Stopwatch();
            t.Start();
            var prog = new Program();
            switch(args[0])
            {
                case "1":
                    prog.Run1().Wait();
                    break;
                case "2":
                    prog.Run2().Wait();
                    break;
                default:
                    Console.WriteLine($"Unknown command: {args[0]}");
                    return;
            }
            t.Stop();
            Console.WriteLine($"Run time: {t.ElapsedMilliseconds}");
        }

        public async Task Run1() 
        {
            for (int i = 0; i < 100; i++)
            {
                await WaitAsync(100);
            }
        }

        public async Task Run2()
        {
            var tasks = new Task[100];
            for (int i = 0; i < 100; i++)
            {
                tasks[i] = WaitAsync(100);
            }

            for (int i = 0; i < 100; i++)
            {
                await tasks[i];
            }
        }
    }
}
