using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using log4net;
using log4net.Repository.Hierarchy;

namespace Log4Net.Appender.SignalR.ConsoleTest
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Starting logging");

            Console.WriteLine("\n Press 1 for Information \n Press 2 for Warning \n Press 3 for Error");
            var test = new LoggerTest();

            while (true)
            {

                var consolKey = Console.ReadKey();
                if (consolKey.Key == ConsoleKey.X)
                {
                    break;
                }
                if (consolKey.Key == ConsoleKey.NumPad1 || consolKey.Key == ConsoleKey.D1)
                {
                    test.Do(1);
                }

                if (consolKey.Key == ConsoleKey.NumPad2 || consolKey.Key == ConsoleKey.D2)
                {
                    test.Do(2);
                }

                if (consolKey.Key == ConsoleKey.NumPad3 || consolKey.Key == ConsoleKey.D3)
                {
                    test.Do(3);
                }

                Console.WriteLine();
            }

            Console.ReadLine();
        }
    }

    public class LoggerTest
    {
        private static readonly ILog Logger = LogManager.GetLogger("ConsoleApp");

        public void Do(int i)
        {
            if (i == 1)
            {
                Logger.Info("This is an information");
            }

            if (i == 2)
            {
                Logger.Warn("This is an Warning");
            }

            if (i == 3)
            {
                Logger.Error("This is an Error message");
            }

            if (i==4)
            {
                Logger.Fatal("This is an Fatal message");
            }

            //for (int i = 0; i < 20; i++)
            //{
            //    Logger.Info("Hello World");
            //}
        }
    }
}
