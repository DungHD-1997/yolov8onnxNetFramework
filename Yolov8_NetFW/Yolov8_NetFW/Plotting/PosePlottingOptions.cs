﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yolov8_NetFW.Plotting
{
    public class PosePlottingOptions : DetectionPlottingOptions
    {
        public static new PosePlottingOptions Default { get; } = new PosePlottingOptions();

        public ISkeleton Skeleton { get; set; }

        public float KeypointConfidence { get; set; }

        public float KeypointRadius { get; set; }

        public float KeypointLineThickness { get; set; }

        public PosePlottingOptions()
        {
            Skeleton = new HumanSkeleton();
            KeypointConfidence = .5F;
            KeypointRadius = 3F;
            KeypointLineThickness = 1.5F;
        }
    }
}
