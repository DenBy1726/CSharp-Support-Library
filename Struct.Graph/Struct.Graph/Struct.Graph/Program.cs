using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Struct;
using Struct.Graph;

namespace Struct
{
   public interface IGraph<T>
    {
        IGraph<T> SubGraph(List<int> vertex);
        T GetArc(int start, int end);

        T this[int i, int j]
        {
            get;set;
        }

        T[] this[int i]
        {
            get;
            set;
        }

        int Length();

        bool IsEmpty();

        /// <summary>
        /// Удаляет все вершины в графе ( устанавливает нулями типа)
        /// </summary>
        void Clear();

        /// <summary>
        /// Добавляет в граф ребро
        /// </summary>
        /// <param name="x">вершина начала ребра</param>
        /// <param name="y">вершина конца ребра</param>
        /// <param name="w">Вес связи</param>
        /// <param name="or">ребро ориентировано?(если false то также создатся обратное ребро</param>
        void AddArc(int x, int y, T w = default(T), bool or = true);



        /// <summary>
        /// Удаляет из графа ребро
        /// </summary>
        /// <param name="x">вершина начала ребра</param>
        /// <param name="y">вершина конца ребра</param>
        void RemoveArc(int x, int y);


        /// <summary>
        /// Проверка есть ли между вершинами ребро
        /// </summary>
        /// <param name="x">вершина начала ребра</param>
        /// <param name="y">вершина конца ребра</param>
        /// <returns></returns>
        bool IsConnected(int x, int y);

