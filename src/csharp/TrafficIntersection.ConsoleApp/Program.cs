using System;
using System.Reactive.Concurrency;
using Serilog;

namespace TrafficIntersection.ConsoleApp
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            Log.Logger = new LoggerConfiguration().WriteTo.ColoredConsole().CreateLogger();

            var trafficController = new TrafficController(Scheduler.Default);

            var intersection = new TrafficIntersection(trafficController);
            intersection.NorthLight.Subscribe(x => Log.Information("North is now {x}", x));
            intersection.SouthLight.Subscribe(x => Log.Information("South is now {x}", x));
            intersection.WestLight.Subscribe(x => Log.Information("West is now {x}", x));
            intersection.EastLight.Subscribe(x => Log.Information("East is now {x}", x));

            Console.ReadLine();
        }
    }
}