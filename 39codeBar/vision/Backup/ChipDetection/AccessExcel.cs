using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;
using Microsoft.Office.Interop.Excel;
using Application = System.Windows.Forms.Application;
//using MiscFunctions;

namespace ChipDetection
{
    public class AccessExcel
    {


        private static AccessExcel mSingletonAccessExcel;

        private const int SW_NORMAL = 1;
        private ApplicationClass mExcel;
        private Workbook mWorkbook;
        private Worksheet mWorksheet;         
        private int currentCellRow;
        private int currentCellCol;
       // private Parameter Parameter.GetInstance();

        public AccessExcel()
        {
           // Parameter.GetInstance() = Parameter.CreateInstance();
            mExcel = new ApplicationClass();
            mExcel.DisplayAlerts = false;
            mExcel.AlertBeforeOverwriting = false;
        }
        public static AccessExcel CreateInstance()
        {
            if ( null == mSingletonAccessExcel)
            {
                mSingletonAccessExcel = new AccessExcel();
            }
            return mSingletonAccessExcel;
        }
        public string OpenExcelFile(string fileName)
        {
            try
            {
	            mWorkbook = mExcel.Workbooks.Open(fileName, 
                                                         0, false, 5, System.Reflection.Missing.Value,
	                                                     System.Reflection.Missing.Value, false,
	                                                     System.Reflection.Missing.Value,
	                                                     System.Reflection.Missing.Value, true, false,
	                                                     System.Reflection.Missing.Value, false, false, false);
	            mExcel.Visible = true;
                currentCellRow = Parameter.GetInstance().StartCellRow;
                currentCellCol = Parameter.GetInstance().StartCellCol;
                mWorksheet = (Worksheet)mWorkbook.Worksheets[1];
	            return string.Empty;
            }
            catch (System.Exception ex)
            {
                return ex.ToString();
            }
        }

        public void SaveDetectionResult(string code,int passCount,int failCount,int summaryCount,string deviceName,string operationID)
        {
            double passRate = (double)passCount/summaryCount;
            //((Range)mWorksheet.Cells[10, 3]).Value2 = failCount;
            //((Range)mWorksheet.Cells[10, 4]).Value2 = 1 - passRate;

            //((Range)mWorksheet.Cells[11, 3]).Value2 = passCount;
            //((Range)mWorksheet.Cells[11, 4]).Value2 = passRate;

            //((Range)mWorksheet.Cells[29, 3]).Value2 = summaryCount;
            //((Range)mWorksheet.Cells[30, 3]).Value2 = passCount;
            //((Range)mWorksheet.Cells[31, 3]).Value2 = passRate;
            //((Range)mWorksheet.Cells[32, 3]).Value2 = failCount;

            //((Range)mWorksheet.Cells[7, 3]).Value2 = summaryCount;
            //((Range)mWorksheet.Cells[8, 3]).Value2 = passCount;
            //((Range)mWorksheet.Cells[9, 3]).Value2 = passRate;
            //((Range)mWorksheet.Cells[10, 3]).Value2 = failCount;

            ////((Range)mWorksheet.Cells[32, 3]).Value2 = failCount;
            //((Range)mWorksheet.Cells[4, 3]).Value2 = code;
            //((Range)mWorksheet.Cells[3, 3]).Value2 = deviceName;
            //((Range)mWorksheet.Cells[5, 3]).Value2 = operationID;

            mWorksheet.Cells[7, 3] = summaryCount.ToString();
            mWorksheet.Cells[8, 3] = passCount.ToString();
            mWorksheet.Cells[9, 3] = passRate.ToString();
            mWorksheet.Cells[10, 3] = failCount.ToString();

            mWorksheet.Cells[4, 3] = code;
            mWorksheet.Cells[3, 3] = deviceName;
            mWorksheet.Cells[5, 3] = operationID;

        }
        public void AddChipStatus(string label, int chipIndex,bool isPass)
        {
           // ((Range)mWorksheet.Cells[currentCellRow, currentCellCol]).Value2 = label;
           // ((Range)mWorksheet.Cells[currentCellRow, currentCellCol]).Interior.ColorIndex = isPass ? 0 : 8;

            //if (chipIndex == 500)
            //{
                
            //}

          
            mWorksheet.get_Range(mWorksheet.Cells[currentCellRow, currentCellCol], mWorksheet.Cells[currentCellRow, currentCellCol]).Interior.ColorIndex = isPass ? 0 : 8;
            mWorksheet.Cells[currentCellRow, Parameter.GetInstance().StartCellCol - 3] = label;

            if (chipIndex == 1)
            {
               // ((Range)mWorksheet.Cells[currentCellRow, Parameter.GetInstance().StartCellCol - 3]).Value2 = chipIndex;
              //  ((Range)mWorksheet.Cells[currentCellRow, Parameter.GetInstance().StartCellCol - 1]).Value2 = "|";

                mWorksheet.Cells[currentCellRow, Parameter.GetInstance().StartCellCol - 3] = chipIndex.ToString();
                mWorksheet.Cells[currentCellRow, Parameter.GetInstance().StartCellCol - 1] = "|"; 
               
            }

            if (chipIndex % Parameter.GetInstance().RowCells==0)
            {
                currentCellCol = Parameter.GetInstance().StartCellCol;
                currentCellRow++;
               // ((Range)mWorksheet.Cells[currentCellRow, Parameter.GetInstance().StartCellCol - 3]).Value2 = chipIndex;
               // ((Range)mWorksheet.Cells[currentCellRow, Parameter.GetInstance().StartCellCol - 1]).Value2 = "|";

                mWorksheet.Cells[currentCellRow, Parameter.GetInstance().StartCellCol - 3] = chipIndex.ToString();
                mWorksheet.Cells[currentCellRow, Parameter.GetInstance().StartCellCol - 1] = "|"; 


            }
            else if (chipIndex%Parameter.GetInstance().SpaceInterval==0)
            {
                currentCellCol += Parameter.GetInstance().SpaceLength+1;
            }
            else
            {
                currentCellCol++;
            }
        }

        public string SaveExcelFile(string fileName)
        {
            try
            {
                mWorkbook.SaveAs(fileName, System.Reflection.Missing.Value, System.Reflection.Missing.Value,
                    System.Reflection.Missing.Value, System.Reflection.Missing.Value,
                    System.Reflection.Missing.Value, XlSaveAsAccessMode.xlNoChange,
                    System.Reflection.Missing.Value, System.Reflection.Missing.Value,
                    System.Reflection.Missing.Value, System.Reflection.Missing.Value,
                    System.Reflection.Missing.Value);
                return string.Empty;
            }
            catch (System.Exception ex)
            {
                return ex.ToString();
            }
        }

        public bool Close()
        {
            if (mExcel != null)
            {
                mExcel.Quit();
            }
            return true;
        }
         
    }
}
