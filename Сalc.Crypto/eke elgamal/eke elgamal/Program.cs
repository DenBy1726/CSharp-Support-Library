using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;
using System.IO;
using System.Numerics;


namespace eke_elgamal
{

    class Program
    {
     
        static void Main(string[] args)
        {
   /*         Calc.Crypto.Crypt coder = new Calc.Crypto.Crypt { Key = "1111111111111111111" };
            string a = coder.Encrypt("Connect");
            byte[] b1 = Encoding.UTF8.GetBytes(a);
            a = Encoding.UTF8.GetString(b1);
            string a1 = coder.Decrypt(a);*/


            Calc.Crypto.HashMd5 h = new Calc.Crypto.HashMd5();
            string str = "a";
            while (true)
            {
                byte[] rez = h.ComputeHash(Encoding.UTF8.GetBytes(str), 0, 0);
                System.Console.WriteLine(Calc.Crypto.HashMd5.HashToHex(rez));
                str += 'a';
            }
            
        
        }
    }
}




namespace Calc
{
    namespace Crypto
    {

        class Crypt
        {
            public string InitialVector
            { get;set;}
            private string sol = "dream";
            public string Algorithm
            {get;set;}
            public int Iteration
            {get;set;}
            public int PasswordSize
            {get;set;}
            public string Key
            { get; set; }

            public Crypt()
            {
                InitialVector = "a8doSuDit0z1hZe#";
                Algorithm = "MD5";
                Iteration = 16;
                PasswordSize = 16;
                Key = "NULL";
            }
            public string Encrypt(string text)
            {
                byte[] initVecB = Encoding.UTF8.GetBytes(InitialVector);
                byte[] solB = Encoding.UTF8.GetBytes(sol);
                byte[] textB = Encoding.UTF8.GetBytes(text);
                PasswordDeriveBytes pass = new PasswordDeriveBytes(Key, solB, Algorithm, Iteration);
                byte[] keyB = pass.GetBytes(PasswordSize);
                RijndaelManaged symmKey = new RijndaelManaged();
                symmKey.Mode = CipherMode.CBC;
                byte[] ciphTextB = null;
                using (ICryptoTransform encryptor = symmKey.CreateEncryptor(keyB, initVecB))
                {
                    using (MemoryStream stream = new MemoryStream())
                    {
                        using (CryptoStream c_stream = new CryptoStream(stream, encryptor, CryptoStreamMode.Write))
                        {
                            c_stream.Write(textB, 0, textB.Length);
                            c_stream.FlushFinalBlock();
                            ciphTextB = stream.ToArray();
                            stream.Close();
                            c_stream.Close();
                        }
                    }
                }
                symmKey.Clear();
                return Convert.ToBase64String(ciphTextB);
            }

