using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;
using FlyCapture2Managed;

using System.Diagnostics;
using System.ComponentModel;
using System.Threading;

namespace ChipDetection
{

    public partial  class HIKCamera
    {
        private static HIKCamera mSingletonHIKCamera;

        private ManagedCameraBase mcamera = null;
      

        private HIKCamera()
        {
           
        }
        public static HIKCamera GetInstance()
        {
            if (null == mSingletonHIKCamera)
            {
                mSingletonHIKCamera = new HIKCamera();
            }
            return mSingletonHIKCamera;
        }

        public bool connect()
        {
            return true;
        }


        public string Initialize()
        {

  
            return string.Empty;
        }


        public string SetTriggerMode()
        {
           
            return string.Empty;
        }
        public string StartGrabImage(IntPtr hwnd,PictureBox pictureBox)
        {
           
            return string.Empty;

        }

        public void Trigger()
        {
           
        }
        public string GrabImage()
        {
            return string.Empty;
        }

        public string StopGrabbing()
        {
            return string.Empty;
        }
        public void Close()
        {
        }        
    }
}