        T[][] ToArray();
    }
   public class GraphItem<T>
        where T: new()
    {
        private int start;

        public int Start
        {
            get { return start; }
            set { start = value; }
        }
        private int finish;

        public int Finish
        {
            get { return finish; }
            set { finish = value; }
        }
        private T weight;

        public T Weight
        {
            get { return weight; }
            set { weight = value; }
        }
    }
   public class Graph<T> : IGraph<T>
       where T : new()
   {
       protected T[][] _content;

       public Graph()
       {

       }

       public Graph(Graph<T> gr)
       {
           _content = (T[][])gr._content.Clone();
       }

       public Graph(T[][] matrix)
       {
           if (matrix.GetLength(0) != matrix.GetLength(1))
               throw new ArgumentException("Матрица должна быть квадратной");
           this._content = matrix;
       }

       public Graph(int dimension)
       {
           if (dimension == 0)
               return;
           Array.Resize(ref _content, dimension);
           for(int i=0;i<dimension;i++)
           {
               Array.Resize(ref _content[i], dimension);
           }
       }

       /// <summary>
       /// Получить ребро
       /// </summary>
       /// <param name="start">вершина начала ребра</param>
       /// <param name="end">вершина конца ребра</param>
       /// <returns>значение ребра</returns>
       public T GetArc(int start,int end)
       {
           if (start >= _content.Length || end >= _content.Length)
               throw new ArgumentException("Вершины, между которыми ребро, должны существовать");
           return _content[start][end];
       }

       public T this[int i,int j]
       {
           get 
           {
               if (i >= _content.Length || j >= _content.Length)
                   throw new IndexOutOfRangeException("Вершины, между которыми ребро, должны существовать");
               return _content[i][j]; 
           }
           set 
           {
               if (i >= _content.Length || j >= _content.Length)
                   throw new IndexOutOfRangeException("Вершины, между которыми ребро, должны существовать");
               _content[i][j] = value; 
           }
       }

       public T[] this[int i]
       {
           get
           {
               if (i >= _content.Length)
                   throw new IndexOutOfRangeException();
               return _content[i];
           }
           set 
           { 
               _content[i] = value;
           }
       }

       public int Length()
       {
           return _content.Length;
       }

       public bool IsEmpty()
       {
           return _content.Length == 0;
       }

       /// <summary>
       /// Удаляет все вершины в графе ( устанавливает нулями типа)
       /// </summary>
       public void Clear()
       {
           Array.Clear(_content,0,_content.Length);
           for(int i=0;i<_content.Length;i++)
           {
               Array.Clear(_content[i], 0, _content.Length);
           }
       }

       /// <summary>
       /// Добавляет в граф ребро
       /// </summary>
       /// <param name="x">вершина начала ребра</param>
       /// <param name="y">вершина конца ребра</param>
       /// <param name="w">Вес связи</param>
       /// <param name="or">ребро ориентировано?(если false то также создатся обратное ребро</param>
       public void AddArc(int x, int y, T w = default(T), bool or = true)
       {
           if (x >= _content.Length || y >= _content.Length)
               throw new IndexOutOfRangeException("Вершины, между которыми ребро, должны существовать");
           _content[x][y] = w;
           if (or == false)
           {
               _content[y][x] = w;
           }
       }

       /// <summary>
       /// Добавляет в граф ребро. возвращает новый граф с добавленным ребром
       /// </summary>
       /// <param name="item">ребро,которое нужно добавить</param>
       public static Graph<T> operator+(Graph<T> gr,GraphItem<T> item)
       {
           Graph<T> newGraph = new Graph<T>(gr);
           newGraph.AddArc(item.Start, item.Finish, item.Weight);
           return newGraph;
       }

     
       /// <summary>
       /// Удаляет из графа ребро
       /// </summary>
       /// <param name="x">вершина начала ребра</param>
       /// <param name="y">вершина конца ребра</param>
       public void RemoveArc(int x, int y)
       {
           AddArc(x, y, default(T), false);
       }

       /// <summary>
       /// Удаляет из графа ребро. Возвращает новый граф без ребра
       /// </summary>
       /// <param name="item">ребро, которое нужно удалить</param>
       public static Graph<T> operator-(Graph<T> gr,GraphItem<T> item)
       {
           Graph<T> newGraph = new Graph<T>(gr);
           newGraph.RemoveArc(item.Start, item.Finish);
           return newGraph;
       }

      /// <summary>
      /// Проверка есть ли между вершинами ребро
      /// </summary>
       /// <param name="x">вершина начала ребра</param>
       /// <param name="y">вершина конца ребра</param>
      /// <returns></returns>
       public bool IsConnected(int x, int y)
       {
           if (x >= _content.Length || y >= _content.Length)
               throw new IndexOutOfRangeException("Вершины, между которыми ребро, должны существовать");
           if (_content[x][y].Equals(default(T)))
               return false;
           else
               return true;
       }

    

       public T[][] ToArray()
       {
           return _content;
       }

       /// <summary>
       /// Возвращает подграф, включающий vertex вершины
       /// </summary>
       /// <param name="vertex"></param>
       /// <returns></returns>
       public IGraph<T> SubGraph(List<int> vertex)
       {
           T[][] newGR = new T[vertex.Count][];
           for (int i = 0; i < vertex.Count; i++)
           {
               newGR[i] = new T[vertex.Count];
           }
           for(int i=0;i<vertex.Count;i++)
           {
               for(int j=0;j<vertex.Count;j++)
               {
                   newGR[i][j] = _content[vertex[i]][vertex[j]];
               }
           }
           return new Graph<T>(newGR);        
       }
   }

    public class WeighGraph : Graph<double>
    {


        /// <summary>
        /// Добавляет в граф ребро
        /// </summary>
        /// <param name="x">вершина начала ребра</param>
        /// <param name="y">вершина конца ребра</param>
        /// <param name="w">Вес связи</param>
        /// <param name="or">ребро ориентировано?(если false то также создатся обратное ребро</param>
        public void AddArc(int x, int y, double w = 1.0, bool or = true)
        {
            if (x >= _content.Length || y >= _content.Length)
                throw new IndexOutOfRangeException("Вершины, между которыми ребро, должны существовать");
            _content[x][y] = w;
            if (or == false)
            {
                _content[y][x] = w;
            }
        }