            public string Decrypt(string text)
            {
                byte[] initVecB = Encoding.UTF8.GetBytes(InitialVector);
                byte[] solB = Encoding.UTF8.GetBytes(sol);
                byte[] ciphText = Convert.FromBase64String(text);
                PasswordDeriveBytes pass = new PasswordDeriveBytes(Key, solB, Algorithm, Iteration);
                byte[] keyB = pass.GetBytes(PasswordSize);
                RijndaelManaged symmKey = new RijndaelManaged();
                symmKey.Mode = CipherMode.CBC;
                byte[] textB = new byte[text.Length];
                int byteCount = 0;
                using (ICryptoTransform decryptor = symmKey.CreateDecryptor(keyB, initVecB))
                {
                    using (MemoryStream stream = new MemoryStream(ciphText))
                    {
                        using (CryptoStream c_stream = new CryptoStream(stream, decryptor, CryptoStreamMode.Read))
                        {
                            byteCount = c_stream.Read(textB, 0, textB.Length);
                            stream.Close();
                            c_stream.Close();
                        }
                    }
                }
                symmKey.Clear();
                return Encoding.UTF8.GetString(textB, 0, byteCount);
            }
            public string Decrypt(byte[] ciphtext)
            {
                byte[] initVecB = Encoding.UTF8.GetBytes(InitialVector);
                byte[] solB = Encoding.UTF8.GetBytes(sol);
                PasswordDeriveBytes pass = new PasswordDeriveBytes(Key, solB, Algorithm, Iteration);
                byte[] keyB = pass.GetBytes(PasswordSize);
                RijndaelManaged symmKey = new RijndaelManaged();
                symmKey.Mode = CipherMode.CBC;
                byte[] textB = new byte[1024];
                int byteCount = 0;
                using (ICryptoTransform decryptor = symmKey.CreateDecryptor(keyB, initVecB))
                {
                    using (MemoryStream stream = new MemoryStream(ciphtext))
                    {
                        using (CryptoStream c_stream = new CryptoStream(stream, decryptor, CryptoStreamMode.Read))
                        {
                            byteCount = c_stream.Read(textB, 0, textB.Length);
                            stream.Close();
                            c_stream.Close();
                        }
                    }
                }
                symmKey.Clear();
                return Encoding.UTF8.GetString(textB, 0, byteCount);

            }
        }
        class HashMd5 : System.Security.Cryptography.MD5
        {
            delegate uint ConstantFunction(uint x, uint y, uint z);

            private UInt32 A, B, C, D;

            private ConstantFunction[] Func;

            private UInt32[] T;

            private int[,] SRange = new int[4, 4] { { 12, 17, 22, 7 }, { 5, 9, 14, 20 }, { 4, 11, 16, 23 }, { 6, 10, 15, 21 } };



