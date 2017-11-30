using AI.Genetic;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AI.Examples
{
    public class LuckyDog : ITask
    {
        double[,] map;
        int xStart,yStart;
        List<Point> sausege = new List<Point>();
       
        public LuckyDog()
        {
            map = new double[1000, 1000];
            xStart = 500;
            yStart = 500;
            sausege.Add(new Point(200, 200));
            sausege.Add(new Point(800, 800));
        }

        public double[,] Map { get => map; set => map = value; }

        public double DoTask(double[] input)
        {
            int curX = xStart, curY = yStart;
            for(int i = 0; i < input.Length / 4; i++)
            {
                curY -= (int)input[i*4+ 0];
                curX += (int)input[i*4+ 1];
                curY += (int)input[i*4+ 2];
                curX -= (int)input[i*4+ 3];
            }
            List<double> rez = new List<double>();
            foreach(var it in sausege)
            {
                rez.Add(FindPath(it, curX, curY));
            }
            return rez.Min();
        }

        public double FindPath(Point p,int x,int y)
        {
            double p_x = p.X - x;
            double p_y = p.Y - y;
            double rez = Math.Sqrt(p_x * p_x + p_y * p_y);
            return rez;
        }
        
    }
}
