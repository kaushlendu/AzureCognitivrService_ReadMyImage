using Microsoft.Azure.CognitiveServices.Vision.ComputerVision;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReadMyImage
{
    class ImageRegionDetails
    {
        public string Text { get; set; }
        public int Coordinate1 { get; set; }
        public int Coordinate2 { get; set; }
        public int Coordinate3 { get; set; }
        public int Coordinate4 { get; set; }
    }
    class ImageReaderAzureVision
    {
        private string cognitiveServicesKey = "513e046c8af04562b3dd2c80e48b1fc4";
        // replace with your URL
        private string cognitiveServicesUrl = "https://readmyimage.cognitiveservices.azure.com/";
        string location = "southeastasia";

        public List<ImageRegionDetails> ProcessImage(string filepath = @"G:\demo.PNG")
        {
            var client = new ComputerVisionClient(new ApiKeyServiceClientCredentials(cognitiveServicesKey));
            client.Endpoint = cognitiveServicesUrl;

            var fileToProcess = File.OpenRead(filepath);
            var apiResult = client.RecognizePrintedTextInStreamAsync(false,
                                                                    fileToProcess);
            apiResult.Wait();

            var ocrResult = apiResult.Result;
            var t = ocrResult.Regions;
            List<ImageRegionDetails> ls = new List<ImageRegionDetails>();
            foreach (var r in ocrResult.Regions)
            {
                foreach (var l in r.Lines)
                {
                    foreach (var w in l.Words)
                    {
                        ImageRegionDetails imageRegionDetails = new ImageRegionDetails();
                        imageRegionDetails.Text = w.Text;
                        var coordinates = w.BoundingBox.Split(',');
                        imageRegionDetails.Coordinate1 = int.Parse(coordinates[0]);
                        imageRegionDetails.Coordinate2 = int.Parse(coordinates[1]);
                        imageRegionDetails.Coordinate3 = int.Parse(coordinates[2]);
                        imageRegionDetails.Coordinate4 = int.Parse(coordinates[3]);
                        ls.Add(imageRegionDetails);
                    }
                }
            }
            return ls;
        }
    }
}
