using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Yolov8_NetFW;
using Yolov8_NetFW.Metadata;
using Yolov8_NetFW.Plotting;
using SixLabors.ImageSharp;
using Emgu.CV.Structure;
using Emgu.CV;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices.WindowsRuntime;
using SixLabors.ImageSharp.Formats.Bmp;
using SixLabors.ImageSharp.Processing;
using Emgu.CV.Plot;
using System.Security.Cryptography;
using static System.Net.Mime.MediaTypeNames;
using Yolov8_NetFW.Data;
using System.Diagnostics;
using Emgu.CV.CvEnum;
using Emgu.CV.Rapid;
//using Microsoft.ML.OnnxRuntime.Tensors;

namespace GUI_YOLOv8
{
    public partial class Form1 : Form
    {
        VideoCapture capture;
        bool pause = false;
        //static string model_path_dect = @"C:\Code\yolov8onnxNetFramework\Yolov8_NetFW\Yolov8_NetFW\models\yolov8s.onnx";
        //static YoloV8 predictor_dect = new YoloV8(model_path_dect);

        //static string model_path_pose = @"C:\Code\yolov8onnxNetFramework\Yolov8_NetFW\Yolov8_NetFW\models\yolov8s-pose.onnx";
        //static YoloV8 predictor_pose = new YoloV8(model_path_pose);

        //static string model_path_seg = @"C:\Code\yolov8onnxNetFramework\Yolov8_NetFW\Yolov8_NetFW\models\yolov8s-seg.onnx";
        //static YoloV8 predictor_seg = new YoloV8(model_path_seg);
        public Form1()
        {
            InitializeComponent();
        }

        //private void button1_Click(object sender, EventArgs e)
        //{
        //    using (OpenFileDialog openFileDialog = new OpenFileDialog())
        //    {
        //        openFileDialog.Filter = "Image Files (*.jpg;*.jpeg;*.png;*.gif;*.bmp)|*.jpg;*.jpeg;*.png;*.gif;*.bmp|All files (*.*)|*.*";
        //        openFileDialog.Multiselect = false;

        //        if (openFileDialog.ShowDialog() == DialogResult.OK)
        //        {
        //            string selectedFile = openFileDialog.FileName;

        //            try
        //            {

        //                Bitmap org = new Bitmap(openFileDialog.FileName);

        //                var output = "../../assets/output";
        //                if (Directory.Exists(output) == false)
        //                    Directory.CreateDirectory(output);
        //                string img_path = @"../../assets/input/sports.jpg";
        //                string full_path = Path.GetFullPath(img_path);
        //                string model_path = @"C:\Code\yolov8onnxNetFramework\Yolov8_NetFW\Yolov8_NetFW\models\yolov8s.onnx";
        //                Console.WriteLine();
        //                Console.WriteLine("================ CLASSIFICATION DEMO ================");
        //                Console.WriteLine();

        //                Console.WriteLine("Loading model...");
        //                var predictor = new YoloV8(model_path);

        //                Console.WriteLine("Working... ({0})", openFileDialog.FileName);
        //                var result = predictor.Detect(openFileDialog.FileName);
        //                predictor.Dispose();

        //                Console.WriteLine();

        //                Console.WriteLine($"Result: {result}");
        //                Console.WriteLine($"Speed: {result.Speed}");

        //                Console.WriteLine();

        //                Console.WriteLine("Plotting and saving...");
        //                var origin = SixLabors.ImageSharp.Image.Load(openFileDialog.FileName);

        //                var ploted = result.PlotImage(origin);

        //                var pathToSave = Path.Combine(output, Path.GetFileName(openFileDialog.FileName));
        //                var fullname = Path.GetFullPath(pathToSave);
        //                pictureBox1.Image = org;
        //                ploted.Save(pathToSave);
        //                origin.Dispose(); ploted.Dispose();
        //                Console.WriteLine();


