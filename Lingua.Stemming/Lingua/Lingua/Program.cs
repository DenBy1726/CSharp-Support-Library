using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;


namespace Lingua
{
    namespace Russian
    {
        static class LetterInfo
        {
            public static readonly char[] Vowels = new char[] { 'а', 'у', 'е', 'э', 'о', 'ы', 'я', 'и', 'ю', 'ё' };
            public static bool IsVowels(char letter)
            {
                return Vowels.Contains(letter);
            }
        }
        static class WordEnding
        {
            /*public static readonly Tuple<string[], string[]> PerfectiveGerund = new Tuple<string[], string[]>
                (new string[] { "в", "вши", "вшись" },
                new string[] { "ив", "ивши", "ившись", "ыв", "ывши", "ывшись" });

            public static readonly string[] Adjective = new string[] {"ее", "ие", "ые", "ое", 
                "ими", "ыми", "ей", "ий", "ый", "ой", "ем", "им", "ым", "ом", "его", "ого"
                , "ему", "ому", "их", "ых", "ую", "юю", "ая", "яя", "ою", "ею"};

             public static readonly Tuple<string[], string[]> Participle = new Tuple<string[], string[]>
               (new string[] { "ем", "нн", "вш", "ющ", "щ" },
                new string[] { "ивш", "ывш", "ующ" });

             public static readonly string[] Reflexive = new string[] { "ся", "сь" };

             public static readonly Tuple<string[], string[]> Verb = new Tuple<string[], string[]>
                (new string[] { "ла", "на", "ете", "йте", "ли", "й", "л", "ем", "н", "ло", "но", "ет"
                    , "ют", "ны", "ть", "ешь", "нно" },
                 new string[] { "ила", "ыла", "ена", "ейте", "уйте", "ите", "или", "ыли", "ей", "уй"
                     , "ил", "ыл", "им", "ым", "ен", "ило", "ыло", "ено", "ят", "ует", "уют", "ит"
                     , "ыт", "ены", "ить", "ыть", "ишь", "ую", "ю"});

             public static readonly string[] Noun = new string[] { "а", "ев", "ов", "ие", "ье", "е", 
                 "иями", "ями", "ами", "еи", "ии", "и", "ией", "ей", "ой", "ий", "й", "иям", "ям", 
                 "ием", "ем", "ам", "ом", "о", "у", "ах", "иях", "ях", "ы", "ь", "ию", "ью", "ю",
                 "ия", "ья", "я" };

             public static readonly string[] Superlative = new string[] {"ейш", "ейше"};

             public static readonly string[] Derivational = new string[] { "ост", "ость" };*/

            //Первая часть - все до гласной. Вторая все что осталось
            public const string AfterFirstVowel = "^(.*?[аеиоуыэюя])(.*)$";
            //Первая часть все до пары гласная-согласная. Вторая все что осталось
            public const string AfterFirstVowelNoVowel = "^(.*?[аеиоуыэюя][^аеиоуыэюя])(.*)$";

            public const string Vowel = "аеиоуыэюя";


            public static string RVPattern = "^(.*?[аеиоуыэюя])(.*)$";
            public static string PerfectiveGerundPattern = "((ив|ивши|ившись|ыв|ывши|ывшись)|((?<=ая)(в|вши|вшись)))$";
            public static string ReflexivePattern = "(с[яь])$";
            public static string AdjectivePattern = "(ее|ие|ые|ое|ими|ыми|ей|ий|ый|ой|ем|им|ым|ом|его|ого|ему|ому|их|ых|ую|юю|ая|яя|ою|ею)$";
            public static string ParticiplePattern = "((ивш|ывш|ующ)|((?<=ая)(ем|нн|вш|ющ|щ))$";
            public static string VerbPattern = "((ила|ыла|ена|ейте|уйте|ите|или|ыли|ей|уй|ил|ыл|им|ым|ен|ило|ыло|ено|ят|ует|уют|ит|ыт|ены|ить|ыть|ишь|ую|ю)$|((?<=[ая])(ла|на|ете|йте|ли|й|л|ем|н|ло|но|ет|ют|ны|ть|ешь|нно)))$";
            public static string NounPattern = "(а|ев|ов|ие|ье|е|иями|ями|ами|еи|ии|и|ией|ей|ой|ий|й|иям|ям|ием|ем|ам|ом|о|у|ах|иях|ях|ы|ь|ию|ью|ю|ия|ья|я)$";
            public static string RvrePattern = "^(.*?[аеиоуыэюя])(.*)$";
            public static string DerivationalPattern = ".*[^аеиоуыэюя]+[аеиоуыэюя].*ость?$";
            public static string DerPattern = "ость?$";
            public static string SuperlativePattern = "(ейше|ейш)$";
            public static string IPattern = "и$";
            public static string PPattern = "ь$";
            public static string NNPattern = "нн$";



        }

