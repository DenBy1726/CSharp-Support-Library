using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algorithm.ML.KMeans
{
    /// <summary>
    /// Класс для отображения хода работы алгоритмов. Алгоритм по ходу выполнения
    /// модифицирует поля, что дает вохможность из вне узнавать состояние алгоритма
    /// </summary>
    class ProgressState
    {
        private string utilizing;

        /// <summary>
        /// Имя класса, использующего данный класс
        /// </summary>
        public string Utilizing
        {
            get { return utilizing; }
            set { utilizing = value; }
        }

        private int сurrentStage;

        /// <summary>
        /// Текущий этап выполнения алгоритма
        /// </summary>
        public int СurrentStage
        {
            get { return сurrentStage; }
            set {
                сurrentStage = value;
                if (CurrentStageChanged != null)
                    CurrentStageChanged(this);
            }
        }

     

        private int maxStage;

        /// <summary>
        /// Количество этапов выполнения алгоритма
        /// </summary>
        public int MaxStage
        {
            get { return maxStage; }
            set { maxStage = value; }
        }
        private string nameOfStage;

        /// <summary>
        /// Название текущего этапа выполнения алгоритма
        /// </summary>
        public string NameOfStage
        {
            get { return nameOfStage; }
            set { nameOfStage = value; }
        }
        int epochStage;

        /// <summary>
        /// Стадии выполнения конкретного этапа
        /// </summary>
        public int EpochStage
        {
            get { return epochStage; }
            set { 
                epochStage = value;
                if(EpochChanged != null)
                    EpochChanged(this);
            }
        }

        int maxEpochStage;


        /// <summary>
        /// Количество стадий выполнения текущего этапа
        /// </summary>
        public int MaxEpochStage
        {
            get { return maxEpochStage; }
            set { maxEpochStage = value; }
        }

        /// <summary>
        /// возникает при изменении текущего этапа
        /// </summary>
        event Action<ProgressState> CurrentStageChanged;
        /// <summary>
        /// возникает по мере продвижения текущего этапа
        /// </summary>
        event Action<ProgressState> EpochChanged;
    }

    /// <summary>
    /// Класс описывает центр кластера для обучения
    /// </summary>
    /// <typeparam name="T">тип</typeparam>
    class ClusterItem<T>
    {
           
        public T Value;

      
        uint count;

        /// <summary>
        /// Количетсво элементов в кластере
        /// </summary>
        public uint Count
        {
            get { return count; }
            set { count = value; }
        }

    }

    /// <summary>
    /// Структура для кластеризации цвета. Хранит суммы по каждой из состовляющих RGB.
    /// </summary>
    struct ColorMassStructure
    {
        public uint R;
        public uint G;
        public uint B;
        public static ColorMassStructure operator /(ColorMassStructure th,uint val)
        {
            ColorMassStructure cStruct = new ColorMassStructure();
            cStruct.R = th.R / val;
            cStruct.G = th.G / val;
            cStruct.B = th.B / val;
            return cStruct;
        }

        public static ColorMassStructure operator +(ColorMassStructure th,ColorMassStructure th2)
        {
            ColorMassStructure cStruct = new ColorMassStructure();
            cStruct.R = th.R + th2.R;
            cStruct.G = th.G + th2.G;
            cStruct.B = th.B + th2.B;
            return cStruct;
        }
    }

    /// <summary>
    /// Класс для хранения кластера цвета изображения
    /// </summary>
    class BitmapCluster : ClusterItem<ColorMassStructure>
    {
        public void Center()
        {
            Value/= Count;
        }
    }

    /// <summary>
    /// Абстрактный класс Кластеризатора
    /// </summary>
    /// <typeparam name="T">тип данных, которые необходимо кластеризовать</typeparam>
    abstract class Clustering<T>
    {
        public Clustering()
        {

        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="clusster">Массив кластеров,содержащий состояние инициализации для каждого кластера. В результате обучения центры масс сместятся</param>
        /// <param name="CompareRule">Правило сравнения элементов</param>
        public Clustering(T[] clusters, Func<T, T, float> compareRule)
        {
            this.clusters = clusters;
            this.errorRule = compareRule;
        }
        /// <summary>
        /// Заупск обучения кластеризации, резуьтат которой будет обучение кластеров для разделения
        /// входных данных на группы
        /// </summary>
        /// <param name="data">Массив входных данных </param>
        /// <param name="progress">Состояние прогресса алгоритма</param>
        /// <returns>Количество итераций, за которое алгоритм сошелся</returns>

        public abstract int Teach(T[] data,ref ProgressState progress);
        public int Compute(T input)
        {
            float[] dimension = new float[clusters.Length];
            for (int i = 0; i < clusters.Length; i++)
            {
                dimension[i] = ErrorRule(input, clusters[i]);
            }
            float min = dimension.Min();
            return Array.FindIndex(dimension,(x)=>x == min);
        }

        T[] clusters;

        /// <summary>
        /// массив центров кластеров
        /// </summary>
        public T[] Clusters
        {
            get { return clusters; }
            set { clusters = value; }
        }

        Func<T, T, float> errorRule;

        /// <param name="CompareRule">Правило сравнения элементов</param>
        public Func<T, T, float> ErrorRule
        {
            get { return errorRule; }
            set { errorRule = value; }
        }
        
    }


    class BitmapClustering : Clustering<System.Drawing.Color>
    {
        public BitmapClustering(System.Drawing.Color[] clusters)
        {
            this.ErrorRule = (p1, p2) => { return (float)Math.Sqrt(Math.Pow(p1.R - p2.R, 2) + Math.Pow(p1.G - p2.G, 2) + Math.Pow(p1.B - p2.B, 2)); };
            this.Clusters = clusters;
        }


        public void InitializeCluster()
        {
            Random rnd = new Random();
            for (int i = 0; i < Clusters.Length; i++)
            {
                Clusters[i] = System.Drawing.Color.FromArgb(255,rnd.Next() % 255, rnd.Next() % 255, rnd.Next() % 255);
            }
        }

        public override int Teach(System.Drawing.Color[] data, ref ProgressState progress)
        {
            if (Clusters.Length < 1)
                return -1;
            progress.Utilizing = this.ToString();

            //рабочее представление кластера
            BitmapCluster[] _clusters = new BitmapCluster[Clusters.Length];

            //первичная инициализация рабочего представления кластера на основе начальных точек
            for (int i = 0; i < _clusters.Length; i++)
            {
              
                _clusters[i] = new BitmapCluster();
                _clusters[i].Value.R = Clusters[i].R;
                _clusters[i].Value.G = Clusters[i].G;
                _clusters[i].Value.B = Clusters[i].B;
            }

            float minRgbError, oldRgbError = 0;
            int epoch = 0;

            while(true)
            {
                //Инициализация рабочего представления кластера для текущей итерации
                for (int i = 0; i < _clusters.Length; i++)
                {
                    byte R = (byte)_clusters[i].Value.R;
                    byte G = (byte)_clusters[i].Value.G;
                    byte B = (byte)_clusters[i].Value.B;
                    System.Drawing.Color C = System.Drawing.Color.FromArgb(255,R, G, B);
                    Clusters[i] = C;
                    _clusters[i].Count = 0;
                    _clusters[i].Value.R = 0;
                    _clusters[i].Value.G = 0;
                    _clusters[i].Value.B = 0;

                }


                for (int i = 0; i < data.Length; i++)
                {
                    // получаем RGB-компоненты пикселя
                    byte R = data[i].R;
                    byte G = data[i].G;
                    byte B = data[i].B;
                    System.Drawing.Color C = System.Drawing.Color.FromArgb(255,R, G, B);


                    minRgbError = 255 * 255 * 255;

                    int clusterIndex = -1;
                    for (int k = 0; k < _clusters.Length; k++)
                    {
                        float euclid = ErrorRule(C, Clusters[k]);
                        if (euclid < minRgbError)
                        {
                            minRgbError = euclid;
                            clusterIndex = k;
                        }
                    }

                    // устанавливаем индекс кластера
                    _clusters[clusterIndex].Count++;
                    _clusters[clusterIndex].Value.R += R;
                    _clusters[clusterIndex].Value.G += G;
                    _clusters[clusterIndex].Value.B += B;
                }

                minRgbError = 0;
                for (int k = 0; k < _clusters.Length; k++)
                {
                    // new color
                    if(_clusters[k].Count > 1)
                        _clusters[k].Center();
                    byte R = (byte)_clusters[k].Value.R;
                    byte G = (byte)_clusters[k].Value.G;
                    byte B = (byte)_clusters[k].Value.B;
                    System.Drawing.Color C = System.Drawing.Color.FromArgb(255,R, G, B);
                    float ecli = ErrorRule(C, Clusters[k]);
                    if (ecli > minRgbError)
                        minRgbError = ecli;
                }

                epoch++;
                if (Math.Abs(minRgbError - oldRgbError) < 1 || minRgbError < 50 || epoch == 50)
                    break;

                oldRgbError = minRgbError;
               
                
            }

            // теперь загоним массив в вектор и отсортируем 
            List<KeyValuePair<int, uint>> colors = new List<KeyValuePair<int,uint>>();

            int colors_count = 0;
            for (int i = 0; i < Clusters.Length; i++)
            {
                KeyValuePair<int, uint> color = new KeyValuePair<int, uint>(i, _clusters[i].Count);
                colors.Add(color);
                if (_clusters[i].Count > 0)
                    colors_count++;
            }

            colors.Sort(CompareClusters);
            for (int i = 0; i < Clusters.Length; i++)
            {
                byte R =  (byte)_clusters[colors[i].Key].Value.R;
                byte G = (byte)_clusters[colors[i].Key].Value.G;
                byte B = (byte)_clusters[colors[i].Key].Value.B;
                Clusters[i] = System.Drawing.Color.FromArgb(255,R, G, B);
            }

            return epoch;
        }

        private static int CompareClusters(KeyValuePair<int, uint> x, KeyValuePair<int, uint> y)
        {
            return y.Value.CompareTo(x.Value);
        }

        public int Teach(System.Drawing.Bitmap data, ref ProgressState progress)
        {

            System.Drawing.Color[] c = new System.Drawing.Color[data.Width * data.Height];
            progress.NameOfStage = "Bitmap to Color List";
            progress.MaxEpochStage = c.Length;
            for (int i = 0; i < data.Width; i++)
            {
                for (int j = 0; j < data.Height; j++)
                {
                    c[i * data.Height + j] = data.GetPixel(i, j);
                    progress.EpochStage = i * data.Width + j;
                }
            }
            return Teach(c,ref progress);
        }

        public System.Drawing.Bitmap Compute(System.Drawing.Bitmap data)
        {
            System.Drawing.Bitmap bmp = (System.Drawing.Bitmap)data.Clone();
            for(int i=0;i<data.Width;i++)
            {
                for(int j=0;j<data.Height;j++)
                {
                    int x = this.Compute(data.GetPixel(i, j));
                    bmp.SetPixel(i, j, this.Clusters[x]);
                }
            }
            return bmp;
        }
    }
}
