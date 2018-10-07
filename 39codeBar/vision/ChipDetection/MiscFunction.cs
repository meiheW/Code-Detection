using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Runtime.InteropServices;
using System.Runtime.Serialization.Formatters.Binary;
using System.Xml.Serialization;
using System.Text;
using System.Windows.Forms;

namespace MiscFunctions
{
    public class MiscFunction
    {
        [DllImport("kernel32.dll", EntryPoint = "SetProcessWorkingSetSize")]
        private static extern int SetProcessWorkingSetSize(IntPtr process, int minSize, int maxSize);

        [DllImport("Kernel32.dll")]
        private static extern bool Beep(int frequency, int duration);

        private static MiscFunction mSingletonMiscFunction = null;

        //public bool RenameFile(string sourceFileName,string)
        //{
        //    FileInfo fileInfo = new FileInfo(sourceFileName)
        //}
        public void Beeps(int frequency, int duration, int count, int delay)
        {
            if (delay < 0)
            {
                delay = 0;
            }
            for (int i = 0; i < count; i++)
            {
                Beep(frequency, duration);
                System.Threading.Thread.Sleep(delay);
            }

        }

        public void ClearMemory()
        {
            GC.Collect();
            GC.WaitForPendingFinalizers();
            //if (Environment.OSVersion.Platform == PlatformID.Win32NT)
            {
                SetProcessWorkingSetSize(System.Diagnostics.Process.GetCurrentProcess().Handle, -1, -1);
            }
        }
        public string GetScreenShotPath()
        {

            return  MiscFunction.GetInstance().GetAssemblyPath() + "ScreenShot\\";
        }
        public static MiscFunction GetInstance()
        {
            if (null == mSingletonMiscFunction)
            {
                mSingletonMiscFunction = new MiscFunction();
            }
            return mSingletonMiscFunction;
        }
        private MiscFunction()
        {

        }
        /// <summary>
        /// 把对象做二进制串行化
        /// </summary>
        /// <param name="fileName">Name of the file.</param>
        /// <param name="fileMode">The file mode.</param>
        /// <param name="obj">The obj.</param>
        public void SerializeBinaryFile(String fileName, FileMode fileMode, object obj)
        {
            FileStream fileStream = null;
            try
            {
                fileStream = new FileStream(fileName, fileMode);
                BinaryFormatter binaryFormatter = new BinaryFormatter();
                binaryFormatter.Serialize(fileStream, obj);
            }
            catch (System.Exception ex)
            {
                throw ex;
                //TODO: Exception Language-SerializeBinaryFile
                //throw new Exception(mResourceManager.GetString("SaveFileString") + fileName + mResourceManager.GetString("ErrorString"), ex);
            }
            finally
            {
                if (fileStream != null)
                {
                    fileStream.Close();
                }
            }
        }

        /// <summary>
        /// 将文件做二进制反串行化
        /// </summary>
        /// <param name="fileName">Name of the file.</param>
        /// <returns></returns>
        public object DeSerializeBinaryFile(String fileName)
        {
            FileStream fileStream = null;
            try
            {
                fileStream = new FileStream(fileName, FileMode.Open);
                BinaryFormatter binaryFormatter = new BinaryFormatter();
                return binaryFormatter.Deserialize(fileStream);
            }
            catch (System.Exception ex)
            {
                throw ex;
                //TODO: Exception Language-DeSerializeBinaryFile
                //throw new Exception(mResourceManager.GetString("LoadFileString") + fileName + mResourceManager.GetString("ErrorString"), ex);
            }
            finally
            {
                if (fileStream != null)
                {
                    fileStream.Close();
                }
            }
        }
        public byte GenerateCheckSum(byte[] buffer, int startPos, int length)
        {
            byte sum = 0;
            for (int i = startPos; i < length; i++)
            {
                sum += buffer[i];
            }
            return sum;
        }
        public void SerializeXMLFile(String fileName, FileMode fileMode, object obj,Type type)
        {
            FileStream fileStream = null;
            try
            {
                fileStream = new FileStream(fileName, fileMode);
                XmlSerializer xmlSerializer = new XmlSerializer(type);
                xmlSerializer.Serialize(fileStream, obj);
            }
            catch (System.Exception ex)
            {
                throw ex;
                //TODO: Exception Language-SerializeBinaryFile
                //throw new Exception(mResourceManager.GetString("SaveFileString") + fileName + mResourceManager.GetString("ErrorString"), ex);
            }
            finally
            {
                if (fileStream != null)
                {
                    fileStream.Close();
                }
            }
        }