        //            }
        //            catch (Exception ex)
        //            {
        //                MessageBox.Show($"Error loading image: {ex.Message}");
        //            }
        //        }
        //    }
        //}

        private void rb_cls_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void rb_dt_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void rb_sm_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void fileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = "Image Files (*.jpg;*.jpeg;*.png;*.gif;*.bmp)|*.jpg;*.jpeg;*.png;*.gif;*.bmp|All files (*.*)|*.*";
                openFileDialog.Multiselect = false;

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    string selectedFile = openFileDialog.FileName;

                    try
                    {
                        var img_org = SixLabors.ImageSharp.Image.Load(openFileDialog.FileName);
                        int img_org_w = img_org.Width, img_org_h = img_org.Height;
                        var resize = get_size(img_org_w, img_org_h);
                        img_org.Mutate(x => x.Resize(resize.Width, resize.Height));
                        pictureBox1.Size = resize;
                        pictureBox1.Image = ImageSharpToBitmap(img_org);
                        img_org.Dispose();
                        if (rb_cls.Checked)
                        {
                            Console.WriteLine(rb_cls.Text.ToString());
                            string model_path = @"C:\Code\yolov8onnxNetFramework\Yolov8_NetFW\Yolov8_NetFW\models\yolov8s-cls.onnx";
                            classifi(model_path ,openFileDialog.FileName);
                        }
                        else if (rb_dt.Checked)
                        {
                            Console.WriteLine(rb_dt.Text.ToString());
                            string model_path = @"C:\Code\yolov8onnxNetFramework\Yolov8_NetFW\Yolov8_NetFW\models\yolov8s.onnx";
                            detect(model_path, openFileDialog.FileName);
                        }
                        else if (rb_sm.Checked)
                        {
                            Console.WriteLine(rb_sm.Text.ToString());
                            string model_path = @"C:\Code\yolov8onnxNetFramework\Yolov8_NetFW\Yolov8_NetFW\models\yolov8s-seg.onnx";
                            segment(model_path, openFileDialog.FileName);
                        }
                        else if (rb_pose.Checked) {
                            Console.WriteLine(rb_pose.Text.ToString());
                            string model_path = @"C:\Code\yolov8onnxNetFramework\Yolov8_NetFW\Yolov8_NetFW\models\yolov8s-pose.onnx";
                            pose(model_path, openFileDialog.FileName);
                        }

                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Error loading image: {ex.Message}");
                    }
                }

            }
        }

        private void classifi(string model_path, string image_path)
        {
            
            Console.WriteLine();
            Console.WriteLine("================ CLASSIFICATION DEMO ================");
            Console.WriteLine();

            Console.WriteLine("Loading model...");
            var predictor = new YoloV8(model_path);

            Console.WriteLine("Working... ({0})", image_path);
            var result = predictor.Classify(image_path);
            predictor.Dispose();

            Console.WriteLine();

            Console.WriteLine($"Result: {result}");
            Console.WriteLine($"Speed: {result.Speed}");

            Console.WriteLine();

            Console.WriteLine("Plotting and saving...");
            var origin = SixLabors.ImageSharp.Image.Load(image_path);

            var ploted = result.PlotImage(origin);
            string path_save_parent = Directory.GetParent(image_path).FullName;
            var pathToSave = Path.Combine(path_save_parent, "result_" + Path.GetFileName(image_path));
            var fullname = Path.GetFullPath(pathToSave);
            ploted.Save(pathToSave);
            var resize = get_size(ploted.Width, ploted.Height);
            ploted.Mutate(x => x.Resize(resize.Width, resize.Height));
            pictureBox2.Image = ImageSharpToBitmap(ploted);
            origin.Dispose(); ploted.Dispose();
            Console.WriteLine();
        }
        private void detect(string model_path, string image_path)
        {

            Console.WriteLine();
            Console.WriteLine("================ DETECT DEMO ================");
            Console.WriteLine();

            Console.WriteLine("Loading model...");
            var predictor = new YoloV8(model_path);

            Console.WriteLine("Working... ({0})", image_path);
            var result = predictor.Detect(image_path);
            predictor.Dispose();

            Console.WriteLine();

            Console.WriteLine($"Result: {result}");
            Console.WriteLine($"Speed: {result.Speed}");

            Console.WriteLine();

            Console.WriteLine("Plotting and saving...");
            var origin = SixLabors.ImageSharp.Image.Load(image_path);

            var ploted = result.PlotImage(origin);
            string path_save_parent = Directory.GetParent(image_path).FullName;
            var pathToSave = Path.Combine(path_save_parent, "result_" + Path.GetFileName(image_path));
            var fullname = Path.GetFullPath(pathToSave);
            ploted.Save(pathToSave);
            var resize = get_size(ploted.Width, ploted.Height);
            ploted.Mutate(x => x.Resize(resize.Width, resize.Height));
            pictureBox2.Image = ImageSharpToBitmap(ploted);
            origin.Dispose(); ploted.Dispose();
            Console.WriteLine();
        }
        private SixLabors.ImageSharp.Image detect(string model_path, SixLabors.ImageSharp.Image image)
        {

            Console.WriteLine();
            Console.WriteLine("================ DETECT DEMO ================");
            Console.WriteLine();

            Console.WriteLine("Loading model...");

            var predictor = new YoloV8(model_path);
            var result = predictor.Detect(image);
            predictor.Dispose();

            Console.WriteLine($"Result: {result}");
            Console.WriteLine($"Speed: {result.Speed}");

            Console.WriteLine("Plotting and saving...");

            var ploted = result.PlotImage(image);
            var resize = get_size(ploted.Width, ploted.Height);
            ploted.Mutate(x => x.Resize(resize.Width, resize.Height));
            return ploted;

        }
        //private SixLabors.ImageSharp.Image detect(YoloV8 predictor_dect, SixLabors.ImageSharp.Image image)
        //{

        //    Console.WriteLine();
        //    Console.WriteLine("================ DETECT DEMO ================");
        //    Console.WriteLine();

        //    Console.WriteLine("Loading model...");
        //    var result = predictor_dect.Detect(image);

        //    Console.WriteLine();

        //    Console.WriteLine($"Result: {result}");
        //    Console.WriteLine($"Speed: {result.Speed}");

        //    Console.WriteLine();

        //    Console.WriteLine("Plotting and saving...");


        //    var ploted = result.PlotImage(image);

        //    var resize = get_size(ploted.Width, ploted.Height);
        //    ploted.Mutate(x => x.Resize(resize.Width, resize.Height));

        //    return ploted;

        //}
        private SixLabors.ImageSharp.Image segment(YoloV8 predictor_seg, SixLabors.ImageSharp.Image image)
        {

            Console.WriteLine();
            Console.WriteLine("================ DETECT DEMO ================");
            Console.WriteLine();

            Console.WriteLine("Loading model...");
            var result = predictor_seg.Segment(image);

            Console.WriteLine();

            Console.WriteLine($"Result: {result}");
            Console.WriteLine($"Speed: {result.Speed}");

            Console.WriteLine();

            Console.WriteLine("Plotting and saving...");


            var ploted = result.PlotImage(image);

            var resize = get_size(ploted.Width, ploted.Height);
            ploted.Mutate(x => x.Resize(resize.Width, resize.Height));

            return ploted;

        }
        private ISegmentationResult segment(YoloV8 predictor_seg, byte[] image)
        {

            Console.WriteLine();
            Console.WriteLine("================ SEGMENT DEMO ================");
            Console.WriteLine();

            Console.WriteLine("Loading model...");
            var result = predictor_seg.Segment(image);
            return result;
            //Console.WriteLine();

            //Console.WriteLine($"Result: {result}");
            //Console.WriteLine($"Speed: {result.Speed}");

            //Console.WriteLine();

            //Console.WriteLine("Plotting and saving...");


            //var ploted = result.PlotImage(image);

            //var resize = get_size(ploted.Width, ploted.Height);
            //ploted.Mutate(x => x.Resize(resize.Width, resize.Height));

            //return ploted;

        }
        private SixLabors.ImageSharp.Image pose(YoloV8 predictor_pose, SixLabors.ImageSharp.Image image)
        {

            Console.WriteLine();
            Console.WriteLine("================ DETECT DEMO ================");
            Console.WriteLine();

            Console.WriteLine("Loading model...");
            var result = predictor_pose.Pose(image);

            Console.WriteLine();

            Console.WriteLine($"Result: {result}");
            Console.WriteLine($"Speed: {result.Speed}");

            Console.WriteLine();

            Console.WriteLine("Plotting and saving...");


            var ploted = result.PlotImage(image);

            var resize = get_size(ploted.Width, ploted.Height);
            ploted.Mutate(x => x.Resize(resize.Width, resize.Height));

            return ploted;

        }
        private IPoseResult pose(YoloV8 predictor_pose, byte[] image)
        {

            Console.WriteLine();
            Console.WriteLine("================ DETECT DEMO ================");
            Console.WriteLine();

            Console.WriteLine("Loading model...");
            var result = predictor_pose.Pose(image);
            return result;
            //Console.WriteLine();

            //Console.WriteLine($"Result: {result}");
            //Console.WriteLine($"Speed: {result.Speed}");

            //Console.WriteLine();

            //Console.WriteLine("Plotting and saving...");


            //var ploted = result.PlotImage(image);

            //var resize = get_size(ploted.Width, ploted.Height);
            //ploted.Mutate(x => x.Resize(resize.Width, resize.Height));

            //return ploted;

        }
        private IDetectionResult detect(YoloV8 predictor_dect, SixLabors.ImageSharp.Image image)
        {

            Console.WriteLine();
            Console.WriteLine("================ DETECT DEMO ================");
            Console.WriteLine();

            Console.WriteLine("Loading model...");
            var result = predictor_dect.Detect(image);
            Console.WriteLine();

            Console.WriteLine($"Result: {result}");
            Console.WriteLine($"Speed: {result.Speed}");
            return result;
        }
        private IDetectionResult detect(YoloV8 predictor_dect,byte[] image)
        {

            Console.WriteLine();
            Console.WriteLine("================ DETECT DEMO ================");
            Console.WriteLine();

            Console.WriteLine("Loading model...");
            var result = predictor_dect.Detect(image);
            Console.WriteLine();

            Console.WriteLine($"Result: {result}");
            Console.WriteLine($"Speed: {result.Speed}");
            return result;
        }
        private void segment(string model_path, string image_path)
        {

            Console.WriteLine();
            Console.WriteLine("================ SEGMENT DEMO ================");
            Console.WriteLine();

            Console.WriteLine("Loading model...");
            var predictor = new YoloV8(model_path);

            Console.WriteLine("Working... ({0})", image_path);
            var result = predictor.Segment(image_path);
            predictor.Dispose();

            Console.WriteLine();

            Console.WriteLine($"Result: {result}");
            Console.WriteLine($"Speed: {result.Speed}");

            Console.WriteLine();

            Console.WriteLine("Plotting and saving...");
            var origin = SixLabors.ImageSharp.Image.Load(image_path);

            var ploted = result.PlotImage(origin);
            string path_save_parent = Directory.GetParent(image_path).FullName;
            var pathToSave = Path.Combine(path_save_parent, "result_" + Path.GetFileName(image_path));
            var fullname = Path.GetFullPath(pathToSave);
            ploted.Save(pathToSave);
            var resize = get_size(ploted.Width, ploted.Height);
            ploted.Mutate(x => x.Resize(resize.Width, resize.Height));
            pictureBox2.Image = ImageSharpToBitmap(ploted);
            origin.Dispose(); ploted.Dispose();
            Console.WriteLine();
        }
        private void pose(string model_path, string image_path)
        {

            Console.WriteLine();
            Console.WriteLine("================ POSE DEMO ================");
            Console.WriteLine();

            Console.WriteLine("Loading model...");
            var predictor = new YoloV8(model_path);

            Console.WriteLine("Working... ({0})", image_path);
            var result = predictor.Pose(image_path);
            predictor.Dispose();

            Console.WriteLine();

            Console.WriteLine($"Result: {result}");
            Console.WriteLine($"Speed: {result.Speed}");

            Console.WriteLine();

            Console.WriteLine("Plotting and saving...");
            var origin = SixLabors.ImageSharp.Image.Load(image_path);

            var ploted = result.PlotImage(origin);
            string path_save_parent = Directory.GetParent(image_path).FullName;
            var pathToSave = Path.Combine(path_save_parent, "result_" + Path.GetFileName(image_path));
            var fullname = Path.GetFullPath(pathToSave);
            ploted.Save(pathToSave);
            var resize = get_size(origin.Width, origin.Height);
            ploted.Mutate(x => x.Resize(resize.Width, resize.Height));
            pictureBox2.Image = ImageSharpToBitmap(ploted);
            origin.Dispose(); ploted.Dispose();
            Console.WriteLine();
        }
        private SixLabors.ImageSharp.Image pose(string model_path, SixLabors.ImageSharp.Image image)
        {

            Console.WriteLine();
            Console.WriteLine("================ POSE DEMO ================");
            Console.WriteLine();

            Console.WriteLine("Loading model...");
            var predictor = new YoloV8(model_path);
            var result = predictor.Pose(image);
            predictor.Dispose();

            Console.WriteLine();

            Console.WriteLine($"Result: {result}");
            Console.WriteLine($"Speed: {result.Speed}");

            Console.WriteLine();

            Console.WriteLine("Plotting and saving...");
            var ploted = result.PlotImage(image);

            var resize = get_size(image.Width, image.Height);
            ploted.Mutate(x => x.Resize(resize.Width, resize.Height));
            image.Dispose();
            return ploted;
        }
        //private SixLabors.ImageSharp.Image segment(string model_path, SixLabors.ImageSharp.Image image)
        //{

        //    Console.WriteLine();
        //    Console.WriteLine("================ POSE DEMO ================");
        //    Console.WriteLine();

        //    Console.WriteLine("Loading model...");
        //    var predictor = new YoloV8(model_path);

        //    //Console.WriteLine("Working... ({0})", image_path);
        //    var result = predictor.Segment(image);
        //    predictor.Dispose();

        //    Console.WriteLine();

        //    Console.WriteLine($"Result: {result}");
        //    Console.WriteLine($"Speed: {result.Speed}");

        //    Console.WriteLine();

        //    Console.WriteLine("Plotting and saving...");
        //    //var origin = SixLabors.ImageSharp.Image.Load(image_path);

        //    var ploted = result.PlotImage(image);

        //    var resize = get_size(image.Width, image.Height);
        //    ploted.Mutate(x => x.Resize(resize.Width, resize.Height));
        //    //pictureBox2.Image = ImageSharpToBitmap(ploted);
        //    image.Dispose(); //ploted.Dispose();
        //    Console.WriteLine();
        //    return ploted;
        //}
        public static Bitmap ImageSharpToBitmap(SixLabors.ImageSharp.Image image)
        {
            if(image == null) return new Bitmap(0,0);
            var stream = new MemoryStream();
            image.Save(stream, BmpFormat.Instance);
            stream.Position = 0;
            return new Bitmap(stream);
        }
        private System.Drawing.Size get_size(int img_org_w, int img_org_h)
        {
            double ratio = 0;
            if (img_org_w > img_org_h)
            {
                ratio = img_org_w / pictureBox1.Width;
                img_org_w = pictureBox1.Width;
                img_org_h = (int)(img_org_h / ratio);
            }
            else
            {
                ratio = img_org_h / pictureBox1.Height;
                img_org_h = pictureBox1.Height;
                img_org_w = (int)(img_org_w / ratio);
            }
            return new System.Drawing.Size(img_org_w,img_org_h);
        }

        private void ts_image_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = "Image Files (*.jpg;*.jpeg;*.png;*.gif;*.bmp)|*.jpg;*.jpeg;*.png;*.gif;*.bmp|All files (*.*)|*.*";
                openFileDialog.Multiselect = false;

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    string selectedFile = openFileDialog.FileName;

                    try
                    {
                        var img_org = SixLabors.ImageSharp.Image.Load(openFileDialog.FileName);
                        int img_org_w = img_org.Width, img_org_h = img_org.Height;
                        var resize = get_size(img_org_w, img_org_h);
                        img_org.Mutate(x => x.Resize(resize.Width, resize.Height));
                        pictureBox1.Size = resize;
                        pictureBox1.Image = ImageSharpToBitmap(img_org);
                        img_org.Dispose();
                        if (rb_cls.Checked)
                        {
                            Console.WriteLine(rb_cls.Text.ToString());
                            string model_path = @"C:\Code\yolov8onnxNetFramework\Yolov8_NetFW\Yolov8_NetFW\models\yolov8s-cls.onnx";
                            classifi(model_path, openFileDialog.FileName);
                        }
                        else if (rb_dt.Checked)
                        {
                            Console.WriteLine(rb_dt.Text.ToString());
                            string model_path = @"C:\Code\yolov8onnxNetFramework\Yolov8_NetFW\Yolov8_NetFW\models\yolov8s.onnx";
                            detect(model_path, openFileDialog.FileName);
                        }
                        else if (rb_sm.Checked)
                        {
                            Console.WriteLine(rb_sm.Text.ToString());
                            string model_path = @"C:\Code\yolov8onnxNetFramework\Yolov8_NetFW\Yolov8_NetFW\models\yolov8s-seg.onnx";
                            segment(model_path, openFileDialog.FileName);
                        }
                        else if (rb_pose.Checked)
                        {
                            Console.WriteLine(rb_pose.Text.ToString());
                            string model_path = @"C:\Code\yolov8onnxNetFramework\Yolov8_NetFW\Yolov8_NetFW\models\yolov8s-pose.onnx";
                            pose(model_path, openFileDialog.FileName);
                        }

                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Error loading image: {ex.Message}");
                    }
                }

            }
        }
        string file_choose = "";
        private void ts_video_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Video Files|*.mp4;*.avi";
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                file_choose = openFileDialog.FileName;
                capture = new VideoCapture(openFileDialog.FileName);
                Mat m = new Mat();
                capture.Read(m);
                var resize = get_size(m.Width, m.Height);
                CvInvoke.Resize(m, m, resize);
                pictureBox1.Image = m.ToBitmap();
            }
        }

        private void ts_video_stop_Click(object sender, EventArgs e)
        {

        }

        private void ts_video_pause_Click(object sender, EventArgs e)
        {
            pause = !pause;
        }
        public static byte[] ImageToByte(System.Drawing.Image img)
        {
            ImageConverter converter = new ImageConverter();
            return (byte[])converter.ConvertTo(img, typeof(byte[]));    
        }
        public static byte[] ImageToByte(SixLabors.ImageSharp.Image img)
        {
            ImageConverter converter = new ImageConverter();
            return (byte[])converter.ConvertTo(img, typeof(byte[]));
        }
        static YoloV8 predictor_dect = new YoloV8(@"C:\Code\yolov8onnxNetFramework\Yolov8_NetFW\Yolov8_NetFW\models\yolov8s.onnx");
        static YoloV8 predictor_pose = new YoloV8(@"C:\Code\yolov8onnxNetFramework\Yolov8_NetFW\Yolov8_NetFW\models\yolov8s-pose.onnx");
        static YoloV8 predictor_seg = new YoloV8(@"C:\Code\yolov8onnxNetFramework\Yolov8_NetFW\Yolov8_NetFW\models\yolov8s-seg.onnx");

        private async void ts_video_play_Click(object sender, EventArgs e)
        {
            if (capture == null)
            {
                return;
            }

            try
            {
                //var model_path = @"C:\Code\yolov8onnxNetFramework\Yolov8_NetFW\Yolov8_NetFW\models\yolov8s.onnx";
                string video_save_path = Path.Combine(Directory.GetParent(file_choose).ToString(), Path.GetFileNameWithoutExtension(file_choose) + "rs.mp4");
                VideoWriter videoWriter = new VideoWriter(video_save_path,25,new System.Drawing.Size(capture.Width,capture.Height),true);
                while (!pause)
                {
                    Mat m = new Mat();
                    Mat m_clone = new Mat();
                    //var m_clone;
                    capture.Read(m);
                    m.CopyTo(m_clone);
                    
                    var resize = get_size(m.Width, m.Height);
                    //Image<Bgr,Byte> img_org = capture.QueryFrame().ToImage<Bgr,Byte>();

                    if (!m.IsEmpty)
                    {
                        SixLabors.ImageSharp.Image rs_img;
                        //CvInvoke.Resize(img_org,img_org,resize);
                        //var bitmap = m.ToBitmap();
                        var imagedatabytes = ImageToByte(m.ToBitmap());
                        //// sua ham detect argument is image and return image de load len tren picture box
                        var new_img = SixLabors.ImageSharp.Image.Load(imagedatabytes);
                        if (rb_dt.Checked)
                        {
                            //string model_path = @"C:\Code\yolov8onnxNetFramework\Yolov8_NetFW\Yolov8_NetFW\models\yolov8s.onnx";
                            //rs_img = detect(model_path,new_img);
                            //rs_img = detect(predictor_dect, new_img);
                            //var boxes = detect(predictor_dect, new_img);
                            var boxes = detect(predictor_dect, imagedatabytes);

                            foreach (var box in boxes.Boxes)
                            {
                                var rect = new System.Drawing.Rectangle();
                                rect.X = box.Bounds.X; rect.Y = box.Bounds.Y;
                                rect.Width = box.Bounds.Width; rect.Height = box.Bounds.Height;
                                CvInvoke.Rectangle(m, rect, new MCvScalar(0, 0, 255), 2, Emgu.CV.CvEnum.LineType.Filled);
                                CvInvoke.PutText(m, box.Class.Name, new System.Drawing.Point(rect.X, rect.Y), Emgu.CV.CvEnum.FontFace.HersheySimplex, 1, new MCvScalar(0, 0, 255), 2);
                                //m.Save("save.jpg");
                            }
                            videoWriter.Write(m);
                            CvInvoke.Resize(m, m, resize);
                            pictureBox2.Image = m.ToBitmap();//ImageSharpToBitmap(rs_img);
                           // rs_img.Dispose();
                        }
                        else if (rb_sm.Checked)
                        {
                            //string model_path = @"C:\Code\yolov8onnxNetFramework\Yolov8_NetFW\Yolov8_NetFW\models\yolov8s-seg.onnx";
                            rs_img = segment(predictor_seg, new_img);
                            //Image<Bgr, byte> process = m.ConvertTo(;
                            //var seg_results = segment(predictor_seg, imagedatabytes);
                            //Image<Bgr, byte> process = m.ToImage<Bgr, Byte>().CopyBlank();
                            //foreach (var box in seg_results.Boxes)
                            //{
                            //    Mat maskMat = new Mat(box.Mask.Height, box.Mask.Width, DepthType.Cv8U, 1);
                            //    //Image<Gray, byte> maskMat = new Image<Gray, byte>(box.Mask.Width, box.Mask.Height);

                            //    for (int x = 0; x < box.Mask.Width; x++)
                            //    {
                            //        for (int y = 0; y < box.Mask.Height; y++)
                            //        {
                            //            var value = box.Mask[x, y];
                            //            maskMat.SetTo(new MCvScalar(value > .5F ? 255 : 0), null);

                            //        }
                            //    }
                            //    maskMat.Save("mask.jpg");
                            //    // Resize the mask if needed
                            //    if (box.Bounds.Width != box.Mask.Width || box.Bounds.Height != box.Mask.Height)
                            //    {
                            //        CvInvoke.Resize(maskMat, maskMat, new System.Drawing.Size(box.Bounds.Width, box.Bounds.Height));
                            //    }
                            //    // Convert the mask to Bgr format for overlay
                            //    //Mat maskBgrMat = new Mat(new System.Drawing.Size(box.Mask.Height, box.Mask.Width),DepthType.Cv8U,3);
                            //    //CvInvoke.CvtColor(maskMat, maskBgrMat, ColorConversion.Gray2Bgr);

                            //    // Overlay the mask onto the original image
                            //    //Mat overlayMat = new Mat();
                            //    //CvInvoke.AddWeighted(m, 1, maskBgrMat, 0.4, 0, overlayMat);
                            //    // Overlay the mask onto the original image
                            //    //Mat overlayMat = new Mat();
                            //    //process.SetValue(new Bgr(0,0,255),maskMat);
                            //    //CvInvoke.AddWeighted(process.Mat, 1, maskBgrMat, 0.4, 0, overlayMat);

                            //    //process = overlayMat.ToImage<Bgr, byte>();
                            //    //m = overlayMat;

                            //    // Draw bounding box
                            //    CvInvoke.Rectangle(process, new System.Drawing.Rectangle(box.Bounds.X, box.Bounds.Y, box.Bounds.Width, box.Bounds.Height), new MCvScalar(0, 0, 255), 2);

                            //    // Draw label
                            //    var label = $"{box.Class.Name} {box.Confidence:N}";
                            //    CvInvoke.PutText(process, label, new System.Drawing.Point(box.Bounds.Left, box.Bounds.Top), FontFace.HersheySimplex, 1, new MCvScalar(0, 0, 255), 2);
                            //}
                            //pictureBox2.Image = m.ToBitmap();//ImageSharpToBitmap(rs_img);
                            //rs_img.Dispose();

                        }
                        else if (rb_pose.Checked)
                        {
                            //string model_path = @"C:\Code\yolov8onnxNetFramework\Yolov8_NetFW\Yolov8_NetFW\models\yolov8s-pose.onnx";
                            //rs_img = pose(model_path, new_img);
                            var pose_results = pose(predictor_pose, imagedatabytes);
                            //pictureBox2.Image = ImageSharpToBitmap(rs_img);
                            //rs_img.Dispose();

                        }
                        else
                        {
                            MessageBox.Show("Please choose mode Detect or Segment or Pose!");
                            return;
                        }
                        //var rs_img = detect(model_path, new_img);
                        CvInvoke.Resize(m_clone, m_clone, resize);
                        pictureBox1.Image = m_clone.ToBitmap();
                        //bitmap.Dispose();
                        m_clone.Dispose();
                        m.Dispose();
                        //new_img.Dispose();
                        double fps = capture.Get(Emgu.CV.CvEnum.CapProp.Fps);
                        Console.WriteLine($"fps: {fps}");
                        await Task.Delay(1000 / (Convert.ToInt32(fps)));
                    }
                    else
                    {
                        break;
                    }
                }
                videoWriter.Dispose();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message );
                MessageBox.Show(ex.Message);
            }
        }
    }
}
