using AForge.Imaging;
using AForge.Imaging.Filters;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POCBlobAForge
{
    class Program
    {
        private static string folder = @"C:\Users\rodol\Documents\Projetos\tcc\POC\POCBlobAForge\POCBlobAForge\";

        static void Main(string[] args)
        {
            try
            {
                Bitmap image = new Bitmap(String.Format("{0}{1}", folder, "mao01.jpg"));

                Grayscale grayscaleFilter = new Grayscale(0.2125, 0.7154, 0.0721);
                Bitmap grayImage = grayscaleFilter.Apply(image);

                HomogenityEdgeDetector homogenityEdgeDetectorFilter = new HomogenityEdgeDetector();
                homogenityEdgeDetectorFilter.ApplyInPlace(grayImage);

                Threshold filter2 = new Threshold(100);
                // apply the filter
                filter2.ApplyInPlace(grayImage);

                // create filter
                ExtractBiggestBlob filter = new ExtractBiggestBlob();
                // apply the filter
                Bitmap biggestBlobsImage = filter.Apply(grayImage);
                biggestBlobsImage.Save(String.Format("{0}{1}", folder, "output.jpg"));

                Process.Start(String.Format("{0}{1}", folder, "output.jpg"));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            Console.ReadKey();
        }
    }
}