        public object DeSerializeXMLFile(String fileName, Type type)
        {
            FileStream fileStream = null;
            try
            {
                fileStream = new FileStream(fileName, FileMode.Open);
                XmlSerializer xmlSerializer = new XmlSerializer(type);
                return xmlSerializer.Deserialize(fileStream);
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (fileStream != null)
                {
                    fileStream.Close();
                }
            }
        }
        public String GetAssemblyPath()
        {
            String _FolderPath = String.Empty;
            String _CodeBase = System.Reflection.Assembly.GetExecutingAssembly().CodeBase;
            _CodeBase = _CodeBase.Substring(8, _CodeBase.Length - 8);
            String[] arrSection = _CodeBase.Split(new char[] { '/' });
            for (int i = 0; i < arrSection.Length - 1; i++)
                _FolderPath += arrSection[i] + "\\";
            return _FolderPath;

        }

        public int CovertTimeStringToSecond(String time)
        {
            try
            {
                if (string.IsNullOrEmpty(time))
                {
                    return 0;

                }
                int second = 0;
                int j = 0;
                String[] timeString = time.Split(new char[] { ':' });
                for (int i = timeString.Length - 1; i >= 0; i--)
                {
                    second += Convert.ToInt32(timeString[i]) * Convert.ToInt32(Math.Pow(60, j));
                    j++;
                }
                return second;
            }
            catch (System.Exception ex)
            {
                //TODO: Exception Language-CovertTimeStringToSecond
                //throw new Exception(mResourceManager.GetString("CovertTimeString") + time + mResourceManager.GetString("SecondErrorString"), ex);
                System.Console.Out.WriteLine(ex.Message);
                return 0;
            }

        }
        public String DateToFileFormatString(DateTime dateTime, String connector)
        {
            return dateTime.Year.ToString() + connector + dateTime.Month.ToString("00") + connector + dateTime.Day.ToString("00");
        }
        public String TimeToFileFormatString(DateTime dateTime, String connector)
        {
            return dateTime.Year.ToString() + connector + dateTime.Month.ToString("00") +
                    connector + dateTime.Day.ToString("00") + connector + dateTime.Hour.ToString("00") +
                    connector + dateTime.Minute.ToString("00") + connector +
                    dateTime.Second.ToString("00");
        }
        public void ShowErrorMessage(Exception ex)
        {
            String errorMessage = ex.ToString();
            Exception innerEx = ex.InnerException;
            //循环显示错误信息的内部异常
            //TODO: 可定义内部异常的显示层数-ShowErrorMessage
            while (innerEx != null)
            {
                errorMessage = errorMessage + "\n\n" + innerEx.ToString();
                innerEx = innerEx.InnerException;
            }
            //TODO: MessageBox Language-ShowErrorMessage
            //MessageBox.Show(errorMessage, mResourceManager.GetString("ErrorHintString"));
        }

