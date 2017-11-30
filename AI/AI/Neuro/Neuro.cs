using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization.Json;

///Пространство имен обеспечивает работу с нейронными сетями
namespace AI.Neuro
{

    /// <summary>
    /// Хранение образов. 
    /// Образом счтается массив Enters[] входов и массив Results[] выходов
    /// </summary>
    [Serializable]
    public class NeuroImage
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

        public NeuroImage(double[] enters, double[] results)
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
    [Serializable]
    public class SingleLayerPerceptron
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

        public int EnterSize
        {
            get { return enterSize; }
            private set { enterSize = value; }
        }
        int resultSize = 1;

        public int ResultSize
        {
            get { return resultSize; }
            private set { resultSize = value; }
        }

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
        public SingleLayerPerceptron(int enterSize = 2, int resultSize = 1)
        {
            this.enterSize = enterSize;
            this.resultSize = resultSize;
            Random gen = new Random();
            weight = new double[enterSize, resultSize];
            t = new double[resultSize];

            for (int i = 0; i < resultSize; i++)
            {
                T[i] = 2 * gen.NextDouble() - 1;
                for (int j = 0; j < enterSize; j++)
                {
                    weight[j, i] = 2 * gen.NextDouble() - 1;
                }
            }
        }

        public SingleLayerPerceptron()
            : this(2, 1)
        {
        }

