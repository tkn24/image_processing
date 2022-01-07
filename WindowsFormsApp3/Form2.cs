using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp3
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }
        private bool okButton = false;
        public bool OKButtonClicked
        {
            get { return okButton; }
        }
        private void Form2_Load(object sender, EventArgs e)
        {

        }

        public void Resim(Bitmap img)
        {
            pictureBox1.Image = img;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            okButton = true;
            this.Close();
        }
    }
}
