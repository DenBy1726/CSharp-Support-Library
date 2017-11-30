using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

///Пространство имен обеспечивает работу с нейронными сетями
namespace Neuro
{
    /// <summary>
    /// Хранение образов. 
    /// Образом счтается массив Enters[] входов и массив Results[] выходов
    /// </summary>
    class NeuroImage
    {
        double[] enters;

        /// <summary>
        /// Входные образы
        /// </summary>
        public double[] Enters
        {
            get { return enters; }
            set { enters = value; }
        }

        /// <summary>
        /// Выходные образы
        /// </summary>
        double[] results;

        public double[] Results
        {
            get { return results; }
            set { results = value; }
        }

        public NeuroImage(double[] enters,double[] results)
        {
            Enters = enters;
            Results = results;
        }
    }

    /// <summary>
    /// Однослойный персептрон
    /// для использования необходимо сперва при помощи метода Add подать образы, 
    /// затем при помощи метода Teach() обучить. Если успешно, то можно распознавать образы
    /// </summary>
    class SingleLayerPerceptron
    {
        List<NeuroImage> image = new List<NeuroImage>();

        /// <summary>
        /// Хранение входных и выходных образов обучающей выборки
        /// </summary>
        internal List<NeuroImage> Image
        {
            get { return image; }
            set { image = value; }
        }
      
        double[,] weight;

        /// <summary>
        /// Хранение весов
        /// </summary>
        public double[,] Weight
        {
            get { return weight; }
            set { weight = value; }
        }
        double[] t;

        /// <summary>
        /// Хранение весов
        /// </summary>
        public double[] T
        {
            get { return t; }
            set { t = value; }
        }

        int enterSize = 2;
        int resultSize = 1;

        private double step = 0.2;

        /// <summary>
        /// Шаг обучения
        /// </summary>
        public double Step
        {
            get { return step; }
            set { step = value; }
        }

        /// <summary>
        /// Инициализация сети случайными величинами
        /// </summary>
        /// <param name="enterSize">Amount of enter</param>
        /// <param name="resultSize">Amount of exit</param>
        public SingleLayerPerceptron(int enterSize = 2,int resultSize = 1)
        {
            this.enterSize = enterSize;
            this.resultSize = resultSize;
            Random gen = new Random();
            weight = new double[enterSize,resultSize];
            t = new double[resultSize];
           
            for (int i = 0; i < resultSize; i++)
            {
                T[i] = 2 * gen.NextDouble() - 1;
                for (int j = 0; j < enterSize; j++)
                {
                    weight[j,i] = 2 * gen.NextDouble() - 1;
                }
            }
        }

        /// <summary>
        /// Add one image,perceptron recognized
        /// return 0 if this image contains else return 1
        /// </summary>
        /// <param name="enter">enter image</param>
        /// <param name="result">exit image</param>
        public int Add(double[] enter, double[] result)
        {
            NeuroImage img = new NeuroImage(enter,result);
            return Add(img);
                  
        }


        /// <summary>
        /// Добавление образа, который песептрону необходимо будет распознать
        /// Если образ уже был добавлен , возвращает 0
        /// </summary>
        /// <param name="img"></param>
        /// <returns></returns>
        public int Add(NeuroImage img)
        {
            if (img.Enters.Length != enterSize || img.Results.Length != resultSize)
                throw (new Exception("Персептрон был настроен на " + enterSize + " входов и " +
           resultSize + "выходов, а в образе присутствует " + img.Enters.Length + " входов и " +
           img.Results.Length + "выходов"));

          
            if (!Contains(img))
            {            
                Image.Add(img);
                return 1;
            }
            else
                return 0;  
        }

        /// <summary>
        /// Метод проверяет содержится ли образ в обучающей выборке
        /// </summary>
        /// <param name="Image"></param>
        /// <param name="img"></param>
        /// <returns></returns>
        public bool Contains(NeuroImage img)
        {
            //выбираем все входные образы
            var query = (from x in Image
                         select x.Enters).ToArray();
            
            for (int i = 0; i < query.Length; i++)
            {
                bool flag = true;
                for (int j = 0; j < img.Enters.Length; j++)
                {
                    if (query[i][j] != img.Enters[j])
                    {
                        flag = false;
                    }
                }
                if (flag == true)
                    return true;
            }
            return false;
        }

        /// <summary>
        /// Обучение сети. Если сеть не может обучиться , возвращает -1,
        /// иначе возвращает количество эпох, затраченных на обучение
        /// </summary>
        /// <param name="epoch"></param>
        /// <returns></returns>
        public int Teach(int epoch = 100)
        {
            int i=0;
            double[] error;
            bool wasError = false;
            double[] answer;
            do
            {
             //   Console.WriteLine("*******************Iteration number {0}********************", i);
                wasError = false;
                for (int j = 0; j < Image.Count; j++)
                {

                    answer = Recognize(Image[j].Enters);
              //      Console.WriteLine("Input: {0},{1} Output: {2}", enters[j][0], enters[j][1], answer);
              //      Console.WriteLine("Etalon: {0}", results[j]);

                    if (!Equal(answer,Image[j].Results))
                    {
                        wasError = true;
                        error = CalculateError(answer,Image[j].Results);
                        Study(Image[j].Enters,Image[j].Results, error);
              //          Console.WriteLine("Modify:");
              //          Console.WriteLine("new weights: {0},{1}, new T: {2}", weight[0], weight[1], T);
                    }
                }
                i++;
            }
            while(i < epoch && wasError == true);
            if (i < epoch)
                return i;
            else
                return -1;
        }

        private double[] CalculateError(double[] answer, double[] p)
        {
            double[] error = new double[answer.Length];
            for (int i = 0; i < error.Length; i++)
                error[i] = answer[i] - p[i];
            return error;
        }

        private bool Equal(double[] answer, double[] p)
        {
            for (int i = 0; i < answer.Length; i++)
            {
                if (answer[i] != p[i])
                    return false;
            }
            return true;
        }

        private void Study(double[] enters,double[] results,double[] error)
        {
            for (int i = 0; i < resultSize; i++)
            {
                for (int j = 0; j < enterSize;j++ )
                    weight[j,i] = weight[j,i] - step * enters[j] * error[i];
                T[i] = T[i] + step * error[i];
            }      
        }

        private void Activation(ref double[] answer)
        {
            for (int i = 0; i < resultSize; i++)
            {
                if (answer[i] > 0)
                    answer[i] = 1.0;
                else
                    answer[i] = 0.0;
            }
        }

        /// <summary>
        /// распознавание входного образа
        /// </summary>
        /// <param name="enter"></param>
        /// <returns></returns>
        public double[] Recognize(double[] enter)
        {
            if (enter.Length != enterSize )
                throw (new Exception("Персептрон был настроен на " + enterSize + " входов, " +
          "а в образе присутствует " + enter.Length + " входов "));
            double[] s = new double[resultSize];
            for (int i = 0; i < resultSize; i++)
            {
                for (int j = 0; j < enterSize; j++)
                {
                    s[i] += (weight[j, i] * enter[j]);
                }
                s[i] -= T[i];
            }
            
            Activation(ref s);
            return s;
        }

    }

}