        /// <summary>
        /// Add one image,perceptron recognized
        /// return 0 if this image contains else return 1
        /// </summary>
        /// <param name="enter">enter image</param>
        /// <param name="result">exit image</param>
        public int Add(double[] enter, double[] result)
        {
            NeuroImage img = new NeuroImage(enter, result);
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
            int i = 0;
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

                    if (!Equal(answer, Image[j].Results))
                    {
                        wasError = true;
                        error = CalculateError(answer, Image[j].Results);
                        Study(Image[j].Enters, Image[j].Results, error);
                        //          Console.WriteLine("Modify:");
                        //          Console.WriteLine("new weights: {0},{1}, new T: {2}", weight[0], weight[1], T);
                    }
                }
                i++;
            }
            while (i < epoch && wasError == true);
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

        private void Study(double[] enters, double[] results, double[] error)
        {
            for (int i = 0; i < resultSize; i++)
            {
                for (int j = 0; j < enterSize; j++)
                    weight[j, i] = weight[j, i] - step * enters[j] * error[i];
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
            if (enter.Length != enterSize)
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

        public void Save(string name)
        {

            BinaryFormatter serializ = new BinaryFormatter();
            using (System.IO.FileStream fs = new System.IO.FileStream(name, System.IO.FileMode.Create))
            {
                serializ.Serialize(fs, this);
            }
        }

        public static SingleLayerPerceptron LoadSingleLayerPerceptron(string name)
        {
            BinaryFormatter serializ = new BinaryFormatter();
            SingleLayerPerceptron obj;
            using (System.IO.FileStream fs = new System.IO.FileStream(name, System.IO.FileMode.Open))
            {
                obj = (SingleLayerPerceptron)serializ.Deserialize(fs);
            }
            return obj;
        }

    }


    class AdaptiveLinearElement
    {
        double[] w;

        bool accept;

        double[] startValue;

        /// <summary>
        /// Набор значенией, описывающих первое состояние системы,
        /// может быть использовано для корректного запуска прогнозирования
        /// </summary>
        public double[] StartValue
        {
            get { return startValue; }
            set { startValue = value; }
        }

        int epochCount;

        /// <summary>
        /// Количетво эпох, затраченных на обучение
        /// </summary>
        public int EpochCount
        {
            get { return epochCount; }
            set { epochCount = value; }
        }

        /// <summary>
        /// успешно ли обучилась сеть
        /// </summary>
        public bool Accept
        {
            get { return accept; }
            set { accept = value; }
        }


        public double[] W
        {
            get { return w; }
        }
        double t;

        public double T
        {
            get { return t; }
        }
        float step = 0.5f;
        float error = 0.000001f;
        int epoch = 100;
        int enter;

        /// <summary>
        /// размерность входного образа(состояния)
        /// </summary>
        public int Enter
        {
            get { return enter; }
            set { enter = value; }
        }

        public AdaptiveLinearElement(double[] images, double[] controls, int enter = 5)
        {
            Random gen = new Random();
            Enter = enter;
            w = new double[Enter];
            for (int i = 0; i < Enter; i++)
            {
                w[i] = gen.NextDouble();
            }
            t = gen.NextDouble();
            Teach(images, controls);
        }

        public double CalculateNext(double[] val)
        {
            double rez = 0;
            for (int i = 0; i < val.Length; i++)
            {
                rez += val[i] * w[i];
            }
            rez -= t;
            return rez;
        }

        public double[] Calculate(double[] val, int n)
        {
            double[] rez = new double[n];
            Array.Copy(val, rez, val.Length);
            for (int i = val.Length; i < n; i++)
            {
                rez[i] = CalculateNext(val);
                Array.Copy(rez, i - val.Length + 1, val, 0, Enter);
            }
            return rez;
        }

        public double[] Calculate(int n)
        {
            double[] rez = new double[n];
            double[] val = (double[])StartValue.Clone();
            Array.Copy(val, rez, val.Length);
            for (int i = val.Length; i < n; i++)
            {
                rez[i] = CalculateNext(val);
                Array.Copy(rez, i - val.Length + 1, val, 0, Enter);
            }
            return rez;
        }

        private void ShiftLeft(ref double[] val)
        {
            for (int i = 0; i < val.Length - 1; i++)
            {
                val[i] = val[i + 1];
            }
        }

        private void Teach(double[] images, double[] controls)
        {
            //System.IO.File.Create("log1.txt");
            EpochCount = 0;
            int imageAmount = images.Length;
            int controlAmount = controls.Length;
            //изменяем размер для хранения всей выборки
            Array.Resize(ref images, images.Length + controls.Length);
            //копируем
            Array.Copy(controls, 0, images, imageAmount, controls.Length);
            double localError;
            int i = 0;
            StartValue = CreateEnter(images, 0);
            //прогоняем по обучающей выборке
            do
            {

                localError = 0;
                double[] localImages;
                //храним вычисленные выражения
                double[] y = new double[imageAmount - Enter];
                for (i = 0; i < imageAmount - Enter; i++)
                {
                    localImages = CreateEnter(images, i);
                    y[i] = CalculateNext(localImages);
                    Study(y[i], images[i + Enter], localImages);
                }

                double[] etalon = CreateEtalon(images, 5, imageAmount);
                localError = ComputeAverageError(y, etalon);
                if (localError <= error)
                {
                    y = new double[images.Length - i - Enter];
                    for (int j = 0; i < images.Length - Enter; i++, j++)
                    {
                        localImages = CreateEnter(images, i);
                        y[j] = CalculateNext(localImages);
                    }
                    etalon = CreateEtalon(images, imageAmount, y.Length);
                    localError = ComputeAverageError(y, etalon);
                    if (localError <= error)
                        break;
                }
                EpochCount++;

            }
            while (EpochCount < epoch);

            if (EpochCount == epoch)
            {
                Accept = false;
            }
            else
            {
                Accept = true;
            }
        }

        private void Study(double y, double _t, double[] localImages)
        {
            for (int i = 0; i < Enter; i++)
            {
                w[i] = w[i] - step * (y - _t) * localImages[i];
            }
            t = t + step * (y - _t);
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="images">массив</param>
        /// <param name="start">начиная с какого элемента</param>
        /// <param name="imageAmount">сколько штук</param>
        /// <param name="Enter"></param>
        /// <returns></returns>
        private double[] CreateEtalon(double[] images, int start, int imageAmount)
        {
            double[] rez = new double[imageAmount];
            for (int i = start, j = 0; j < imageAmount; i++, j++)
            {
                rez[i - start] = images[i];
            }
            return rez;
        }

        private double ComputeAverageError(double[] y, double[] t)
        {
            double val = 0;
            for (int i = 0; i < y.Length; i++)
            {
                val += Math.Pow(y[i] - t[i], 2);
            }
            return val / 2;
        }

        private double[] CreateEnter(double[] images, int id)
        {
            double[] rez = new double[Enter];
            for (int i = 0; i < Enter; i++)
            {
                rez[i] = images[id + i];
            }
            return rez;
        }

    }

    namespace MLP
    {
        ///функции ошибок
        namespace ErrorFunction
        {
            [Serializable]
            /// <summary>
            /// class implement Half Squared Euclidian error distance 
            /// inheritance by IPartialError<double>
            /// Реализует Евклидову среднеквадратичную функцию ошибки
            /// </summary>
            public class HalfEuclid : IPartialErrorFunction<double>
            {

                public double Derivative(double v, double t)
                {
                    return v - t;
                }

                public double Compute(double[] v, double[] t)
                {
                    if (v.Length != t.Length)
                        throw new FormatException("Sizes of enters pulses and etalons must be equal");
                    double error = 0;
                    for (int i = 0; i < v.Length; i++)
                    {
                        error += (v[i] - t[i]) * (v[i] - t[i]);
                    }
                    return error;
                }

                public override string ToString()
                {
                    return "Half Squared Euclidian Error Distance";
                }


            }

            /// <summary>
            /// class implement logarithm likelihood error distance 
            /// inheritance by IPartialError<double>
            /// Реализуюет функцию логарифмического правдоподобия
            /// </summary>
            public class LogLikelihood : IPartialErrorFunction<double>
            {

                public double Derivative(double v, double t)
                {
                    return -(v / t - (1 - v) / (1 - t));
                }

                public double Compute(double[] v, double[] t)
                {
                    if (v.Length != t.Length)
                        throw new FormatException("Sizes of enters pulses and etalons must be equal");
                    double d = 0;
                    for (int i = 0; i < v.Length; i++)
                    {
                        d += v[i] * Math.Log(t[i]) + (1 - v[i]) * Math.Log(1 - t[i]);
                    }
                    return -d;
                }

                public override string ToString()
                {
                    return "Logarithm Likelihood Error Distance";
                }

            }

            /// <summary>
            /// Реализует перекрестную энтропию для softmax слоя
            /// </summary>
            public class CrossEntropy : IPartialErrorFunction<double>
            {
                internal CrossEntropy()
                {
                }

                /// <summary>
                /// \sum_i v1_i * ln(v2_i)
                /// </summary>
                public double Compute(double[] v1, double[] v2)
                {
                    double d = 0;
                    for (int i = 0; i < v1.Length; i++)
                    {
                        d += v1[i] * Math.Log(v2[i]);
                    }
                    return -d;
                }

                public double Derivative(double v1, double v2)
                {
                    return v1 - v2;
                }

            }
        }

        ///функции активации
        namespace ActivateFunction
        {
            [Serializable]
            /// <summary>
            /// implement Sigmoidal funcion activation
            /// </summary>
            public class Sigmoid : IDifferentiableActivation<double>
            {
                private double _alpha;

                /// <summary>
                /// parametr of Sigmoidal function
                /// </summary>
                public double Alpha
                {
                    get { return _alpha; }
                    private set { _alpha = value; }
                }


                /// <param name="alpha">parametr of Sigmoidal function</param>
                public Sigmoid(double alpha = 1)
                {
                    Alpha = alpha;
                }

                public double Compute(double sum)
                {
                    double r = (1 / (1 + Math.Exp(-1 * _alpha * sum)));
                    return r;
                }

                public double Derivative(double v)
                {
                    double sigm = Compute(v);
                    return _alpha * sigm * (1 - sigm);
                }

                public override string ToString()
                {
                    return "Sigmoid:" + Alpha;
                }
            }

            [Serializable]
            /// <summary>
            /// implement Hypebolic tangens function activation
            /// </summary>
            public class HyperbolicTan : IDifferentiableActivation<double>
            {

                private double _alpha;

                /// <summary>
                /// parametr of Hypebolic tangens function
                /// </summary>
                public double Alpha
                {
                    get { return _alpha; }
                    private set { _alpha = value; }
                }


                /// <param name="alpha">parametr of Hypebolic tangens function</param>
                public HyperbolicTan(double alpha = 1)
                {
                    Alpha = alpha;
                }

                public double Compute(double sum)
                {
                    return (Math.Tanh(_alpha * sum));
                }

                public double Derivative(double v)
                {
                    double t = Math.Tanh(_alpha * v);
                    return _alpha * (1 - t * t);
                }

                public override string ToString()
                {
                    return "Hyperbolic tangens:" + Alpha;
                }
            }

            [Serializable]
            /// <summary>
            /// implement linear function activation
            /// </summary>
            public class Linear : IDifferentiableActivation<double>
            {

                public double Compute(double sum)
                {
                    return sum;
                }

                public double Derivative(double v)
                {
                    return 1;
                }

                public override string ToString()
                {
                    return "Linear";
                }
            }

            [Serializable]
            public class BipolarSigmoid : IDifferentiableActivation<double>
            {
                private double _alpha;

                /// <summary>
                /// parametr of Sigmoidal function
                /// </summary>
                public double Alpha
                {
                    get { return _alpha; }
                    private set { _alpha = value; }
                }


                /// <param name="alpha">parametr of Sigmoidal function</param>
                public BipolarSigmoid(double alpha = 1)
                {
                    Alpha = alpha;
                }

                public double Derivative(double v)
                {
                    double sigm = Compute(v);
                    return 2 * (1 - sigm) * sigm;
                }

                public double Compute(double sum)
                {
                    return 2 / (1 + Math.Exp(-1 * _alpha * sum)) - 1;
                }
            }

            [Serializable]
            /// <summary>
            /// Используется неявно softmax слоем
            /// </summary>
            class SoftMax : IDifferentiableActivation<double>
            {
                private ISingleLayer<double> _layer = null;
                private int _ownPosition = 0;

                internal SoftMax(ISingleLayer<double> layer, int ownPosition)
                {
                    _layer = layer;
                    _ownPosition = ownPosition;
                }
                public double Compute(double x)
                {
                    double numerator = Math.Exp(x);
                    double denominator = numerator;
                    for (int i = 0; i < _layer.Neuron.Length; i++)
                    {
                        if (i == _ownPosition)
                        {
                            continue;
                        }
                        denominator += Math.Exp(_layer.Neuron[i].LastPulse);
                    }


                    return numerator / denominator;
                }



                public double Derivative(double x)
                {
                    double y = Compute(x);
                    return y * (1 - y);
                }
            }

            [Serializable]
            class Relu : IDifferentiableActivation<double>
            {
                public double Compute(double sum)
                {
                    if (sum > 0)
                        return sum;
                    else
                        return 0.01 * sum;
                }
                public double Derivative(double sum)
                {
                    if (sum > 0)
                        return 1;
                    else
                        return 0.01;
                }
            }

            [Serializable]
            class BipolarTreshhold : IDifferentiableActivation<double>
            {
                public double Compute(double sum)
                {
                    if (sum > 0)
                        return 1;
                    else
                        return -1;
                }
                public double Derivative(double sum)
                {
                    return 0;
                }
            }
        }

        /// <summary>
        /// base interface of neural network
        /// базовый интерфейс для всех объектов нейронной сети. Является признаком принадлежности 
        /// класса к данной библиотеке
        /// </summary>
        public interface INeuroObject
        {

        }

        interface INeuroRandom : INeuroObject
        {
            double NextDouble();
        }

        public abstract class StandartRandom : INeuroRandom
        {
            protected Random gen;
            public abstract double NextDouble();

            public StandartRandom(int randomInit)
            {
                gen = new Random(randomInit);
            }

            public StandartRandom()
            {
                gen = new Random();
            }
        }

        public class NaturalNeuroRandom : StandartRandom
        {
            public NaturalNeuroRandom(int randomInit)
                : base(randomInit)
            { }

            public NaturalNeuroRandom()
                : base()
            { }
            public override double NextDouble()
            {
                return gen.NextDouble();
            }
        }

        public class NeuroRandom : StandartRandom
        {
            public NeuroRandom(int randomInit)
                : base(randomInit)
            { }

            public NeuroRandom()
                : base()
            { }
            public override double NextDouble()
            {
                return gen.NextDouble() * gen.Next() % 2 == 0 ? -1 : 1;
            }
        }

        public class HalfNeuroRandom : NeuroRandom
        {
            HalfNeuroRandom(int randomInit)
                : base(randomInit)
            { }
            public HalfNeuroRandom()
                : base()
            { }
            public override double NextDouble()
            {
                return base.NextDouble() / 2;
            }
        }

        /// <summary>
        /// base interface of function
        /// базовый интерфейс для всех функций
        /// </summary>
        public interface IFunction : INeuroObject
        {

        }

        /// <summary>
        /// this interface commit implement base error functional
        /// Базовый интерфейс для функций ошибки. Обязует реализовать метод расчета ошибки
        /// по значениям входов и эталонным значениям.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        public interface IErrorFunction<T> : IFunction
        {
            /// <summary>
            /// Calculate error between enter image and etalon image
            /// </summary>
            /// <param name="v"> enter image </param>
            /// <param name="t"> etalon image </param>
            /// <returns> value of error</returns>
            T Compute(T[] v, T[] t);
        }

        /// <summary>
        /// this interface commit implement the Error function,that calculate error of certain layer
        /// Интефейс для тех функций ошибки, производная которых вычисляется по всем нейронам
        /// Обязует реализовать функцию вычисления первой производной
        /// </summary>
        /// <typeparam name="T">type of pulse, that move to neural network (such double)</typeparam>
        public interface IFullErrorFunction<T> : IErrorFunction<T>
        {

            /// <summary>
            /// Calculate first partial derivative by index
            /// </summary>
            /// <param name="v">enters</param>
            /// <param name="t">etalons</param>
            /// <param name="index">point of the derivative</param>
            /// <returns>first derivative</returns>
            T Derivative(T[] v, T[] t, int index);


        }

        /// <summary>
        /// this interface commit implement the Error function,that calculate partial error of certain layer
        /// Интефейс для тех функций ошибки, производная которых вычисляется по текущему нейронам
        /// Обязует реализовать функцию вычисления первой производной
        /// </summary>
        /// <typeparam name="T">type of pulse, that move to neural network (such double)</typeparam>
        public interface IPartialErrorFunction<T> : IErrorFunction<T>
        {
            /// <summary>
            /// Calculate first partial derivative
            /// </summary>
            /// <param name="v">enter</param>
            /// <param name="t">etalon</param>
            /// <returns>first partial derivative</returns>
            T Derivative(T v, T t);


        }



        /// <summary>
        /// this interface commit implement the activation function,that calculate output pulse certain neuron
        /// Интерфейс функций активации. Обязует реализовать метод расчета выходного значения по входному.
        /// </summary>
        /// <typeparam name="T">type of pulse, that move to neural network (such double)</typeparam>
        public interface IActivation<T> : IFunction
        {
            /// <summary>
            /// calculates an output pulse, that corresponds to the input pulse
            /// </summary>
            /// <param name="sum">value, that has accumulated on the neuron</param>
            /// <returns>value, which is the output pulse</returns>
            T Compute(T sum);


        }

        /// <summary>
        /// this interface commit implement the activation function,that calculate first partial derivative
        /// Интерфейс дифференцируемой функций активации. 
        /// Обязует реализовать метод взятия производной от входного сигнала.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        public interface IDifferentiableActivation<T> : IActivation<T>
        {
            /// <summary>
            /// Calculate first partial derivative by current function using pulse value
            /// </summary>
            /// <param name="v">current pulse</param>
            /// <returns>first partial derivative</returns>
            T Derivative(T v);
        }

        /// <summary>
        /// interface commit implement the one generalized neuron
        /// Интефейс обобщенного нейрона, обладающего весами, порогом,функцией активации, хранящим
        /// информацию о последнем состоянии, и обладающим методами расчета взвешенной суммы и функцией активации
        /// </summary>
        /// <typeparam name="T"></typeparam>
        public interface INeuron<T> : INeuroObject
        {
            /// <summary>
            /// Vector weights of current neuron
            /// </summary>
            T[] Weights
            {
                get;
                set;
            }

            /// <summary>
            /// Offset of current neuron
            /// </summary>
            T Offset
            {
                get;
                set;
            }

            /// <summary>
            /// Calculate pulse, that enter to current neuron
            /// </summary>
            /// <param name="inputVector">vector pulse arriving at the neuron </param>
            /// <returns>Accumulated value</returns>
            T Compute(T[] inputVector);

            /// <summary>
            /// Calculate output which is a function of input
            /// </summary>
            /// <param name="outputPulse">Accumulated value</param>
            /// <returns>Output pulse</returns>
            T Activate(T outputPulse);

            /// <summary>
            /// last output pulse
            /// </summary>
            T LastState
            {
                get;
                set;
            }

            /// <summary>
            /// last accumulated value
            /// </summary>
            T LastPulse
            {
                get;
                set;
            }

            /// <summary>
            /// the partial derivative of the function at the neuron error adder
            /// </summary>
            double CurrentError
            {
                get;
                set;
            }

            /// <summary>
            /// activate function of the current neuron
            /// </summary>
            IDifferentiableActivation<T> ActivationFunction { get; }

        }

        /// <summary>
        /// implement base neuron,that weight and ofsset is double value
        /// Реализация нейрона, взвешенная сумма которого вычисляется по формуле:
        /// сумма по i (wi*xi)  - T
        /// </summary>
        [DataContract]
        public class Neuron : INeuron<double>
        {
            [DataMember]
            double[] weight;

            public double[] Weights
            {
                get
                {
                    return weight;
                }
                set { weight = value; }
            }

            [DataMember]
            double offset;

            public double Offset
            {
                get { return offset; }
                set { offset = value; }
            }

            [DataMember]
            double lastState;

            /// <summary>
            /// result
            /// </summary>
            public double LastState
            {
                get { return lastState; }
                set { lastState = value; }
            }

            [DataMember]
            double lastPulse;

            /// <summary>
            /// sum
            /// </summary>
            public double LastPulse
            {
                get
                {
                    return lastPulse;
                }
                set
                {
                    lastState = value;
                }
            }

            [DataMember]
            double currentError;

            public double CurrentError
            {
                get { return currentError; }
                set { currentError = value; }
            }

            IDifferentiableActivation<double> function;

            [DataMember]
            public IDifferentiableActivation<double> ActivationFunction
            {
                get { return function; }
                private set { function = value; }
            }

            /// <summary>
            /// 
            /// </summary>
            /// <param name="dimension">amount of weights(dimension of prevent layer)</param>
            /// <param name="f">function activation</param>
            public Neuron(int dimension, IDifferentiableActivation<double> f)
            {
                weight = new double[dimension];
                function = f;
            }

            public Neuron(Neuron neuron)
            {
                this.function = neuron.ActivationFunction;
                this.currentError = neuron.CurrentError;
                this.lastPulse = neuron.LastPulse;
                this.lastState = neuron.LastState;
                this.offset = neuron.Offset;
                this.weight = neuron.Weights;
            }

            /// <summary>
            /// 
            /// </summary>
            /// <param name="dimension">amount o weights(dimension of prevent layer)</param>
            /// <param name="f">function activation</param>
            /// <param name="gen">random generator, that help of initialize neuron</param>
            public Neuron(int dimension, IDifferentiableActivation<double> f, Random gen)
                : this(dimension, f)
            {
                gen = new Random();
                for (int i = 0; i < dimension; i++)
                {
                    weight[i] = gen.NextDouble();
                }
                offset = gen.NextDouble();
            }

            public double Compute(double[] inputVector)
            {
                if (weight.Length != inputVector.Length)
                    throw new FormatException("Sizes of enters pulses and weights must be equal");
                double p = 0;
                for (int i = 0; i < inputVector.Length; i++)
                {
                    p += weight[i] * inputVector[i];
                }
                lastPulse = p - Offset;
                lastState = Activate(lastPulse);
                return lastState;
            }

            public double Activate(double outputPulse)
            {
                lastState = function.Compute(outputPulse);
                return LastState;
            }

            public override string ToString()
            {
                return "Neuron:" + weight.Length;
            }
        }

        /// <summary>
        /// It is the basic entity for the implementation layer
        /// Интерфейс обобщенного слоя нейронов.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        public interface ILayer<T> : INeuroObject
        { }

        /// <summary>
        /// interface commit implement functional of the one layer of neurons
        /// Интерфейс обобщенного простого слоя. Содержит информацию о последнем состоянии,размерности,
        /// нейронах, которые в него входят и обязует реализовать метод расчета выходного сигнала от
        /// входного
        /// </summary>
        /// <typeparam name="T"></typeparam>
        public interface ISingleLayer<T> : ILayer<T>
        {
            /// <summary>
            /// last accumulated value
            /// </summary>
            T[] LastPulse
            {
                get;
            }

            /// <summary>
            /// Amount of neurons this layer
            /// </summary>
            int Dimension
            {
                get;
            }

            /// <summary>
            /// array of neurons this layer
            /// </summary>
            INeuron<T>[] Neuron
            {
                get;
            }

            /// <summary>
            /// calculate pulse, that accumulate this layer
            /// </summary>
            /// <param name="inputVector">input pulse</param>
            /// <returns>accumulate pulse</returns>
            T[] Compute(T[] inputVector);

        }

        /// <summary>
        /// implement base layer,that weight and ofsset is double value
        /// Реализация Простого слоя, состоящего из нейронов реализующих интефейс INeuron<double>
        /// </summary>
        [DataContract]
        public class SingleLayer : ISingleLayer<double>
        {
            [DataMember]
            double[] lastPulse;

            public double[] LastPulse
            {
                get
                {
                    return lastPulse;
                }
            }

            [DataMember]
            int dimension;

            public int Dimension
            {
                get
                {
                    return dimension;
                }
            }

            [DataMember]
            Neuron[] neuron;

            public INeuron<double>[] Neuron
            {
                get
                {
                    return neuron;
                }
            }

            /// <summary>
            /// 
            /// </summary>
            /// <param name="inputDimension">amount of neuron</param>
            /// <param name="outputDimension">amount of weights(dimension of prevent layer)</param>
            /// <param name="f">function activation</param>
            public SingleLayer(int inputDimension, int outputDimension, IDifferentiableActivation<double> f)
            {
                this.dimension = outputDimension;
                neuron = new Neuron[dimension];
                lastPulse = new double[dimension];
                for (int i = 0; i < dimension; i++)
                {
                    neuron[i] = new Neuron(inputDimension, f);
                }
            }

            public SingleLayer(SingleLayer layer)
            {
                this.dimension = layer.dimension;
                this.lastPulse = layer.lastPulse;
                this.neuron = layer.neuron;
            }

            /// <summary>
            /// 
            /// </summary>
            /// <param name="dimension">amount of neuron</param>
            /// <param name="prevDimension">amount of weights(dimension of prevent layer)</param>
            /// <param name="f">function activation</param>
            /// <param name="gen">random generator, that help of initialize neuron</param>
            public SingleLayer(int inputDimension, int outputDimension, IDifferentiableActivation<double> f, Random gen)
            {
                this.dimension = outputDimension;
                neuron = new Neuron[dimension];
                lastPulse = new double[dimension];
                for (int i = 0; i < dimension; i++)
                {
                    neuron[i] = new Neuron(inputDimension, f, gen);
                }
            }

            /// <summary>
            /// 
            /// </summary>
            /// <param name="neuron">array of neuron this layer</param>
            public SingleLayer(Neuron[] neuron)
            {
                this.dimension = neuron.Length;
                this.neuron = neuron;
                lastPulse = new double[neuron.Length];
            }

            public double[] Compute(double[] inputVector)
            {
                double[] output = new double[Dimension];
                for (int i = 0; i < Dimension; i++)
                {
                    neuron[i].Compute(inputVector);
                    output[i] = neuron[i].LastState;
                }
                lastPulse = output;
                return LastPulse;
            }

            public override string ToString()
            {
                return "Layer:" + dimension;
            }
        }

        /// <summary>
        /// SoftMax слой, который использует(неявно) SoftMax функцию
        /// </summary>
        [DataContract]
        public class SoftMaxLayer : ISingleLayer<double>
        {

            [DataMember]
            double[] lastPulse;

            public double[] LastPulse
            {
                get
                {
                    return lastPulse;
                }
            }

            [DataMember]
            int dimension;

            public int Dimension
            {
                get
                {
                    return dimension;
                }
            }

            [DataMember]
            INeuron<double>[] neuron;

            public INeuron<double>[] Neuron
            {
                get
                {
                    return neuron;
                }
            }

            /// <summary>
            /// 
            /// </summary>
            /// <param name="dimension">amount of neuron</param>
            /// <param name="prevDimension">amount of weights(dimension of prevent layer)</param>
            /// <param name="f">function activation</param>
            public SoftMaxLayer(int inputDimension, int outputDimension)
            {
                this.dimension = outputDimension;
                neuron = new INeuron<double>[dimension];
                lastPulse = new double[dimension];
                for (int i = 0; i < dimension; i++)
                {
                    neuron[i] = new Neuron(inputDimension, new ActivateFunction.SoftMax(this, i));
                }
            }

            public SoftMaxLayer(SoftMaxLayer layer)
            {
                this.dimension = layer.dimension;
                this.lastPulse = layer.lastPulse;
                this.neuron = layer.neuron;
            }

            /// <summary>
            /// 
            /// </summary>
            /// <param name="dimension">amount of neuron</param>
            /// <param name="prevDimension">amount of weights(dimension of prevent layer)</param>
            /// <param name="f">function activation</param>
            /// <param name="gen">random generator, that help of initialize neuron</param>
            public SoftMaxLayer(int inputDimension, int outputDimension, Random gen)
            {
                this.dimension = outputDimension;
                neuron = new INeuron<double>[dimension];
                lastPulse = new double[dimension];
                for (int i = 0; i < dimension; i++)
                {
                    neuron[i] = new Neuron(inputDimension, new ActivateFunction.SoftMax(this, i), gen);
                }
            }



            public double[] Compute(double[] inputVector)
            {
                double[] numerators = new double[neuron.Length];
                double denominator = 0;
                for (int i = 0; i < neuron.Length; i++)
                {
                    numerators[i] = Math.Exp(neuron[i].Compute(inputVector));
                    denominator += numerators[i];
                }
                double[] output = new double[neuron.Length];
                for (int i = 0; i < neuron.Length; i++)
                {
                    output[i] = numerators[i] / denominator;
                    neuron[i].LastState = output[i];
                }
                lastPulse = output;
                return LastPulse;
            }

            public override string ToString()
            {
                return "Layer:" + dimension;
            }
        }




        /// <summary>
        /// interface commit implement functional of the set of layer of neurons
        /// Интерфейс множества обобщенных слоев
        /// </summary>
        /// <typeparam name="T"></typeparam>
        public interface IMultiLayer<T> : ILayer<T>, IEnumerable
        {
            ISingleLayer<T>[] Layer
            {
                get;
            }

            int Length
            {
                get;
            }
            ISingleLayer<double> this[int index]
            {
                get;
            }


        }


        /// implement base set of layer,that weight and ofsset is double value
        /// Реализация интефейса IMultiLayer
        [DataContract]
        [KnownType(typeof(SoftMaxLayer))]
        public class MultiLayer : IMultiLayer<double>
        {
            [DataMember]
            ISingleLayer<double>[] layer;

            public ISingleLayer<double>[] Layer
            {
                get { return layer; }
            }

            public int Length
            {
                get { return layer.Length; }
            }

            public ISingleLayer<double> this[int index]
            {
                get
                {
                    return layer[index];
                }
            }


            public MultiLayer(ISingleLayer<double>[] layer)
            {
                this.layer = layer;
            }

            public MultiLayer(MultiLayer mlayer)
            {
                this.layer = mlayer.Layer;
            }


            public override string ToString()
            {
                return "MultiLayer:" + layer.Length;
            }




            public IEnumerator GetEnumerator()
            {
                return Layer.GetEnumerator();
            }
        }

        /// <summary>
        ///interface commit implement functional of the neural network
        /// </summary>
        /// <typeparam name="T"></typeparam>
        public interface INeuralNetwork<T> : INeuroObject
        {
            /// <summary>
            /// layers of the neyral network
            /// </summary>
            IMultiLayer<T> Layer
            {
                get;
            }

            /// <summary>
            /// 
            /// </summary>
            /// <param name="input">enter pulse</param>
            /// <returns>result pulse</returns>
            T[] Compute(T[] input);

            /// <summary>
            /// keeps network to stream
            /// </summary>
            /// <returns></returns>
            void Save(string filename);

            /// <summary>
            /// Train this network
            /// </summary>
            /// <param name="study">training set</param>
            /// <param name="check">test set</param>
            void Train(IList<NeuroImage<T>> study, IList<NeuroImage<T>> check);

            void DefaultInitialise();
        }

        /// <summary>
        /// interface commit implement algorithm of the learning network
        /// </summary>
        /// <typeparam name="T"></typeparam>
        public interface ILearn<T> : INeuroObject
        {
            /// <summary>
            /// 
            /// </summary>
            /// <param name="network">network, which is train</param>
            /// <param name="data">array of training set</param>
            /// <param name="check">array of testing set</param>
            void Train(INeuralNetwork<T> network, IList<NeuroImage<T>> data, IList<NeuroImage<T>> check);
        }

        [DataContract]
        public class DifferintiableLearningConfig : ILearningConfig
        {
            public override string ToString()
            {
                return "Step:" + Step + " MinError:" + MinError + " CurrentError:" + CurrentError;
            }
            [DataMember]
            public double Step
            {
                get;
                set;
            }
            [DataMember]
            /// <summary>
            /// Size of the butch. -1 means fullbutch size. 
            /// </summary>
            public int Batch
            {
                get;
                set;
            }
            [DataMember]
            public double Regularization
            {
                get;
                set;
            }
            [DataMember]
            public int MaxEpoch
            {
                get;
                set;
            }
            [DataMember]
            public int CurrentEpoch
            {
                get;
                protected set;
            }
            [DataMember]
            public bool Succes
            {
                get;
                protected set;
            }
            [DataMember]
            /// <summary>
            /// If cumulative error for all training examples is less then MinError, then algorithm stops 
            /// </summary>
            public double MinError
            {
                get;
                set;
            }
            [DataMember]
            /// <summary>
            /// If cumulative error change for all training examples is less then MinErrorChange, then algorithm stops 
            /// </summary>
            public double MinChangeError
            {
                get;
                set;
            }
            [DataMember]
            public double CurrentError
            {
                get;
                set;
            }

            [DataMember]
            public double RecogniseError { get; protected set; }

            [DataMember]
            /// <summary>
            /// Function to minimize
            /// </summary>
            public IPartialErrorFunction<double> ErrorFunction { get; set; }

            [DataMember]

            public double TeachPercent { get; protected set; }

            public double RecognisePercent { get; protected set; }

            public bool UseRandomShuffle { get; set; }

            public DifferintiableLearningConfig(IPartialErrorFunction<double> f)
            {
                Batch = -1;
                Step = 0.1;
                Regularization = 0;
                MaxEpoch = 100;
                CurrentEpoch = 0;
                Succes = false;
                MinError = 0.001;
                MinChangeError = 0.001;
                CurrentError = Double.MaxValue;
                ErrorFunction = f;
                OneImageMinError = 0;
                UseRandomShuffle = true;
                RecogniseError = 0;
                RecognisePercent = 0;
            }

            public DifferintiableLearningConfig(DifferintiableLearningConfig config)
            {
                Batch = config.Batch;
                Step = config.Step;
                Regularization = config.Regularization;
                MaxEpoch = config.MaxEpoch;
                CurrentEpoch = config.CurrentEpoch;
                Succes = config.Succes;
                MinError = config.MinError;
                MinChangeError = config.MinChangeError;
                CurrentError = config.CurrentError;
                ErrorFunction = config.ErrorFunction;
                OneImageMinError = config.OneImageMinError;
                UseRandomShuffle = config.UseRandomShuffle;
                RecogniseError = config.RecogniseError;
                RecognisePercent = config.RecognisePercent;
            }

            public double OneImageMinError
            {
                get;
                set;
            }


        }

        [DataContract]
        public class LearningConfig : ILearningConfig
        {
            public override string ToString()
            {
                return "Step:" + Step + " MinError:" + MinError + " CurrentError:" + CurrentError;
            }
            [DataMember]
            public double Step
            {
                get;
                set;
            }
            [DataMember]
            /// <summary>
            /// Size of the butch. -1 means fullbutch size. 
            /// </summary>
            public int Batch
            {
                get;
                set;
            }
            [DataMember]
            public double Regularization
            {
                get;
                set;
            }
            [DataMember]
            public int MaxEpoch
            {
                get;
                set;
            }
            [DataMember]
            public int CurrentEpoch
            {
                get;
                protected set;
            }
            [DataMember]
            public bool Succes
            {
                get;
                set;
            }
            [DataMember]
            /// <summary>
            /// If cumulative error for all training examples is less then MinError, then algorithm stops 
            /// </summary>
            public double MinError
            {
                get;
                set;
            }
            [DataMember]
            /// <summary>
            /// If cumulative error change for all training examples is less then MinErrorChange, then algorithm stops 
            /// </summary>
            public double MinChangeError
            {
                get;
                set;
            }
            [DataMember]
            public double CurrentError
            {
                get;
                set;
            }
            [DataMember]
            /// <summary>
            /// Function to minimize
            /// </summary>
            public IErrorFunction<double> ErrorFunction { get; set; }



            public LearningConfig(IErrorFunction<double> f)
            {
                Batch = -1;
                Step = 0.1;
                Regularization = 0;
                MaxEpoch = 100;
                CurrentEpoch = 0;
                Succes = false;
                MinError = 0.001;
                MinChangeError = 0.001;
                CurrentError = Double.MaxValue;
                ErrorFunction = f;
            }

            public LearningConfig(LearningConfig config)
            {
                Batch = config.Batch;
                Step = config.Step;
                Regularization = config.Regularization;
                MaxEpoch = config.MaxEpoch;
                CurrentEpoch = config.CurrentEpoch;
                Succes = config.Succes;
                MinError = config.MinError;
                MinChangeError = config.MinChangeError;
                CurrentError = config.CurrentError;
                ErrorFunction = config.ErrorFunction;
            }


            public double OneImageMinError
            {
                get;
                set;
            }
        }


        public interface ILearningConfig : INeuroObject
        {

            [DataMember]
            double OneImageMinError
            {
                get;
                set;
            }

            [DataMember]
            double Step
            {
                get;
                set;
            }
            [DataMember]
            /// <summary>
            /// Size of the butch. -1 means fullbutch size. 
            /// </summary>
            int Batch
            {
                get;
                set;
            }
            [DataMember]
            double Regularization
            {
                get;
                set;
            }
            [DataMember]
            int MaxEpoch
            {
                get;
                set;
            }

            [DataMember]
            /// <summary>
            /// If cumulative error for all training examples is less then MinError, then algorithm stops 
            /// </summary>
            double MinError
            {
                get;
                set;
            }
            [DataMember]
            /// <summary>
            /// If cumulative error change for all training examples is less then MinErrorChange, then algorithm stops 
            /// </summary>
            double MinChangeError
            {
                get;
                set;
            }
            [DataMember]
            double CurrentError
            {
                get;
                set;
            }

        }


        [DataContract]
        public class BackPropogation : DifferintiableLearningConfig, ILearn<double>
        {
            public BackPropogation(DifferintiableLearningConfig config)
                : base(config)
            {

            }

            public void Train(INeuralNetwork<double> network, IList<NeuroImage<double>> data, IList<NeuroImage<double>> check)
            {
                MultiLayer net = network.Layer as MultiLayer;

                if (Batch < 1 || Batch > data.Count)
                {
                    Batch = data.Count;
                }
                double currentError = Single.MaxValue;
                double lastError = 0;
                int epochNumber = 0;
                do
                {
                    lastError = currentError;
                    DateTime dtStart = DateTime.Now;

                    #region one epoche

                    //preparation for epoche
                    int[] trainingIndices = new int[data.Count];
                    for (int i = 0; i < data.Count; i++)
                    {
                        trainingIndices[i] = i;
                    }
                    if (Batch > 0)
                    {
                        Shuffle(ref trainingIndices);
                    }





                    //process data set
                    int currentIndex = 0;
                    do
                    {


                        //process one batch
                        for (int curr = currentIndex; curr < currentIndex + Batch && curr < data.Count; curr++)
                        {
                            //forward pass
                            network.Compute(data[curr].Input);

                            int last = net.Layer.Length - 1;

                            //ошибка выходного слоя по каждому нейрону
                            for (int j = 0; j < net.Layer[last].Neuron.Length; j++)
                            {
                                net.Layer[last].Neuron[j].CurrentError = 0;
                                net.Layer[last].Neuron[j].CurrentError = ErrorFunction.Derivative(net.Layer[last].Neuron[j].LastState, data[curr].Output[j]);
                            }

                            //ошибка скрытых слоев по каждому слою кроме выходного
                            for (int k = net.Length - 2; k >= 0; k--)
                            {
                                //для каждого нейрона
                                for (int j = 0; j < net[k].Neuron.Length; j++)
                                {
                                    net.Layer[k].Neuron[j].CurrentError = 0;
                                    //сумма по всем нейронам следующего слоя
                                    for (int i = 0; i < net[k + 1].Neuron.Length; i++)
                                    {
                                        //errorj = sum_for_i(errori * F'(Sj) * wij)
                                        net.Layer[k].Neuron[j].CurrentError += net.Layer[k + 1].Neuron[i].CurrentError
                                            * net.Layer[k + 1].Neuron[i].Weights[j] *
                                            net.Layer[k + 1].Neuron[i].ActivationFunction.Derivative(net.Layer[k + 1].Neuron[i].LastPulse);
                                        if (j == 0)
                                        {
                                            net.Layer[k].Neuron[j].CurrentError *= data[curr].Input[k];
                                        }
                                    }

                                }
                            }

                        }

                        //update weights and bias

                        for (int i = net.Layer.Length - 1; i >= 0; i--)//по всем слоям в обратном порядке
                        {
                            for (int j = 0; j < net[i].Neuron.Length; j++)//для каждого нейрона
                            {
                                net[i].Neuron[j].Offset += Step * net[i].Neuron[j].CurrentError
                                    * net[i].Neuron[j].ActivationFunction.Derivative(net[i].Neuron[j].LastPulse);
                                for (int k = 0; k < net[i].Neuron[j].Weights.Length; k++)//для каждого веса
                                {

                                    if (i == 0)
                                    {
                                        net[i].Neuron[j].Weights[k] -= Step * net[i].Neuron[j].CurrentError
                                    * net[i].Neuron[j].ActivationFunction.Derivative(net[i].Neuron[j].LastPulse);

                                    }
                                    else
                                    {
                                        net[i].Neuron[j].Weights[k] -= Step * net[i].Neuron[j].CurrentError
                                    * net[i].Neuron[j].ActivationFunction.Derivative(net[i].Neuron[j].LastPulse)
                                    * net[i - 1].Neuron[k].LastState;
                                    }
                                }
                            }
                        }

                        currentIndex += Batch;
                    } while (currentIndex < data.Count);

                    //recalculating error on all data
                    currentError = 0;
                    for (int i = 0; i < data.Count; i++)
                    {
                        double[] realOutput = network.Compute(data[i].Input);
                        currentError += ErrorFunction.Compute(data[i].Output, realOutput);
                    }
                    currentError *= 1d / data.Count;

                    #endregion

                    epochNumber++;

                } while (epochNumber < MaxEpoch &&
                         currentError > MinError &&
                         Math.Abs(currentError - lastError) > MinChangeError);


                if (CurrentEpoch < MaxEpoch && CurrentError < MinError)
                    Succes = true;
            }
            private void Shuffle(ref int[] ar)
            {
                Random gen = new Random();
                int n = ar.Length;
                for (int i = 0; i < n; i++)
                {
                    // Swap(ref ar, gen.Next(n), gen.Next(n));
                }
            }

            private void Swap(ref int[] ar, int a, int b)
            {
                int t = ar[a];
                ar[a] = ar[b];
                ar[b] = t;
            }
        }

        [DataContract]
        public class SimpleBackPropogation : DifferintiableLearningConfig, ILearn<double>
        {
            public SimpleBackPropogation(DifferintiableLearningConfig config)
                : base(config)
            {

            }

            /// <summary>
            /// Обучения для отображения входных сигралов в выходные
            /// </summary>
            /// <param name="network"></param>
            /// <param name="data"></param>
            /// <param name="check"></param>
            public void Train(INeuralNetwork<double> network, IList<NeuroImage<double>> data, IList<NeuroImage<double>> check)
            {
                //получаем удобную ссылку на сеть
                MultiLayer net = network.Layer as MultiLayer;
                this.CurrentEpoch = 0;
                this.Succes = false;
                double lastError = 0;
                double[][] y = new double[data.Count][];

                do
                {
                    lastError = CurrentError;
                    //Перемешивание образов из выборки для данной эпохи
                    if (UseRandomShuffle == true)
                    {
                        RandomShuffle(ref data);
                    }
                    for (int curr = 0; curr < data.Count; curr++)
                    {



                        //считаем текущие выходы и импульсы  на каждом нейроне
                        y[curr] = network.Compute(data[curr].Input);

                        //делаем оценку для текущего образа, и если погрешность допустима,идем
                        //на следующий образ. Данная модификация ускоряет процесс обучения и
                        //уменьшает среднеквадратичную ошибку
                        if (Compare(data[curr].Output, network.Layer[net.Layer.Length - 1].LastPulse) == true)
                        {
                            continue;
                        }



                        //считаем ошибку выходного слоя
                        OutputLayerError(data, net, curr);

                        //считаем ошибки скрытых слоев
                        HiddenlayerError(net);

                        //модифицируем веса и пороги
                        ModifyParametrs(data, net, curr);



                    }

                    //расчитываем ошибку
                    ComputeError(data, network,y);

                    //производим перерасчет на основании регуляризации
                    ComputeRegularizationError(data, net);


                    System.Console.WriteLine("Eposh #" + CurrentEpoch.ToString() +
                   " finished; current error is " + CurrentError.ToString()
                   );
                    CurrentEpoch++;
                }
                while (CurrentEpoch < MaxEpoch && CurrentError > MinError &&
                    Math.Abs(CurrentError - lastError) > MinChangeError);

                ComputeRecognizeError(check, network);


            }



            private void ComputeRecognizeError(IList<NeuroImage<double>> check, INeuralNetwork<double> network)
            {
                int accept = 0;
                double LastError = 0;
                RecogniseError = 0;
                for (int i = 0; i < check.Count; i++)
                {
                    double[] realOutput = network.Compute(check[i].Input);
                    double[] result = new double[realOutput.Length];
                    LastError = ErrorFunction.Compute(check[i].Output, realOutput);
                    RecogniseError += LastError;
                    double max = realOutput.Max();
                    int index = realOutput.ToList().IndexOf(max);
                    result[index] = 1;
                    if (ArrayCompare(result, check[i].Output) == true)
                    {
                        accept++;
                    }
                }
                RecognisePercent = (double)accept / (double)check.Count;
                RecogniseError /= 2;
            }

            public static bool ArrayCompare(double[] a, double[] b)
            {
                if (a.Length == b.Length)
                {
                    for (int i = 0; i < a.Length; i++)
                    {
                        if (a[i] != b[i]) { return false; };
                    }
                    return true;
                }
                return false;
            }

            /// <summary>
            /// Учет регуляризации для расчета среднеквадратичной ошибки
            /// </summary>
            /// <param name="data">обучающая выборка</param>
            /// <param name="net">сеть</param>
            private void ComputeRegularizationError(IList<NeuroImage<double>> data, MultiLayer net)
            {
                if (Math.Abs(Regularization - 0d) > Double.Epsilon)
                {
                    double reg = 0;
                    for (int layerIndex = 0; layerIndex < net.Layer.Length; layerIndex++)
                    {
                        for (int neuronIndex = 0; neuronIndex < net.Layer[layerIndex].Neuron.Length; neuronIndex++)
                        {
                            for (int weightIndex = 0; weightIndex < net.Layer[layerIndex].Neuron[neuronIndex].Weights.Length; weightIndex++)
                            {
                                reg += net.Layer[layerIndex].Neuron[neuronIndex].Weights[weightIndex] *
                                        net.Layer[layerIndex].Neuron[neuronIndex].Weights[weightIndex];
                            }
                        }
                    }
                    CurrentError += Regularization * reg / (2 * data.Count);
                }
            }

            /// <summary>
            /// Расчет ошибки сети
            /// </summary>
            /// <param name="data">обучающая выборка</param>
            /// <param name="network">сеть</param>
            private void ComputeError(IList<NeuroImage<double>> data, INeuralNetwork<double> network, double[][] realOutput)
            {
                int accept = 0;
                double LastError = 0;
                CurrentError = 0;
                for (int i = 0; i < data.Count; i++)
                {
                    // double[] realOutput = network.Compute(data[i].Input);
                    //  double[] result = new double[realOutput.Length];
                    LastError = ErrorFunction.Compute(data[i].Output, realOutput[i]);
                    CurrentError += LastError;
                    /*    double max = realOutput.Max();
                        int index = realOutput.ToList().IndexOf(max);
                        result[index] = 1;
                        if (ArrayCompare(result, data[i].Output) == true)
                        {
                            accept++;
                        }   */
                }
                TeachPercent = (double)accept / (double)data.Count;
                CurrentError /= 2;
            }


            /// <summary>
            /// Модификация настраиваемых параметров сети
            /// </summary>
            /// <param name="data">обучающая выборка</param>
            /// <param name="net"><сеть/param>
            /// <param name="curr">номер текущего образа</param>
            private void ModifyParametrs(IList<NeuroImage<double>> data, MultiLayer net, int curr)
            {
                for (int i = net.Layer.Length - 1; i >= 0; i--)//по всем слоям в обратном порядке
                {
                    for (int j = 0; j < net[i].Neuron.Length; j++)//для каждого нейрона
                    {
                        double temp = Step * net[i].Neuron[j].CurrentError
                            * net[i].Neuron[j].ActivationFunction.Derivative(net[i].Neuron[j].LastPulse);
                        net[i].Neuron[j].Offset += temp;
                        for (int k = 0; k < net[i].Neuron[j].Weights.Length; k++)//для каждого веса
                        {

                            if (i == 0)
                            {
                                net[i].Neuron[j].Weights[k] -= temp
                            * data[curr].Input[k] + Regularization * net[i].Neuron[j].Weights[k] / data.Count;
                            }
                            else
                            {
                                net[i].Neuron[j].Weights[k] -= temp
                            * net[i - 1].Neuron[k].LastState + Regularization * net[i].Neuron[j].Weights[k] / data.Count;
                            }
                        }
                    }
                }
            }

            /// <summary>
            /// Расчет ошибки скрытых слоев
            /// </summary>
            /// <param name="net">сеть</param>
            private static void HiddenlayerError(MultiLayer net)
            {
                for (int k = net.Length - 2; k >= 0; k--)
                {
                    //для каждого нейрона
                    for (int j = 0; j < net[k].Neuron.Length; j++)
                    {
                        net.Layer[k].Neuron[j].CurrentError = 0;
                        //сумма по всем нейронам следующего слоя
                        for (int i = 0; i < net[k + 1].Neuron.Length; i++)
                        {
                            //errorj = sum_for_i(errori * F'(Sj) * wij)
                            net.Layer[k].Neuron[j].CurrentError += net.Layer[k + 1].Neuron[i].CurrentError
                                * net.Layer[k + 1].Neuron[i].Weights[j] *
                                net.Layer[k + 1].Neuron[i].ActivationFunction.Derivative(net.Layer[k + 1].Neuron[i].LastPulse);
                        }

                    }
                }
            }

            /// <summary>
            /// Расчет ошибки выходного слоя
            /// </summary>
            /// <param name="data">обучающая выборка</param>
            /// <param name="net">сеть</param>
            /// <param name="curr">номер текущего образа</param>
            /// 
            private void OutputLayerError(IList<NeuroImage<double>> data, MultiLayer net, int curr)
            {
                int last = net.Layer.Length - 1;
                //ошибка выходного слоя по каждому нейрону
                for (int j = 0; j < net.Layer[last].Neuron.Length; j++)
                {
                    net.Layer[last].Neuron[j].CurrentError = 0;
                    net.Layer[last].Neuron[j].CurrentError = ErrorFunction.Derivative(net.Layer[last].Neuron[j].LastState, data[curr].Output[j]);
                }
            }

            private void RandomShuffle(ref IList<NeuroImage<double>> data)
            {
                Random gen = new Random();
                int ind1, ind2;
                for (int i = 0; i < data.Count; i++)
                {
                    ind1 = gen.Next(0, data.Count);
                    ind2 = gen.Next(0, data.Count);
                    Swap(data, ind1, ind2);
                }
            }

            private void Swap(IList<NeuroImage<double>> data, int ind1, int ind2)
            {
                NeuroImage<double> temp = data[ind1];
                data[ind1] = data[ind2];
                data[ind2] = temp;
            }

            private bool Compare(double[] p1, double[] p2)
            {
                double error = 0.0;
                for (int i = 0; i < p1.Length; i++)
                {
                    error += (p1[i] - p2[i]) * (p1[i] - p2[i]);
                }
                error /= 2;
                if (error < OneImageMinError)
                    return true;
                else
                    return false;
            }


        }


        [DataContract]
        [KnownType(typeof(BackPropogation))]
        [KnownType(typeof(ErrorFunction.CrossEntropy))]
        [KnownType(typeof(ErrorFunction.HalfEuclid))]
        [KnownType(typeof(ErrorFunction.LogLikelihood))]
        public class MultiLayerNeuralNetwork : INeuralNetwork<double>
        {
            [DataMember]
            MultiLayer layer;

            public IMultiLayer<double> Layer
            {
                get { return layer; }
            }

            [DataMember]
            ILearn<double> learn;

            public ILearn<double> Learn
            {
                get { return learn; }
            }

            public MultiLayerNeuralNetwork(MultiLayer layer, ILearn<double> learn)
            {
                this.layer = layer;
                this.learn = learn;
            }


            public double[] Compute(double[] input)
            {
                for (int i = 0; i < layer.Layer.Length; i++)
                {
                    layer.Layer[i].Compute(input);
                    input = layer.Layer[i].LastPulse;
                }
                return input;
            }

            public void Save(string filename)
            {
                DataContractJsonSerializer jsonFormatter = new DataContractJsonSerializer(typeof(MultiLayerNeuralNetwork), new List<Type>
                    {
                        typeof(Neuro.MLP.DifferintiableLearningConfig),
                        typeof(Neuro.MLP.LearningConfig),
                        typeof(Neuro.MLP.MultiLayer),
                        typeof(Neuro.MLP.Neuron),
                        typeof(Neuro.MLP.SimpleBackPropogation),
                        typeof(Neuro.MLP.SingleLayer),
                        typeof(Neuro.MLP.ActivateFunction.Sigmoid),
                        typeof(Neuro.MLP.ActivateFunction.Relu)
                    });
                using (System.IO.FileStream fs = new System.IO.FileStream(filename, System.IO.FileMode.Create))
                {
                    jsonFormatter.WriteObject(fs, this);
                }
            }

            static public MultiLayerNeuralNetwork Load(string path)
            {
                DataContractJsonSerializer jsonFormatter = new DataContractJsonSerializer(typeof(MultiLayerNeuralNetwork), new List<Type>
                    {
                        typeof(Neuro.MLP.DifferintiableLearningConfig),
                        typeof(Neuro.MLP.LearningConfig),
                        typeof(Neuro.MLP.MultiLayer),
                        typeof(Neuro.MLP.Neuron),
                        typeof(Neuro.MLP.SimpleBackPropogation),
                        typeof(Neuro.MLP.SingleLayer),
                        typeof(Neuro.MLP.ActivateFunction.Sigmoid),
                        typeof(Neuro.MLP.ActivateFunction.Relu)
                    });
                MultiLayerNeuralNetwork mlp;
                using (System.IO.FileStream fs = new System.IO.FileStream(path, System.IO.FileMode.OpenOrCreate))
                {
                    mlp = (MultiLayerNeuralNetwork)jsonFormatter.ReadObject(fs);
                }
                return mlp;
            }

            public void Train(IList<NeuroImage<double>> study, IList<NeuroImage<double>> check)
            {
                Learn.Train(this, study, check);
            }

            public void DefaultInitialise()
            {
                int hiddenElement = AmountOfHiddenNeuron();
                double beta = 0.7 * Math.Pow(hiddenElement, 1.0 / (layer[0].Neuron[0].Weights.Length));
                HalfNeuroRandom gen = new HalfNeuroRandom();
                Random gen2 = new Random();
                for (int i = 0; i < layer.Length; i++)
                {
                    for (int j = 0; j < layer[i].Neuron.Length; j++)
                    {
                        double temp = 0;
                        for (int k = 0; k < layer[i].Neuron[j].Weights.Length; k++)
                        {
                            layer[i].Neuron[j].Weights[k] = gen.NextDouble();
                            temp += layer[i].Neuron[j].Weights[k] * layer[i].Neuron[j].Weights[k];
                        }
                        for (int k = 0; k < layer[i].Neuron[j].Weights.Length; k++)
                        {
                            layer[i].Neuron[j].Weights[k] = (beta * layer[i].Neuron[j].Weights[k]) / temp;
                        }
                        layer[i].Neuron[j].Offset = -beta + gen2.NextDouble() * (2 * beta);
                    }
                }

            }

            private int AmountOfHiddenNeuron()
            {
                int amount = 0;
                for (int i = 0; i < layer.Length; i++)
                {
                    amount += layer[i].Neuron.Length;
                }
                return amount;
            }

            public override string ToString()
            {
                return "NeyralNetwork:" + layer.Layer.Length;
            }
        }

        [DataContract]
        /// <summary>
        /// set of image that arrives to neural network
        /// </summary>
        /// <typeparam name="T"></typeparam>
        public class NeuroImage<T>
        {
            [DataMember]
            /// <summary>
            /// Входные образы
            /// </summary>
            public double[] Input
            {
                get;
                set;
            }

            [DataMember]
            /// <summary>
            /// Выходные образы
            /// </summary>
            public double[] Output
            {
                get;
                set;
            }

            public NeuroImage(double[] enters, double[] results)
            {
                Input = enters;
                Output = results;
            }


            public override string ToString()
            {
                return "NeuroImage:" + Input.Length + "|" + Output.Length;
            }
        }

    }
}
