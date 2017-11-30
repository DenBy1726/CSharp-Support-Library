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
                fixed (byte* s = Encoding.Default.GetBytes("косой"), p = Encoding.Default.GetBytes(Path))
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

    class FormatedMorph :Morph
    {
        public override List<List<string>> MorphAnalys(string word)
        {
            List<List<string>> morph = base.MorphAnalys(word).Where((x) => x.Count > 1).ToList();
            if (morph.Count > 0)
                morph[0].RemoveAt(0);
            return morph;  
            
        }
    }
    class Program
    {
       public static void Main()
        {
            FormatedMorph m = new FormatedMorph();
           var rez = m.MorphAnalys("Косой");
        }
        
    }
}
