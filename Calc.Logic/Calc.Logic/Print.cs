using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Output
{
   static class PrintConsole
    {
       static public void BooleanExpression(string exp)
       {
           System.Console.WriteLine("Expression: " + exp);
           HashSet<char> var = Variable(exp);
           int var_count = var.Count;
           foreach (char c in var)
           {
               System.Console.Write(c + " ");
           }
           System.Console.WriteLine("f");
           bool[] arg = new bool[var_count];
           for (int i = 0; i < Math.Pow(2, var_count); i++)
           {
               char[] vars = Convert.ToString(i, 2).PadLeft(8, '0').ToArray();
               Array.Reverse(vars);
               for (int j = 0; j < var_count; j++)
               {
                   arg[j] = Convert.ToBoolean(Convert.ToInt32(vars[j]) - 48);
                   System.Console.Write(Convert.ToInt32(vars[j]) - 48 + " ");
               }
               System.Console.Write(Convert.ToInt32(Calc.Logic.Expression.Result(exp, arg)));
               System.Console.WriteLine();
           }

       }

       static public HashSet<char> Variable(string exp)
       {
           HashSet<char> data = new HashSet<char>();
           foreach (char c in exp)
           {
               if (Output.Validator.IsBooleanVariable(c))
               {
                   data.Add(c);
               }
           }
           return data;
       }

    }
}