            public HashMd5()
            {
                /*  
                * •	Потребуются 4 функции для четырёх раундов. 
                * Введём функции от трёх параметров — слов, результатом также будет слово.
                */
                FunctionInitialize(out Func);

                /*  •	Определим таблицу констант T[1..64] — 64-элементная таблица данных,
               * построенная следующим образом: T[i] = int(4294967296 * | sin(i) | ), где 4294967296 = 2^32.
              */
                ConstantTableInitialize(out T);

            }
            protected override void HashCore(byte[] array, int ibStart, int cbSize)
            {
                /* Шаг 1. Выравнивание потока
                     Сначала дописывают единичный бит в конец потока(байт 0x80), 
                 * затем необходимое число нулевых бит. Входные данные выравниваются так, 
                 * чтобы их новый размер L' был сравним с 448 по модулю 512 (L’ = 512 × N + 448). 
                 * Выравнивание происходит, даже если длина уже сравнима с 448.
                 */

                int oldSize = array.Length; // длинна в байтах
                int newSize;
                FlowEqualization(ref array, oldSize, out newSize); //считаем новый размер

                /* Шаг 2. Добавление длины сообщения
                     В оставшиеся 64 бита дописывают 64-битное представление длины данных 
                 * (количество бит в сообщении) до выравнивания. Сначала записывают младшие 4 байта.
                 * Если длина превосходит 2^64 − 1, то дописывают только младшие биты. 
                 * После этого длина потока станет кратной 512. Вычисления будут основываться на 
                 * представлении этого потока данных в виде массива слов по 512 бит.
                 */
                AddMessageLength(ref array, oldSize);


                /*Шаг 3. Инициализация буфера
                 * Для вычислений инициализируются 4 переменных размером по 32 бита 
                 * и задаются начальные значения шестнадцатеричными числами 
                 * (шестнадцатеричное представление, сначала младший байт):
                 *  А = 01 23 45 67;
                    В = 89 AB CD EF;
                    С = FE DC BA 98;
                    D = 76 54 32 10.
                */

                BufferInitialize(out A, out B, out C, out D);





                /*•	Выровненные данные разбиваются на блоки (слова) по 32 бита*/
                //  byte[,] words = SplitIntoWords(array, 4);


                /* Шаг 4. Вычисление в цикле
                 Заносим в блок данных элемент n из массива. 
                 * Сохраняются значения A, B, C и D, оставшиеся после операций над предыдущими 
                 * блоками (или их начальные значения, если блок первый)
                 */

                //каждый блок состоит из 16 4-байтоывх чисел.
                int blockAmount = (array.Length) / 16 / 4;
                for (int i = 0, counter = 0; i < blockAmount; i++)
                {
                    UInt32 AA = A,
                           BB = B,
                           CC = C,
                           DD = D;
                    UInt32[] X = new UInt32[16];

                    for (int j = 0; j < 16; j++)
                    {
                        X[j] = BytesToUInt32(array, counter * 4);
                        counter++;
                    }

                    Do(ref AA, ref BB, ref CC, ref DD, X[0], 7, T[0], Func[0]);
                    Do(ref DD, ref AA, ref BB, ref CC, X[1], 12, T[1], Func[0]);
                    Do(ref CC, ref DD, ref AA, ref BB, X[2], 17, T[2], Func[0]);
                    Do(ref BB, ref CC, ref DD, ref AA, X[3], 22, T[3], Func[0]);
                    Do(ref AA, ref BB, ref CC, ref DD, X[4], 7, T[4], Func[0]);
                    Do(ref DD, ref AA, ref BB, ref CC, X[5], 12, T[5], Func[0]);
                    Do(ref CC, ref DD, ref AA, ref BB, X[6], 17, T[6], Func[0]);
                    Do(ref BB, ref CC, ref DD, ref AA, X[7], 22, T[7], Func[0]);
                    Do(ref AA, ref BB, ref CC, ref DD, X[8], 7, T[8], Func[0]);
                    Do(ref DD, ref AA, ref BB, ref CC, X[9], 12, T[9], Func[0]);
                    Do(ref CC, ref DD, ref AA, ref BB, X[10], 17, T[10], Func[0]);
                    Do(ref BB, ref CC, ref DD, ref AA, X[11], 22, T[11], Func[0]);
                    Do(ref AA, ref BB, ref CC, ref DD, X[12], 7, T[12], Func[0]);
                    Do(ref DD, ref AA, ref BB, ref CC, X[13], 12, T[13], Func[0]);
                    Do(ref CC, ref DD, ref AA, ref BB, X[14], 17, T[14], Func[0]);
                    Do(ref BB, ref CC, ref DD, ref AA, X[15], 22, T[15], Func[0]);

                    Do(ref AA, ref BB, ref CC, ref DD, X[1], 5, T[16], Func[1]);
                    Do(ref DD, ref AA, ref BB, ref CC, X[6], 9, T[17], Func[1]);
                    Do(ref CC, ref DD, ref AA, ref BB, X[11], 14, T[18], Func[1]);
                    Do(ref BB, ref CC, ref DD, ref AA, X[0], 20, T[19], Func[1]);
                    Do(ref AA, ref BB, ref CC, ref DD, X[5], 5, T[20], Func[1]);
                    Do(ref DD, ref AA, ref BB, ref CC, X[10], 9, T[21], Func[1]);
                    Do(ref CC, ref DD, ref AA, ref BB, X[15], 14, T[22], Func[1]);
                    Do(ref BB, ref CC, ref DD, ref AA, X[4], 20, T[23], Func[1]);
                    Do(ref AA, ref BB, ref CC, ref DD, X[9], 5, T[24], Func[1]);
                    Do(ref DD, ref AA, ref BB, ref CC, X[14], 9, T[25], Func[1]);
                    Do(ref CC, ref DD, ref AA, ref BB, X[3], 14, T[26], Func[1]);
                    Do(ref BB, ref CC, ref DD, ref AA, X[8], 20, T[27], Func[1]);
                    Do(ref AA, ref BB, ref CC, ref DD, X[13], 5, T[28], Func[1]);
                    Do(ref DD, ref AA, ref BB, ref CC, X[2], 9, T[29], Func[1]);
                    Do(ref CC, ref DD, ref AA, ref BB, X[7], 14, T[30], Func[1]);
                    Do(ref BB, ref CC, ref DD, ref AA, X[12], 20, T[31], Func[1]);

                    Do(ref AA, ref BB, ref CC, ref DD, X[5], 4, T[32], Func[2]);
                    Do(ref DD, ref AA, ref BB, ref CC, X[8], 11, T[33], Func[2]);
                    Do(ref CC, ref DD, ref AA, ref BB, X[11], 16, T[34], Func[2]);
                    Do(ref BB, ref CC, ref DD, ref AA, X[14], 23, T[35], Func[2]);
                    Do(ref AA, ref BB, ref CC, ref DD, X[1], 4, T[36], Func[2]);
                    Do(ref DD, ref AA, ref BB, ref CC, X[4], 11, T[37], Func[2]);
                    Do(ref CC, ref DD, ref AA, ref BB, X[7], 16, T[38], Func[2]);
                    Do(ref BB, ref CC, ref DD, ref AA, X[10], 23, T[39], Func[2]);
                    Do(ref AA, ref BB, ref CC, ref DD, X[13], 4, T[40], Func[2]);
                    Do(ref DD, ref AA, ref BB, ref CC, X[0], 11, T[41], Func[2]);
                    Do(ref CC, ref DD, ref AA, ref BB, X[3], 16, T[42], Func[2]);
                    Do(ref BB, ref CC, ref DD, ref AA, X[6], 23, T[43], Func[2]);
                    Do(ref AA, ref BB, ref CC, ref DD, X[9], 4, T[44], Func[2]);
                    Do(ref DD, ref AA, ref BB, ref CC, X[12], 11, T[45], Func[2]);
                    Do(ref CC, ref DD, ref AA, ref BB, X[15], 16, T[46], Func[2]);
                    Do(ref BB, ref CC, ref DD, ref AA, X[2], 23, T[47], Func[2]);

                    Do(ref AA, ref BB, ref CC, ref DD, X[0], 6, T[48], Func[3]);
                    Do(ref DD, ref AA, ref BB, ref CC, X[7], 10, T[49], Func[3]);
                    Do(ref CC, ref DD, ref AA, ref BB, X[14], 15, T[50], Func[3]);
                    Do(ref BB, ref CC, ref DD, ref AA, X[5], 21, T[51], Func[3]);
                    Do(ref AA, ref BB, ref CC, ref DD, X[12], 6, T[52], Func[3]);
                    Do(ref DD, ref AA, ref BB, ref CC, X[3], 10, T[53], Func[3]);
                    Do(ref CC, ref DD, ref AA, ref BB, X[10], 15, T[54], Func[3]);
                    Do(ref BB, ref CC, ref DD, ref AA, X[1], 21, T[55], Func[3]);
                    Do(ref AA, ref BB, ref CC, ref DD, X[8], 6, T[56], Func[3]);
                    Do(ref DD, ref AA, ref BB, ref CC, X[15], 10, T[57], Func[3]);
                    Do(ref CC, ref DD, ref AA, ref BB, X[6], 15, T[58], Func[3]);
                    Do(ref BB, ref CC, ref DD, ref AA, X[13], 21, T[59], Func[3]);
                    Do(ref AA, ref BB, ref CC, ref DD, X[4], 6, T[60], Func[3]);
                    Do(ref DD, ref AA, ref BB, ref CC, X[11], 10, T[61], Func[3]);
                    Do(ref CC, ref DD, ref AA, ref BB, X[2], 15, T[62], Func[3]);
                    Do(ref BB, ref CC, ref DD, ref AA, X[9], 21, T[63], Func[3]);

                    A += AA;
                    B += BB;
                    C += CC;
                    D += DD;
                }

                byte[] rezultHashCode;
                CreateHashCode(out rezultHashCode, A, B, C, D);


                this.HashValue = rezultHashCode;
            }

