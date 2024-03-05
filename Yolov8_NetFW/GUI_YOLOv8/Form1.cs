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
//using Microsoft.ML.OnnxRuntime.Tensors;

namespace GUI_YOLOv8
{
    public partial class Form1 : Form
    {
        VideoCapture capture;
        bool pause = false;
        static string model_path_dect = @"C:\Code\yolov8onnxNetFramework\Yolov8_NetFW\Yolov8_NetFW\models\yolov8s.onnx";
        static YoloV8 predictor_dect = new YoloV8(model_path_dect);

        static string model_path_pose = @"C:\Code\yolov8onnxNetFramework\Yolov8_NetFW\Yolov8_NetFW\models\yolov8s-pose.onnx";
        static YoloV8 predictor_pose = new YoloV8(model_path_pose);

        static string model_path_seg = @"C:\Code\yolov8onnxNetFramework\Yolov8_NetFW\Yolov8_NetFW\models\yolov8s-seg.onnx";
        static YoloV8 predictor_seg = new YoloV8(model_path_seg);
        public Form1()
        {
            InitializeComponent();


                        //string image_path = @"C:\Code\YOLOv8-GUI\data\bus.jpg";
                        //Console.WriteLine("Working... ({0})", image_path);
                        //var result = predictor_dect.Detect(image_path);
                        //predictor_dect.Dispose();

                        //Console.WriteLine();

                        //Console.WriteLine($"Result: {result}");
                        //Console.WriteLine($"Speed: {result.Speed}");

                        //Console.WriteLine();

                        //Console.WriteLine("Plotting and saving...");
                        //var origin = SixLabors.ImageSharp.Image.Load(image_path);

                        //var ploted = result.PlotImage(origin);
                        ////string path_save_parent = Directory.GetParent(image_path).FullName;
                        ////var pathToSave = Path.Combine(path_save_parent, "result_" + Path.GetFileName(image_path));
                        ////var fullname = Path.GetFullPath(pathToSave);
                        ////ploted.Save(pathToSave);
                        //var resize = get_size(ploted.Width, ploted.Height);
                        //ploted.Mutate(x => x.Resize(resize.Width, resize.Height));
                        ////pictureBox2.Image = ImageSharpToBitmap(ploted);
                        //origin.Dispose(); ploted.Dispose();
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
            //Console.WriteLine("Working... ({0})", image_path);
            var result = predictor.Detect(image);
            predictor.Dispose();

            Console.WriteLine();

            Console.WriteLine($"Result: {result}");
            Console.WriteLine($"Speed: {result.Speed}");

            Console.WriteLine();

            Console.WriteLine("Plotting and saving...");
            //var origin = SixLabors.ImageSharp.Image.Load(image_path);

            var ploted = result.PlotImage(image);
            //string path_save_parent = Directory.GetParent(image_path).FullName;
            //var pathToSave = Path.Combine(path_save_parent, "result_" + Path.GetFileName(image_path));
            //var fullname = Path.GetFullPath(pathToSave);
            //ploted.Save(pathToSave);
            var resize = get_size(ploted.Width, ploted.Height);
            ploted.Mutate(x => x.Resize(resize.Width, resize.Height));
            //pictureBox2.Image = ImageSharpToBitmap(ploted);
            //origin.Dispose(); 
            //ploted.Dispose();
            return ploted;
            //Console.WriteLine();
        }
        private SixLabors.ImageSharp.Image detect(SixLabors.ImageSharp.Image image)
        {

            Console.WriteLine();
            Console.WriteLine("================ DETECT DEMO ================");
            Console.WriteLine();

            Console.WriteLine("Loading model...");

            //var predictor = new YoloV8(model_path);
            //Console.WriteLine("Working... ({0})", image_path);
            var result = predictor_dect.Detect(image);
            //model.Dispose();

            Console.WriteLine();

            Console.WriteLine($"Result: {result}");
            Console.WriteLine($"Speed: {result.Speed}");

            Console.WriteLine();

            Console.WriteLine("Plotting and saving...");
            //var origin = SixLabors.ImageSharp.Image.Load(image_path);

            var ploted = result.PlotImage(image);
            //string path_save_parent = Directory.GetParent(image_path).FullName;
            //var pathToSave = Path.Combine(path_save_parent, "result_" + Path.GetFileName(image_path));
            //var fullname = Path.GetFullPath(pathToSave);
            //ploted.Save(pathToSave);
            var resize = get_size(ploted.Width, ploted.Height);
            ploted.Mutate(x => x.Resize(resize.Width, resize.Height));
            //pictureBox2.Image = ImageSharpToBitmap(ploted);
            //origin.Dispose(); 
            //ploted.Dispose();
            return ploted;
            //Console.WriteLine();
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

            //Console.WriteLine("Working... ({0})", image_path);
            var result = predictor.Pose(image);
            predictor.Dispose();

            Console.WriteLine();

            Console.WriteLine($"Result: {result}");
            Console.WriteLine($"Speed: {result.Speed}");

            Console.WriteLine();

            Console.WriteLine("Plotting and saving...");
            //var origin = SixLabors.ImageSharp.Image.Load(image_path);

            var ploted = result.PlotImage(image);
            //string path_save_parent = Directory.GetParent(image_path).FullName;
            //var pathToSave = Path.Combine(path_save_parent, "result_" + Path.GetFileName(image_path));
            //var fullname = Path.GetFullPath(pathToSave);
            //ploted.Save(pathToSave);
            var resize = get_size(image.Width, image.Height);
            ploted.Mutate(x => x.Resize(resize.Width, resize.Height));
            //pictureBox2.Image = ImageSharpToBitmap(ploted);
            image.Dispose(); //ploted.Dispose();
            Console.WriteLine();
            return ploted;
        }
        private SixLabors.ImageSharp.Image segment(string model_path, SixLabors.ImageSharp.Image image)
        {

            Console.WriteLine();
            Console.WriteLine("================ POSE DEMO ================");
            Console.WriteLine();

            Console.WriteLine("Loading model...");
            var predictor = new YoloV8(model_path);

            //Console.WriteLine("Working... ({0})", image_path);
            var result = predictor.Segment(image);
            predictor.Dispose();

            Console.WriteLine();

            Console.WriteLine($"Result: {result}");
            Console.WriteLine($"Speed: {result.Speed}");

            Console.WriteLine();

            Console.WriteLine("Plotting and saving...");
            //var origin = SixLabors.ImageSharp.Image.Load(image_path);

            var ploted = result.PlotImage(image);
            //string path_save_parent = Directory.GetParent(image_path).FullName;
            //var pathToSave = Path.Combine(path_save_parent, "result_" + Path.GetFileName(image_path));
            //var fullname = Path.GetFullPath(pathToSave);
            //ploted.Save(pathToSave);
            var resize = get_size(image.Width, image.Height);
            ploted.Mutate(x => x.Resize(resize.Width, resize.Height));
            //pictureBox2.Image = ImageSharpToBitmap(ploted);
            image.Dispose(); //ploted.Dispose();
            Console.WriteLine();
            return ploted;
        }
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

