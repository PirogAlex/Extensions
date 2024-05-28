using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using PirogAlex.ExtensionsLib.System;

namespace ConsoleApp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //Console.WriteLine("Hello, World!");

            //Подключение логгера если нужен
            //using var loggerFactory = LoggerFactory.Create(builder =>
            //{
            //    builder
            //        .AddFilter("Microsoft", LogLevel.Warning)
            //        .AddFilter("System", LogLevel.Warning)
            //        //.AddFilter("LoggingConsoleApp.Program", LogLevel.Debug)
            //        .AddFilter("ConsoleApp.Program", LogLevel.Debug)
            //        .AddConsole();
            //});
            //ILogger logger = loggerFactory.CreateLogger<Program>();
            //logger.LogInformation("Example log message");


            /*
             Linux:

               Дефолтный Path.Combine :/app/\abc\abc
                   кроссплатформенный :/app/abc/abc
               
               Дефолтный Path.Combine :/abc/abc
                   кроссплатформенный :/app/abc/abc
               
               Дефолтный Path.Combine :/ed/inside/TestPositive.pptx
                   кроссплатформенный :/ed/inside/TestPositive.pptx
               
               Дефолтный Path.Combine :/ed/inside/TestPositive2.pptx
                   кроссплатформенный :/ed/inside/TestPositive2.pptx
               
               Дефолтный Path.Combine :/TestPositive3.pptx
                   кроссплатформенный :/ed/inside/TestPositive3.pptx
               
               Дефолтный Path.Combine :/TestPositive4.pptx
                   кроссплатформенный :/ed/inside/TestPositive4.pptx
             */

            /*
             Windows:

               Дефолтный Path.Combine :\abc\abc
                   кроссплатформенный :E:\Source code\PersonalGitHab\Extensions\PirogAlex.Extensions\ConsoleApp\bin\Debug\net6.0\abc\abc
               
               Дефолтный Path.Combine :/abc/abc
                   кроссплатформенный :E:\Source code\PersonalGitHab\Extensions\PirogAlex.Extensions\ConsoleApp\bin\Debug\net6.0\abc\abc
               
               Дефолтный Path.Combine :/ed/inside\TestPositive.pptx
                   кроссплатформенный :/ed/inside\TestPositive.pptx
               
               Дефолтный Path.Combine :/ed/inside/TestPositive2.pptx
                   кроссплатформенный :/ed/inside/TestPositive2.pptx
               
               Дефолтный Path.Combine :/TestPositive3.pptx
                   кроссплатформенный :/ed/inside\TestPositive3.pptx
               
               Дефолтный Path.Combine :/TestPositive4.pptx
                   кроссплатформенный :/ed/inside/TestPositive4.pptx
             */

            //// Windows
            // \abc\abc
            // /abc/abc
            ////Linux (Mac например)
            // /Users/pirogalex/testapp/bin/Debug/netcoreapp3.1/\abc\abc
            // /abc/abc
            Console.WriteLine("Дефолтный Path.Combine :" + Path.Combine(Environment.CurrentDirectory, "\\abc\\abc"));
            Console.WriteLine("    кроссплатформенный :" + CrossPlatform.PathCombine(Environment.CurrentDirectory, "\\abc\\abc"));
            Console.WriteLine();
            Console.WriteLine("Дефолтный Path.Combine :" + Path.Combine(Environment.CurrentDirectory, "/abc/abc"));
            Console.WriteLine("    кроссплатформенный :" + CrossPlatform.PathCombine(Environment.CurrentDirectory, "/abc/abc"));
            Console.WriteLine();
            Console.WriteLine("Дефолтный Path.Combine :" + Path.Combine("/ed/inside", "TestPositive.pptx"));
            Console.WriteLine("    кроссплатформенный :" + CrossPlatform.PathCombine("/ed/inside", "TestPositive.pptx"));
            Console.WriteLine();
            Console.WriteLine("Дефолтный Path.Combine :" + Path.Combine("/ed/inside/", "TestPositive2.pptx"));
            Console.WriteLine("    кроссплатформенный :" + CrossPlatform.PathCombine("/ed/inside/", "TestPositive2.pptx"));
            Console.WriteLine();
            Console.WriteLine("Дефолтный Path.Combine :" + Path.Combine("/ed/inside", "/TestPositive3.pptx"));
            Console.WriteLine("    кроссплатформенный :" + CrossPlatform.PathCombine("/ed/inside", "/TestPositive3.pptx"));
            Console.WriteLine();
            Console.WriteLine("Дефолтный Path.Combine :" + Path.Combine("/ed/inside/", "/TestPositive4.pptx"));
            Console.WriteLine("    кроссплатформенный :" + CrossPlatform.PathCombine("/ed/inside/", "/TestPositive4.pptx"));
            Console.WriteLine();

        }
    }
}
