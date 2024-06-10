using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using PirogAlex.ExtensionsLib.CrossPlatform;

namespace ConsoleApp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello, World!");

            bool runAllTests = false;

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


            if (runAllTests)
            {
                Test1_PathCombine();
            }
            Test2_PathGetFileNameAndDirectoryName();
        }

        private static void Test2_PathGetFileNameAndDirectoryName()
        {
            /*
             Linux:

               Дефолтный Path.Combine (имя):D:\WORK Projects\eDiscovery\filesToSearchTest\01. Ячейка на основном листе.xlsx
                   кроссплатформенный (имя):1. Ячейка на основном листе.xlsx
               Дефолтный Path.Combine (путь):
                   кроссплатформенный (путь):D:/WORK Projects/eDiscovery/filesToSearchTest
                   кроссплатформенный (путь):D:\WORK Projects\eDiscovery\filesToSearchTest
                            направление слэша зависит от целевой платформы
             */

            /*
             Windows:

               Дефолтный Path.Combine (имя):01. Ячейка на основном листе.xlsx
                   кроссплатформенный (имя):01. Ячейка на основном листе.xlsx
               Дефолтный Path.Combine (путь):D:\WORK Projects\eDiscovery\filesToSearchTest
                   кроссплатформенный (путь):D:\WORK Projects\eDiscovery\filesToSearchTest
             */
            var crossPlatform = new CrossPlatform(TargetPlatform.Windows);
            Console.WriteLine();
            Console.WriteLine("Тест разбивки цельного пути на путь и имя файла...");
            //D:\WORK Projects\eDiscovery\filesToSearchTest\01. Ячейка на основном листе.xlsx
            var filePathWithName = "D:\\WORK Projects\\eDiscovery\\filesToSearchTest\\01. Ячейка на основном листе.xlsx";
            Console.WriteLine("Дефолтный Path.Combine (имя):" + Path.GetFileName(filePathWithName));
            Console.WriteLine("    кроссплатформенный (имя):" + crossPlatform.PathGetFileName(filePathWithName));
            Console.WriteLine("Дефолтный Path.Combine (путь):" + Path.GetDirectoryName(filePathWithName));
            Console.WriteLine("    кроссплатформенный (путь):" + crossPlatform.PathGetDirectoryName(filePathWithName));
            Console.WriteLine("Тест разбивки пути завершён!");
            Console.WriteLine();
        }

        private static void Test1_PathCombine()
        {
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
            var crossPlatform = new CrossPlatform(TargetPlatform.Linux);
            Console.WriteLine("Дефолтный Path.Combine :" + Path.Combine(Environment.CurrentDirectory, "\\abc\\abc"));
            Console.WriteLine("    кроссплатформенный :" + crossPlatform.PathCombine(Environment.CurrentDirectory, "\\abc\\abc"));
            Console.WriteLine();
            Console.WriteLine("Дефолтный Path.Combine File.Exists(...):" + File.Exists(Path.Combine(Environment.CurrentDirectory, "\\abc\\abc\\TestPositive.pptx")));
            Console.WriteLine("    кроссплатформенный File.Exists(...):" + File.Exists(crossPlatform.PathCombine(Environment.CurrentDirectory, "\\abc\\abc\\TestPositive.pptx")));
            Console.WriteLine();
            Console.WriteLine("Дефолтный Path.Combine :" + Path.Combine(Environment.CurrentDirectory, "/abc/abc"));
            Console.WriteLine("    кроссплатформенный :" + crossPlatform.PathCombine(Environment.CurrentDirectory, "/abc/abc"));
            Console.WriteLine();
            Console.WriteLine("Дефолтный Path.Combine :" + Path.Combine("/ed/inside", "TestPositive.pptx"));
            Console.WriteLine("    кроссплатформенный :" + crossPlatform.PathCombine("/ed/inside", "TestPositive.pptx"));
            Console.WriteLine();
            Console.WriteLine("Дефолтный Path.Combine :" + Path.Combine("/ed/inside/", "TestPositive2.pptx"));
            Console.WriteLine("    кроссплатформенный :" + crossPlatform.PathCombine("/ed/inside/", "TestPositive2.pptx"));
            Console.WriteLine();
            Console.WriteLine("Дефолтный Path.Combine :" + Path.Combine("/ed/inside", "/TestPositive3.pptx"));
            Console.WriteLine("    кроссплатформенный :" + crossPlatform.PathCombine("/ed/inside", "/TestPositive3.pptx"));
            Console.WriteLine();
            Console.WriteLine("Дефолтный Path.Combine :" + Path.Combine("/ed/inside/", "/TestPositive4.pptx"));
            Console.WriteLine("    кроссплатформенный :" + crossPlatform.PathCombine("/ed/inside/", "/TestPositive4.pptx"));
            Console.WriteLine();
            Console.WriteLine("Дефолтный Path.Combine :" + Path.Combine("D:\\WORK Projects\\eDiscovery\\filesToSearchTest", "тест уведомления при обновлении файла.docx"));
            Console.WriteLine("    кроссплатформенный :" + crossPlatform.PathCombine("D:\\WORK Projects\\eDiscovery\\filesToSearchTest", "тест уведомления при обновлении файла.docx"));
            Console.WriteLine();


            Console.WriteLine();
            Console.WriteLine("Ситуация такова, что агент(сторона работающая с полученным путём) и сервер(тот кто формирует путь) могут находится на разных OS Platform.");
            Console.WriteLine("     Если агент будет на Windows, то он поймёт любой путь что сформирует сервер.");
            Console.WriteLine("     Но если агент будет на Linux, то он поймёт путь что сформируется с косой чертой только как в браузере '/'." +
                              " С такой чертой сервер на Linux и сервер на Windows формирует только в следующих случаях:");
            Console.WriteLine("\"TestPositive2.pptx\" и \"/TestPositive4.pptx\"");
            Console.WriteLine("     Так как именно в них basePath оканчивается на разделитель каталогов с нужной косой чертой в нужную для Linux сторону.");
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine("  upd 001:  Ввёл указание целевой платформы. Теперь формирование пути не зависит от формирующей его стороны, а зависит от указанной целевой платформы. Все разделители будут подогнаны под указанную целевую платформу!");
            Console.WriteLine();
        }
    }
}