        public double ConvertActualIntervalFromPixel(double pixelDistance, double pixelValueRatio, double interval)
        {
            return interval * pixelValueRatio / pixelDistance;
        }
        public void CalculateLineLeastSquare(double[] xData, double[] yData, ref double slope, ref double intercept, ref double linearity)
        {
            double sumOfYData = 0;
            double sumOfXData = 0;
            double squreSumOfXData = 0;
            double squreSumOfYData = 0;
            double sumOfXYData = 0;

            double averageOfXData = 0;
            double averageOfYData = 0;
            double multipleOfSumYData = 0;
            double multipleOfSumXData = 0;
            double minatorValue = 0;
            double varianceOfXYData = 0;
            double varianceOfXData = 0;
            double varianceOfYData = 0;

            int dataCount = xData.Length;
            if (dataCount != yData.Length || dataCount <= 0)
            {
                //throw new Exception(mResourceManager.GetString("CalculateLineLeastSquareError"));
            }
            int actualDataCount = 0;
            for (int i = 0; i < dataCount; i++)
            {
                if (yData[i].Equals(double.NaN))
                {
                    continue;
                }
                actualDataCount++;
                sumOfYData += yData[i];
                sumOfXData += xData[i];
                squreSumOfYData += Math.Pow(yData[i], 2);
                squreSumOfXData += Math.Pow(xData[i], 2);
                sumOfXYData += yData[i] * xData[i];
            }

            averageOfXData = sumOfXData / actualDataCount;
            averageOfYData = sumOfYData / actualDataCount;
            multipleOfSumYData = Math.Pow(sumOfYData, 2);
            multipleOfSumXData = Math.Pow(sumOfXData, 2);
            minatorValue = actualDataCount * squreSumOfXData - multipleOfSumXData;

            intercept = (squreSumOfXData * sumOfYData - sumOfXData * sumOfXYData) / minatorValue;
            slope = (actualDataCount * sumOfXYData - sumOfYData * sumOfXData) / minatorValue;

            if (slope == 0)
            {
                linearity = -1;
            }
            else
            {
                for (int i = 0; i < dataCount; i++)
                {
                    if (yData[i].Equals(double.NaN))
                    {
                        continue;
                    }
                    varianceOfXYData += (xData[i] - averageOfXData) * (yData[i] - averageOfYData);
                    varianceOfXData += Math.Pow((xData[i] - averageOfXData), 2);
                    varianceOfYData += Math.Pow((yData[i] - averageOfYData), 2);

                }
                if (varianceOfXYData != 0)
                {

                    linearity = varianceOfXYData / Math.Sqrt(varianceOfXData * varianceOfYData);
                }
                else
                {
                    linearity = -1;
                }
            }
        }

       public bool IsValidPath(String strPath)
        {
            try
            {
                if (strPath.IndexOfAny(Path.GetInvalidPathChars()) != -1)
                    return false;
                String pathRoot = Path.GetPathRoot(strPath);

                String directoryName = Path.GetDirectoryName(strPath);
                if (string.IsNullOrEmpty(directoryName))
                {
                    if (pathRoot.Equals(strPath))
                    {
                        return true;
                        //directoryName = strPath;
                    }
                }

                String strDirectoryName = Path.Combine(
                    pathRoot == null ? "" : pathRoot,
                    directoryName == null ? "" : directoryName);
                if (strDirectoryName == "") return false;
                return strPath.Replace(strDirectoryName, "").IndexOfAny(Path.GetInvalidFileNameChars()) != -1;
            }
            catch (System.Exception)
            {
                return false;
                //TODO: Exception Language-IsValidPath
                //throw new Exception("IsValidPath", ex);
            }
        }
        public bool CheckPathIsExist(String strPath)
        {
            try
            {
                if (IsValidPath(strPath))
                {
                    return Directory.Exists(Path.GetDirectoryName(strPath));
                }
                return false;
            }
            catch (System.Exception)
            {
                return false;
            }
        }

        public bool CheckPathIsExist(string path, bool isCreatePath)
        {
            path = path.Trim();
            if (path.Length > 0)
            {
                if (!path.EndsWith("\\"))
                {
                    path = path + "\\";
                }
                if (IsValidPath(path))
                {
                    if (Directory.Exists(path))
                    {
                        return true;
                    }
                    if (isCreatePath)
                    {
                        DialogResult result = MessageBox.Show("CreateNewDirectory", "Hint", MessageBoxButtons.YesNo);
                        if (result == DialogResult.Yes)
                        {
                            try
                            {
                                Directory.CreateDirectory(path);
                                return true;
                            }
                            catch
                            {

                            }
                        }
                    }
                }
            }
            return false;
        }

        public double CalculateRotateSpeed(double diameter,double chipLength,int scanSpeed)
        {
            double speed=0;
            double circleLength = Math.PI*diameter;
            double cycleChipCount = circleLength/chipLength;
            speed = scanSpeed/cycleChipCount*60;
            return speed;
        }

