using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Microsoft.CSharp;

namespace CSharpScriptInterpreter
{
    public class CodeProvider
    {
        public string Path { get; set; }
        private Assembly Assembly { get; set; }

        public bool PreviewRun(out string mes)
        {
            // 1.CSharpCodePrivoder
            CSharpCodeProvider cSharpCodePrivoder = new CSharpCodeProvider();
            StringBuilder stringBuilder = new StringBuilder();
            // 3.CompilerParameters
            CompilerParameters objCompilerParameters = new CompilerParameters();
            objCompilerParameters.ReferencedAssemblies.Add("System.dll");
            objCompilerParameters.ReferencedAssemblies.Add("Microsoft.CSharp.dll");
            objCompilerParameters.ReferencedAssemblies.Add("System.Core.dll");
            objCompilerParameters.ReferencedAssemblies.Add("System.Data.dll");
            objCompilerParameters.ReferencedAssemblies.Add("System.Net.Http.dll");
            objCompilerParameters.ReferencedAssemblies.Add("System.Xml.dll");
            objCompilerParameters.ReferencedAssemblies.Add("System.Xml.Linq.dll");
            objCompilerParameters.GenerateExecutable = false;
            objCompilerParameters.GenerateInMemory = true;

            // 4.CompilerResults
            CompilerResults cr = cSharpCodePrivoder.CompileAssemblyFromFile(objCompilerParameters, Path);

            if (cr.Errors.HasErrors)
            {
                stringBuilder.AppendLine("脚本错误：");
                foreach (CompilerError err in cr.Errors)
                {
                    stringBuilder.AppendLine(err.ErrorText);
                }
                mes = stringBuilder.ToString();
                return false;
            }
            else
            {
                // 通过反射，调用HelloWorld的实例
                this.Assembly = cr.CompiledAssembly;
                mes = null;
                return true;
            }

        }

        public bool Run(out string mes)
        {
            try
            {
                Type[] types = this.Assembly.GetTypes();

                List<MethodInfo> methods = new List<MethodInfo>();
                foreach (Type type in types)
                {
                    methods.Add(type.GetMethod("Main"));
                }
                foreach (MethodInfo methodInfo in methods)
                {
                    object instance = Activator.CreateInstance(methodInfo.DeclaringType);
                    methodInfo.Invoke(instance, null);
                }

                mes = null;
                return true;
            }
            catch (TargetInvocationException e)
            {
                mes =e.Message+"==>内部错误信息：" + e.InnerException.Message;
                return false;
            }
            catch (Exception e)
            {
                mes = e.Message;
                return false;
            }
            
        }
    }
}
