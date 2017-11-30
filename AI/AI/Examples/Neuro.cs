/*using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AI.Neuro.MLP;
using Excel = Microsoft.Office.Interop.Excel;

namespace MLP_Multilayer
{
    class Program
    {
        const string path = @"E:\Кабинет\Курс 3\МРЗВиС\Лабы\MLP\MLP\data.xlsx";
        const string startFolder = @"E:\Кабинет\Курс 3\Курсач\Обучающая выборка версия 2";
        static List<NeuroImage<double>> learningSet, testingSet;
        static List<NeuroImage<double>> learningSeries, testingSeries;
        static int teachAmount = 30;
        static int recogniseAmount = 15;
        static int inputDimension = 10;
        static int hiddenDimension = 4;
        static void Main(string[] args)
        {
         //   CreateLearningSerialSet(path, out learningSeries, out testingSeries);
       //     TimeSeries();
            CreateLearningSet(startFolder, out learningSet, out testingSet);
          //  test3();
        //    test1();
          test2();


           
        }

        private static void CreateLearningSerialSet(string path, out List<NeuroImage<double>> learningSeries, out List<NeuroImage<double>> testingSeries)
        {


            Excel.Application excDoc = new Excel.Application();
            Excel.Workbook excBook = excDoc.Workbooks.Open(path);
            Excel.Worksheet excSheet = excBook.Worksheets.get_Item(1);
            Excel.Range excRange = excSheet.get_Range("B1", "B"+ teachAmount + recogniseAmount + 1);

            string oneImageData;
           
            double[] data = new double[teachAmount + recogniseAmount];
            learningSeries = new List<NeuroImage<double>>();
            testingSeries = new List<NeuroImage<double>>();
            for (int i = 1; i < teachAmount + recogniseAmount; i++)
            {
                oneImageData = Convert.ToString(excRange.Cells[i].Value2);
                data[i-1] = double.Parse(oneImageData);
            }
            
            for(int i=0;i<teachAmount - inputDimension;i++)
            {
                NeuroImage<double> tempImage = new NeuroImage<double>(
                    data.Skip(i).Take(inputDimension).ToArray(),
                    new double[] { data[i + inputDimension] }
                    );
                learningSeries.Add(tempImage);
            }
            for (int i = teachAmount - inputDimension; i < teachAmount + recogniseAmount - inputDimension; i++)
            {
                NeuroImage<double> tempImage = new NeuroImage<double>(
                    data.Skip(i).Take(inputDimension).ToArray(),
                    new double[] { data[i + inputDimension] }
                    );
                testingSeries.Add(tempImage);
            }
            excDoc.Quit();
             
        }

        private static void TimeSeries()
        {
            ISingleLayer<double>[] layers = new ISingleLayer<double>[2];
            layers[0] = new SingleLayer(inputDimension, hiddenDimension, new Neuro.MLP.ActivateFunction.Sigmoid(), new Random());
            layers[1] = new SingleLayer(hiddenDimension, 1, new Neuro.MLP.ActivateFunction.Linear(), new Random());
            MultiLayer mLayer = new MultiLayer(layers);
            DifferintiableLearningConfig config = new DifferintiableLearningConfig(new Neuro.MLP.ErrorFunction.HalfEuclid());
            config.Step = 0.5;
            config.MinError = 0.00001;
            config.MaxEpoch = 50000;
            config.MinChangeError = 0;
          //  config.MinChangeError = 0.0000001;
            config.UseRandomShuffle = false;
            SimpleBackPropogation learn = new SimpleBackPropogation(config);
            MultiLayerNeuralNetwork network = new MultiLayerNeuralNetwork(mLayer, learn);
          //  network.DefaultInitialise();
            network.Train(learningSeries, testingSeries);
            Test(network);
        }

        private static void Test(MultiLayerNeuralNetwork network)
        {
            var image = learningSeries[0].Input;
            double res;
            Excel.Application excDoc = new Excel.Application();
            Excel.Workbook excBook = excDoc.Workbooks.Open(path);
            Excel.Worksheet excSheet = excBook.Worksheets.get_Item(1);
            Excel.Range excRange = excSheet.get_Range("C1", "C" + teachAmount + recogniseAmount + 1);
            for (int i = 0; i < inputDimension; i++)
            {
                excRange.Cells[i + 2].Value2 = image[i];
            }
            for (int i = 0; i < teachAmount + recogniseAmount - inputDimension; i++)
            {
                res = network.Compute(image)[0];
                var temp = image[image.Length - 1];
                excRange.Cells[i + inputDimension + 2].Value2 = res;
                Array.Copy(image, 1, image, 0, image.Length - 1);
                image[inputDimension-1] = res;
                Console.WriteLine(res);
            }
            excDoc.Quit();
            

        }

        private static void test2()
        {

                ISingleLayer<double>[] layers = new ISingleLayer<double>[2];
                layers[0] = new SingleLayer(100, 36, new Neuro.MLP.ActivateFunction.Sigmoid(), new Random());
                layers[1] = new SingleLayer(36, 10, new Neuro.MLP.ActivateFunction.Relu(), new Random());
                MultiLayer mLayer = new MultiLayer(layers);
                DifferintiableLearningConfig config = new DifferintiableLearningConfig(new Neuro.MLP.ErrorFunction.HalfEuclid());
                config.Step = 0.1;
                config.OneImageMinError = 0.01;
                config.MinError = 0.5;
                config.MinChangeError = 0.0000001;
                config.UseRandomShuffle = true;
                config.MaxEpoch = 10000;
                SimpleBackPropogation learn = new SimpleBackPropogation(config);
                MultiLayerNeuralNetwork network = new MultiLayerNeuralNetwork(mLayer, learn);
                network.DefaultInitialise();
                network.Train(learningSet, testingSet);
                 Test(network, learningSet, testingSet);
                Console.WriteLine((network.Learn as DifferintiableLearningConfig).RecognisePercent);
                network.Save("fsdf" +".json");
                network = MultiLayerNeuralNetwork.Load("fsdf.json");
                Test(network, learningSet, testingSet);
        }

        private static void test1()
        {
            IList<NeuroImage<double>> im = new List<NeuroImage<double>>();
            im.Add(new NeuroImage<double>(new double[] { 0, 0 }, new double[] { 0 }));
            im.Add(new NeuroImage<double>(new double[] { 0, 1 }, new double[] { 1 }));
            im.Add(new NeuroImage<double>(new double[] { 1, 0 }, new double[] { 1 }));
            im.Add(new NeuroImage<double>(new double[] { 1, 1 }, new double[] { 0 }));

            ISingleLayer<double>[] Layers = new SingleLayer[2];
            Random gen = new Random();
            Layers[0] = new SingleLayer(2, 2, new Neuro.MLP.ActivateFunction.Sigmoid(), gen);
            Layers[1] = new SingleLayer(2, 1, new Neuro.MLP.ActivateFunction.Sigmoid(), gen);
            MultiLayer ml = new MultiLayer(Layers);
            DifferintiableLearningConfig config = new DifferintiableLearningConfig(new Neuro.MLP.ErrorFunction.HalfEuclid());
            config.Step = 1;
            config.MaxEpoch = 20000;

            SimpleBackPropogation learn = new SimpleBackPropogation(config);
            MultiLayerNeuralNetwork mlp = new MultiLayerNeuralNetwork(ml, learn);

            mlp.Train(im, im);
            Console.WriteLine("{0} + {1} = {2}", im[0].Input[0], im[0].Input[1], mlp.Compute(im[0].Input)[0]);
            Console.WriteLine("{0} + {1} = {2}", im[1].Input[0], im[1].Input[1], mlp.Compute(im[1].Input)[0]);
            Console.WriteLine("{0} + {1} = {2}", im[2].Input[0], im[2].Input[1], mlp.Compute(im[2].Input)[0]);
            Console.WriteLine("{0} + {1} = {2}", im[3].Input[0], im[3].Input[1], mlp.Compute(im[3].Input)[0]);

        }

        private static void test3()
        {
            IList<NeuroImage<double>> im = new List<NeuroImage<double>>();
            im.Add(new NeuroImage<double>(new double[] { 0, 0 }, new double[] { 0 }));
            im.Add(new NeuroImage<double>(new double[] { 0, 1 }, new double[] { 1 }));
            im.Add(new NeuroImage<double>(new double[] { 1, 0 }, new double[] { 1 }));
            im.Add(new NeuroImage<double>(new double[] { 1, 1 }, new double[] { 0 }));

            ISingleLayer<double>[] Layers = new SingleLayer[2];
            Random gen = new Random();
            Layers[0] = new SingleLayer(2, 2, new Neuro.MLP.ActivateFunction.Sigmoid(), gen);
            Layers[1] = new SingleLayer(2, 1, new Neuro.MLP.ActivateFunction.Sigmoid(), gen);
            MultiLayer ml = new MultiLayer(Layers);
            DifferintiableLearningConfig config = new DifferintiableLearningConfig(new Neuro.MLP.ErrorFunction.HalfEuclid());
            config.Step = 1;
            config.Batch = -1;
            config.MaxEpoch = 10000;
            BackPropogation learn = new BackPropogation(config);
            MultiLayerNeuralNetwork mlp = new MultiLayerNeuralNetwork(ml, learn);

            mlp.Train(im, im);
            Console.WriteLine("{0} + {1} = {2}", im[0].Input[0], im[0].Input[1], mlp.Compute(im[0].Input)[0]);
            Console.WriteLine("{0} + {1} = {2}", im[1].Input[0], im[1].Input[1], mlp.Compute(im[1].Input)[0]);
            Console.WriteLine("{0} + {1} = {2}", im[2].Input[0], im[2].Input[1], mlp.Compute(im[2].Input)[0]);
            Console.WriteLine("{0} + {1} = {2}", im[3].Input[0], im[3].Input[1], mlp.Compute(im[3].Input)[0]);

        }

        private static void test4()
        {

        }


        /// <summary>
        /// Create testing set, that is part of learning set
        /// </summary>
        /// <param name="learningSet"></param>
        /// <param name="testingSet"></param>
        private static void DivideByTestingSet(List<KeyValuePair<int,NeuroImage<double>>>set ,
            ref List<NeuroImage<double>> learningSet, ref List<NeuroImage<double>> testingSet)
        {
            int index;
            int end = Convert.ToInt32(set.Count *0.8);
            using (System.IO.StreamWriter f = System.IO.File.AppendText("1.txt"))
            {
                for (index = 0; index < end; index++)
                {
                    learningSet.Add(set[index].Value);
                    for(int i=0;i<learningSet.Last().Input.Length;i++)
                    {
                        f.WriteLine(learningSet.Last().Input[i]);
                    }
                    for (int i = 0; i < learningSet.Last().Output.Length; i++)
                    {
                        f.WriteLine(learningSet.Last().Output[i]);
                    }

                }
                for (; index < set.Count; index++)
                {
                    testingSet.Add(set[index].Value);
                }
            }
        }

        /// <summary>
        /// Create learning set by multiply of image
        /// </summary>
        /// <param name="startFolder"></param>
        /// <returns></returns>
        private static void CreateLearningSet(string startFolder,out List<NeuroImage<double>> learningSet
            ,out List<NeuroImage<double>> testingSet)
        {
            learningSet = new List<NeuroImage<double>>();
            testingSet = new List<NeuroImage<double>>();
            var directories = System.IO.Directory.EnumerateDirectories(startFolder);
            var Set = new List<KeyValuePair<int,NeuroImage<double>>>();
            int folderPos = 0;
            System.Drawing.Bitmap currentImage = null;
            System.Drawing.Size blockSize = new System.Drawing.Size(10,10);
            System.Drawing.Point nullPoint = new System.Drawing.Point(0,0);
            System.Drawing.Rectangle blockRect = new System.Drawing.Rectangle(nullPoint, blockSize);
            int similarity;
            foreach(string folder in directories)
            {
                var files = System.IO.Directory.EnumerateFiles(folder);
                double[] inputSet = new double[100];
                double[] outputSet = new double[10];
                outputSet[folderPos++] = 1;
                foreach(string file in files)
                {
                    currentImage = (System.Drawing.Bitmap)System.Drawing.Bitmap.FromFile(file);
                    similarity = int.Parse(file.Split('\\').Last().Split('$')[0].Split('_')[0]);
                    if(currentImage.Size != blockSize)
                    {
                        currentImage = currentImage.Clone(blockRect,currentImage.PixelFormat);             
                    }
                    inputSet = CreateInputFromImage(currentImage);
                    Set.Add(new KeyValuePair<int, NeuroImage<double>>(similarity, 
                        new NeuroImage<double>(inputSet, outputSet)));
                }
                Set.Sort(new NeuroComparator());
                DivideByTestingSet(Set, ref learningSet, ref testingSet);
                Set.Clear();
            }
        }

        /// <summary>
        /// Compute one Input image 
        /// 1 - white
        /// 0 - black
        /// </summary>
        /// <param name="currentImage"></param>
        /// <returns></returns>
        private static double[] CreateInputFromImage(System.Drawing.Bitmap currentImage)
        {
            double[] inputSet = new double[100];
            double inputPoint = 0;
            System.Drawing.Color currentPixel;
            for (int i = 0; i < currentImage.Width; i++)
            {
                for (int j = 0; j < currentImage.Height; j++)
                {
                    currentPixel = currentImage.GetPixel(i, j);
                    inputPoint = ((currentPixel.R + currentPixel.G + currentPixel.B) / 3) / 256.0;
                    inputSet[currentImage.Height * i + j] = inputPoint;
                }
            }
            return inputSet;

        }

        private static void Test(AForge.Neuro.ActivationNetwork network, List<NeuroImage<double>> learningSet)
        {
            
            int i = 0;
            foreach(NeuroImage<double> image in learningSet)
            {
                double[] realOutput = new double[10];
                double[] result = new double[10];
                double[] realResult = new double[10];
                realOutput = network.Compute(image.Input);
                double max = realOutput.Max();
                int index = realOutput.ToList().IndexOf(max);
                result[index] = 1;
                if (ArrayCompare(result, image.Output) == true)
                {
                    i++;
                }              
            }
            Console.WriteLine(i);
            Console.WriteLine(learningSet.Count);
        }
        private static void Test(MultiLayerNeuralNetwork network, List<NeuroImage<double>> learningSet, List<NeuroImage<double>> testingSet)
        {

            int i = 0;
            foreach (NeuroImage<double> image in learningSet)
            {
                double[] realOutput = new double[10];
                double[] result = new double[10];
                double[] realResult = new double[10];
                realOutput = network.Compute(image.Input);
                double max = realOutput.Max();
                int index = realOutput.ToList().IndexOf(max);
                result[index] = 1;
                if (ArrayCompare(result, image.Output) == true)
                {
                    i++;
                }
            }
            foreach (NeuroImage<double> image in testingSet)
            {
                double[] realOutput = new double[10];
                double[] result = new double[10];
                double[] realResult = new double[10];
                realOutput = network.Compute(image.Input);
                double max = realOutput.Max();
                int index = realOutput.ToList().IndexOf(max);
                result[index] = 1;
                if (ArrayCompare(result, image.Output) == true)
                {
                    i++;
                }
            }
            Console.WriteLine(i);
            Console.WriteLine(learningSet.Count);
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
    }
}
*/