            private int CalculateShift(int k)
            {
                int x = k / 16,
                    y = k % 4;
                return SRange[x, y];
            }

            private int CalculateDataIndex(int k)
            {
                switch (k / 16)
                {
                    case 0:
                        { return k; }
                    case 1:
                        { return (1 + 5 * (k - 16)) % 16; }
                    case 2:
                        { return (5 + 3 * (k - 32)) % 16; }
                    case 3:
                        { return (7 * (k - 48)) % 16; }
                    default:
                        { throw (new IndexOutOfRangeException("Индекс в алгоритме MD5 не должен превышать 63")); }
                }
            }

            private void UpdateOrderVariable(ref int[] v_ord)
            {
                for (int i = 0; i < v_ord.Length; i++)
                {
                    v_ord[i] = ((--v_ord[i] < 0) ? v_ord[i] + 4 : v_ord[i]);
                }
            }

            private void CreateHashCode(out byte[] rezultHashCode, uint A, uint B, uint C, uint D)
            {
                rezultHashCode = new byte[16];
                byte[] rezultA = UInt32ToBytes(A);
                byte[] rezultB = UInt32ToBytes(B);
                byte[] rezultC = UInt32ToBytes(C);
                byte[] rezultD = UInt32ToBytes(D);
                Array.Copy(rezultA, 0, rezultHashCode, 0, 4);
                Array.Copy(rezultB, 0, rezultHashCode, 4, 4);
                Array.Copy(rezultC, 0, rezultHashCode, 8, 4);
                Array.Copy(rezultD, 0, rezultHashCode, 12, 4);
            }