        public List<GraphItem<double>> GetArcs()
        {
            List<GraphItem<double>> items = new List<GraphItem<double>>();
            for (int i = 0; i < _content.Length; i++)
            {
                for (int j = 0; j < _content.Length; j++)
                {
                    if (_content[i][j] != 0)
                        items.Add(new GraphItem<double> { Start = i, Finish = j, Weight = _content[i][j] });
                }
            }
            return items;
        }

        
        /// <summary>
        /// Рекурсивная процедура поиска циклов
        /// </summary>
        /// <param name="curr">стартовая вершина</param>
        /// <param name="end">конечная вершина (служебное)</param>
        /// <param name="used">помеченные вершины</param>
        /// <param name="loop">текущий цикл</param>
        /// <param name="rez">список циклов(результат вычисления)</param>
        /// <param name="unavailable">вершина с которой пришли(служебное)</param>
        private void ContainsLoop(int curr, int end, bool[] used, List<int> loop, List<List<int>> rez, int unavailable = -1)
        {
            //если u == endV, то эту вершину перекрашивать не нужно, иначе мы в нее не вернемся, а вернуться необходимо
              if (curr != end)
                  used[curr] = true;
              else if (loop.Count >= 2)
              {
                  loop.Reverse();
                  //есть ли палиндром для этого цикла графа?
                  bool flag = false; 
                  for (int i = 0; i < rez.Count; i++)
                      if (rez[i].SequenceEqual(loop))
                      {
                          flag = true;
                          break;
                      }
                  if (!flag)
                  {
                      loop.Reverse();
                      rez.Add(new List<int>(loop));
                  }
                  return;
              }
            //для каждого ребра

              for (int w = 0; w < _content[curr].Length;w++)
              {
                  if (w == unavailable)
                      continue;
                  if (IsConnected(curr, w) == false)
                  {
                      continue;
                  }
                  if(used[w] == false)
                  {
                      List<int> cycleNEW = new List<int>(loop);
                      cycleNEW.Add(w);
                      ContainsLoop(w, end, used, cycleNEW, rez, w);
                      used[w] = false;
                  }
              }
      
        }


        /// <summary>
        /// возвращает первый найденный цикл в графе
        /// </summary>
        /// <param name="v"></param>
        /// <returns></returns>
        public List<List<int>> ContainsLoop()
        {

            List<List<int>> res = new List<List<int>>();
            bool[] color = new bool[Length()];
            
            for (int i = 0; i < Length(); i++)
            {
                List<int> loop = new List<int>();
                loop.Add(i);
                ContainsLoop(i, i, color,  loop, res,-1);
            }


            return res.Where((x) => x.Count > 3).Distinct(new ListEqualityComparer()).ToList();
        }

        private class ListEqualityComparer : IEqualityComparer<List<int>>
        {

            public bool Equals(List<int> x, List<int> y)
            {
                return x.SequenceEqual(y);
            }



            public int GetHashCode(List<int> obj)
            {
                return obj.Sum((x)=>x*x);
            }
        }

         public WeighGraph(WeighGraph gr)
             :base(gr)
       {
           
       }

       public WeighGraph(double[][] matrix)
           :base(matrix)
       {
          
       }

       public WeighGraph(int dimension)
           :base(dimension)
       {

       }
    }
}

namespace Struct
{

    class ListEqualityComparer : IEqualityComparer<List<int>>
    {

        public bool Equals(List<int> x, List<int> y)
        {
            return x.GetHashCode() != y.GetHashCode();
        }



        public int GetHashCode(List<int> obj)
        {
            return obj.Sum();
        }
    }


    class Program
    {



        static void Main(string[] args)
        {
            WeighGraph gr = new WeighGraph(5);
            gr.AddArc(0, 1, or: false);
            gr.AddArc(1, 2, or: false);
            gr.AddArc(0,3, or: false);
            gr.AddArc(2, 3, or: false);
            gr.AddArc(2, 4, or: false);
            gr.AddArc(3, 4, or: false);

            var loop = gr.ContainsLoop();
            Dictionary<List<int>, int> set = new Dictionary<List<int>, int>(new ListEqualityComparer());

            for (int i = 0; i < loop.Count; i++)
            {
                HashSet<int> hTemp = new HashSet<int>(loop[i]);
                List<int> vertex = hTemp.ToList();
                if (set.ContainsKey(vertex) == false)
                    set[vertex] = i;
            }



            foreach (var it in set)
            {
                for (int i = 0; i < loop[it.Value].Count; i++)
                    Console.Write(loop[it.Value][i]+1);
                Console.WriteLine();
            }
        }


    }
}
