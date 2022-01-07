using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp3
{
    public partial class Form1 : Form
    {
        List<Panel> paneller = new List<Panel>();
        int gecerliEkran = 0;
        int resim = 0;
        public static Bitmap[] resimler = new Bitmap[6];
        int index;
        public Form1()
        {
            InitializeComponent();

            for (int i = 0; i < 6; i++)
            {
                resimler[i] = null;
            }
        }
        Image ZoomPicture(Image img, Size size)
        {
            Bitmap bm = new Bitmap(img, Convert.ToInt32(img.Width * size.Width), Convert.ToInt32(img.Height * size.Height));
            Graphics gpu = Graphics.FromImage(bm);
            gpu.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
            return bm;
        }

        PictureBox org;

        private void Form1_Load(object sender, EventArgs e)
        {

            paneller.Add(panel1);
            paneller.Add(panel2);
            paneller.Add(panel3);
            paneller.Add(panel4);
            paneller.Add(panel5);
            paneller.Add(panel6);
            paneller[0].BringToFront();

            comboBox1.Enabled = false;
            comboBox2.Enabled = false;
            comboBox3.Enabled = false;

            button2.Visible = false;
            label1.Text = "Resim Seç";

            trackBar1.Minimum = 1;
            trackBar1.Maximum = 6;
            trackBar1.SmallChange = 1;
            trackBar1.LargeChange = 1;
            trackBar1.UseWaitCursor = false;
            this.DoubleBuffered = true;
            org = new PictureBox();



        }

        private void button2_Click(object sender, EventArgs e)
        {


            gecerliEkran--;
            if (gecerliEkran == 0)
            {
                button2.Visible = false;
            }
            if (gecerliEkran > -1)
            {
                button1.Visible = true;
                if (index > 0)
                {
                    paneller[--index].BringToFront();
                }
            }


        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (resimler[0] != null)
            {

                if (gecerliEkran != 2)
                {
                    trackBar1.Visible = false;
                }


                gecerliEkran++;
                if (gecerliEkran == 5)
                {
                    button1.Visible = false;
                }
                if (gecerliEkran < 6)
                {
                    button2.Visible = true;
                    if (index < paneller.Count - 1)
                    {
                        paneller[++index].BringToFront();
                        switch (gecerliEkran)
                        {
                            case 1:
                                resimler[1] = resimler[0];
                                pictureBox2.Image = resimler[1];
                                pictureBox2.SizeMode = PictureBoxSizeMode.Normal;

                                break;
                            case 2:
                                resimler[2] = resimler[1];
                                pictureBox3.Image = resimler[2];
                                break;
                            case 3:
                                resimler[3] = resimler[2];
                                pictureBox4.Image = resimler[3];

                                break;
                            case 4:
                                resimler[4] = resimler[3];
                                pictureBox5.Image = resimler[4];
                                break;
                            case 5:
                                resimler[5] = resimler[4];
                                pictureBox6.Image = resimler[5];

                                break;
                        }
                        resim++;

                    }


                }

            }
            else
            {
                MessageBox.Show("Lütfen Resim Seçiniz !", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void button3_Click(object sender, EventArgs e)
        {
            // Resim Aç
            OpenFileDialog open = new OpenFileDialog();
            open.Title = "Resim Seç";
            // image filters  
            open.Filter = "Image Files(*.jpg; *.jpeg; *.bmp; *.png)|*.jpg; *.jpeg;  *.bmp; *.png";
            if (open.ShowDialog() == DialogResult.OK)
            {

                pictureBox1.Image = new Bitmap(open.FileName);
                // image file path  
                resimler[0] = new Bitmap(open.FileName);
            }
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton1.Checked == true)
            {
                comboBox1.Enabled = true;


            }
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton2.Checked == true)
            {
                comboBox1.Enabled = false;
                resimler[0] = (Bitmap)pictureBox1.Image;
                pictureBox2.Image = resimler[0];
                resimler[1] = resimler[0];
                trackBar1.Visible = false;

            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

            switch (comboBox1.SelectedIndex)
            {

                case 0:
                    pictureBox2.Image = Grayscale(resimler[1]);
                    resimler[1] = Grayscale(resimler[1]);

                    break;
                case 1:
                    pictureBox2.Image = Siyahbeyaz(Grayscale(resimler[1]));
                    resimler[1] = Siyahbeyaz(Grayscale(resimler[1]));
                    break;
                case 2:
                    trackBar1.Visible = true;
                    org.Image = pictureBox2.Image;

                    break;
                case 3: MessageBox.Show(" Resimden istenilen bölgenin kesilip alınması"); break;
                default:
                    break;
            }
        }

        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton3.Checked == true)
            {
                comboBox2.Enabled = false;
                pictureBox3.Image = resimler[1];
                resimler[2] = resimler[1];

            }
        }

        private void radioButton4_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton4.Checked == true)
            {
                comboBox2.Enabled = true;

            }
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (comboBox2.SelectedIndex)
            {

                case 0: HistoGram(resimler[2]); break;
                case 1:
                    resimler[2] = H_esitleme(Grayscale(resimler[1]));
                    pictureBox3.Image = resimler[2];
                    break;
                case 2:
                    MessageBox.Show("Görüntü Nicemleme");
                    break;

                default:
                    break;
            }
        }

        private void radioButton5_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton5.Checked == true)
            {
                comboBox3.Enabled = false;
                pictureBox4.Image = resimler[2];
                resimler[3] = resimler[2];


            }
        }

        private void radioButton6_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton6.Checked == true)
            {
                comboBox3.Enabled = true;

            }
        }

        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (comboBox3.SelectedIndex)
            {

                case 0: MessageBox.Show(" Gaussian Bulanıklaştırma filtresi"); break;
                case 1:
                    resimler[3] = keskinlestir(resimler[2]);
                    pictureBox4.Image = resimler[3];
                    break;
                case 2:
                    resimler[3] = sobelalg(resimler[3]);
                    pictureBox4.Image = resimler[3];
                    break;
                case 3:
                    resimler[3] = meanfiltresi(resimler[3]);
                    pictureBox4.Image = resimler[3];
                 
                    break;
                case 4:

                    resimler[3] = medianfiltresi(resimler[3]); // Tuz biberde etkili !
                    pictureBox4.Image = resimler[3];
                    break;
                case 5:

                    // kontra

                    break;

                default:
                    break;
            }
        }

        private void radioButton7_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton7.Checked == true)
            {
                comboBox4.Enabled = false;
                pictureBox5.Image = resimler[3];
                resimler[4] = resimler[3];
            }
        }

        private void radioButton8_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton8.Checked == true)
            {
                comboBox4.Enabled = true;

            }
        }

        private void comboBox4_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (comboBox4.SelectedIndex)
            {

                case 0: 
                    resimler[4] = resimler[3];
                    resimler[4] = Siyahbeyaz(resimler[4]);
                    resimler[4] = genisleme(resimler[4]);
                    pictureBox5.Image = resimler[4];

                    break;
                case 1: MessageBox.Show(" Siyah beyaz resimde erozyon"); break;
                case 2: MessageBox.Show(" İskelet çıkartma (Skeletonization)"); break;
                default:
                    break;
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            //Kaydetme
            string type = @"JPG|*.jpg";
            switch (comboBox5.SelectedIndex)
            {
                case 0:
                    type = @"JPG|*.jpg";
                    break;
                case 1:
                    type = @"BMP|*.bmp";
                    break;
                case 2:
                    type = @"PNG|*.png";
                    break;

                default:
                    break;
            }
            using (SaveFileDialog saveFileDialog = new SaveFileDialog() { Filter = type })
            {
                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    pictureBox6.Image.Save(saveFileDialog.FileName);
                    MessageBox.Show("Başarıyla Kaydedildi!", "Kayıt", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }

        }

        public Bitmap Grayscale(Bitmap img)
        {
            for (int i = 0; i < img.Height - 1; i++)
            {
                for (int j = 0; j < img.Width - 1; j++)
                {
                    int deger = (img.GetPixel(j, i).R + img.GetPixel(j, i).G + img.GetPixel(j, i).B) / 3;
                    Color renk;
                    renk = Color.FromArgb(deger, deger, deger);
                    img.SetPixel(j, i, renk);
                }

            }

            return img;

        }

        private void HistoGram(Bitmap bmp)
        {

            int[] histogram_r = new int[256];
            float max = 0;

            for (int i = 0; i < bmp.Width; i++)
            {
                for (int j = 0; j < bmp.Height; j++)
                {
                    int redValue = bmp.GetPixel(i, j).R;
                    histogram_r[redValue]++;
                    if (max < histogram_r[redValue])
                        max = histogram_r[redValue];
                }
            }

            int histHeight = 128;
            Bitmap img = new Bitmap(256, histHeight + 10);
            using (Graphics g = Graphics.FromImage(img))
            {
                for (int i = 0; i < histogram_r.Length; i++)
                {
                    float pct = histogram_r[i] / max;
                    g.DrawLine(Pens.Black,
                        new Point(i, img.Height - 5),
                        new Point(i, img.Height - 5 - (int)(pct * histHeight))
                        );
                }
            }

            Form2 m = new Form2();
            m.Resim(img);
            m.ShowDialog();
        }

        public Bitmap Siyahbeyaz(Bitmap input_img)
        {


            int ortalama;
            int toplam = 0;
            int width, height;
            Color input_clr;
            int R = 0, G = 0, B = 0;
            Bitmap output_img = new Bitmap(input_img.Width, input_img.Height);
            width = input_img.Width;
            height = input_img.Height;


            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < height; j++)
                {
                    input_clr = input_img.GetPixel(i, j);
                    toplam += input_clr.R + input_clr.G + input_clr.B;
                }
            }
            ortalama = Convert.ToInt32(toplam / (width * height * 3));

            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < height; j++)
                {
                    input_clr = input_img.GetPixel(i, j);
                    if (input_clr.R < ortalama)
                        R = 0;
                    else
                        R = 255;
                    if (input_clr.G < ortalama)
                        G = 0;
                    else
                        G = 255;
                    if (input_clr.B < ortalama)
                        B = 0;
                    else
                        B = 255;

                    toplam = R + G + B;
                    if (toplam > 255)
                    {
                        toplam = 255;
                    }
                    output_img.SetPixel(i, j, Color.FromArgb(toplam, toplam, toplam));
                }
            }

            return output_img;

        }

        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            if (trackBar1.Value != 0)
            {
                pictureBox2.Image = null;
                pictureBox2.Image = ZoomPicture(org.Image, new Size(trackBar1.Value, trackBar1.Value));
            }
        }

        private void panel6_Paint(object sender, PaintEventArgs e)
        {

        }


        private Bitmap H_esitleme(Bitmap input_img)
        {

            int width, height;
            Color input_clr;
            int R = 0, G = 0, B = 0;
            Bitmap output_img = new Bitmap(input_img.Width, input_img.Height);
            width = input_img.Width;
            height = input_img.Height;

            width = input_img.Width;
            height = input_img.Height;
            output_img = new Bitmap(width, height);

            int[] histogram_r = new int[256];
            int[] histogram_g = new int[256];
            int[] histogram_b = new int[256];

            int[] cdf_r = histogram_r;
            int[] cdf_g = histogram_g;
            int[] cdf_b = histogram_b;

            uint pixels = (uint)input_img.Height * (uint)input_img.Width;
            decimal Const = 255 / (decimal)pixels;

            for (int i = 0; i < input_img.Width; i++)
            {
                for (int j = 0; j < input_img.Height; j++)
                {
                    input_clr = input_img.GetPixel(i, j);

                    histogram_r[(int)input_clr.R]++;
                    histogram_g[(int)input_clr.G]++;
                    histogram_b[(int)input_clr.B]++;
                }
            }

            for (int r = 1; r <= 255; r++)
            {
                cdf_r[r] = cdf_r[r] + cdf_r[r - 1];
                cdf_g[r] = cdf_g[r] + cdf_g[r - 1];
                cdf_b[r] = cdf_b[r] + cdf_b[r - 1];
            }

            for (int y = 0; y < input_img.Height; y++)
            {
                for (int x = 0; x < input_img.Width; x++)
                {
                    Color pixelColor = input_img.GetPixel(x, y);

                    R = Convert.ToInt32(cdf_r[pixelColor.R] * Const);
                    G = Convert.ToInt32(cdf_r[pixelColor.G] * Const);
                    B = Convert.ToInt32(cdf_r[pixelColor.B] * Const);

                    output_img.SetPixel(x, y, Color.FromArgb(R, G, B));
                }
            }

            return output_img;

        }


        public static Bitmap keskinlestir(Bitmap image)
        {
            Bitmap sharpenImage = new Bitmap(image.Width, image.Height);

            int filterWidth = 3;
            int filterHeight = 3;
            int w = image.Width;
            int h = image.Height;

            double[,] filter = new double[filterWidth, filterHeight];

            filter[0, 0] = filter[0, 1] = filter[0, 2] = filter[1, 0] = filter[1, 2] = filter[2, 0] = filter[2, 1] = filter[2, 2] = -1;
            filter[1, 1] = 9;

            double factor = 1.0;
            double bias = 0.0;

            Color[,] result = new Color[image.Width, image.Height];

            for (int x = 0; x < w; ++x)
            {
                for (int y = 0; y < h; ++y)
                {
                    double red = 0.0, green = 0.0, blue = 0.0;

                    for (int filterX = 0; filterX < filterWidth; filterX++)
                    {
                        for (int filterY = 0; filterY < filterHeight; filterY++)
                        {
                            int imageX = (x - filterWidth / 2 + filterX + w) % w;
                            int imageY = (y - filterHeight / 2 + filterY + h) % h;


                            Color imageColor = image.GetPixel(imageX, imageY);

                            red += imageColor.R * filter[filterX, filterY];
                            green += imageColor.G * filter[filterX, filterY];
                            blue += imageColor.B * filter[filterX, filterY];
                        }
                        int r = Math.Min(Math.Max((int)(factor * red + bias), 0), 255);
                        int g = Math.Min(Math.Max((int)(factor * green + bias), 0), 255);
                        int b = Math.Min(Math.Max((int)(factor * blue + bias), 0), 255);

                        result[x, y] = Color.FromArgb(r, g, b);
                    }
                }
            }
            for (int i = 0; i < w; ++i)
            {
                for (int j = 0; j < h; ++j)
                {
                    sharpenImage.SetPixel(i, j, result[i, j]);
                }
            }
            return sharpenImage;
        }



        public Bitmap sobelalg(Bitmap image)
        {
            Bitmap gri, buffer;
            int i, j, valX, valY, gradient;
            Color renk;
            int[,] GX = new int[3, 3];
            int[,] GY = new int[3, 3];


            gri = Grayscale(image);
            buffer = new Bitmap(gri.Width, gri.Height);
            GX[0, 0] = -1;
            GX[0, 1] = 0;
            GX[0, 2] = 1;


            GX[1, 0] = -2;
            GX[1, 1] = 0;
            GX[1, 2] = 2;

            GX[2, 0] = -1;
            GX[2, 1] = 0;
            GX[2, 2] = 1;

            GY[0, 0] = -1;
            GY[0, 1] = -2;
            GY[0, 2] = -1;

            GY[1, 0] = 0;
            GY[1, 1] = 0;
            GY[1, 2] = 0;

            GY[2, 0] = 1;
            GY[2, 1] = 2;
            GY[2, 2] = 1;
            GY[2, 2] = 1;

            for (i = 0; i < gri.Height - 1; i++)
            {
                for (j = 0; j < gri.Width - 1; j++)
                {
                    if (i == 0 || i == gri.Height - 1 || j == 0 || j == gri.Width - 1)
                    {
                        renk = Color.FromArgb(255, 255, 255);
                        buffer.SetPixel(j, i, renk);
                        valX = 0;
                        valY = 0;
                    }
                    else
                    {
                        valX = gri.GetPixel(j - 1, i - 1).R * GX[0, 0] +
                         gri.GetPixel(j, i - 1).R * GX[0, 1] +
                         gri.GetPixel(j + 1, i - 1).R * GX[0, 2] +
                         gri.GetPixel(j - 1, i).R * GX[1, 0] +
                         gri.GetPixel(j, i).R * GX[1, 1] +
                         gri.GetPixel(j + 1, i).R * GX[1, 2] +
                         gri.GetPixel(j - 1, i + 1).R * GX[2, 0] +
                         gri.GetPixel(j, i + 1).R * GX[2, 1] +
                         gri.GetPixel(j + 1, i + 1).R * GX[2, 2];

                        valY = gri.GetPixel(j - 1, i - 1).R * GY[0, 0] +
                             gri.GetPixel(j, i - 1).R * GY[0, 1] +
                             gri.GetPixel(j + 1, i - 1).R * GY[0, 2] +
                             gri.GetPixel(j - 1, i).R * GY[1, 0] +
                             gri.GetPixel(j, i).R * GY[1, 1] +
                             gri.GetPixel(j + 1, i).R * GY[1, 2] +
                             gri.GetPixel(j - 1, i + 1).R * GY[2, 0] +
                             gri.GetPixel(j, i + 1).R * GY[2, 1] +
                             gri.GetPixel(j + 1, i + 1).R * GY[2, 2];

                        gradient = Convert.ToInt32(Math.Abs(valX) + Math.Abs(valY));
                        if (gradient < 0)
                            gradient = 0;
                        if (gradient > 255)
                            gradient = 255;

                        buffer.SetPixel(j, i, Color.FromArgb(gradient, gradient, gradient));


                    }
                }


            }

            return buffer;
        }


        public Bitmap medianfiltresi(Bitmap image)
        {
            Bitmap buffer = new Bitmap(image.Width, image.Height);
            Color renk;

            for (int i = 0; i < image.Height; i++)
            {
                for (int j = 0; j < image.Width; j++)
                {
                    if ((i == 0) || (i == image.Height - 1) || (j == 0) || (j == image.Width - 1))
                        continue;
                    else
                    {
                        int ortanca = ortancayiBul(image, j, i);
                        renk = Color.FromArgb(ortanca, ortanca, ortanca);
                        buffer.SetPixel(j, i, renk);

                    }
                }
            }

            return buffer;
        }


        private int ortancayiBul(Bitmap image, int j, int i)
        {
            int[] dizi = new int[9];
            Color renk;

            int sagKomsu, sagUstCaprazKomsu, ustKomsu, solUstCaprazKomsu, solKomsu, solAltCaprazKomsu, altKomsu, sagAltCaprazKomsu;
            sagKomsu = image.GetPixel(j + 1, i).R;
            sagUstCaprazKomsu = image.GetPixel(j + 1, i - 1).R;
            ustKomsu = image.GetPixel(j, i - 1).R;
            solUstCaprazKomsu = image.GetPixel(j - 1, i - 1).R;
            solKomsu = image.GetPixel(j - 1, i).R;
            solAltCaprazKomsu = image.GetPixel(j - 1, i + 1).R;
            altKomsu = image.GetPixel(j, i + 1).R;
            sagAltCaprazKomsu = image.GetPixel(j + 1, i + 1).R;

            dizi[0] = image.GetPixel(j, i).R;
            dizi[1] = sagKomsu;
            dizi[2] = sagUstCaprazKomsu;
            dizi[3] = ustKomsu;
            dizi[4] = solUstCaprazKomsu;
            dizi[5] = solKomsu;
            dizi[6] = solAltCaprazKomsu;
            dizi[7] = altKomsu;
            dizi[8] = sagAltCaprazKomsu;

            for (i = 0; i < 8; i++)
            {
                for (j = i + 1; j < 9; j++)
                {
                    if (dizi[i] < dizi[j])
                    {
                        continue;
                    }
                    else
                    {
                        int temp = dizi[j];
                        dizi[j] = dizi[i];
                        dizi[i] = temp;
                    }
                }
            }

            return dizi[4];
        }


        public Bitmap meanfiltresi(Bitmap img)
        {
            Bitmap orgPic = new Bitmap(img);
            Bitmap tmpPic = new Bitmap(img);
            for (int y = 1; y < orgPic.Height - 1; y++)
            {
                for (int x = 1; x < orgPic.Width - 1; x++)
                {
                    Point[] arrayP = new Point[] {  new Point(x - 1, y - 1),
                                                    new Point(x - 0, y - 1),
                                                    new Point(x + 1, y - 1),
                                                    new Point(x - 1, y - 0),
                                                    new Point(x - 0, y - 0),
                                                    new Point(x + 1, y - 0),
                                                    new Point(x - 1, y + 1),
                                                    new Point(x - 0, y + 1),
                                                    new Point(x + 1, y + 1)};

                    int sumA = 0, sumR = 0, sumG = 0, sumB = 0;
                    foreach (Point item in arrayP)
                    {
                        sumA += orgPic.GetPixel(item.X, item.Y).A;
                        sumR += orgPic.GetPixel(item.X, item.Y).R;
                        sumG += orgPic.GetPixel(item.X, item.Y).G;
                        sumB += orgPic.GetPixel(item.X, item.Y).B;
                    }
                    int meanARGB = 0x01000000 * (sumA / 9) +
                                   0x00010000 * (sumR / 9) +
                                   0x00000100 * (sumG / 9) +
                                   0x00000001 * (sumB / 9);

                    tmpPic.SetPixel(x, y, Color.FromArgb(meanARGB));
                }
            }
            return tmpPic;
        }


        public Bitmap genisleme(Bitmap SrcImage)
        {

            Bitmap tempbmp = new Bitmap(SrcImage.Width, SrcImage.Height);


            BitmapData SrcData = SrcImage.LockBits(new Rectangle(0, 0,
                SrcImage.Width, SrcImage.Height), ImageLockMode.ReadOnly,
                PixelFormat.Format24bppRgb);


            BitmapData DestData = tempbmp.LockBits(new Rectangle(0, 0, tempbmp.Width,
                tempbmp.Height), ImageLockMode.ReadWrite, PixelFormat.Format24bppRgb);


            byte[,] sElement = new byte[5, 5] {
        {0,0,1,0,0},
        {0,1,1,1,0},
        {1,1,1,1,1},
        {0,1,1,1,0},
        {0,0,1,0,0}
    };


            int size = 5;
            byte max, clrValue;
            int radius = size / 2;
            int ir, jr;

            unsafe
            {


                for (int colm = radius; colm < DestData.Height - radius; colm++)
                {

                    byte* ptr = (byte*)SrcData.Scan0 + (colm * SrcData.Stride);
                    byte* dstPtr = (byte*)DestData.Scan0 + (colm * SrcData.Stride);


                    for (int row = radius; row < DestData.Width - radius; row++)
                    {
                        max = 0;
                        clrValue = 0;


                        for (int eleColm = 0; eleColm < 5; eleColm++)
                        {
                            ir = eleColm - radius;
                            byte* tempPtr = (byte*)SrcData.Scan0 +
                                ((colm + ir) * SrcData.Stride);

                            for (int eleRow = 0; eleRow < 5; eleRow++)
                            {
                                jr = eleRow - radius;


                                clrValue = (byte)((tempPtr[row * 3 + jr] +
                                    tempPtr[row * 3 + jr + 1] + tempPtr[row * 3 + jr + 2]) / 3);

                                if (max < clrValue)
                                {
                                    if (sElement[eleColm, eleRow] != 0)
                                        max = clrValue;
                                }
                            }
                        }

                        dstPtr[0] = dstPtr[1] = dstPtr[2] = max;

                        ptr += 3;
                        dstPtr += 3;
                    }
                }
            }


            SrcImage.UnlockBits(SrcData);
            tempbmp.UnlockBits(DestData);


            return tempbmp;
        }




    }
}
