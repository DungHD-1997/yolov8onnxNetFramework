using SixLabors.ImageSharp;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yolov8_NetFW.Metadata;
using Yolov8_NetFW.Plotting;


namespace Yolov8_NetFW
{
    internal class Program
    {
        private static string output = "../../assets/output";
        static async Task PoseDemo(string image, string model)
        {
            Console.WriteLine();
            Console.WriteLine("================ POSE DEMO ================");
            Console.WriteLine();

            Console.WriteLine("Loading model...");
            var predictor = new YoloV8(model);
            Console.WriteLine("Working...");
            var result =  predictor.PoseAsync(image).GetAwaiter().GetResult();

            Console.WriteLine();
            predictor.Dispose();
            Console.WriteLine($"Result: {result}");
            Console.WriteLine($"Speed: {result.Speed}");

            Console.WriteLine();

            Console.WriteLine("Plotting and saving...");
            var origin = Image.Load(image);

            var ploted = await result.PlotImageAsync(origin);

            var pathToSave = Path.Combine(output, Path.GetFileName(image));

            ploted.Save(pathToSave);
            origin.Dispose();
            ploted.Dispose();
        }
        static async Task DetectDemo(string image, string model)
        {
            Console.WriteLine();
            Console.WriteLine("================ DETECTION DEMO ================");
            Console.WriteLine();

            Console.WriteLine("Loading model...");
            var predictor = new YoloV8(model);

            Console.WriteLine("Working...");
            var result = await predictor.DetectAsync(image);

            Console.WriteLine();
            predictor.Dispose();
            Console.WriteLine($"Result: {result}");
            Console.WriteLine($"Speed: {result.Speed}");

            Console.WriteLine();

            Console.WriteLine("Plotting and saving...");
            var origin = Image.Load(image);

            var ploted = await result.PlotImageAsync(origin);

            var pathToSave = Path.Combine(output, Path.GetFileName(image));

            ploted.Save(pathToSave);
            origin.Dispose(); ploted.Dispose();
        }

        public static async Task SegmentDemo(string image, string model)
        {
            Console.WriteLine();
            Console.WriteLine("================ SEGMENTATION DEMO ================");
            Console.WriteLine();

            Console.WriteLine("Loading model...");
            var predictor = new YoloV8(model);
            Console.WriteLine("Working...");
            var result = await predictor.SegmentAsync(image);
            predictor.Dispose();

            Console.WriteLine();

            Console.WriteLine($"Result: {result}");
            Console.WriteLine($"Speed: {result.Speed}");

            Console.WriteLine();

            Console.WriteLine("Plotting and saving...");
            var origin = Image.Load(image);

            var ploted = await result.PlotImageAsync(origin, new SegmentationPlottingOptions { MaskConfidence = .65F });

            var filename = $"{Path.GetFileNameWithoutExtension(image)}_seg";
            var extension = Path.GetExtension(image);

            var pathToSave = Path.Combine(output, filename + extension);

            ploted.Save(pathToSave);
            origin.Dispose(); ploted.Dispose();
        }

        static async Task ClassifyDemo(string[] images, string model)
        {
            Console.WriteLine();
            Console.WriteLine("================ CLASSIFICATION DEMO ================");
            Console.WriteLine();

            Console.WriteLine("Loading model...");
            var predictor = new YoloV8(model);
            foreach (var image in images)
            {
                Console.WriteLine("Working... ({0})", image);
                var result = await predictor.ClassifyAsync(image);
                predictor.Dispose();

                Console.WriteLine();

                Console.WriteLine($"Result: {result}");
                Console.WriteLine($"Speed: {result.Speed}");

                Console.WriteLine();

                Console.WriteLine("Plotting and saving...");
                var origin = Image.Load(image);

                var ploted = await result.PlotImageAsync(origin);

                var pathToSave = Path.Combine(output, Path.GetFileName(image));

                ploted.Save(pathToSave);
                origin.Dispose(); ploted.Dispose();
                Console.WriteLine();
            }
        }
        static void Main(string[] args)
        {
            var output = "../../assets/output";
            if (Directory.Exists(output) == false)
                Directory.CreateDirectory(output);
            string img_path = @"../../assets/input/sports.jpg";
            string full_path = Path.GetFullPath(img_path);
            string model_path = "../../models/yolov8s-pose.onnx";
            PoseDemo(img_path, model_path).GetAwaiter().GetResult();

            //DetectDemo(img_path, model_path).GetAwaiter().GetResult(); 

            //SegmentDemo(img_path, model_path).GetAwaiter().GetResult();
            //string[] img_cls = { @"C:\Code\Yolov8_NetFW\Yolov8_NetFW\assets\input\toaster.jpg" };
            //ClassifyDemo(img_cls, model_path).GetAwaiter().GetResult();
        }

    }
}
