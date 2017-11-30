using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Drawing.Imaging;

namespace Work
{
    /// <summary>
    /// Статические методы для работы с изображениями
    /// </summary>
    class BitmapWorker
    {
        /// <summary>
        /// Перерасчет размера с соблюдением пропорций с опорой на ширину
        /// </summary>
        /// <param name="size">Текущий размер</param>
        /// <param name="new_width"> Ширина, относительно которой будет расчитываться высота</param>
        /// <returns> Новай размер</returns>
        static public Size RecalculateSize(Size size, int new_width)
        {
            Size new_metrics = new System.Drawing.Size(0, 0);
            if (new_width == 0 || size.Height == 0 || size.Width == 0)
            {
                throw (new DivideByZeroException("Одно из значений размеров было равно нулю"));
            }
            float difference = (float)size.Width / (float)new_width;
            new_metrics.Width = new_width;
            new_metrics.Height = Convert.ToInt32(size.Height / difference);
            return new_metrics;
        }

        /// <summary>
        /// Изменение размеров изображения
        /// </summary>
        /// <param name="source">Изображение ,размер которго меняется</param>
        /// <param name="size"> Новый размер</param>
        /// <returns>Новое изображение</returns>
        static public Bitmap Zoom(Bitmap source, Size size)
        {
            int width = size.Width;
            int height = size.Height;
            Bitmap dest = new Bitmap(width, height);
            using (Graphics gr = Graphics.FromImage(dest))
            {
                gr.FillRectangle(Brushes.White, 0, 0, width, height);  // Очищаем экран
                gr.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;

                float srcwidth = source.Width;
                float srcheight = source.Height;
                float dstwidth = width;
                float dstheight = height;

                if (srcwidth <= dstwidth && srcheight <= dstheight)  // Исходное изображение меньше целевого
                {
                    int left = (width - source.Width) / 2;
                    int top = (height - source.Height) / 2;
                    Rectangle srcRect = new Rectangle(left, top, width, height);
                    Rectangle dstRect = new Rectangle(0, 0, source.Width, source.Height);
                    gr.DrawImage(source, dstRect, dstRect, GraphicsUnit.Pixel);
                }
                else if (srcwidth / srcheight > dstwidth / dstheight)  // Пропорции исходного изображения более широкие
                {
                    float cy = srcheight / srcwidth * dstwidth;
                    float top = ((float)dstheight - cy) / 2.0f;
                    if (top < 1.0f) top = 0;
                    gr.DrawImage(source, 0, top, dstwidth, cy);
                }
                else  // Пропорции исходного изображения более узкие
                {
                    float cx = srcwidth / srcheight * dstheight;
                    float left = ((float)dstwidth - cx) / 2.0f;
                    if (left < 1.0f) left = 0;
                    gr.DrawImage(source, left, 0, cx, dstheight);
                }
                return dest.Clone(new Rectangle(0, 0, dest.Width, dest.Height), PixelFormat.Format8bppIndexed);
            }
        }

        /// <summary>
        /// Перевод цветовой палитры изображения в чернобелый
        /// </summary>
        /// <param name="source">Изображение, которое необходимо перевести</param>
        /// <returns>Результат</returns>
        static public Bitmap ColorToGray(Bitmap source)
        {
            return ColorTo(source, PixelFormat.Format1bppIndexed);
        }

        /// <summary>
        /// Перевод цветовой палитры изображения в 8-битную
        /// </summary>
        /// <param name="source">Изображение, которое необходимо перевести</param>
        /// <returns>Результат</returns>
        static public Bitmap ColorTo8Bit(Bitmap source)
        {
            Bitmap image = source.Clone(new Rectangle(0, 0, source.Width, source.Height), source.PixelFormat);
            for (int i = 0; i < image.Width; i++)
            {
                for (int j = 0; j < image.Height; j++)
                {
                    Color curr = image.GetPixel(i, j);
                    image.SetPixel(i, j, CreateColor(curr.R, curr.G, curr.B));
                }
            }
            return image;
        }
        /// <summary>
        /// Конвертирует входящий цвет в 8-бит
        /// </summary>
        /// <param name="r">Красная состовляющая</param>
        /// <param name="g">Зеленая состовляющая</param>
        /// <param name="b">Синия состовляющая</param>
        /// <returns>Цвет</returns>
        static private Color CreateColor(int r, int g, int b)
        {
            return Color.FromArgb(r & 224, g & 224, b & 192);
        }

        /// <summary>
        /// Перевод цветовой палитры изображения в другую
        /// </summary>
        /// <param name="source">Изображение, которое необходимо перевести</param>
        /// <param name="pxf">Цветовая палитра</param>
        /// <returns></returns>
        static public Bitmap ColorTo(Bitmap source, PixelFormat pxf)
        {
            return source.Clone(new Rectangle(0, 0, source.Width, source.Height), pxf);
        }
        /// <summary>
        /// Обрезает часть изображения(источник при этом не изменяется)
        /// </summary>
        /// <param name="source">Изображение, с которого необходимо обрезать</param>
        /// <param name="rect">Область,которую необходимо обрезать</param>
        /// <returns>Кусок изображения в прямоугольнике</returns>
        static public Bitmap Cut(Bitmap source, Rectangle rect)
        {
            return source.Clone(rect, source.PixelFormat);
        }

        /// <summary>
        /// Округляет размеры до таких, чтобы в него можно было вписать множество прямоугольнков без остатка
        /// Округдение производится в меньшую сторону
        /// </summary>
        /// <param name="source">Исходный размер</param>
        /// <param name="rect">Прямоугольник относительно размера которого округляем</param>
        /// <returns></returns>
        static public Size Round(Size source, Size rect)
        {
           return new Size(rect.Width * (source.Width / rect.Width), rect.Height * (source.Height / rect.Height));
        }

        static public Bitmap DrawGrid(Bitmap source,Size blockSize)
        {
            Bitmap copy = source.Clone(new Rectangle(0, 0, source.Width, source.Height),PixelFormat.Format32bppArgb);
            using (Graphics gr = Graphics.FromImage(copy))
            {
                Pen pen = new Pen(Color.Green, 1);
                int x = source.Width / blockSize.Width;
                int y = source.Height / blockSize.Height;
                for (int i = 0; i < x; i++)
                {
                    gr.DrawLine(pen, new Point(i * blockSize.Width, 0), new Point(i * blockSize.Width, copy.Height));
                }
                for (int i = 0; i < y; i++)
                {
                    gr.DrawLine(pen, new Point(0, i * blockSize.Height), new Point(copy.Width, i * blockSize.Height));
                }
            }
            return copy;
        }

        static public Bitmap DrawFrame(Bitmap source, Rectangle block)
        {
            Bitmap copy = source.Clone(new Rectangle(0, 0, source.Width, source.Height), source.PixelFormat);
            using (Graphics gr = Graphics.FromImage(copy))
            {
                Pen pen = new Pen(Color.Red, 1);
                gr.DrawRectangle(pen, block);
            }
            return copy;
        }

        static public Bitmap Resize(Bitmap source, Size newSize)
        {
            return source.Clone(new Rectangle(0, 0, newSize.Width, newSize.Height), source.PixelFormat);
        }
    }
}
