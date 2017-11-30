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
            public static readonly char[] Vowels = new char[] { '�', '�', '�', '�', '�', '�', '�', '�', '�', '�' };
            public static bool IsVowels(char letter)
            {
                return Vowels.Contains(letter);
            }
        }
        static class WordEnding
        {
            /*public static readonly Tuple<string[], string[]> PerfectiveGerund = new Tuple<string[], string[]>
                (new string[] { "�", "���", "�����" },
                new string[] { "��", "����", "������", "��", "����", "������" });

            public static readonly string[] Adjective = new string[] {"��", "��", "��", "��", 
                "���", "���", "��", "��", "��", "��", "��", "��", "��", "��", "���", "���"
                , "���", "���", "��", "��", "��", "��", "��", "��", "��", "��"};

             public static readonly Tuple<string[], string[]> Participle = new Tuple<string[], string[]>
               (new string[] { "��", "��", "��", "��", "�" },
                new string[] { "���", "���", "���" });

             public static readonly string[] Reflexive = new string[] { "��", "��" };

             public static readonly Tuple<string[], string[]> Verb = new Tuple<string[], string[]>
                (new string[] { "��", "��", "���", "���", "��", "�", "�", "��", "�", "��", "��", "��"
                    , "��", "��", "��", "���", "���" },
                 new string[] { "���", "���", "���", "����", "����", "���", "���", "���", "��", "��"
                     , "��", "��", "��", "��", "��", "���", "���", "���", "��", "���", "���", "��"
                     , "��", "���", "���", "���", "���", "��", "�"});

             public static readonly string[] Noun = new string[] { "�", "��", "��", "��", "��", "�", 
                 "����", "���", "���", "��", "��", "�", "���", "��", "��", "��", "�", "���", "��", 
                 "���", "��", "��", "��", "�", "�", "��", "���", "��", "�", "�", "��", "��", "�",
                 "��", "��", "�" };

             public static readonly string[] Superlative = new string[] {"���", "����"};

             public static readonly string[] Derivational = new string[] { "���", "����" };*/

            //������ ����� - ��� �� �������. ������ ��� ��� ��������
            public const string AfterFirstVowel = "^(.*?[���������])(.*)$";
            //������ ����� ��� �� ���� �������-���������. ������ ��� ��� ��������
            public const string AfterFirstVowelNoVowel = "^(.*?[���������][^���������])(.*)$";

            public const string Vowel              = "���������"; 

            
            public static string RVPattern = "^(.*?[���������])(.*)$";
            public static string PerfectiveGerundPattern = "((��|����|������|��|����|������)|((?<=��)(�|���|�����)))$";
            public static string ReflexivePattern = "(�[��])$";
            public static string AdjectivePattern = "(��|��|��|��|���|���|��|��|��|��|��|��|��|��|���|���|���|���|��|��|��|��|��|��|��|��)$";
            public static string ParticiplePattern = "((���|���|���)|((?<=��)(��|��|��|��|�))$";
            public static string VerbPattern = "((���|���|���|����|����|���|���|���|��|��|��|��|��|��|��|���|���|���|��|���|���|��|��|���|���|���|���|��|�)$|((?<=[��])(��|��|���|���|��|�|�|��|�|��|��|��|��|��|��|���|���)))$";
            public static string NounPattern = "(�|��|��|��|��|�|����|���|���|��|��|�|���|��|��|��|�|���|��|���|��|��|��|�|�|��|���|��|�|�|��|��|�|��|��|�)$";
            public static string RvrePattern = "^(.*?[���������])(.*)$";
            public static string DerivationalPattern = ".*[^���������]+[���������].*����?$";
            public static string DerPattern = "����?$";
            public static string SuperlativePattern = "(����|���)$";
            public static string IPattern = "�$";
            public static string PPattern = "�$";
            public static string NNPattern = "��$";



        }

        static class Stemming
        {
      /*      /// <summary>
            /// ������� ����� ����� ������ �������. 
            /// ��� ����� ���� ������, ���� ������� � ����� �����������
            /// </summary>
            /// <param name="word">�������� �����</param>
            /// <returns>RV �����</returns>
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
            /// ������� ����� ����� ������� ��������� ��������-����������
            /// </summary>
            /// <param name="word">�������� �����</param>
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
                /*����� ��������� PERFECTIVE GERUND. ���� ��� ����������?�?������� ��� � 
                 * ��������� ���� ���.�����, ������� ��������� REFLEXIVE (���� ��� ����������).
                 * ����� � ��������� ������� ������� ������� ���������: ADJECTIVAL, VERB, NOUN.
                 * ��� ������ ���� �� ��� �������?�?��� �����������.*/

                if(Regex.IsMatch(word, WordEnding.PerfectiveGerundPattern) == true)
                {
                    word = Regex.Replace(word, WordEnding.PerfectiveGerundPattern, string.Empty);
                }
                else 
                {
                    word = Regex.Replace(word, WordEnding.ReflexivePattern, string.Empty);
                    if(Regex.IsMatch(word, WordEnding.AdjectivePattern) == true)
                    {
                        word = Regex.Replace(word, WordEnding.AdjectivePattern, string.Empty);
                    }
                    else if(Regex.IsMatch(word, WordEnding.VerbPattern) == true)
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
                 ���� ����� ������������ �� �?�?������� �.
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
                word = word.Replace('�', '�');
                Match m = Regex.Match(word, WordEnding.AfterFirstVowel);
                string RV = m.Groups[2].ToString();
                if(RV != "")
                    word = Regex.Match(word, "^(.*?)" + RV +"$").Groups[1].ToString();
                Step1(ref RV);
                Step2(ref RV);
                Step3(ref RV);
                Step4(ref RV);

                return word + RV;

            }

            private static void Step4(ref string word)
            {
                if (Regex.IsMatch(word, "��$") == true)
                {
                    word = Regex.Replace(word, "��$", "�");
                } 
                else if(Regex.IsMatch(word, WordEnding.SuperlativePattern) == true)
                {
                    word = Regex.Replace(word, WordEnding.SuperlativePattern, string.Empty);
                    if (Regex.IsMatch(word, "��$") == true)
                    {
                        word = Regex.Replace(word, "��$", "�");
                    } 
                }
                else if(Regex.IsMatch(word, "�$") == true)
                {
                    word = Regex.Replace(word, "�$", "");
                }
            }

       

           
        }
    }
}