            private void Do(ref uint A, ref  uint B, ref  uint C, ref  uint D, uint X, int s, uint T, ConstantFunction F)
            {
                A = B + (Calc.Logic.Function.ROL((A + F(B, C, D) + X + T), s));
            }

            private uint BytesToUInt32(byte[] words, int start)
            {

                uint output;
                output = ((UInt32)words[start]) | (((UInt32)words[start + 1]) << 8) |
                     (((UInt32)words[start + 2]) << 16) | (((UInt32)words[start + 3]) << 24);
                return output;
                /*  byte[] temp = new byte[4] { words[start], words[start + 1], words[start + 2], words[start + 3] };
                  return BitConverter.ToUInt32(temp, 0);*/
            }
            private byte[] UInt32ToBytes(uint value)
            {
                //алгоритм перевода порядкого номера
                //в координату таблицы. Напрмер 100-ый элемент таблицы размерности 2x64
                //лежит в точке (1,36) (64 * 1 + 36)
                byte[] rezult = new byte[4];
                rezult[0] = Convert.ToByte(value % 256);
                rezult[1] = Convert.ToByte((value >> 8) % 256);
                rezult[2] = Convert.ToByte((value >> 16) % 256);
                rezult[3] = Convert.ToByte((value >> 24) % 256);
                return rezult;
            }



            private void ConstantTableInitialize(out uint[] T)
            {
                T = new UInt32[64];
                for (int i = 0; i < 64; i++)
                {
                    T[i] = (UInt32)(4294967296 * Math.Abs(Math.Sin(i + 1)));
                }
            }

            private void FunctionInitialize(out ConstantFunction[] Func)
            {
                Func = new ConstantFunction[4]
                {
                  ((x, y, z) => (x & y) | (~x & z)),
                  ((x, y, z) => (x & z) | (y & ~z)),
                  ((x, y, z) => x ^ y ^ z),
                  ((x, y, z) => y ^ (x | ~z)),
                };
            }

            private void BufferInitialize(out uint A, out uint B, out uint C, out uint D)
            {
                A = 0x67452301;
                B = 0xEFCDAB89;
                C = 0x98BADCFE;
                D = 0x10325476;
            }



