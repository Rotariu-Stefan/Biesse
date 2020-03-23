using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.IO;
using Excel = Microsoft.Office.Interop.Excel;
using Microsoft.Office.Interop.Excel;
using System.Data.OleDb;

namespace AggiornaProrogheGeisp.Utils
{
    public static class ExcelUtil
    {

        /// <summary>
        /// this functio create a excel file with the values from a table 
        /// </summary>
        public static string Export(string path, System.Data.DataTable dt)
        {
            string output = "OK";
            try
            {
                //create a new instance of excel file 
                Excel.ApplicationClass excel = new Excel.ApplicationClass();
                excel.Application.Workbooks.Add(true);

                //first let's extract the columns and create a little header

                Excel.Worksheet worksheet = (Excel.Worksheet)excel.ActiveSheet;
                worksheet.get_Range(worksheet.Cells[1, 6], worksheet.Cells[65535, 6]).EntireColumn.NumberFormat = "@";
                //worksheet.get_Range(worksheet.Cells[1, 6], worksheet.Cells[65535, 6]).EntireColumn.NumberFormat = "#";
                worksheet.Activate();

                int horizontal = 1;
                int vertical = 1;
                foreach (DataColumn dc in dt.Columns)
                {
                    excel.Cells[vertical, horizontal] = dc.ColumnName;

                    horizontal++;
                }

                //go to next row
                vertical++;

                foreach (DataRow row in dt.Rows)
                {
                    horizontal = 1;
                    foreach (DataColumn column in dt.Columns)
                    {

                        excel.Cells[vertical, horizontal] = row[column];

                        horizontal++;
                    }
                    vertical++;
                }
                //save the excel file

                worksheet.Name = "PROROGHE Non Lavorate";
                //DirectoryInfo dr = new DirectoryInfo(path);
                //dr.Create();
                FileInfo fi = new FileInfo(path);
                if (fi.Exists == true)
                    fi.Delete();
                worksheet.SaveAs(path, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing
                , Type.Missing, Type.Missing, Type.Missing, Type.Missing);
                excel.Workbooks.Close();
                excel.Quit();

            }
            catch (Exception ex)
            {
                output = ex.Message + "\n" + ex.StackTrace;
            }
            return output;
        }

       

        static private void releaseObject(object obj)
        {
            try
            {
                System.Runtime.InteropServices.Marshal.ReleaseComObject(obj);
                obj = null;
            }
            catch 
            {
                //  Logger.Log(ex.ToString(), LogType.Error);
                obj = null;
            }
            finally
            {
                GC.Collect();
            }
        }


        public static System.Data.DataTable Import(String path)
        {
            Microsoft.Office.Interop.Excel.ApplicationClass app = new Microsoft.Office.Interop.Excel.ApplicationClass();
            Microsoft.Office.Interop.Excel.Workbook workBook = app.Workbooks.Open(path, 0, true, 5, "", "", true, Microsoft.Office.Interop.Excel.XlPlatform.xlWindows, "\t", false, false, 0, true, 1, 0);

            Microsoft.Office.Interop.Excel.Worksheet workSheet = (Microsoft.Office.Interop.Excel.Worksheet)workBook.ActiveSheet;

            int index = 0;
            object rowIndex = 1;

            System.Data.DataTable dt = new System.Data.DataTable();
            dt.Columns.Add("Velina");
            dt.Columns.Add("Contratto");
            dt.Columns.Add("StatoAgente");
            dt.Columns.Add("Banca");

            DataRow row;

            while (((Microsoft.Office.Interop.Excel.Range)workSheet.Cells[rowIndex, 2]).Value2 != null)
            {
                rowIndex = 1 + index;
                row = dt.NewRow();
                if ((((Microsoft.Office.Interop.Excel.Range)workSheet.Cells[rowIndex, 2]).Value2 != null))
                {
                    row[0] = Convert.ToString(((Microsoft.Office.Interop.Excel.Range)workSheet.Cells[rowIndex, 3]).Value2);
                    row[1] = Convert.ToString(((Microsoft.Office.Interop.Excel.Range)workSheet.Cells[rowIndex, 4]).Value2);
                    row[2] = Convert.ToString(((Microsoft.Office.Interop.Excel.Range)workSheet.Cells[rowIndex, 27]).Value2);
                    row[3] = Convert.ToString(((Microsoft.Office.Interop.Excel.Range)workSheet.Cells[rowIndex, 2]).Value2);
                    index++;
                    dt.Rows.Add(row);
                }
            }
            app.Workbooks.Close();
            return dt;
        }

        
    }
}
