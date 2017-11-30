using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Struct;

namespace Struct
{
   public struct GraphItem<T>
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
   public class Graph<T>
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
               throw new ArgumentException("ћатрица должна быть квадратной");
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
       /// ѕолучить ребро
       /// </summary>
       /// <param name="start">вершина начала ребра</param>
       /// <param name="end">вершина конца ребра</param>
       /// <returns>значение ребра</returns>
       public T GetArc(int start,int end)
       {
           if (start >= _content.Length || end >= _content.Length)
               throw new ArgumentException("¬ершины, между которыми ребро, должны существовать");
           return _content[start][end];
       }

       public T this[int i,int j]
       {
           get 
           {
               if (i >= _content.Length || j >= _content.Length)
                   throw new IndexOutOfRangeException("¬ершины, между которыми ребро, должны существовать");
               return _content[i][j]; 
           }
           set 
           {
               if (i >= _content.Length || j >= _content.Length)
                   throw new IndexOutOfRangeException("¬ершины, между которыми ребро, должны существовать");
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
       /// ”дал€ет все вершины в графе ( устанавливает нул€ми типа)
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
       /// ƒобавл€ет в граф ребро
       /// </summary>
       /// <param name="x">вершина начала ребра</param>
       /// <param name="y">вершина конца ребра</param>
       /// <param name="w">¬ес св€зи</param>
       /// <param name="or">ребро ориентировано?(если false то также создатс€ обратное ребро</param>
       public void AddArc(int x, int y, T w = default(T), bool or = true)
       {
           if (x >= _content.Length || y >= _content.Length)
               throw new IndexOutOfRangeException("¬ершины, между которыми ребро, должны существовать");
           _content[x][y] = w;
           if (or == false)
           {
               _content[y][x] = w;
           }
       }

       /// <summary>
       /// ƒобавл€ет в граф ребро. возвращает новый граф с добавленным ребром
       /// </summary>
       /// <param name="item">ребро,которое нужно добавить</param>
       public static Graph<T> operator+(Graph<T> gr,GraphItem<T> item)
       {
           Graph<T> newGraph = new Graph<T>(gr);
           newGraph.AddArc(item.Start, item.Finish, item.Weight);
           return newGraph;
       }

     
       /// <summary>
       /// ”дал€ет из графа ребро
       /// </summary>
       /// <param name="x">вершина начала ребра</param>
       /// <param name="y">вершина конца ребра</param>
       public void RemoveArc(int x, int y)
       {
           AddArc(x, y, default(T), false);
       }

       /// <summary>
       /// ”дал€ет из графа ребро. ¬озвращает новый граф без ребра
       /// </summary>
       /// <param name="item">ребро, которое нужно удалить</param>
       public static Graph<T> operator-(Graph<T> gr,KeyValuePair<int, int> item)
       {
           Graph<T> newGraph = new Graph<T>(gr);
           newGraph.RemoveArc(item.Key, item.Value);
           return newGraph;
       }

      /// <summary>
      /// ѕроверка есть ли между вершинами ребро
      /// </summary>
       /// <param name="x">вершина начала ребра</param>
       /// <param name="y">вершина конца ребра</param>
      /// <returns></returns>
       public bool IsConnected(int x, int y)
       {
           if (x >= _content.Length || y >= _content.Length)
               throw new IndexOutOfRangeException("¬ершины, между которыми ребро, должны существовать");
           if (_content[x][y].Equals(default(T)))
               return false;
           else
               return true;
       }

       public T[][] ToArray()
       {
           return _content;
       }
   }
}