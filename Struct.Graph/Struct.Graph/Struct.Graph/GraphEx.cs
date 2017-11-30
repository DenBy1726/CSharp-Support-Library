using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Struct.Graph
{
    /// <summary>
    /// Расширение класс граф
    /// </summary>
    /// <typeparam name="T"></typeparam>
    static class GraphEx
    {


        /// <summary>
        /// в sources есть как минимум одна вершина, которая связана со всеми из destinies
        /// </summary>
        /// <param name="sources"></param>
        /// <param name="destinies"></param>
        /// <returns></returns>
        public static bool LeastOneAttachedToAll<T>(this IGraph<T> gr, List<int> sources, List<int> destinies)
        {
            foreach (int it in sources)
            {
                if (AttachedToAll(gr, it, destinies))
                    return true;
            }
            return false;
        }

        /// <summary>
        /// //проверяет связана ли вершина со всеми из множества
        /// </summary>
        /// <param name="source"></param>
        /// <param name="dest"></param>
        /// <returns></returns>
        public static bool AttachedToAll<T>(this IGraph<T> gr, int source, List<int> dest)
        {
            foreach (int it in dest)
            {
                if (gr.IsConnected(source, it) == false)
                    return false;
            }
            return true;
        }

        /// <summary>
        /// //создает новый вектор из вершин из множества old, не связанных с v
        /// </summary>
        /// <param name="gr"></param>
        /// <param name="old"></param>
        /// <param name="v"></param>
        /// <returns></returns>
        public static List<int> NotAttachedVertexes<T>(this IGraph<T> gr, List<int> old, int v)
        {
            List<int> rez = new List<int>();
            rez.Select((x) => x);
            foreach (int it in old)
            {
                if (gr.IsConnected(it, v) == false)
                    rez.Add(it);
            }
            return rez;
        }


        /// <summary>
        /// //Функция рекурсивно ищет клики
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="gr"></param>
        /// <param name="cand"></param>
        /// <param name="not"></param>
        /// <param name="comp"></param>
        /// <param name="result"></param>
        private static void findCliques<T>(this IGraph<T> gr, List<int> cand, List<int> not, List<int> comp, ref List<int> result)
        {
            //Алгоритм выполняетяс до тех пор пока множество кандидат не пусто
            //и как минимум одна вершина из уже пройденных свазана со всеми кандидатами
            while (cand.Count != 0 && LeastOneAttachedToAll(gr, not, cand) == false)
            {
                //1) удаляем из кандидаты вершину и добавляем ее в текущюю клику
                int v = cand.Last();
                comp.Add(v);
                //2) формируем новые кандидаты, удаляя из старых все вершины, не связные с удаленной
                // тоже самое проделываем и с уже пройденными ( исключаем заведомо ложные варианты)
                List<int> new_candidates = NotAttachedVertexes(gr, cand, v);
                List<int> new_not = NotAttachedVertexes(gr, not, v);
                if (new_not.Count != 0 && new_candidates.Count == 0)
                {
                    if (comp.Count() > result.Count)
                        result = comp;
                }
                else
                {
                    findCliques(gr, new_candidates, new_not, comp, ref result);
                }
                comp.RemoveAt(comp.Count - 1);
                cand.RemoveAt(cand.Count - 1);
                not.Add(v);
            }
        }

        /// <summary>
        /// //функция ищет максимальный полный подграф и возвращает номера вершин, которые он включает
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="gr"></param>
        /// <returns></returns>
        public static List<int> FindCliques<T>(this IGraph<T> gr)
        {
            List<int> comp = new List<int>();
            List<int> not = new List<int>();
            List<int> cand = new List<int>();
            List<int> rez = new List<int>();
            for (int i = 0; i < gr.Length(); i++)
                cand.Add(i);
            findCliques(gr, cand, not, comp, ref rez);
            return rez;
        }

      


    }
}
