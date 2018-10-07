using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
//using System.Drawing;


namespace ChipDetection
{

    [Flags]
    public enum SizeChangeDirection
    {
        NA = 0x00,
        LEFT = 0x01,
        RIGHT = 0x02,
        TOP = 0x04,
        BOTTOM = 0x08,
        MOVE = 0x10
    }


    [Serializable]
    public class ChipTemplate
    {
        public string PhotoFile
        {
            get;
            set;            
        }
        public string Name
        {
            get;
            set;
        }
        public Rectangle chipRectangle
        {
            get;
            set;
        }

        public Rectangle objectRectangle
        {
            get;
            set;
        }

        private Point downpoint;
        private Point curpoint;
        private Point prepoint;
        private SizeChangeDirection curstatus = SizeChangeDirection.NA;
        private int modflag = 0;

        public object Clone()
        {
            return this.MemberwiseClone(); //浅复制
        }
        public ChipTemplate()
        {
            chipRectangle = new Rectangle(                
                Parameter.GetInstance().ObjectCaptureLeft,
                Parameter.GetInstance().ObjectCaptureTop,
                Parameter.GetInstance().ObjectWidth,
                Parameter.GetInstance().ObjectHeight
                );


            objectRectangle = new Rectangle(
               Parameter.GetInstance().FeatureCaptureLeft,
               Parameter.GetInstance().FeatureCaptureTop,
               Parameter.GetInstance().FeatureWidth,
               Parameter.GetInstance().FeatureHeight
               );

            PhotoFile = Parameter.GetInstance().DefaultTemplateFile;
            
        }

        public void MouseMove(MouseEventArgs e)
        {
            curpoint = e.Location;

            if (GetCurrStatus(curpoint,false) == SizeChangeDirection.NA)
            {

                Cursor.Current = Cursors.Default;

            }
            else if (GetCurrStatus(curpoint, false) == SizeChangeDirection.MOVE)
            {

                Cursor.Current = Cursors.SizeAll;

            }
            else if (GetCurrStatus(curpoint, false) == SizeChangeDirection.TOP ||
                     GetCurrStatus(curpoint, false) == SizeChangeDirection.BOTTOM)
            {
                Cursor.Current = Cursors.SizeNS;
            }
            else if (GetCurrStatus(curpoint, false) == SizeChangeDirection.LEFT ||
                    GetCurrStatus(curpoint, false) == SizeChangeDirection.RIGHT)
            {
                Cursor.Current = Cursors.SizeWE;
            }

            int dltx = curpoint.X - prepoint.X;
            int dlty = curpoint.Y - prepoint.Y;

            if ( curstatus == SizeChangeDirection.MOVE)
            {
                if (modflag == 1)
                {

                    Rectangle tempRect = new Rectangle();
                    tempRect = objectRectangle;
                    tempRect.X = objectRectangle.X+dltx;
                    tempRect.Y = objectRectangle.Y+dlty;
                    objectRectangle = tempRect;
                    
                }
                else if (modflag == 2)
                {

                    Rectangle tempRect = new Rectangle();
                    tempRect = chipRectangle;
                    tempRect.X = chipRectangle.X + dltx;
                    tempRect.Y = chipRectangle.Y + dlty;
                    chipRectangle = tempRect;

                }
            }
            else if (curstatus == SizeChangeDirection.LEFT)
            {
                if (modflag == 1)
                {

                    Rectangle tempRect = new Rectangle();
                    tempRect = objectRectangle;
                    tempRect.X = curpoint.X;
                    tempRect.Width = tempRect.Width - dltx;
                    objectRectangle = tempRect;

                }
                else if (modflag == 2)
                {

                    Rectangle tempRect = new Rectangle();
                    tempRect = chipRectangle;
                    tempRect.X = curpoint.X;
                    tempRect.Width = tempRect.Width - dltx;
                    chipRectangle = tempRect;

                }

            }

            else if (curstatus == SizeChangeDirection.RIGHT)
            {
                if (modflag == 1)
                {

                    Rectangle tempRect = new Rectangle();
                    tempRect = objectRectangle;
                    tempRect.Width = tempRect.Width + dltx;
                    objectRectangle = tempRect;

                }
                else if (modflag == 2)
                {

                    Rectangle tempRect = new Rectangle();
                    tempRect = chipRectangle;
                    tempRect.Width = tempRect.Width + dltx;
                    chipRectangle = tempRect;

                }

            }


            else if (curstatus == SizeChangeDirection.TOP)
            {
                if (modflag == 1)
                {

                    Rectangle tempRect = new Rectangle();
                    tempRect = objectRectangle;
                    tempRect.Y = curpoint.Y;
                    tempRect.Height = tempRect.Height - dlty;
                    objectRectangle = tempRect;

                }
                else if (modflag == 2)
                {

                    Rectangle tempRect = new Rectangle();
                    tempRect = chipRectangle;
                    tempRect.Y = curpoint.Y;
                    tempRect.Height = tempRect.Height - dlty;
                    chipRectangle = tempRect;

                }

            }

            else if (curstatus == SizeChangeDirection.BOTTOM)
            {
                if (modflag == 1)
                {

                    Rectangle tempRect = new Rectangle();
                    tempRect = objectRectangle;
                    tempRect.Height = tempRect.Height + dlty;
                    objectRectangle = tempRect;

                }
                else if (modflag == 2)
                {

                    Rectangle tempRect = new Rectangle();
                    tempRect = chipRectangle;
                    tempRect.Height = tempRect.Height + dlty;
                    chipRectangle = tempRect;

                }

            }


            prepoint = curpoint;
             
        }

