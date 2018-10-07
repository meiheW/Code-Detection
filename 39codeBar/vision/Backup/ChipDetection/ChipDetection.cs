using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;

namespace ChipDetection
{
    public enum DetectionStatus
    {
        NA,
        Pass,
        Failure,
    }
    public class ChipDetection
    {
        

        //private static ChipDetection mSingletonChipDetection;
        
        private long mLastTimerTicker;

        private int mImageCaptureLeft;
        private int mImageCaptureTop;
        private int mImageCaptureWidth;
        private int mImageCaptureHeight;

        private int mFrameOutLeft;
        private int mFrameOutTop;
        private int mFrameOutWidth;
        private int mFrameOutHeight;

        private int mChipOutLeft;
        private int mChipOutTop;
        private int mChipOutWidth;
        private int mChipOutHeight;
        private int mChangeToNAStatusDelayIndex;
        private double mProcessTime;

        private int mPassCount;
        private int mFailureCount;
        private int mSummaryCount;

        private ChipTemplate mChipTemplate;
        private DetectionStatus mDetectionStatusValue;
        

        public double ProcessTime
        {
            get { return mProcessTime; }
        }
        
        public ChipTemplate ChipTemplate
        {
            get { return mChipTemplate; }
            set { mChipTemplate = value; }
        }
        public int ChangeToNAStatusDelayIndex
        {
            get { return mChangeToNAStatusDelayIndex; }
            set { mChangeToNAStatusDelayIndex = value; }
        }
       
        public DetectionStatus DetectionStatusValue
        {
            get { return mDetectionStatusValue; }
        }      

        public int PassCount
        {
            get { return mPassCount; }
        }
        public int FailureCount
        {
            get { return mFailureCount; }
        }
        public int SummaryCount
        {
            get { return mSummaryCount; }
        }
        public int ChipOutLeft
        {
            get { return mChipOutLeft; }
        }
        public int ChipOutTop
        {
            get { return mChipOutTop; }
        }
        public int ChipOutWidth
        {
            get { return mChipOutWidth; }
        }
        public int ChipOutHeight
        {
            get { return mChipOutHeight; }
        }
        public int FrameOutLeft
        {
            get { return mFrameOutLeft; }
        }
        public int FrameOutTop
        {
            get { return mFrameOutTop; }
        }
        public int FrameOutWidth
        {
            get { return mFrameOutWidth; }
        }
        public int FrameOutHeight
        {
            get { return mFrameOutHeight; }
        }

        public ChipDetection()
        {
            ResetDectionStatus();
        }

        public void ResetDectionStatus()
        {
            mPassCount = 0;
            mFailureCount = 0;
            mSummaryCount = 0;
            mDetectionStatusValue = DetectionStatus.NA;
        }
        public void InitializeChipCaptureRange()
        {
 
            mImageCaptureLeft = mChipTemplate.FeatureCaptureRectangle.X;
            mImageCaptureTop = mChipTemplate.FeatureCaptureRectangle.Y;
            mImageCaptureWidth = mChipTemplate.FeatureCaptureRectangle.Width;
            mImageCaptureHeight = mChipTemplate.FeatureCaptureRectangle.Height;

        }       
        public void CreateChipTemplate(ChipTemplate template)
        {
            Rectangle featureRectangle = template.FeatureRectangle;
            Rectangle objectRectangle = template.ObjectRectangle;            
            mChipTemplate = template;
            InitializeChipCaptureRange();

        }
        public void ProcessImageByTemplate(IntPtr pData)
        {


        }
    }
}
