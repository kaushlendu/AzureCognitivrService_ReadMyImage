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
        private string cognitiveServicesKey = "513e046c8af04562b3dd2c80e48b1fc4";
        // replace with your URL
        private string cognitiveServicesUrl = "https://readmyimage.cognitiveservices.azure.com/";
        string location = "southeastasia";
        private static readonly string subscriptionKey = "4c50be791876429f935bd3fb1ac4306d";
        private static readonly string endpoint = "https://api.cognitive.microsofttranslator.com/";

        // Add your location, also known as region. The default is global.
        // This is required if using a Cognitive Services resource.
       // private static readonly string location = "YOUR_RESOURCE_LOCATION"
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //var client = new ComputerVisionClient(new ApiKeyServiceClientCredentials(cognitiveServicesKey));
            //client.Endpoint = cognitiveServicesUrl;

            //var fileToProcess = File.OpenRead(@"G:\demo.PNG");
            //var apiResult = client.RecognizePrintedTextInStreamAsync(false,
            //                                                        fileToProcess);
            //apiResult.Wait();

            //var ocrResult = apiResult.Result;
            //var t = ocrResult.Regions;
            //StringBuilder sb = new StringBuilder();
            //var image = new Bitmap(this.pictureBox2.Width, this.pictureBox2.Height);
            //Font font = new Font("TimesNewRoman", 25, FontStyle.Bold, GraphicsUnit.Pixel);
            //var graphics = Graphics.FromImage(image);
            //foreach (var r in ocrResult.Regions)
            //{
            //    foreach (var l in r.Lines)
            //    {
            //        foreach (var w in l.Words)
            //        {
            //            sb.Append(" " + w.Text);

            //            var coordinates = w.BoundingBox.Split(',');
            //            graphics.FillRectangle(Brushes.Bisque, new Rectangle(int.Parse(coordinates[0]), int.Parse(coordinates[1]), int.Parse(coordinates[2]), int.Parse(coordinates[3])));
            //            string s = translate(w.Text).Result;
            //            graphics.DrawString(s, font, Brushes.Green, new Point(int.Parse(coordinates[0]), int.Parse(coordinates[1])));

            //            // Console.WriteLine($"Found word {w.Text} at" +
            //            //  $" X {coordinates[0]} and Y {coordinates[1]}");
            //        }
            //    }
            //}
            //this.pictureBox2.Image = image;
            //label1.Text = sb.ToString();

            //ImageReaderAzureVision imageReaderAzureVision = new ImageReaderAzureVision();
            //var result = imageReaderAzureVision.ProcessImage();
            //TextTranslator textTranslator = new TextTranslator();
            //foreach (var l in result)
            //{
            //    sb.Append(" " + l.Text);
            //    graphics.FillRectangle(Brushes.Bisque, new Rectangle(l.Coordinate1, l.Coordinate2, l.Coordinate3, l.Coordinate4));
            //    string s = textTranslator.translate(l.Text).Result;
            //    graphics.DrawString(s, font, Brushes.Green, new Point(l.Coordinate1, l.Coordinate2));
            //}
        }

        private async Task<string>  translate(string text) {
            string route = "/translate?api-version=3.0&from=en&to=hi";
            object[] body = new object[] { new { Text = text } };
            var requestBody = JsonConvert.SerializeObject(body);

            using (var client = new HttpClient())
            using (var request = new HttpRequestMessage())
            {
                // Build the request.
                request.Method = HttpMethod.Post;
                request.RequestUri = new Uri(endpoint + route);
                request.Content = new StringContent(requestBody, Encoding.UTF8, "application/json");
                request.Headers.Add("Ocp-Apim-Subscription-Key", subscriptionKey);
                request.Headers.Add("Ocp-Apim-Subscription-Region", location);

                // Send the request and get response.
                HttpResponseMessage response = await client.SendAsync(request).ConfigureAwait(false);
                // Read response as a string.
                string result = await response.Content.ReadAsStringAsync();
                string temp= result.Substring(result.IndexOf("text")+7 , result.Length- result.IndexOf("text") - 7);
                return temp.Substring(0,temp.IndexOf(",") -1);


            }
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

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                string sourceFile = openFileDialog1.FileName;
                string directoryPath = System.IO.Path.GetDirectoryName(openFileDialog1.FileName );
            }

            var image = new Bitmap(this.pictureBox2.Width, this.pictureBox2.Height);
            Font font = new Font("TimesNewRoman", 25, FontStyle.Bold, GraphicsUnit.Pixel);
            var graphics = Graphics.FromImage(image);
            graphics.DrawString("hello", font, Brushes.Green, new Point(0, 0));
            this.pictureBox2.Image = image;
        }
    }
}
