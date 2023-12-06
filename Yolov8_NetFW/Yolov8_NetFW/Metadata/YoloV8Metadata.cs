using SixLabors.ImageSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yolov8_NetFW.Metadata
{
    public class YoloV8Metadata
    {
        public string Author { get; }

        public string Description { get; }

        public string Version { get; } 
        public YoloV8Task Task { get; } 

        public int Batch { get; }

        public Size ImageSize { get; } 

        public IReadOnlyList<YoloV8Class> Classes { get; }
        public YoloV8Metadata(string author,
                        string description,
                        string version,
                        YoloV8Task task,
                        int batch,
                        Size imageSize,
                        IReadOnlyList<YoloV8Class> classes)
        {
            Author = author;
            Description = description;
            Version = version;
            Task = task;
            Batch = batch;
            ImageSize = imageSize;
            Classes = classes;
        }
        public static YoloV8Metadata Parse(IDictionary<string, string> metadata)
        {
            var author = metadata["author"];
            var description = metadata["description"];
            var version = metadata["version"];

            /*            var task = metadata["task"] switch
                        {
                            "pose" => YoloV8Task.Pose,
                            "detect" => YoloV8Task.Detect,
                            "segment" => YoloV8Task.Segment,
                            "classify" => YoloV8Task.Classify,
                            _ => throw new ArgumentException("Unknow YoloV8 'task' value")
                        };*/
            string  taskString = metadata["task"];
            YoloV8Task task;
            switch (taskString)
            {
                case "pose":
                    task = YoloV8Task.Pose;
                    break;
                case "detect":
                    task = YoloV8Task.Detect;
                    break;
                case "segment":
                    task = YoloV8Task.Segment;
                    break;
                case "classify":
                    task = YoloV8Task.Classify;
                    break;
                default:
                    throw new ArgumentException("Unknown YoloV8 'task' value");
            }
            var batch = int.Parse(metadata["batch"]);

            var imageSize = ParseSize(metadata["imgsz"]);
            var classes = ParseClasses(metadata["names"]);

            if (task is YoloV8Task.Pose)
            {
                var keypointShape = ParseKeypointShape(metadata["kpt_shape"]);

                return new YoloV8PoseMetadata(author,
                                              description,
                                              version,
                                              task,
                                              batch,
                                              imageSize,
                                              classes,
                                              keypointShape);
            }

            return new YoloV8Metadata(author,
                                      description,
                                      version,
                                      task,
                                      batch,
                                      imageSize,
                                      classes);
        }

        #region Static Parsers
        private static Size ParseSize(string text)
        {
            text = text.Substring(1, text.Length - 2); // '[640, 641]' => '640, 640'

            var split = text.Split(new[] { ", " }, StringSplitOptions.None);

            var x = int.Parse(split[0]);
            var y = int.Parse(split[1]);

            return new Size(x, y);
        }

        //private static Size ParseSize(string text)
        //{
        //    //text = text[1..^1]; // '[640, 641]' => '640, 640'
        //    text = text.Substring(1, text.Length - 2);
        //    var split = text.Split(',');

        //    var x = int.Parse(split[0].Trim());
        //    var y = int.Parse(split[1].Trim());

        //    return new Size(x, y);
        //}
        private static KeypointShape ParseKeypointShape(string text)
        {
            text = text.Substring(1, text.Length - 2); // '[17, 3]' => '17, 3'

            var split = text.Split(new[] { ", " }, StringSplitOptions.None);

            var count = int.Parse(split[0]);
            var channels = int.Parse(split[1]);

            return new KeypointShape(count, channels);
        }

        //private static KeypointShape ParseKeypointShape(string text)
        //{
        //    //text = text[1..^1]; // '[17, 3]' => '17, 3'
        //    text = text.Substring(1, text.Length - 2);
        //    var split = text.Split(',');

        //    var count = int.Parse(split[0].Trim());
        //    var channels = int.Parse(split[1].Trim());

        //    return new KeypointShape(count, channels);
        //}

        //private static YoloV8Class[] ParseClasses(string text)
        //{
        //    //text = text[1..^1];
        //    text = text.Substring(1, text.Length - 2);

        //    var split = text.Split(',');
        //    var count = split.Length;

        //    var names = new YoloV8Class[count];

        //    for (int i = 0; i < count; i++)
        //    {
        //        var value = split[i].Trim();

        //        var valueSplit = value.Split(':');

        //        var id = int.Parse(valueSplit[0].Trim());
        //        var name = valueSplit[1][1..^1].Replace('_', ' ');

        //        names[i] = new YoloV8Class(id, name);
        //    }

        //    return names;
        //}
        private static YoloV8Class[] ParseClasses(string text)
        {
            text = text.Substring(1, text.Length - 2);

            var split = text.Split(new[] { ", " }, StringSplitOptions.None);
            var count = split.Length;

            var names = new YoloV8Class[count];

            for (int i = 0; i < count; i++)
            {
                var value = split[i];

                var valueSplit = value.Split(new[] { ": " }, StringSplitOptions.None);

                var id = int.Parse(valueSplit[0]);
                var name = valueSplit[1].Substring(1, valueSplit[1].Length - 2).Replace('_', ' ');

                names[i] = new YoloV8Class(id, name);
            }

            return names;
        }

        #endregion
    }
}
