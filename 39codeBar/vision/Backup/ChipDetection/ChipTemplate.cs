using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace ChipDetection
{
    [Serializable]
    public class ChipTemplate
    {
        [NonSerialized]
        //private Bitmap mPhoto;

        private string mPhotoFile;
        private Rectangle mFeatureRectangle;
        private Rectangle mObjectRectangle;

        private Rectangle mFeatureDisplayRectangle;
        private Rectangle mObjectDisplayRectangle;

        private Rectangle mFeatureCaptureRectangle;
        private Rectangle mObjectCaptureRectangle;
        private Rectangle mFeatureCaptureDisplayRectangle;
        private Rectangle mObjectCaptureDisplayRectangle;

        private string mName;
        public string Name
        {
            get { return mName; }
            set { mName = value; }
        }
 
        public System.Drawing.Rectangle FeatureCaptureRectangle
        {
            get { return mFeatureCaptureRectangle; }
            set { mFeatureCaptureRectangle = value; }
        }
        public System.Drawing.Rectangle ObjectCaptureRectangle
        {
            get { return mObjectCaptureRectangle; }
            set { mObjectCaptureRectangle = value; }
        }
        public System.Drawing.Rectangle FeatureCaptureDisplayRectangle
        {
            get { return mFeatureCaptureDisplayRectangle; }
            set { mFeatureCaptureDisplayRectangle = value; }
        }
        public System.Drawing.Rectangle ObjectCaptureDisplayRectangle
        {
            get { return mObjectCaptureDisplayRectangle; }
            set { mObjectCaptureDisplayRectangle = value; }
        }

        private int mFeatureGreyDelta;
        private double mFeatureSimilarity;
        private int mObjectGreyDelta;
        private double mObjectSimilarity;

        public int FeatureGreyDelta
        {
            get { return mFeatureGreyDelta; }
            set { mFeatureGreyDelta = value; }
        }
        public double FeatureSimilarity
        {
            get { return mFeatureSimilarity; }
            set { mFeatureSimilarity = value; }
        }
        public int ObjectGreyDelta
        {
            get { return mObjectGreyDelta; }
            set { mObjectGreyDelta = value; }
        }
        public double ObjectSimilarity
        {
            get { return mObjectSimilarity; }
            set { mObjectSimilarity = value; }
        }
        public string PhotoFile
        {
            get { return mPhotoFile; }
            set { mPhotoFile = value; }
        }
        public System.Drawing.Rectangle FeatureRectangle
        {
            get { return mFeatureRectangle; }
            set { mFeatureRectangle = value; }
        }
        public System.Drawing.Rectangle ObjectRectangle
        {
            get { return mObjectRectangle; }
            set { mObjectRectangle = value; }
        }

        public System.Drawing.Rectangle FeatureDisplayRectangle
        {
            get { return mFeatureDisplayRectangle; }
            set { mFeatureDisplayRectangle = value; }
        }
        public System.Drawing.Rectangle ObjectDisplayRectangle
        {
            get { return mObjectDisplayRectangle; }
            set { mObjectDisplayRectangle = value; }
        }

        public object Clone()
        {
            return this.MemberwiseClone(); //浅复制
        }
        public ChipTemplate()
        {
        }

        public void GenerateObjectCaptureRectangle()
        {

            Rectangle rectangle =new Rectangle();
            rectangle.X = ObjectRectangle.Left - Parameter.GetInstance().ObjectCaptureLeft;
            rectangle.Y = ObjectRectangle.Top - Parameter.GetInstance().ObjectCaptureTop;
            rectangle.Width = ObjectRectangle.Width + Parameter.GetInstance().ObjectCaptureLeft + Parameter.GetInstance().ObjectCaptureRight;
            rectangle.Height = ObjectRectangle.Height + Parameter.GetInstance().ObjectCaptureTop + Parameter.GetInstance().ObjectCaptureBottom;
            mObjectCaptureRectangle = rectangle;
            mObjectCaptureDisplayRectangle = GenerateDisplayRectangle(mObjectCaptureRectangle);

        }
        public void GenerateFeatureCaptureRectangle()
        {
 
            Rectangle rectangle = new Rectangle();
            rectangle.X = mFeatureRectangle.Left - Parameter.GetInstance().FeatureCaptureLeft;
            rectangle.Y = mFeatureRectangle.Top - Parameter.GetInstance().FeatureCaptureTop;
            rectangle.Width = mFeatureRectangle.Width + Parameter.GetInstance().FeatureCaptureLeft + Parameter.GetInstance().FeatureCaptureRight;
            rectangle.Height = mFeatureRectangle.Height + Parameter.GetInstance().FeatureCaptureTop + Parameter.GetInstance().FeatureCaptureBottom;
            mFeatureCaptureRectangle = rectangle;
            mFeatureCaptureDisplayRectangle = GenerateDisplayRectangle(mFeatureCaptureRectangle);
        }

        public Rectangle GenerateRealRectangle(Rectangle displayRectangle)
        {
            displayRectangle.X = (int)(displayRectangle.X * Parameter.GetInstance().WidthRatio);
            displayRectangle.Y = (int)(displayRectangle.Y * Parameter.GetInstance().HeightRatio);
            displayRectangle.Width = (int)(displayRectangle.Width * Parameter.GetInstance().WidthRatio);
            displayRectangle.Height = (int)(displayRectangle.Height * Parameter.GetInstance().HeightRatio);
            return displayRectangle;
        }
        private Rectangle GenerateDisplayRectangle(Rectangle rectangle)
        {

            rectangle.X = (int)(rectangle.X / Parameter.GetInstance().WidthRatio);
            rectangle.Y = (int)(rectangle.Y / Parameter.GetInstance().HeightRatio);
            rectangle.Width = (int)(rectangle.Width / Parameter.GetInstance().WidthRatio);
            rectangle.Height = (int)(rectangle.Height / Parameter.GetInstance().HeightRatio);
            return rectangle;
        }

        public void InitializeDisplaySize()
        {
            mFeatureDisplayRectangle = GenerateDisplayRectangle(mFeatureRectangle);
            mObjectDisplayRectangle = GenerateDisplayRectangle(mObjectRectangle);

            mFeatureCaptureDisplayRectangle = GenerateDisplayRectangle(mFeatureCaptureRectangle);
            mObjectCaptureDisplayRectangle = GenerateDisplayRectangle(mObjectCaptureRectangle);

            mObjectRectangle = GenerateRealRectangle(mObjectDisplayRectangle);
            mFeatureRectangle = GenerateRealRectangle(mFeatureDisplayRectangle);

            GenerateObjectCaptureRectangle();
            GenerateFeatureCaptureRectangle();

        }
    }
}