            private void AddMessageLength(ref byte[] array, int oldSize)
            {
                byte[] byteSize = BitConverter.GetBytes(Convert.ToInt64(oldSize * 8));
                Array.Resize(ref array, array.Length + byteSize.Length);
                Array.Copy(byteSize, 0, array, array.Length - byteSize.Length, byteSize.Length);
                if (array.Length % 64 != 0)
                    throw (new Exception("На шаге 2 длинна бит массива не кратна 512"));
            }

            private int CalculateNewSize(int oldSize)
            {
                //получаем размер в байтах
                int val = (512 * (((8 * oldSize) / 512)) + 448) / 8;
                if (val <= oldSize)
                    return (512 * (((8 * oldSize) / 512) + 1) + 448) / 8;
                else
                    return val;
            }

            private void FlowEqualization(ref byte[] array, int oldSize, out int newSize)
            {
                newSize = CalculateNewSize(oldSize);
                Array.Resize(ref array, newSize);
                array[oldSize] = 0x80;
            }

            protected override byte[] HashFinal()
            {
                return this.Hash;
            }

            public static string HashToHex(byte[] array)
            {
                StringBuilder hex = new StringBuilder();
                foreach (byte it in array)
                {
                    hex.AppendFormat("{0:x2}", it);
                }
                string a = hex.ToString();
                return a;
            }

            public static BigInteger HashToBig(byte[] array)
            {
                BigInteger num = new BigInteger(array);
                return num;
            }

            public override void Initialize()
            {

            }

        }
        class ECPRSA 
        {
            public static void GenerateKey(out BigInteger N,out BigInteger E,out BigInteger D)
            {
                Calc.Generate.Random gen = new Calc.Generate.Random();
                BigInteger P = gen.GetPrimeBig(72);
                BigInteger Q = gen.GetPrimeBig(72);
                N = P * Q;
                BigInteger phi = (P - 1) * (Q - 1);
                E = gen.GetPrimeBig(64);
                D = Calc.BigIntWorker.InverseMod(E, phi);
            }

            public static BigInteger ComputeSignature(BigInteger M, BigInteger D, BigInteger N)
            {
                return BigInteger.ModPow(M, D, N);
            }

            public static BigInteger CheckSignature(BigInteger signature, BigInteger E, BigInteger N)
            {
                return BigInteger.ModPow(signature, E, N);
            }


        }

        class DataRemove
        {
            static public void Delete(byte[] data)
            {
                int size = data.Length;
                for (int i = 0; i < size; i++)
                {
                    data[i] = 0;
                }
            }
            static public void Delete(string path)
            {
                FileStream file = System.IO.File.Open(path, FileMode.Open);
                byte[] data = new byte[1024];
                int curr = 0;
                while (curr * 1024 < file.Length)
                {
                    if (file.Length - curr * 1024 < 1024)
                    {
                        Array.Resize(ref data, Convert.ToInt16(file.Length - curr * 1024));
                    }

                    file.Read(data,0, data.Length);
                    file.Position -= data.Length;
                    Delete(data);
                    file.Write(data, 0, data.Length);
                    curr++;
                }
                file.Close();
                System.IO.File.Delete(path);
            }

            static public void Delete(string path,ref long processed)
            {
                FileStream file = System.IO.File.Open(path, FileMode.Open);
                int part = 1024;
                byte[] data = new byte[part];
                int curr = 0;
                while (curr * part < file.Length)
                {
                    if (file.Length - curr * part < part)
                    {
                        Array.Resize(ref data, Convert.ToInt16(file.Length - curr * part));
                    }

                    file.Read(data, 0, data.Length);
                    file.Position -= data.Length;
                    Delete(data);
                    file.Write(data, 0, data.Length);
                    curr++;
                    processed = curr * part;
                }
                file.Close();
                System.IO.File.Delete(path);
            }
        }

    }

   
}