        static class Stemming
        {
            /*      /// <summary>
                  /// область слова после первой гласной. 
                  /// Она может быть пустой, если гласные в слове отсутствуют
                  /// </summary>
                  /// <param name="word">исходное слово</param>
                  /// <returns>RV форма</returns>
                  public static string RV(string word)
                  {
                      if(word.Length < 2)
                          return "";
                      StringBuilder copyWord = new StringBuilder(word);
                      do
                      {
                          copyWord.Remove(0, 1);
                      }
                      while (copyWord.Length > 0 && LetterInfo.IsVowels(copyWord[0]) == false);
                      copyWord.Remove(0, 1);
                      return copyWord.ToString();
                  }

                  /// <summary>
                  /// область слова после первого сочетания “гласная-согласная”
                  /// </summary>
                  /// <param name="word">исходной слово</param>
                  /// <returns></returns>
                  public static string R1(string word)
                  {
                      if (word.Length < 2)
                          return "";
                      StringBuilder copyWord = new StringBuilder(word);
                      do
                      {
                          if (copyWord.Length < 2)
                              return "";
                          if(LetterInfo.IsVowels(copyWord[0]) == true && LetterInfo.IsVowels(copyWord[1]) == false )
                          {
                              copyWord.Remove(0, 2);
                              break;
                          }
                          copyWord.Remove(0, 1);
                      }
                      while (copyWord.Length > 0);
                      return copyWord.ToString();
                  }

                  */

            private static string RV1(ref string word)
            {
                Match m = Regex.Match(word, WordEnding.AfterFirstVowelNoVowel);
                return m.Groups[2].ToString();
            }

            private static string RV2(ref string word)
            {
                Match m = Regex.Match(RV1(ref word), WordEnding.AfterFirstVowelNoVowel);
                return m.Groups[2].ToString();
            }

            private static void Step1(ref string word)
            {
                /*Найти окончание PERFECTIVE GERUND. Если оно существует?—?удалить его и 
                 * завершить этот шаг.Иначе, удаляем окончание REFLEXIVE (если оно существует).
                 * Затем в следующем порядке пробуем удалить окончания: ADJECTIVAL, VERB, NOUN.
                 * Как только одно из них найдено?—?шаг завершается.*/

                if (Regex.IsMatch(word, WordEnding.PerfectiveGerundPattern) == true)
                {
                    word = Regex.Replace(word, WordEnding.PerfectiveGerundPattern, string.Empty);
                }
                else
                {
                    word = Regex.Replace(word, WordEnding.ReflexivePattern, string.Empty);
                    if (Regex.IsMatch(word, WordEnding.AdjectivePattern) == true)
                    {
                        word = Regex.Replace(word, WordEnding.AdjectivePattern, string.Empty);
                    }
                    else if (Regex.IsMatch(word, WordEnding.VerbPattern) == true)
                    {
                        word = Regex.Replace(word, WordEnding.VerbPattern, string.Empty);
                    }
                    else if (Regex.IsMatch(word, WordEnding.NounPattern) == true)
                    {
                        word = Regex.Replace(word, WordEnding.NounPattern, string.Empty);
                    }

                }
            }

            private static void Step2(ref string word)
            {
                /*
                 Если слово оканчивается на и?—?удаляем и.
                 */

                if (Regex.IsMatch(word, WordEnding.IPattern) == true)
                {
                    word = Regex.Replace(word, WordEnding.IPattern, string.Empty);
                }
            }

            private static void Step3(ref string word)
            {
                if (Regex.IsMatch(RV2(ref word), WordEnding.DerPattern))
                {
                    word = Regex.Replace(word, WordEnding.DerPattern, string.Empty);
                }

            }

            public static string Basis(string word)
            {
                word = word.ToLower();
                word = word.Replace('ё', 'е');
                Match m = Regex.Match(word, WordEnding.AfterFirstVowel);
                string RV = m.Groups[2].ToString();
                if (RV != "")
                    word = Regex.Match(word, "^(.*?)" + RV + "$").Groups[1].ToString();
                Step1(ref RV);
                Step2(ref RV);
                Step3(ref RV);
                Step4(ref RV);

                return word + RV;

            }

            private static void Step4(ref string word)
            {
                if (Regex.IsMatch(word, "нн$") == true)
                {
                    word = Regex.Replace(word, "нн$", "н");
                }
                else if (Regex.IsMatch(word, WordEnding.SuperlativePattern) == true)
                {
                    word = Regex.Replace(word, WordEnding.SuperlativePattern, string.Empty);
                    if (Regex.IsMatch(word, "нн$") == true)
                    {
                        word = Regex.Replace(word, "нн$", "н");
                    }
                }
                else if (Regex.IsMatch(word, "ь$") == true)
                {
                    word = Regex.Replace(word, "ь$", "");
                }
            }




        }
    }
}

namespace Lingua
{
    class Program
    {
        static void Main(string[] args)
        {
            /* string text = Russian.Stemming.RV("Противоестественном");
             text = Russian.Stemming.R1("Противоестественном");
             text = Russian.Stemming.R1(text);*/

            Test("test.txt");
            string input = Console.ReadLine();
            var x = Russian.Stemming.Basis(input);
            Console.WriteLine(x);
         
           
        }

        private static void Test(string p)
        {
            string AllText = System.IO.File.ReadAllText(p,Encoding.Default);
            string[] Tokens = AllText.Split(new char[] { '\n', '\r' }).Where(x => x != String.Empty).ToArray();
            for(int i=0;i<60;i++)
            {
                var x = Russian.Stemming.Basis(Tokens[i]);
                if (x != Tokens[60 + i])
                {
                    Console.WriteLine("{0} != {1}", x, Tokens[60 + i]);
                    
                }

            }
            for (int i = 120; i < 180; i++)
            {
                var x = Russian.Stemming.Basis(Tokens[i]);
                if (x != Tokens[60 + i])
                    Console.WriteLine("{0} != {1}", x, Tokens[60 + i]);

            }
        }
    }
}
