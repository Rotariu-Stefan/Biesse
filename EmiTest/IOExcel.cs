using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ExcelLibrary.SpreadSheet;


namespace IExplorerControlsTest
{
    class IOExcel
    {
        public static void write(List<Melodie> elements)
        {
            string file = @"C:\\newdoc.xls";
            Workbook workbook = new Workbook();
            Worksheet worksheet = new Worksheet("First Sheet");

            for (int row = 0; row < elements.Count; row++ ) //2 cells / row 
                for (int cell = 0; cell < 1; cell++)
                {
                    worksheet.Cells[row, cell] = new Cell( elements[row].Artist);
                    worksheet.Cells[row, cell+1] = new Cell( elements[row].Piesa);
                }
            workbook.Worksheets.Add(worksheet);
            //workbook.Save(file);
        }
    }
}
