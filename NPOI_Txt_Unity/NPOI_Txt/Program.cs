using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NPOI.XSSF.UserModel;
using NPOI.SS.UserModel;
using System.IO;
using NPOI.HSSF.Util;
using NPOI.HSSF.UserModel;

namespace NPOI_Txt
{
    class Program
    {
        static void Main(string[] args)
        {
            string from = args[0];
            string to = args[1];
            string name = Path.GetFileName(from);
            int index = name.LastIndexOf(".");
            name = name.Substring(0, index);
            //string outPath = Directory.GetCurrentDirectory() + "/" + name + ".xml";

            string outputDir = to;// AppDomain.CurrentDomain.SetupInformation.ApplicationBase + "/output_txt";
            if (!Directory.Exists(outputDir))
                Directory.CreateDirectory(outputDir);
            string output = outputDir + "/" + name + ".txt";

            using (FileStream stream = File.Open(from, FileMode.Open, FileAccess.Read))
            {
                IWorkbook book = new XSSFWorkbook(stream);

                ISheet s = book.GetSheetAt(0);

                System.IO.FileStream fsTxtFile = new System.IO.FileStream(output, System.IO.FileMode.Create, System.IO.FileAccess.ReadWrite);
                System.IO.StreamWriter swTxtFile = new System.IO.StreamWriter(fsTxtFile, Encoding.GetEncoding("UTF-8"));

                IRow titleRow = s.GetRow(0);
                int column = titleRow.LastCellNum;

                for (int i = 0; i < s.LastRowNum+1; i++)
                {
                    IRow row = s.GetRow(i);
                    for (int j = 0; j < column; j++)
                    {
                        ICell cell = row.GetCell(j);
                        string cellStr = "";
                        //cell.SetCellType(CellType.String);
                        if (cell.CellType == CellType.Numeric)
                        {
                            cellStr = cell.NumericCellValue.ToString();
                        }
                        else
                        {
                            cell.SetCellType(CellType.String);
                            cellStr = cell.StringCellValue;// Convert.ToString(dt.Rows[i][j]);
                        }
                        if (string.IsNullOrEmpty(cellStr))
                        {
                            swTxtFile.Write("null");
                        }
                        else
                            swTxtFile.Write(cellStr);
                        if (j != column - 1)
                        {
                            swTxtFile.Write('\t');
                        }
                    }

                    swTxtFile.Write("\r\n");
                }
                swTxtFile.Flush();
                swTxtFile.Close();
            }
        }
    }
}
