using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ConsoleApplication10
{
    class Program
    {
        private static void Main()
        {
            var cancelSource = new CancellationTokenSource();
            new Thread(() =>
            {
                try
                {
                    Work(cancelSource.Token); //).Start();
                }
                catch (OperationCanceledException)
                {
                    Console.WriteLine("Canceled!");
                }
            }).Start();

            Thread.Sleep(1000);
            cancelSource.Cancel(); // Safely cancel worker.
            Console.ReadLine();
        }
        private static void Work(CancellationToken cancelToken)
        {
            while (true)
            {
                if (cancelToken.IsCancellationRequested)
                {
                    return;
                }
                Console.WriteLine(DateTime.Now.ToString("fffff"));
            }
            //或者
            for (int i = 0; i < 100; i++)
            {
                if (cancelToken.IsCancellationRequested)
                {
                    break;
                }
            }
        }
    }
}
