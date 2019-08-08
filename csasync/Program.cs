using System;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Timers;

namespace csasync
{
  
    class Program
    {
        private async Task WaitAsync(int ms)
        {
            await Task.Delay(ms);
        }

        private Task Wait2Async(int ms)
        {
            var interval = TimeSpan.FromMilliseconds(ms);
            var start = DateTime.Now;
            while (DateTime.Now - start < interval);
            return Task.FromResult(true);
        }

        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            var t = new Stopwatch();
            t.Start();
            new Program().Run2().Wait();
            t.Stop();
            Console.WriteLine($"Run time: {t.ElapsedMilliseconds}");
        }

        public async Task Run1() 
        {
            for (int i = 0; i < 10; i++)
                await Wait2Async(100);
        }

        public async Task Run2()
        {
            var tasks = new Task[10];
            for (int i = 0; i < 10; i++)
            {
                tasks[i] = Wait2Async(100);
            }
            await Task.WhenAll(tasks);
        }
    }
}
