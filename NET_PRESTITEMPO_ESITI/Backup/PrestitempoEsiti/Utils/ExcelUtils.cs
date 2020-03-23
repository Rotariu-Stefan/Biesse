using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Excel = Microsoft.Office.Interop.Excel;
using System.Windows.Forms;
using System.Data;
using System.IO;
namespace PrestitempoInserimento.Utils
{
    class ExcelUtils
    {
        public static string WriteExcel(string name,List<DataTable> dataTables, List<string> SheetsNames)
        {
            string path = ConfigUtils.GetExcelPath() + name + ".xls";
            if (File.Exists(path))
            {
               string lastNumber ;
               lastNumber = name.Substring(name.Length - 1, 1);
               name = name.Substring(0, name.Length - 1); 
               int numVal = Convert.ToInt32(lastNumber);
               numVal++;
               name = name + Convert.ToString(numVal);
               path = ConfigUtils.GetExcelPath() + name + ".xls";
            }

            Logger.Log("Creating file " +AppDomain.CurrentDomain.BaseDirectory+ name+".xls", LogType.Operation);

                Excel.Application xlApp ;
                Excel.Workbook xlWorkBook ;

                object misValue = System.Reflection.Missing.Value;
                try
                {
                    xlApp = new Excel.Application();
                    xlWorkBook = xlApp.Workbooks.Add(misValue);
                    for (int i = 0; i < dataTables.Count; i++)
                    {


                        Excel.Worksheet xlWorkSheet;
                        xlWorkSheet = (Excel.Worksheet)xlWorkBook.Worksheets.get_Item(i+1);
                        xlWorkSheet.Name = SheetsNames[i];
                        fiilHeadersWorkSheet(xlWorkSheet, dataTables[i]);
                        fillWorkSheet(xlWorkSheet, dataTables[i]);
                        releaseObject(xlWorkSheet);
                    }
                    
                    try
                    {
                        File.Delete(path);
                    }
                    catch { }
                    try
                    {
                        xlWorkBook.SaveAs(path, Excel.XlFileFormat.xlWorkbookNormal, misValue, misValue, misValue, misValue, Excel.XlSaveAsAccessMode.xlExclusive, misValue, misValue, misValue, misValue, misValue);

                    }
                    catch
                    {
                        try
                        {
                            string directory = AppDomain.CurrentDomain.BaseDirectory;
                            try
                            {
                                File.Delete(directory + name + ".xls");
                            }
                            catch { }
                            xlWorkBook.SaveAs(directory + name + ".xls", Excel.XlFileFormat.xlWorkbookNormal, misValue, misValue, misValue, misValue, Excel.XlSaveAsAccessMode.xlExclusive, misValue, misValue, misValue, misValue, misValue);
                        }
                        catch (Exception ex)
                        {
                            MailUtils.Send("Impossibile creare il file di excel",
                                            "PRESTITEMPO ESITI: errori nell’inserimento degli esiti su sito committente",
                                            new List<string>(),
                                            ConfigUtils.GetCaricoAddress(), ConfigUtils.GetPostAddress());
                            throw (ex);
                        }
                    }
                    xlWorkBook.Close(true, misValue, misValue);
                    xlApp.Quit();
                    Logger.Log("Created file" + name + ".xls", LogType.Operation);
                    releaseObject(xlWorkBook);
                    releaseObject(xlApp);
                }
                catch (Exception ex)
                {
                    Logger.Log("Could not create file " + name + ".xls" + "\n" + ex.StackTrace.ToString(), LogType.Error);
                }

                return path;
            
        }
        private static void fiilHeadersWorkSheet(Excel.Worksheet xlWorkSheet,DataTable dataTable)
        {
            int i = 1;
            foreach (DataColumn c in dataTable.Columns)
            {
                string u = c.ColumnName.ToString();
                xlWorkSheet.Cells[1, i] = u;
                i++;
            }
        }
        private static void fillWorkSheet(Excel.Worksheet xlWorkSheet, DataTable dataTable)
        {
            int xlRow =2;
            int xlCol =1;
            foreach (DataRow dr in dataTable.Rows)
            {
                    xlCol = 1;
                    foreach (var item in  dr.ItemArray)
                    {
                        xlWorkSheet.Cells[xlRow, xlCol] = item.ToString();
                        xlCol++;
                    }
                    xlRow++;
             }
        }
        private static void releaseObject(object obj)
        {
            try
            {
                System.Runtime.InteropServices.Marshal.ReleaseComObject(obj);
                obj = null;
            }
            catch (Exception ex)
            {
                obj = null;
                Logger.Log("Exception Occured while releasing object " + ex.StackTrace.ToString(), LogType.Error);
            }
            finally
            {
                GC.Collect();
            }
        }
    }
}
