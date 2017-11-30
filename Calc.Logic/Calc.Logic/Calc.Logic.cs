using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calc
{
    namespace Logic
    {
       static class Function
       {
           

           static public bool Zero()
           {
               return false;
           }
           static public bool One()
           {
               return true;
           }
           static public bool Not(bool val)
           {
               return !val;
           }
           static public bool Repeat(bool val)
           {
               return val;
           }
           static public bool NotOr(bool val1, bool val2)
           {
               return !(val1 || val2);
           }
         static  public bool XOR(bool val1, bool val2)
           {
               return val1 ^ val2;
           }
           static public bool NotAnd(bool val1, bool val2)
           {
               return !(val1 && val2);
           }
           static public bool And(bool val1, bool val2)
           {
               return val1 && val2;
           }
           static public bool Equal(bool val1, bool val2)
           {
               return val1 == val2;
           }
           static public bool Implication(bool val1, bool val2)
           {
               return !(val1 && !val2);
           }
           static public bool ReverseImplication(bool val1, bool val2)
           {
               return !(!val1 && val2);
           }
           static public bool Or(bool val1, bool val2)
           {
               return val1 || val2;
           }
       }

        /// <summary>
        /// Класс вычисляет булевы выражения.
        /// Правила заполнения:
        /// 1) Разрешены скобки
        /// 2) Разрешены константы 0 и 1
        /// 3) Разрешены переменные, допустимы малые и большие латинские буквы
        /// 4) Разрешено отрицаний. Для этого перед переменной поставьте знак '!'
        /// 5) Список функций :
         ///  Конъюнкция (логическое И) - '&'
         ///  Дизъюнкция (логическое ИЛИ)- '|'
         ///  Эквивалентность (равенство)- '='
         ///  Прямая Импликация ( меньше или равно)- '>' ( так как класически используется ->)
         ///  Обратная Импликация (больше или равно)- '<'(так как классически <-)
         ///  ХОR (Исключающее ИЛИ)- '^'
         ///  Штрих Шефера  (НЕ-И) - '\\'
         ///  Стрелка Пирса (НЕ-ИЛИ)-'/'
        /// 
        /// </summary>
       static class Expression
       {
           
           private static Dictionary<char,bool> vars;
           private static string expr;
           
           static public bool Result(string exp,params bool[] value)
           {
               bool rez = false;               
               expr = exp;
               vars = new Dictionary<char, bool>();
               getVariableList();
               setState(value);
               updateNegative();
               calculate(ref expr);
               return Convert.ToBoolean(Convert.ToInt32(expr));
           }

           private static void updateNegative()
           {
               foreach (KeyValuePair<char, bool> it in vars)
               {
                   expr = expr.Replace("!" + it.Key, Convert.ToInt32((!it.Value)).ToString());
               }
           }

           private static void calculate(ref string exp)
           {
               char[] data = new char[3];
               int index = 0;
               int s_index = 0;
               while (s_index < 3)
               {
                   if (IsVariable(exp[index]))
                   {
                       if (vars.ContainsKey(exp[index]))
                       {
                           data[s_index++] = exp[index];
                       }
                       else
                       {
                           throw (new InvalidOperationException("Invalide variable"));
                       }
                   }
                   if (IsFunction(exp[index]))
                   {
                       data[s_index++] = exp[index];
                   }
                   if (exp[index] == '(')
                   {

                       string new_exp = stackCutter(ref exp);
                       string calc_exp = String.Copy(new_exp);
                       calculate(ref calc_exp);
                       exp = exp.Replace("("+new_exp +")", calc_exp);
                       s_index++;
                   }
                   index++;
               }
               bool token_rez = calcToken(data);
               string token_str = Convert.ToInt32(token_rez).ToString();
               exp = exp.Replace(new string(data), token_str);
               if (exp.Length != 1)
                   calculate(ref exp);
               
           }

           private static string stackCutter(ref string exp)
           {
               int stack = 0;
               int index = 0;
               int start = 0;
               bool flag = true;
               while (stack != 0 || flag)
               {
                   if (exp[index] == '(')
                   {
                       stack++;
                       if (flag == true)
                       {
                           flag = false;
                           start = index;
                           exp.Remove(index, 1);
                       }
                   }
                   if (exp[index] == ')')
                   {
                       stack--;
                       if (flag == true)
                       {
                           flag = false;
                           start = index;
                       }
                   }
                   index++;
               }
               exp.Remove(index-1, 1);
               return exp.Substring(start + 1, index - start -2);
           }

           private static bool calcToken(char[] token)
           {
               bool[] arg = new bool[2];
               if (IsVariable(token[0]))
                   arg[0] = vars[token[0]];
               else
                   arg[0] = Convert.ToBoolean(Convert.ToInt32(token[0])-48);
               if (IsVariable(token[2]))
                   arg[1] = vars[token[2]];
               else
                   arg[1] = Convert.ToBoolean(Convert.ToInt32(token[2])-48);
               switch (token[1])
               {
                   case '&':
                       {
                           return Function.And(arg[0], arg[1]);
                       }
                   case '|':
                       {
                           return Function.Or(arg[0], arg[1]);
                       }
                   case '=':
                       {
                           return Function.Equal(arg[0], arg[1]);
                       }
                   case '>':
                       {
                           return Function.Implication(arg[0], arg[1]);
                       }
                   case '<':
                       {
                           return Function.ReverseImplication(arg[0], arg[1]);
                       }
                   case '^':
                       {
                           return Function.XOR(arg[0], arg[1]);
                       }
                   case '\\':
                       {
                           return Function.NotAnd(arg[0], arg[1]);
                       }
                   case '/':
                       {
                           return Function.NotOr(arg[0], arg[1]);
                       }
                   default:
                       throw (new InvalidOperationException("Invalide opeartion"));



               }
           }

           static private void getVariableList()
           {
               vars.Clear();
               foreach (char c in expr)
               {
                   if (IsVariable(c))
                   {
                       vars[c] = false;
                   }
               }
           }

           static private void setState(params bool[] values)
           {
               int end = Math.Min(vars.Count,values.Length);
               List<KeyValuePair<char, bool>> varList = vars.ToList();
               int i = 0;
               foreach (KeyValuePair<char, bool> it in varList)
               {

                   char c = it.Key;
                   vars[c] = values[i];
                   i++;
                   if (i == end)
                       break;
               }
               vars.ToDictionary(k => k);
           }

           static bool IsFunction(char c)
           {
               if (c == '&'
                   || c == '|'
                   || c == '='
                   || c == '!'
                   || c == '1'
                   || c == '0'
                   || c == '>'
                   || c == '<'
                   || c == '^'
                   || c == '\\'
                   || c == '/'
                   )
                   return true;
               else
                   return false;
           }
           static bool IsVariable(char c)
           {
               if ((c >= 'a' && c <= 'z') || (c >= 'A' && c <= 'Z'))
                   return true;
               else
                   return false;
           }
       }
    }
}

