using System;
using LuxImperium.Services;

namespace LuxImperiumConsole
{
    internal abstract class Program
    {
        private static LuxImperium.LuxImperiumGovernor _luxImperiumGovernor;
        
        public static void Main(string[] args)
        {
            var stop = false;

            Console.WriteLine("Loading scene file");
            IActionFactory actionFactory = new ActionFactory();
            _luxImperiumGovernor = new LuxImperium.LuxImperiumGovernor(actionFactory);
            _luxImperiumGovernor.LoadScene("TestScenes\\test.scene");
            Console.WriteLine("Scene file loaded");
     
            Help();
            
            while (!stop)
            {
                if (Console.KeyAvailable)
                {
                    var k = Console.ReadKey(true);

                    switch (k.Key)
                    {
                        case ConsoleKey.F1:
                            Help();
                            break;
                        case ConsoleKey.Q:
                        case ConsoleKey.Escape:
                            Console.WriteLine("Stopping LuxImperium");
                            _luxImperiumGovernor.Stop();
                            stop = true;
                            Console.WriteLine("Quitting");
                            break;
                        case ConsoleKey.L:
                            Console.WriteLine("Starting LuxImperium");
                            _luxImperiumGovernor.Start();
                            break;
                        case ConsoleKey.S:
                            Console.WriteLine("Stopping LuxImperium");
                            _luxImperiumGovernor.Stop();
                            break;

                    }
                }
            }
            
        }
        
        private static void Help()
        {
            Console.WriteLine("Escape/Q : Quit");
            Console.WriteLine("F1 : Help");
            Console.WriteLine("L : LuxImperium start");
            Console.WriteLine("S : LuxImperium stop");
        }
    }
}