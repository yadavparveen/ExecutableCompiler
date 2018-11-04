using Microsoft.CSharp;
using System;
using System.CodeDom.Compiler;

using System.Reflection;
using System.Text;

namespace ExecutableString
{
    class Program
    {
        static void Main(string[] args)
        {
            var dateTime= Execute("return DateTime.Now");
            var conditionalStaement = Execute("if (5 < 2) { return 5; } else { return 2; }" );
            var add = Execute("return 5+10");
            var addWithBrackets = Execute("return (2*10) + (3*20)");
            var divideWithBrackets = Execute("return (10/5) - (20/2)");


            Console.ReadKey();
        }



        private static string CreateExecuteMethodTemplate(string content)
        {
            var builder = new StringBuilder();

            builder.Append("using System;");
            builder.Append("\r\nnamespace Lab");
            builder.Append("\r\n{");
            builder.Append("\r\npublic sealed class Cal");
            builder.Append("\r\n{");
            builder.Append("\r\npublic static object Execute()");
            builder.Append("\r\n{");
            builder.AppendFormat("\r\n {0};", content);
            //builder.AppendFormat("\r\nreturn a;");
            builder.Append("\r\n}");
            builder.Append("\r\n}");
            builder.Append("\r\n}");

            return builder.ToString();
        }

        private static object Execute(string content)
        {
            var codeProvider = new CSharpCodeProvider();
            var compilerParameters = new CompilerParameters
            {
                GenerateExecutable = false,
                GenerateInMemory = true
            };

            compilerParameters.ReferencedAssemblies.Add("system.dll");

            string sourceCode = CreateExecuteMethodTemplate(content);
            CompilerResults compilerResults = codeProvider.CompileAssemblyFromSource(compilerParameters, sourceCode);
            Assembly assembly = compilerResults.CompiledAssembly;
            Type type = assembly.GetType("Lab.Cal");
            MethodInfo methodInfo = type.GetMethod("Execute");

            return methodInfo.Invoke(null, null);
        }
    }

}
