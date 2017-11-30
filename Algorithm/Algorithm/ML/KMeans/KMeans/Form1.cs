using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Serialization;

namespace KMeans
{
    
    public partial class Form1 : Form
    {
        public List<ColorBuffer> Colors = new List<ColorBuffer>();
        public Form1()
        {
            InitializeComponent();

            if (lb_Color.Items.Count > 0 && pb_Source.Image != null)
                btn_Start.Enabled = true;
            else
                btn_Start.Enabled = false;
 
        }


        /// <summary>
        /// изменение цвета на ЭУ для работы с цветом. Обновляет данные в связанных ЭУ
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cc_Colors_OnValueChange(object sender, EventArgs e)
        {
            Color temp = cc_Colors.Value;
            tb_Red.Text = temp.R.ToString();
            tb_Green.Text = temp.G.ToString();
            tb_Blue.Text = temp.B.ToString();
            tb_Hex.Text = ColorTranslator.ToHtml(temp);
            int index = lb_Color.SelectedIndex;
            if (index != -1)
            {
                ((ColorBuffer)Colors[index]).FromColor(temp);
                ((ColorBuffer)lb_Color.Items[index]).FromColor(temp);
                lb_Color.Update();
            }
        }

        private void btn_Add_Click(object sender, EventArgs e)
        {
            Colors.Add(new ColorBuffer());
            lb_Color.Items.Add(new ColorBuffer());

            if (lb_Color.Items.Count > 0 && pb_Source.Image != null)
                btn_Start.Enabled = true;
            else
                btn_Start.Enabled = false;
        }

        private void lb_Color_SelectedIndexChanged(object sender, EventArgs e)
        {
            int index = lb_Color.SelectedIndex;
            if(index != -1)
            {
                cc_Colors.Value = Colors[index].ToColor();
            }
        }

        private void btn_SetColor_Click(object sender, EventArgs e)
        {
            int index = lb_Color.SelectedIndex;
            if (index != -1)
            {
                Colors[index].FromColor(cc_Colors.Value);
                ((ColorBuffer)lb_Color.Items[index]).FromColor(cc_Colors.Value);
            }
        }

        private void btn_Remove_Click(object sender, EventArgs e)
        {
            int index = lb_Color.SelectedIndex;
            if (index != -1)
            {
                Colors.RemoveAt(index);
                lb_Color.Items.RemoveAt(index);
                if (index != 0)
                    lb_Color.SelectedIndex = index - 1;
                else if (lb_Color.Items.Count > 0)
                    lb_Color.SelectedIndex = 0;
            }
            if (lb_Color.Items.Count > 0 && pb_Source.Image != null)
                btn_Start.Enabled = true;
            else
                btn_Start.Enabled = false;
        }

        private void btn_Random_Click(object sender, EventArgs e)
        {
            Colors.Add(new ColorBuffer(true));
            lb_Color.Items.Add(new ColorBuffer(true));

            if (lb_Color.Items.Count > 0)
                btn_Start.Enabled = true;
            else
                btn_Start.Enabled = false;
        }

 

        private void Form1_DragEnter(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.Move;
        }

        private void Form1_DragDrop(object sender, DragEventArgs e)
        {
            int x = this.PointToClient(new Point(e.X, e.Y)).X;

            int y = this.PointToClient(new Point(e.X, e.Y)).Y;


            if (x >= pb_Source.Location.X && x <= pb_Source.Location.X + pb_Source.Width && y >= pb_Source.Location.Y && y <= pb_Source.Location.Y + pb_Source.Height)
            {

                string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);

                pb_Source.Image = Image.FromFile(files[0]);

                btn_FromImageColor.Visible = true;

            }
        }

        private void btn_FromImageColor_Click(object sender, EventArgs e)
        {
            Random rnd = new Random();
            Bitmap source = (Bitmap)pb_Source.Image;
            int x = rnd.Next(0, source.Width);
            int y = rnd.Next(0, source.Height);
            Color color = source.GetPixel(x, y);
            ColorBuffer buffer = new ColorBuffer();
            buffer.FromColor(color);
            Colors.Add(buffer);
            lb_Color.Items.Add(buffer);

            if (lb_Color.Items.Count > 0 && pb_Source.Image != null)
                btn_Start.Enabled = true;
            else
                btn_Start.Enabled = false;
        }

