using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using System.Diagnostics;

namespace ChipDetection
{

    public class ChipDetection
    {
        [StructLayout(LayoutKind.Sequential)]
        public struct Result_stru
        {
            public int x;
            public int y;
            public int status;
        };


        [DllImport(@"myprocess.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall, EntryPoint = "gray2rbg")]
        private extern static void gray2rbg(IntPtr src, IntPtr des, int srcw, int srch, int desw, int desh, int desstride);

        [DllImport(@"myprocess.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall, EntryPoint = "detectcode")]
        private extern static void detectcode();

        [DllImport(@"myprocess.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall, EntryPoint = "findcode")]
        private extern static int findcode(IntPtr pdata, UInt32 w, UInt32 h);

        [DllImport(@"myprocess.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall, EntryPoint = "process")]
        private extern static int process(IntPtr pdata, UInt32 w, UInt32 h, String val);

        [DllImport(@"myprocess.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall, EntryPoint = "drawline")]
        private extern static void drawline(IntPtr pdata, UInt32 w, UInt32 h, Double x1,Double x2,Double y1,Double y2);

        [DllImport(@"myprocess.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall, EntryPoint = "saveimage")]
        private extern static void saveimage(IntPtr pdata, UInt32 w, UInt32 h, int i); 

        [DllImport(@"myprocess.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall, EntryPoint = "test")]
        private extern static int test();


        [DllImport(@"myprocess.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall, EntryPoint = "teststring")]
        private extern static int teststring(String val);


        public int CodeNum;

        public void ResetDectionStatus()
        { 
            /***检测结果****/

            CodeNum = 0;
        }


        public void MyProcess(IntPtr bufptr, UInt32 width, UInt32 height, String content)
        {
            DateTime beforProc = System.DateTime.Now;

            try 
            {
                CodeNum = process(bufptr, width, height, content);
                
            }
            catch (Exception ex)
            {
                System.Console.Out.WriteLine(ex.Message);
            }

            //结束时间
            DateTime afterProc = System.DateTime.Now;
            TimeSpan ts = afterProc.Subtract(beforProc);
            //Debug.WriteLine("处理音筒-算法耗时{0}ms", ts.TotalMilliseconds);
        }


        public void savebuffer(IntPtr buf, UInt32 width, UInt32 height,int i)
        {
            saveimage(buf, width, height, i);
            //Debug.WriteLine("successfully save image.");
        }

        public int myfind(IntPtr buf, UInt32 w, UInt32 h)
        {

            return findcode(buf, w, h);
        
        }
        //检测二维码
        public int DetectCode()
        {
            try
            {
                
               
                 detectcode();
                
                
                return 2;

            }
            catch (Exception ex)
            {
                System.Console.Out.WriteLine(ex.Message);
                return -1;
            }
           
        }




        static public void getimage(IntPtr src, IntPtr des, int srcw, int srch, int desw, int desh,int desstride)
        {
            gray2rbg(src,des,srcw,srch,desw,desh,desstride);
        }
        
    
        
      
        //测试划线
        public void Drawline(IntPtr pData, UInt32 w, UInt32 h, Double x1, Double x2, Double y1, Double y2)
        {
            try 
            {
                drawline(pData, w, h, x1, x2, y1, y2);
            }
            catch (Exception ex)
            {
                System.Console.Out.WriteLine(ex.Message);
            }
            
            
        }
    

    }
}