        public int CalculateMotorPulseFrequency(double speed,double pulsesOneCycle)
        {
            int frequency = (int)(pulsesOneCycle * speed/60);
            return frequency;
        }

        /// <summary>
        /// 使用byte[]数据，生成256色灰度　BMP 位图
        /// </summary>
        /// <param name="originalImageData"></param>
        /// <param name="originalWidth"></param>
        /// <param name="originalHeight"></param>
        /// <returns></returns>
        public Bitmap CreateBitmap(byte[] originalImageData, int originalWidth, int originalHeight)
        {
            //指定8位格式，即256色
            Bitmap resultBitmap = new Bitmap(originalWidth, originalHeight, System.Drawing.Imaging.PixelFormat.Format8bppIndexed);
            //将该位图存入内存中
            MemoryStream curImageStream = new MemoryStream();
            resultBitmap.Save(curImageStream, System.Drawing.Imaging.ImageFormat.Bmp);
            curImageStream.Flush();

            //由于位图数据需要DWORD对齐（4byte倍数），计算需要补位的个数
            int curPadNum = ((originalWidth * 8 + 31) / 32 * 4) - originalWidth;

            //最终生成的位图数据大小
            int bitmapDataSize = ((originalWidth * 8 + 31) / 32 * 4) * originalHeight;

            //数据部分相对文件开始偏移，具体可以参考位图文件格式
            int dataOffset = ReadData(curImageStream, 10, 4);

            //改变调色板，因为默认的调色板是32位彩色的，需要修改为256色的调色板
            int paletteStart = 54;
            int paletteEnd = dataOffset;
            int color = 0;

            for (int i = paletteStart; i < paletteEnd; i += 4)
            {
                byte[] tempColor = new byte[4];
                tempColor[0] = (byte)color;
                tempColor[1] = (byte)color;
                tempColor[2] = (byte)color;
                tempColor[3] = (byte)0;
                color++;

                curImageStream.Position = i;
                curImageStream.Write(tempColor, 0, 4);
            }

            //最终生成的位图数据，以及大小，高度没有变，宽度需要调整
            byte[] destImageData = new byte[bitmapDataSize];
            int destWidth = originalWidth + curPadNum;

            //生成最终的位图数据，注意的是，位图数据 从左到右，从下到上，所以需要颠倒
            for (int originalRowIndex = originalHeight - 1; originalRowIndex >= 0; originalRowIndex--)
            {
                int destRowIndex = originalHeight - originalRowIndex - 1;

                for (int dataIndex = 0; dataIndex < originalWidth; dataIndex++)
                {
                    //同时还要注意，新的位图数据的宽度已经变化destWidth，否则会产生错位
                    destImageData[destRowIndex * destWidth + dataIndex] = originalImageData[originalRowIndex * originalWidth + dataIndex];
                }
            }

            //将流的Position移到数据段   
            curImageStream.Position = dataOffset;

            //将新位图数据写入内存中
            curImageStream.Write(destImageData, 0, bitmapDataSize);

            curImageStream.Flush();

            //将内存中的位图写入Bitmap对象
            resultBitmap = new Bitmap(curImageStream);

            return resultBitmap;
        }

        /// <summary>
        /// 从内存流中指定位置，读取数据
        /// </summary>
        /// <param name="curStream"></param>
        /// <param name="startPosition"></param>
        /// <param name="length"></param>
        /// <returns></returns>
        public int ReadData(MemoryStream curStream, int startPosition, int length)
        {
            int result = -1;

            byte[] tempData = new byte[length];
            curStream.Position = startPosition;
            curStream.Read(tempData, 0, length);
            result = BitConverter.ToInt32(tempData, 0);

            return result;
        }

        /// <summary>
        /// 向内存流中指定位置，写入数据
        /// </summary>
        /// <param name="curStream"></param>
        /// <param name="startPosition"></param>
        /// <param name="length"></param>
        /// <param name="value"></param>
        public void WriteData(MemoryStream curStream, int startPosition, int length, int value)
        {
            curStream.Position = startPosition;
            curStream.Write(BitConverter.GetBytes(value), 0, length);
        }

    }
}
