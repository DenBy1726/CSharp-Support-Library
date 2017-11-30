using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;

namespace Lingua
{
    class Morph
    {
        [DllImport("MorfologLibrary.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "Morfolog")]
        unsafe public static extern byte* Morfolog(byte* word, byte* path);

        private string path = "Database\\";

        /// <summary>
        /// Место, где хранится папка с базой данных
        /// </summary>
        public string Path
        {
            get { return path; }
            set { path = value; }
        }

        public virtual List<List<string>> MorphAnalys(string word)
        {
            unsafe
            {
                fixed (byte* s = Encoding.Default.GetBytes(word), p = Encoding.Default.GetBytes(Path))
                {

                    //получаем массив байт характеристики слова
                    byte* rez = Morfolog(s, p);
                    byte[] arr = new byte[1024];
                    Marshal.Copy((IntPtr)rez, arr, 0, 1024);
                    //преобразуем в строку
                    string rezString = Encoding.Default.GetString(arr);
                    var tokens = rezString.Split(';');
                    List<List<string>> morph = new List<List<string>>();
                    foreach (string token in tokens)
                    {
                        morph.Add(token.Split(new string[] { "|" }, StringSplitOptions.RemoveEmptyEntries).ToList());
                    }
                    //получаем полезную часть
                    //var sss = ss.Split('\0')[0].Split(new string[]{"|"}, StringSplitOptions.RemoveEmptyEntries);
                    //var sss = ss.Split("|", StringSplitOptions.RemoveEmptyEntries);
                    // var str = MarshalUnsafeCStringToString(b, System.Text.Encoding.UTF8);

                    return morph;
                }
            }
        }

    }

    class FormatedMorph : Morph
    {
        public override List<List<string>> MorphAnalys(string word)
        {
            List<List<string>> morph = base.MorphAnalys(word).Where((x) => x.Count > 1).ToList();
            if (morph.Count > 0)
                morph[0].RemoveAt(0);
            return morph;

        }
    }

    class StructuredMorph
    {
        [DllImport("MorfologLibrary.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "MorfologStruct")]
        unsafe public static extern TMorfAnswerMarshal* MorfologStruct(byte* word, byte* path,ref int res);
        private string path = "Database\\";

        /// <summary>
        /// Место, где хранится папка с базой данных
        /// </summary>
        public string Path
        {
            get { return path; }
            set { path = value; }
        }

        public virtual List<TMorfAnswer> MorphAnalys(string word)
        {
            List<TMorfAnswer> safeElements = new List<TMorfAnswer>();
            unsafe
            {
                fixed (byte* s = Encoding.Default.GetBytes(word), p = Encoding.Default.GetBytes(Path))
                {

                    int state = 0;
                    //получаем указатель на массив структур
                    TMorfAnswerMarshal* rez = MorfologStruct(s, p, ref state);

                    if (state < 0)
                        return null;
                    List<TMorfAnswerMarshal> elements = new List<TMorfAnswerMarshal>();
                    int sizeOfStruct = Marshal.SizeOf(typeof(TMorfAnswerMarshal));

                    //перебиваем unmanaged указатель на массив структур в managed список структур
                    for (int i = 0; i < state; i++)
                    {
                        var tempElement = (TMorfAnswerMarshal)Marshal.PtrToStructure((IntPtr)rez + sizeOfStruct * i, typeof(TMorfAnswerMarshal));
                        elements.Add(tempElement);
                    }


                    //теперь необходимо перекопировать TMorfAnswerMarshal в TMorfAnswer
                    foreach (var it in elements)
                    {
                        //процесс преобразования wchar_t в string
                        byte[] arr = new byte[1024];
                        Marshal.Copy((IntPtr)it.MainForm, arr, 0, 1024);

                        Marshal.FreeCoTaskMem((IntPtr)it.MainForm);
                        string form = Encoding.Default.GetString(arr).Split(new char[] { '\0' })[0];
                        // string form = string.Join("", rezString);

                        TMorfAnswer tempElement = new TMorfAnswer();

                        //перекопируем все поля
                        tempElement.Alternative = it.Alternative;
                        tempElement.Animation = it.Animation;
                        tempElement.Aspect = it.Aspect;
                        tempElement.Case = it.Case;
                        tempElement.ComparativeDegree = it.ComparativeDegree;
                        tempElement.Gender = it.Gender;
                        tempElement.MainForm = form;
                        tempElement.Mood = it.Mood;
                        tempElement.Number = it.Number;
                        tempElement.Person = it.Person;
                        tempElement.Reflection = it.Reflection;
                        tempElement.Class = it.Class;
                        tempElement.Time = it.Time;
                        tempElement.Voice = it.Voice;
                        safeElements.Add(tempElement);
                    }

                    Marshal.DestroyStructure((IntPtr)rez, typeof(TMorfAnswerMarshal));
                }

            }

            return safeElements;
        }
    }

    public enum Class
    {
        Particles, Interjections, PersonalPronouns, CardinalNumbers,
        PossessiveAdjectives, ShortAdjectives, Substantives,
        Adjectives, Verbs, Prepositions, Conjunctions,
        Adverbs, Predicates, PunctuationMarks, ShortParticiples,
        CompDegreeAdjectives, Participles, AdverbialParticiples,

        /* Мои добавления */
        Pronouns,         // Местоимения-сущ. типа "кто-то"
        SubConjunctions,  // Подчинительные союзы
        AdverbsDegree,     // Наречия типа степени
        StateCategory
            // Как обозначается т. н. "категория состояния"?
             , esUndefined //pom
    };

    public enum Animation { Any, Animated, UnAnimated, Undefined };

    public enum Gender { Any, Masculine, Feminine, Neuter, Undefined };

    public enum Number { Any, Single, Multiple, Undefined };

    public enum Case
    {
        Any, Nominative, Genitive, Dative,
        eccusative, Instrumental, Prepositional,
        SecondGenitive, SecondPrepositional, Undefined
    };

    public enum Aspect { Any, Imperfect, Perfect, Undefined };

    public  enum Person { Any, First, Second, Third, Undefined };

    public enum Voice { Any, Active, Passive, Undefined };

    public enum Reflection { Any, Reflexive, Nonreflexive, Undefined };

    public  enum Time { Any, Present, Future, Past, Undefined };

    public enum Mood
    {
        Any, Present, Future, Past, Subjunctive,
        Imperative, Infinitive, Undefined
    };

    public  enum ComparativeDegree { Any, Strong, Weak, Undefined };

    public enum AlternativeForm { Any, Yes, Undefined };

    //структура для Маршалинга из unmanaged кода
    [StructLayout(LayoutKind.Sequential)]
    unsafe struct TMorfAnswerMarshal
    {
        public byte* MainForm;
        public Class Class;
        public Animation Animation;
        public Gender Gender;
        public Number Number;
        public Case Case;
        public AlternativeForm Alternative;
        public Aspect Aspect;
        public Person Person;
        public Voice Voice;
        public Reflection Reflection;
        public Time Time;
        public Mood Mood;
        public ComparativeDegree ComparativeDegree;
    };

    //безопасная версия TMorfAnswerMarshal
    class TMorfAnswer
    {

        public string MainForm
        {
            get;
            set;
        }

        public Class Class
        {
            get;
            set;
        }

        public Animation Animation
        {
            get;
            set;
        }
        public Gender Gender
        {
            get;
            set;
        }
        public Number Number
        {
            get;
            set;
        }
        public Case Case
        {
            get;
            set;
        }
        public AlternativeForm Alternative
        {
            get;
            set;
        }
        public Aspect Aspect
        {
            get;
            set;
        }
        public Person Person
        {
            get;
            set;
        }
        public Voice Voice
        {
            get;
            set;
        }
        public Reflection Reflection
        {
            get;
            set;
        }
        public Time Time
        {
            get;
            set;
        }
        public Mood Mood
        {
            get;
            set;
        }
        public ComparativeDegree ComparativeDegree
        {
            get;
            set;
        }
    };

}