        private void btn_Start_Click(object sender, EventArgs e)
        {
            Color[] cluster = new Color[Colors.Count];
            for (int i = 0; i < cluster.Length; i++)
            {
                cluster[i] = Colors[i].ToColor();
            }
            Algorithm.ML.KMeans.BitmapClustering means = new Algorithm.ML.KMeans.BitmapClustering(cluster);
            Algorithm.ML.KMeans.ProgressState pr = new Algorithm.ML.KMeans.ProgressState();
            Task t = Task.Run(() =>
            {
                means.Teach((Bitmap)pb_Source.Image, ref pr);

                Bitmap image = means.Compute((Bitmap)pb_Source.Image);
                this.Invoke((Action)(() =>
                {
                    pb_Cluster.Image = image;
                }));

            }
            );


            while(t.IsCompleted == false)
            {
                if (ob_Progress.Value < ob_Progress.Maximum)
                    ob_Progress.Value++;
                else
                    ob_Progress.Value = 0;
                Application.DoEvents();
            }
            
           

            for(int i=0;i<means.Clusters.Length;i++)
            {
                ColorBuffer buffer = new ColorBuffer();
                buffer.FromColor(means.Clusters[i]);
                Colors[i] = buffer;
                lb_Color.Items[i] = buffer;
            }
            ob_Progress.Value = 0;
        }

        private void tableLayoutPanel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void toolTip1_Popup(object sender, PopupEventArgs e)
        {


        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (pb_Cluster.Image == null)
                return;

            SaveFileDialog dlg = new SaveFileDialog();
            dlg.Filter = "picture|*.png";
            if(dlg.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                pb_Cluster.Image.Save(dlg.FileName);
            }
        }

        private void btn_Load_Click(object sender, EventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.Filter = "xml file|*.xml";
            if (dlg.ShowDialog() != System.Windows.Forms.DialogResult.OK)
                return;
            string path = dlg.FileName;
            XmlSerializer serial = new XmlSerializer(typeof(List<ColorBuffer>));
            using(var stream = System.IO.File.Open(path,System.IO.FileMode.Open))
            {
                try
                {
                    Colors = (List<ColorBuffer>)serial.Deserialize(stream);
                }
                catch(Exception exc)
                {
                    MessageBox.Show(exc.Message.ToString());
                }
            }
            lb_Color.Items.Clear();
            lb_Color.Items.AddRange(Colors.ToArray());
        }

        private void btn_Save_Click(object sender, EventArgs e)
        {
            SaveFileDialog dlg = new SaveFileDialog();
            dlg.Filter = "xml file|*.xml";
            if (dlg.ShowDialog() != System.Windows.Forms.DialogResult.OK)
                return;
            string path = dlg.FileName;
            XmlSerializer serial = new XmlSerializer(typeof(List<ColorBuffer>));
            using (var stream = System.IO.File.Open(path, System.IO.FileMode.Create))
            {
                try
                {
                    serial.Serialize(stream,Colors);
                }
                catch (Exception exc)
                {
                    MessageBox.Show(exc.Message.ToString());
                }
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            Colors.Clear();
            lb_Color.Items.Clear();
        }

        private void tableLayoutPanel1_CellPaint(object sender, TableLayoutCellPaintEventArgs e)
        {
            Graphics g = e.Graphics;
            Rectangle r = e.CellBounds;

            using (Pen pen = new Pen(Color.SeaGreen, 0/*1px width despite of page scale, dpi, page units*/ ))
            {
                pen.Alignment = System.Drawing.Drawing2D.PenAlignment.Center;
                // define border style
                pen.DashStyle = System.Drawing.Drawing2D.DashStyle.Solid;

                // decrease border rectangle height/width by pen's width for last row/column cell
                if (e.Row == (tableLayoutPanel1.RowCount - 1))
                {
                    r.Height -= 1;
                }

                if (e.Column == (tableLayoutPanel1.ColumnCount - 1))
                {
                    r.Width -= 1;
                }

                // use graphics mehtods to draw cell's border
                e.Graphics.DrawRectangle(pen, r);
            }
        }

        private void bunifuImageButton1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void bunifuImageButton2_Click(object sender, EventArgs e)
        {
            if (WindowState == FormWindowState.Maximized)
            {
                WindowState = FormWindowState.Normal;
            }
            else
                WindowState = FormWindowState.Maximized;
        }

        private void bunifuImageButton3_Click(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Minimized;
        }

    }
}
