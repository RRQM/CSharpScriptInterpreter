using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.CSharp;

namespace CSharpScriptInterpreter
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length > 0)
            {
                foreach (string arg in args)
                {
                    if (File.Exists(arg))
                    {
                        Run(arg);
                    }
                    else
                    {
                        Order(arg);
                    }
                }

            }
            else
            {
              string order=  Console.ReadLine();
                if (File.Exists(order))
                {
                    Run(order);
                }
                else
                {
                    Order(order);
                }
            }


        }

        static void Run(string path)
        {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            Console.WriteLine("执行开始");
            CodeProvider codeProvider = new CodeProvider();
            codeProvider.Path = path;

            string mes;
            if (!codeProvider.PreviewRun(out mes))
            {
                Console.WriteLine(mes);
                return;
            }

            if (!codeProvider.Run(out mes))
            {
                Console.WriteLine(mes);
                return;
            }
            stopwatch.Stop();
            Console.WriteLine("执行结束,用时：{0}ms", stopwatch.Elapsed.TotalMilliseconds);
            Console.WriteLine();

        }

        static void Order(string orderString)
        {
            switch (orderString)
            {
                case "-":
                    {
                        ConsoleWriteLine("我爱你们",ConsoleColor.Magenta);
                        break;
                    }
                default:ConsoleWriteLine("指令无效",ConsoleColor.Red);
                    break;
            }
        }

        static void ConsoleWriteLine(string mes,ConsoleColor consoleColor= ConsoleColor.White)
        {
            Console.ForegroundColor = consoleColor;
            Console.WriteLine(mes);
            Console.ForegroundColor = ConsoleColor.White;
        }
    }
}
