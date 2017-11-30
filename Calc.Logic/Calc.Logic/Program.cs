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
           
           private static string expr;

           //Основной метод, который выполняет основные вычисления и возвращает результат
           static public bool Result(string exp, params bool[] value)        
           {                      
               expr = exp;
               initializeVariables(value);
               calculate(ref expr);
               return Convert.ToBoolean(Convert.ToInt32(expr));
           }



           //основная функция вычисления
           private static void calculate(ref string exp)
           {
               if (exp.Length == 1)
                   return;
               char[] data = new char[3]; //хранение текущего токена ( 2 аргумента константы + операция)
               int index = 0; //индекс текущего символа выражения
               int s_index = 0; //индекс текущего токена
               while (s_index < 3)
               {
                   if (exp.Length == 1)
                       return;
                   //выборка функции ( 0 и 1 считаются как функция константа)
                   if (IsFunction(exp[index]))
                   {
                       data[s_index++] = exp[index];
                   }
                   
                   //если скобки то рекурсивно запускаем вычисление от скобки
                   if (exp[index] == '(')
                   {
                       //срезаем скобочную последовательность, и в дальнейшем работаем с ее копией
                       string new_exp = stackCutter(ref exp);
                       string calc_exp = String.Copy(new_exp);
                       calculate(ref calc_exp);
                       //в calc_exp теперь результат вычисления, 
                       //заменяем скобочную последовательность эквивалентным результатом
                       exp = exp.Replace("("+new_exp +")", calc_exp);
                       s_index++;
                   }
                   index++;
               }
               //вычисляем значени токена и заменяем токен на вычисленное значение
               bool token_rez = calcToken(data);
               string token_str = Convert.ToInt32(token_rez).ToString();
               exp = exp.Replace(new string(data), token_str);
               //Один символ остается только тогда, 
               //когда выражение было трансформировано в результат, иначе продолжаем вычисления
               if (exp.Length != 1)
                   calculate(ref exp);
               
           }

           //функция срезает закрытую внешнюю скобочную последовательность
           //из строки источника удаляются скобки. Возвращает содержимое скобок
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

           //Расчет токена
           private static bool calcToken(char[] token)
           {
               bool[] arg = new bool[2];
               //Если переменная , то достаем значение, иначе константа, тогда переводим её в значение
               arg[0] = Convert.ToBoolean(Convert.ToInt32(token[0])-48);
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

           //инициализация переменных
           private static void initializeVariables(bool[] value)
           {
               int index = 0;
               HashSet<char> vars = new HashSet<char>();
               foreach (char c in expr)
               {
                   if (IsVariable(c))
                   {
                       if (vars.Contains(c) == false)
                       {
                           vars.Add(c);
                           expr = expr.Replace("!" + c, Convert.ToInt32((!value[index])).ToString());
                           expr = expr.Replace(new string(c, 1), Convert.ToInt32((value[index])).ToString());
                           index++;
                       }
                   }
               }
           }

           //проверка символа является ли он функцией
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
           //проверка может ли быть символ переменной
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
            string exp4 = "A&(B&(C&(D&(E&(a&b)))))";
            string exp5 = "1\\0";
            string exp6 = "(A>B)&(A>(!B|C))&(A>C)";
            string exp7 = "c|(!b&(!c|d))";
            string exp8 = "!b&(!c|d)|c";
            string exp9 = "((a|!a)&(!b|!d)&(!b|!c)&(!c|d))|((!b|c)&(c|d))";
            string exp10 = "(!b|(!d&!c)&(!c|d))|c";
            string exp11 = "((x|y)&z)>(!y|x)";
            string exp12 = "(C>(D>E))>((E>F)>(((A>B)>C)>(D>(!A>F))))";
            string exp13 = "!x&((y>z)^!y)";
            string exp14 = "(a&c)|((b|!d)&(!a|!d)&(d|b)&(!a|d))|(a&!c)";
            string exp15 = "a|(b&!a)";
            //string exp6 = "!(A&B)";

            Output.PrintConsole.BooleanExpression(exp14);
            Output.PrintConsole.BooleanExpression(exp15);

            //Table(exp5);
           // Calc.Logic.Expression.Result(exp,);
        }
    }
}