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

        public static void ExportToExcel()
        {
            InitializeWorkbook();
            //创建日期格式
            var format = hssfworkbook.CreateDataFormat();
            var datetimestyle = hssfworkbook.CreateCellStyle();
            datetimestyle.DataFormat = format.GetFormat("yyyy年m月d日h时mm分");
         

            //创建超链接格式
            var hlinkFont = hssfworkbook.CreateFont();
            hlinkFont.Underline = FontUnderlineType.Single;
            hlinkFont.Color = HSSFColor.Blue.Index;
            var hlinkStyle = hssfworkbook.CreateCellStyle();
            hlinkStyle.SetFont(hlinkFont);

            //创建表头
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


               

                var link = new HSSFHyperlink(HyperlinkType.Document);
                link. Address = (pumpinfo.PumpName + "信息!A1");
                linkCell.Hyperlink = (link);
                linkCell.CellStyle = hlinkStyle;
               
               




                var sheet2 = hssfworkbook.CreateSheet(pumpinfo.PumpName + "信息");
                var row3 = sheet2.CreateRow(0);
                row3.CreateCell(0).SetCellValue("启动时间");
                row3.CreateCell(1).SetCellValue("停止时间");
                row3.CreateCell(2).SetCellValue("运行时间");
                for (int j = 0; j < pumpinfo.PumpTimes.Count; j++)
                {
                    var row4 = sheet2.CreateRow(j + 1);
                    var cell2 = row4.CreateCell(0);
                        cell2.SetCellValue(pumpinfo.PumpTimes[j].StartTime);
                        cell2.CellStyle = datetimestyle;
                    var cell3 = row4.CreateCell(1);
                        cell3.SetCellValue(pumpinfo.PumpTimes[j].StopTime);
                        cell3.CellStyle = datetimestyle;
                    row4.CreateCell(2).SetCellValue(pumpinfo.PumpTimes[j].RunTime.ToString());
                  
                    
                }
                sheet2.DefaultColumnWidth =22;
            

            }
            sheet.AutoSizeColumn(0);
            sheet.AutoSizeColumn(1);
            sheet.AutoSizeColumn(2);
         
            WriteToFile();
          
            
          
        

        }
        static void InitializeWorkbook()
        {
            hssfworkbook = new HSSFWorkbook();

            ////create a entry of DocumentSummaryInformation
            DocumentSummaryInformation dsi = PropertySetFactory.CreateDocumentSummaryInformation();
            dsi.Company = "Cuijian";
            hssfworkbook.DocumentSummaryInformation = dsi;

            ////create a entry of SummaryInformation
            SummaryInformation si = PropertySetFactory.CreateSummaryInformation();
            si.Subject = "CodeCj";
            hssfworkbook.SummaryInformation = si;
        }
        static void WriteToFile()
        {
            //Write the stream data of workbook to the root directory
            FileStream file = new FileStream(DateTime.Now.ToString("yy年M月dd日h时mm分")+"导出.xls", FileMode.Create);
            hssfworkbook.Write(file);
            file.Close();
            if (MessageBox.Show("导出成功,是否打开?", "成功", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                var path = Application.StartupPath + "\\"+DateTime.Now.ToString("yy年M月dd日h时mm分") + "导出.xls";
                System.Diagnostics.Process.Start(path);
            }
        }
    }
}
