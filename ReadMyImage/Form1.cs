using Microsoft.Azure.CognitiveServices.Vision.ComputerVision;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ReadMyImage
{
    public partial class Form1 : Form
    {
       
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
           
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            var digres =openFileDialog1.ShowDialog();
            if (digres == DialogResult.OK)
            {
                string filepath = openFileDialog1.FileName;
                pictureBox1.ImageLocation = filepath;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            StringBuilder sb = new StringBuilder();
            var image = new Bitmap(this.pictureBox1.Width, this.pictureBox1.Height);
            Font font = new Font("TimesNewRoman", 25, FontStyle.Bold, GraphicsUnit.Pixel);
            var graphics = Graphics.FromImage(image);
            ImageReaderAzureVision imageReaderAzureVision = new ImageReaderAzureVision();
            var result = imageReaderAzureVision.ProcessImage();
            TextTranslator textTranslator = new TextTranslator();
            foreach (var l in result)
            {
                sb.Append(" " + l.Text);
                string s = textTranslator.translate(l.Text).Result;

                graphics.FillRectangle(Brushes.Bisque, new Rectangle(l.Coordinate1, l.Coordinate2, l.Coordinate3, l.Coordinate4));
                graphics.DrawString(s, font, Brushes.Green, new Point(l.Coordinate1, l.Coordinate2));
            }
            this.pictureBox2.Image = image;
            pictureBox2.Refresh();
            label1.Text = sb.ToString();
        }
    }
}