        private void ts_video_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Video Files|*.mp4;*.avi";
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
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
        //YoloV8 predictor = new YoloV8(@"C:\Code\yolov8onnxNetFramework\Yolov8_NetFW\Yolov8_NetFW\models\yolov8s.onnx");
        private async void ts_video_play_Click(object sender, EventArgs e)
        {
            if (capture == null)
            {
                return;
            }

            try
            {
                //var model_path = @"C:\Code\yolov8onnxNetFramework\Yolov8_NetFW\Yolov8_NetFW\models\yolov8s.onnx";

                while (!pause)
                {
                    Mat m = new Mat();
                    capture.Read(m);
                    var resize = get_size(m.Width, m.Height);
                    //Image<Bgr,Byte> img_org = capture.QueryFrame().ToImage<Bgr,Byte>();

                    if (!m.IsEmpty)
                    {
                        SixLabors.ImageSharp.Image rs_img;
                        //CvInvoke.Resize(img_org,img_org,resize);
                        var bitmap = m.ToBitmap();
                        var imagedatabytes = ImageToByte(bitmap);
                        //// sua ham detect argument is image and return image de load len tren picture box
                        var new_img = SixLabors.ImageSharp.Image.Load(imagedatabytes);
                        if (rb_dt.Checked)
                        {
                            //string model_path = @"C:\Code\yolov8onnxNetFramework\Yolov8_NetFW\Yolov8_NetFW\models\yolov8s.onnx";
                            rs_img = detect(new_img);
                            pictureBox2.Image = ImageSharpToBitmap(rs_img);
                            rs_img.Dispose();

                            
                        }
                        else if (rb_sm.Checked)
                        {
                            string model_path = @"C:\Code\yolov8onnxNetFramework\Yolov8_NetFW\Yolov8_NetFW\models\yolov8s-seg.onnx";
                            rs_img = segment(model_path, new_img);
                            pictureBox2.Image = ImageSharpToBitmap(rs_img);
                            rs_img.Dispose();

                        }
                        else if (rb_pose.Checked)
                        {
                            string model_path = @"C:\Code\yolov8onnxNetFramework\Yolov8_NetFW\Yolov8_NetFW\models\yolov8s-pose.onnx";
                            rs_img = pose(model_path, new_img);
                            pictureBox2.Image = ImageSharpToBitmap(rs_img);
                            rs_img.Dispose();

                        }
                        else
                        {
                            MessageBox.Show("Please choose mode Detect or Segment or Pose!");
                            return;
                        }
                        //var rs_img = detect(model_path, new_img);
                        CvInvoke.Resize(m, m, resize);
                        pictureBox1.Image = m.ToBitmap();
                        bitmap.Dispose();
                        m.Dispose();
                        new_img.Dispose();
                        double fps = capture.Get(Emgu.CV.CvEnum.CapProp.Fps);
                        await Task.Delay(1000 / Convert.ToInt32(fps));
                    }
                    else
                    {
                        break;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
