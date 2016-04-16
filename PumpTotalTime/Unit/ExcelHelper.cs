using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using NPOI.HPSF;
using NPOI.HSSF.UserModel;
using NPOI.HSSF.Util;
using NPOI.SS.Formula.Functions;
using NPOI.SS.UserModel;

namespace PumpTotalTime.Unit
{
    public static class ExcelHelper
    {
        public static HSSFWorkbook hssfworkbook;

        public static void fasf()
        {
            InitializeWorkbook();
            var sheet= hssfworkbook.CreateSheet("电机信息");
            var row = sheet.CreateRow(0);
            row.CreateCell(0).SetCellValue("电机ID");
            row.CreateCell(1).SetCellValue("电机名称");
            row.CreateCell(2).SetCellValue("电机运行时间");
           
            for (int i = 0; i < ConfigPump.Pumpinfos.Count; i++)
            {
                var pumpinfo = ConfigPump.Pumpinfos[i];
                var row2 = sheet.CreateRow(i+1);
                var linkCell = row2.CreateCell(0);
                   linkCell. SetCellValue(pumpinfo.PumpId);
                row2.CreateCell(1).SetCellValue(pumpinfo.PumpName);
                row2.CreateCell(2).SetCellValue(pumpinfo.TotalRunTime.ToString());


                var hlink_style = hssfworkbook.CreateCellStyle();
                var hlink_font = hssfworkbook.CreateFont();
                hlink_font.Underline=FontUnderlineType.Single;
                hlink_font.Color = HSSFColor.Blue.Index;
                hlink_style.SetFont(hlink_font);

                var link = new HSSFHyperlink(HyperlinkType.Document);
                link. Address = (pumpinfo.PumpName + "信息!A1");
            

                linkCell.Hyperlink = (link);
                linkCell.CellStyle = hlink_style;
               
               




                var sheet2 = hssfworkbook.CreateSheet(pumpinfo.PumpName + "信息");
                var row3 = sheet2.CreateRow(0);
                row3.CreateCell(0).SetCellValue("启动时间");
                row3.CreateCell(1).SetCellValue("停止时间");
                row3.CreateCell(2).SetCellValue("运行时间");
                for (int j = 0; j < pumpinfo.PumpTimes.Count; j++)
                {
                    var row4 = sheet2.CreateRow(j + 1);
                    row4.CreateCell(0).SetCellValue(pumpinfo.PumpTimes[j].StartTime);
                    row4.CreateCell(1).SetCellValue(pumpinfo.PumpTimes[j].StopTime);
                    row4.CreateCell(2).SetCellValue(pumpinfo.PumpTimes[j].RunTime.ToString());
                }
            }
            WriteToFile();
          
        

        }
        static void InitializeWorkbook()
        {
            hssfworkbook = new HSSFWorkbook();

            ////create a entry of DocumentSummaryInformation
            DocumentSummaryInformation dsi = PropertySetFactory.CreateDocumentSummaryInformation();
            dsi.Company = "NPOI Team";
            hssfworkbook.DocumentSummaryInformation = dsi;

            ////create a entry of SummaryInformation
            SummaryInformation si = PropertySetFactory.CreateSummaryInformation();
            si.Subject = "NPOI SDK Example";
            hssfworkbook.SummaryInformation = si;
        }
        static void WriteToFile()
        {
            //Write the stream data of workbook to the root directory
            FileStream file = new FileStream(@"test.xls", FileMode.Create);
            hssfworkbook.Write(file);
            file.Close();
        }
    }
}