        public SizeChangeDirection GetCurrStatus( Point p, bool bupdate = true)
        {
            SizeChangeDirection x = SizeChangeDirection.NA;


            if (Math.Abs(objectRectangle.Top - p.Y) < 3 &&
                 p.X > objectRectangle.Left && p.X < objectRectangle.Right)
            {
                if (bupdate)   modflag = 1;

                return SizeChangeDirection.TOP;
            }
            else if (Math.Abs(chipRectangle.Top - p.Y) < 3 &&
                 p.X > chipRectangle.Left && p.X < chipRectangle.Right)
            {
                if (bupdate) modflag = 2;

                return SizeChangeDirection.TOP;
            }


            else if (Math.Abs(objectRectangle.Left - p.X) < 3 &&
                  p.Y > objectRectangle.Top && p.Y < objectRectangle.Bottom)
            {
                if (bupdate) modflag = 1;

                return SizeChangeDirection.LEFT;
            }
            else if (Math.Abs(chipRectangle.Left - p.X) < 3 &&
                  p.Y > chipRectangle.Top && p.Y < chipRectangle.Bottom)
            {
                if (bupdate) modflag = 2;

                return SizeChangeDirection.LEFT;
            }


            else if (Math.Abs(objectRectangle.Right - p.X) < 3 &&
                 p.Y > objectRectangle.Top && p.Y < objectRectangle.Bottom)
            {
                if (bupdate) modflag = 1;

                return SizeChangeDirection.RIGHT;
            }
            else if (Math.Abs(chipRectangle.Right - p.X) < 3 &&
                  p.Y > chipRectangle.Top && p.Y < chipRectangle.Bottom)
            {
                if (bupdate) modflag = 2;

                return SizeChangeDirection.RIGHT;
            }
            else if (Math.Abs(objectRectangle.Bottom - p.Y) < 3 &&
                p.X > objectRectangle.Left && p.X < objectRectangle.Right)
            {
                if (bupdate) modflag = 1;

                return SizeChangeDirection.BOTTOM;
            }
            else if (Math.Abs(chipRectangle.Bottom - p.Y) < 3 &&
                 p.X > chipRectangle.Left && p.X < chipRectangle.Right)
            {
                if (bupdate) modflag = 2;

                return SizeChangeDirection.BOTTOM;
            }
            else if( objectRectangle.Contains(p) )
            {
                if (bupdate) modflag = 1;

                return SizeChangeDirection.MOVE;
            }
            else if (chipRectangle.Contains(p))
            {
                if (bupdate) modflag = 2;

                return SizeChangeDirection.MOVE;
            }
            return x;
        }
        public void MouseDown(MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                downpoint = e.Location;

                curstatus = GetCurrStatus(downpoint);
                prepoint = curpoint = downpoint;
            }

        }
        public void MouseUp(MouseEventArgs e)
        {

            curstatus = SizeChangeDirection.NA;
            modflag = 0;

        }
        public void Draw( Graphics g)
        {
            Pen pen1 = new Pen(Color.Yellow, 2);
            pen1.DashStyle = System.Drawing.Drawing2D.DashStyle.Dash;
            g.DrawRectangle(pen1, chipRectangle);
            pen1.DashStyle = System.Drawing.Drawing2D.DashStyle.Solid;
            g.DrawRectangle(pen1, objectRectangle);           
        }
    }
}
