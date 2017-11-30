using AI.Genetic;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AI.Examples
{
    public class ReductionTask : ITask
    {
        double[][] points;
        double[,] dimensions;
        public ReductionTask(int pointsCount, int pointDimension, int maxDim)
        {
            points = new double[pointsCount][];
            dimensions = new double[pointsCount, pointsCount];
            Random r = new Random();
            for (int i = 0; i < pointsCount; i++)
            {
                points[i] = new double[pointDimension];
                do
                {
                    for (int j = 0; j < pointDimension; j++)
                    {
                        points[i][j] = (r.Next(0,2) == 1 ? -1 : 1) * r.NextDouble() + 1;
                    }
                } while (ComputeDimension(points[i], Enumerable.Repeat(1.0, pointDimension).ToArray()) <= 1);
                
            }

            for (int i = 0; i < pointsCount; i++)
            {
                for (int j = 0; j < pointsCount; j++)
                {
                    Dimensions[i, j] = ComputeDimension(points[i], points[j]);
                }
            }
        }

        public double[][] Points { get => points; set => points = value; }
        public double[,] Dimensions { get => dimensions; set => dimensions = value; }

        public double DoTask(double[] input)
        {
            double[][] inputP  = new double[points.Length][];
            double[,] dimensionP = new double[points.Length, points.Length];
            for (int i = 0; i < points.Length; i++)
            {
                inputP[i] = new double[2];
                for (int j = 0; j < 2; j++)
                {
                    inputP[i][j] = input[i * 2 + j];
                }
            }
            for (int i = 0; i < points.Length; i++)
            {
                for (int j = 0; j < points.Length; j++)
                {
                    dimensionP[i,j] = ComputeDimension(inputP[i], inputP[j]);
                }
            }

            double rez = 0;
            for (int i = 0; i < points.Length; i++)
            {
                for (int j = 0; j < points.Length; j++)
                {
                    rez += Math.Abs(dimensionP[i, j] - dimensions[i, j]);
                }
            }
            return rez;
        }

        public double ComputeDimension(double[] x1,double[] x2)
        {
            double sum = 0;
            for(int i = 0; i < x1.Length; i++)
            {
                sum += Math.Pow(x1[i] - x2[i],2);
            }
            return Math.Sqrt(sum);
        }
    }
}
