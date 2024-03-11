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
using static Emgu.Util.Platform;
using Emgu.CV.Util;
using SixLabors.Fonts;
using SixLabors.ImageSharp.Drawing;
using SixLabors.ImageSharp.PixelFormats;
using Yolov8_NetFW.Selectors;
//using Microsoft.ML.OnnxRuntime.Tensors;

namespace GUI_YOLOv8
{
    public partial class Form1 : Form
    {
        VideoCapture capture;
        bool pause = false;
        public Form1()
        {
            InitializeComponent();
        }


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
            var pathToSave = System.IO.Path.Combine(path_save_parent, "result_" + System.IO.Path.GetFileName(image_path));
            var fullname = System.IO.Path.GetFullPath(pathToSave);
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
            var pathToSave = System.IO.Path.Combine(path_save_parent, "result_" + System.IO.Path.GetFileName(image_path));
            var fullname = System.IO.Path.GetFullPath(pathToSave);
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
        private ISegmentationResult segment(YoloV8 predictor_seg, byte[] image)
        {

            Console.WriteLine();
            Console.WriteLine("================ SEGMENT DEMO ================");
            Console.WriteLine();

            Console.WriteLine("Loading model...");
            var result = predictor_seg.Segment(image);
            return result;

        }
        private IPoseResult pose(YoloV8 predictor_pose, byte[] image)
        {

            Console.WriteLine();
            Console.WriteLine("================ DETECT DEMO ================");
            Console.WriteLine();

            Console.WriteLine("Loading model...");
            var result = predictor_pose.Pose(image);
            return result;

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
            var pathToSave = System.IO.Path.Combine(path_save_parent, "result_" + System.IO.Path.GetFileName(image_path));
            var fullname = System.IO.Path.GetFullPath(pathToSave);
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
            var pathToSave = System.IO.Path.Combine(path_save_parent, "result_" + System.IO.Path.GetFileName(image_path));
            var fullname = System.IO.Path.GetFullPath(pathToSave);
            ploted.Save(pathToSave);
            var resize = get_size(origin.Width, origin.Height);
            ploted.Mutate(x => x.Resize(resize.Width, resize.Height));
            pictureBox2.Image = ImageSharpToBitmap(ploted);
            origin.Dispose(); ploted.Dispose();
            Console.WriteLine();
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
        public static Image<Bgr, Byte> Overlay(Image<Bgr, Byte> target, Image<Bgr, Byte> overlay, System.Drawing.Point start_point) {

            Bitmap bmp = target.AsBitmap();
            Graphics gra = Graphics.FromImage(bmp);
            gra.CompositingMode = System.Drawing.Drawing2D.CompositingMode.SourceOver;
            gra.DrawImage(overlay.AsBitmap(), start_point);
            return target;
        }
        public static Image<Bgr, Byte> PosePlotImage(IPoseResult result, Image<Bgr, Byte> originImage, PosePlottingOptions options)
        {
            //var process = originImage.Load(true);

            //EnsureSize(process.Size(), result.Image);

            var size = result.Image;

            var ratio = Math.Max(size.Width, size.Height) / 640F;

            //var textOptions = new TextOptions(options.FontFamily.CreateFont(options.FontSize * ratio));

            //var textPadding = options.TextHorizontalPadding * ratio;

            //var boxBorderThickness = options.BoxBorderThickness * ratio;

            var radius = options.KeypointRadius * ratio;
            //var lineThickness = options.KeypointLineThickness * ratio;

            foreach (var box in result.Boxes)
            {
                var label = $"{box.Class.Name} {box.Confidence:N}";
                var rect = new System.Drawing.Rectangle(box.Bounds.X, box.Bounds.Y, box.Bounds.Width, box.Bounds.Height);
                CvInvoke.Rectangle(originImage, rect, new MCvScalar(0, 0, 255),2, Emgu.CV.CvEnum.LineType.Filled);
                CvInvoke.PutText(originImage, label, new System.Drawing.Point(box.Bounds.X, box.Bounds.Y), Emgu.CV.CvEnum.FontFace.HersheySimplex, 1, new MCvScalar(0, 0, 255), 2);
                // Draw lines
                for (int i = 0; i < options.Skeleton.Connections.Count; i++)
                {
                    var connection = options.Skeleton.Connections[i];

                    IKeypoint first = box.Keypoints[connection.First];
                    IKeypoint second = box.Keypoints[connection.Second];

                    if (first.Confidence < options.KeypointConfidence || second.Confidence < options.KeypointConfidence)
                        continue;

                    var points = new SixLabors.ImageSharp.PointF[]
                    {
                    first.Point,
                    second.Point,
                    };
                    
                    var point1 = new System.Drawing.Point(Int32.Parse((points[0].X.ToString())), Int32.Parse((points[0].Y.ToString())));
                    var point2 = new System.Drawing.Point(Int32.Parse((points[1].X.ToString())), Int32.Parse((points[1].Y.ToString())));

                    CvInvoke.Line(originImage,point1,point2,new MCvScalar(255,0,0) ,2);

                }
                // Draw keypoints
                for (int i = 0; i < box.Keypoints.Count; i++)
                {
                    var keypoint = box.Keypoints[i];

                    if (keypoint.Confidence < options.KeypointConfidence)
                        continue;

                    //var ellipse = new EllipsePolygon(keypoint.Point, radius);

                    //var keypointColor = options.Skeleton.GetKeypointColor(keypoint.Index);

                    var size_ellipe = new System.Drawing.SizeF(Int32.Parse((radius.ToString())), Int32.Parse((radius.ToString())));
                    var center_point = new System.Drawing.PointF(keypoint.Point.X, keypoint.Point.Y);
                    //CvInvoke.Ellipse(originImage, center_point, size_ellipe,angle:180.0, color: new MCvScalar(0, 0, 255), thickness: -1);
                    CvInvoke.Ellipse(originImage,new RotatedRect(center_point, size_ellipe,180),new MCvScalar(0,0,255),thickness:-1);
                }
                originImage.Save("lines_ellipe.jpg");
            }
            return originImage;
        }
        private Image<Bgr, byte> SegmentPlotImage(ISegmentationResult result,Image<Bgr, byte>originImage)
        {
            //process.Save("CopyBlank.jpg");
            foreach (var box in result.Boxes)
            {
                Image<Bgr, byte> maskMat = new Image<Bgr, byte>(box.Mask.Width, box.Mask.Height);
                for (int x = 0; x < box.Mask.Width; x++)
                {
                    for (int y = 0; y < box.Mask.Height; y++)
                    {
                        var value = box.Mask[x, y];
                        if (value > .5F)
                        {
                            maskMat.Data[y, x, 0] = 255;//b
                            maskMat.Data[y, x, 1] = 0;  //g
                            maskMat.Data[y, x, 2] = 0;  //r
                        }

                    }
                }
                // Resize the mask if needed
                if (box.Bounds.Width != box.Mask.Width || box.Bounds.Height != box.Mask.Height)
                {
                    CvInvoke.Resize(maskMat, maskMat, new System.Drawing.Size(box.Bounds.Width, box.Bounds.Height));
                }
                // c2 draw mask to image
                Mat transparentMask = new Mat(originImage.Size, DepthType.Cv8U, 3);
                transparentMask.SetTo(new MCvScalar(0, 0, 0));

                Mat mat = new Mat();
                VectorOfVectorOfPoint findcontours = new VectorOfVectorOfPoint();
                CvInvoke.FindContours(maskMat.Convert<Gray, Byte>(), findcontours, mat, Emgu.CV.CvEnum.RetrType.External, Emgu.CV.CvEnum.ChainApproxMethod.ChainApproxSimple);
                CvInvoke.FillPoly(transparentMask, findcontours, new MCvScalar(255, 0, 0), offset: new System.Drawing.Point(box.Bounds.X, box.Bounds.Y));
                CvInvoke.AddWeighted(originImage, 1, transparentMask, 0.5, 0, originImage);// transparent mask
                //originImage.Save("AddWeighted.jpg");
                //mask.Save("ThresholdBinary.jpg");
                //process.Save("test_findcontours.jpg");
                //CvInvoke.FillPoly(process, findcontours, new MCvScalar(255, 0, 0), offset:new System.Drawing.Point(box.Bounds.X, box.Bounds.Y));
                // c1 draw mask to image
                //var mask = maskMat.Convert<Gray, Byte>().SmoothGaussian(3).ThresholdBinary(new Gray(20),new Gray(255)).Erode(1);// find mask 
                //var rect = new System.Drawing.Rectangle(box.Bounds.X, box.Bounds.Y, box.Bounds.Width, box.Bounds.Height);
                //process.Save("test_findcontours.jpg");
                //process.ROI = rect;
                //process.SetValue(new Bgr(255, 0, 0), mask);// set color blue with mask to process
                //maskMat.SetValue(new Bgr(0, 0, 0), mask.Not());
                //process._Or(maskMat);
                //process.ROI = System.Drawing.Rectangle.Empty;

                // Draw bounding box
                CvInvoke.Rectangle(originImage, new System.Drawing.Rectangle(box.Bounds.X, box.Bounds.Y, box.Bounds.Width, box.Bounds.Height), new MCvScalar(0, 0, 255), 2);

                // Draw label
                var label = $"{box.Class.Name} {box.Confidence:N}";
                CvInvoke.PutText(originImage, label, new System.Drawing.Point(box.Bounds.Left, box.Bounds.Top), FontFace.HersheySimplex, 1, new MCvScalar(0, 0, 255), 2);
                //process.Save("process.jpg");
                transparentMask.Dispose();
            }

            return originImage;
        }

        private async void btn_play_Click(object sender, EventArgs e)
        {
            if (capture == null)
            {
                return;
            }

            try
            {
                if (pause) pause = !pause;// set false to play video after press pause button
                string video_save_path = System.IO.Path.Combine(Directory.GetParent(file_choose).ToString(), System.IO.Path.GetFileNameWithoutExtension(file_choose) + "rs.mp4");
                VideoWriter videoWriter = new VideoWriter(video_save_path, 25, new System.Drawing.Size(capture.Width, capture.Height), true);
                while (!pause)
                {
                    Mat m = new Mat();
                    Mat m_clone = new Mat();
                    capture.Read(m);
                    m.CopyTo(m_clone);

                    var resize = get_size(m.Width, m.Height);

                    if (!m.IsEmpty)
                    {
                        var imagedatabytes = ImageToByte(m.ToBitmap());
                        if (rb_dt.Checked)
                        {
                            var boxes = detect(predictor_dect, imagedatabytes);
                            foreach (var box in boxes.Boxes)
                            {
                                var label = $"{box.Class.Name} {box.Confidence:N}";
                                var rect = new System.Drawing.Rectangle();
                                rect.X = box.Bounds.X; rect.Y = box.Bounds.Y;
                                rect.Width = box.Bounds.Width; rect.Height = box.Bounds.Height;
                                CvInvoke.Rectangle(m, rect, new MCvScalar(0, 0, 255), 2, Emgu.CV.CvEnum.LineType.Filled);
                                CvInvoke.PutText(m,label, new System.Drawing.Point(rect.X, rect.Y), Emgu.CV.CvEnum.FontFace.HersheySimplex, 1, new MCvScalar(0, 0, 255), 2);
                                //m.Save("save.jpg");
                            }
                            videoWriter.Write(m);
                            CvInvoke.Resize(m, m, resize);
                            pictureBox2.Image = m.ToBitmap();//ImageSharpToBitmap(rs_img);
                                                             // rs_img.Dispose();
                        }
                        else if (rb_sm.Checked)
                        {
                            var seg_results = segment(predictor_seg, imagedatabytes);
                            Image<Bgr, byte> plot_image = SegmentPlotImage(seg_results, m.ToImage<Bgr, byte>());
                            videoWriter.Write(plot_image);
                            Image<Bgr, byte> result_img = new Image<Bgr, byte>(resize.Width, resize.Height);
                            CvInvoke.Resize(plot_image, result_img, resize);
                            pictureBox2.Image = result_img.ToBitmap();//ImageSharpToBitmap(rs_img);
                            plot_image.Dispose();
                            result_img.Dispose();

                        }
                        else if (rb_pose.Checked)
                        {
                            var pose_results = pose(predictor_pose, imagedatabytes);
                            var pose_option = new PosePlottingOptions();
                            var plot_image = PosePlotImage(pose_results, m.ToImage<Bgr, byte>(), pose_option);
                            videoWriter.Write(plot_image);
                            Image<Bgr, byte> result_img = new Image<Bgr, byte>(resize.Width, resize.Height);
                            CvInvoke.Resize(plot_image, result_img, resize);
                            pictureBox2.Image = result_img.ToBitmap();//ImageSharpToBitmap(rs_img);
                            result_img.Dispose();
                            plot_image.Dispose();

                        }
                        else
                        {
                            MessageBox.Show("Please choose mode Detect or Segment or Pose!");
                            return;
                        }
                        //var rs_img = detect(model_path, new_img);
                        CvInvoke.Resize(m_clone, m_clone, resize);
                        pictureBox1.Image = m_clone.ToBitmap();
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
                Console.WriteLine(ex.Message);
                MessageBox.Show(ex.Message);
            }
        }

        private void btn_pause_Click(object sender, EventArgs e)
        {
            pause = !pause;
        }
    }
}
