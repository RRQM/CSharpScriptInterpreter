using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Diagnostics;
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
                Run(args);
            }
            else
            {
                Console.WriteLine("请输入脚本路径：");
                Run(Console.ReadLine());
            }


        }

        static void Run(params string[] paths)
        {
            foreach (string path in paths)
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
                Console.WriteLine("执行结束,用时：{0}ms",stopwatch.Elapsed.TotalMilliseconds);
                Console.WriteLine();
            }
        }
    }
}
