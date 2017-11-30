using AI.Genetic;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AI.Examples
{
    public class TSPTask : ITask
    {
        List<PointF> points = new List<PointF>();

        double[,] dimension;

        public List<PointF> Points { get => points; set => points = value; }
        public double[,] Dimension { get => dimension; set => dimension = value; }

        public TSPTask(PointF center, double radius, int count)
        {
            Dimension = new double[count, count];
            GenerateMapByCircle(center, radius, count);
            ComputeMatrixDimensional();
        }

        public TSPTask()
        {

            points.Add(new PointF(0, 2.5f));
            points.Add(new PointF(0, 7.5f));
            points.Add(new PointF(2.5f, 10));
            points.Add(new PointF(7.5f, 10));
            points.Add(new PointF(10, 7.5f));
            points.Add(new PointF(10, 2.5f));
            points.Add(new PointF(7.5f, 0));
            points.Add(new PointF(2.5f, 0));
            Dimension = new double[8, 8];
            ComputeMatrixDimensional();
        }
            

        public double DoTask(double[] input)
        {
            double rez = 0;
            rez += dimension[0, (int)input[0]-1];
            for(int i = 0; i < input.Length - 1; i++)
            {
                rez += dimension[(int)input[i] - 1, (int)input[i + 1] - 1];
            }
            rez += dimension[(int)input.Last()-1,0];
            return rez;
        }

        public void GenerateMapByCircle(PointF center,double radius,int count)
        {
            if(radius > center.X || radius > center.Y)
            {
                throw new Exception("Значение точки должно быть положительным. Изменить center или уменьшите radius");
            }
            for(int i = 0; i < count; i++)
            {
                float x = (float)(center.X * Math.Cos(2*Math.PI / count * i)) + center.X;
                float y = (float)(center.Y * Math.Sin(2*Math.PI / count * i)) + center.Y;
                points.Add(new PointF(x, y));
            }
        }

        public void ComputeMatrixDimensional()
        {
            for(int i = 0; i < points.Count; i++)
            {
                for(int j = 0; j < points.Count; j++)
                {
                    Dimension[i, j] = Math.Sqrt(
                        Math.Pow(points[i].X - points[j].X, 2) +
                        Math.Pow(points[i].Y - points[j].Y, 2));
                }
            }
        }
    }
}