namespace Program
{
    class Program
    {
       static void Table(string exp)
        {
            System.Console.WriteLine("Expression: " + exp);
            HashSet<char> var = Variable(exp);
            int var_count = var.Count;
            foreach (char c in var)
            {
                System.Console.Write(c +" " );
            }
            System.Console.WriteLine("f");
            bool[] arg = new bool[var_count];
            for (int i = 0; i < Math.Pow(2, var_count); i++)
            {
                char[] vars = Convert.ToString(i, 2).PadLeft(8, '0').ToArray();
                Array.Reverse(vars);
                for (int j = 0; j < var_count; j++)
                {
                    arg[j] = Convert.ToBoolean(Convert.ToInt32(vars[j])-48);
                    System.Console.Write(Convert.ToInt32(vars[j]) - 48 + " ");
                }
                System.Console.Write(Convert.ToInt32(Calc.Logic.Expression.Result(exp,arg)));
                System.Console.WriteLine();
            }
        }

       static bool IsVariable(char c)
       {
           if ((c >= 'a' && c <= 'z') || (c >= 'A' && c <= 'Z'))
               return true;
           else
               return false;
       }
       static HashSet<char> Variable(string exp)
       {
           HashSet<char> data = new HashSet<char>();
           foreach (char c in exp)
           {
               if (IsVariable(c))
               {
                   data.Add(c);
               }
           }
           return data;
       }
        static void Main(string[] args)
        {
            string exp = "A&B|(C>(D&A))";
            string exp2 = "(A>B)&(!A>B)";
            string exp3 = "B|(A>C)";

            Table(exp3);
           // Calc.Logic.Expression.Result(exp,);
        }
    }
}