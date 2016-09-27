using System;
using System.Collections.Generic;
//using System.Linq;
using System.Web;
using System.Text;
using System.Drawing;
using System.IO;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using NPOI.SS.Util;
using NPOI.HSSF.Util;
using NPOI.POIFS.FileSystem;
using NPOI.HPSF;

namespace XPay.Classes
{
      public class ExcelFuncs
    {
        Retriever ret = new Retriever();
        XObjs.Applicant c_app = new XObjs.Applicant();

        public void CreateReportExcelGenISW(System.Web.UI.Page pg, List<Classes.XObjs.ReportItemGenISW> ri,string g_tot, string filename, string sheetname)
        {
            // Create a new workbook
            var workbook = new HSSFWorkbook();
            var sheet = workbook.CreateSheet(sheetname);
            //Create Header Style
            var headerLabelCellStyle = workbook.CreateCellStyle();
            headerLabelCellStyle.Alignment = HorizontalAlignment.Center;
            headerLabelCellStyle.BorderBottom = BorderStyle.Thick;
            headerLabelCellStyle.FillForegroundColor = HSSFColor.SeaGreen.Index;
            headerLabelCellStyle.FillPattern = FillPattern.SolidForeground;

            var headerLabelFont = workbook.CreateFont();
            headerLabelFont.Boldweight = (short)FontBoldWeight.Normal;
            headerLabelFont.FontName = "Calibri";
            headerLabelFont.Color = HSSFColor.White.Index;
            headerLabelFont.FontHeightInPoints = Convert.ToInt16(8);
            headerLabelCellStyle.SetFont(headerLabelFont);

            //Create Data Style
            var dataCellStyle = workbook.CreateCellStyle();
            dataCellStyle.Alignment = HorizontalAlignment.Center;

            var dataFont = workbook.CreateFont();
            dataFont.Boldweight = (short)FontBoldWeight.Normal;
            dataFont.FontName = "Calibri";
            dataFont.FontHeightInPoints = Convert.ToInt16(8);
            dataCellStyle.SetFont(dataFont);
            // Add header labels
            var rowIndex = 0;
            var row = sheet.CreateRow(rowIndex);
            var cell = row.CreateCell(0); cell.SetCellValue("S/N"); cell.CellStyle = headerLabelCellStyle;

            cell = row.CreateCell(1); cell.SetCellValue("APPLICANT NAME"); cell.CellStyle = headerLabelCellStyle;
            cell = row.CreateCell(2); cell.SetCellValue("APPLICANT ADDRESS"); cell.CellStyle = headerLabelCellStyle;
            cell = row.CreateCell(3); cell.SetCellValue("APPLICANT E-MAIL"); cell.CellStyle = headerLabelCellStyle;
            cell = row.CreateCell(4); cell.SetCellValue("APPLICANT MOBILE"); cell.CellStyle = headerLabelCellStyle;
            cell = row.CreateCell(5); cell.SetCellValue("TRANSACTION ID"); cell.CellStyle = headerLabelCellStyle;
            cell = row.CreateCell(6); cell.SetCellValue("PAYMENT DATE"); cell.CellStyle = headerLabelCellStyle;
            cell = row.CreateCell(7); cell.SetCellValue("CONVENIENCE FEES(NGN)"); cell.CellStyle = headerLabelCellStyle;
            cell = row.CreateCell(8); cell.SetCellValue("TECH AMOUNT(NGN)"); cell.CellStyle = headerLabelCellStyle;
            cell = row.CreateCell(9); cell.SetCellValue("Qty"); cell.CellStyle = headerLabelCellStyle;
            cell = row.CreateCell(10); cell.SetCellValue("Cld_Fees"); cell.CellStyle = headerLabelCellStyle;
            cell = row.CreateCell(11); cell.SetCellValue("TOTAL FEES(NGN)"); cell.CellStyle = headerLabelCellStyle;
            cell = row.CreateCell(12); cell.SetCellValue("DATE"); cell.CellStyle = headerLabelCellStyle;
            cell = row.CreateCell(13); cell.SetCellValue("PAYMENT REF"); cell.CellStyle = headerLabelCellStyle;
            cell = row.CreateCell(14); cell.SetCellValue("ITEM CODE"); cell.CellStyle = headerLabelCellStyle;
            cell = row.CreateCell(15); cell.SetCellValue("ITEM DESCRIPTION"); cell.CellStyle = headerLabelCellStyle;
          //  cell = row.CreateCell(14); cell.SetCellValue("USED STATUS"); cell.CellStyle = headerLabelCellStyle;
            rowIndex++;
            int sn = 1;

            foreach (XPay.Classes.XObjs.ReportItemGenISW r in ri)
            {
                
                row = sheet.CreateRow(rowIndex);  //row.CreateCell(0).
                cell = row.CreateCell(0); cell.SetCellValue(sn.ToString()); cell.CellStyle = dataCellStyle;
                cell = row.CreateCell(1); cell.SetCellValue(r.applicant_name); cell.CellStyle = dataCellStyle;
                cell = row.CreateCell(2); cell.SetCellValue(r.applicant_address); cell.CellStyle = dataCellStyle;
                cell = row.CreateCell(3); cell.SetCellValue(r.applicant_xemail); cell.CellStyle = dataCellStyle;
                cell = row.CreateCell(4); cell.SetCellValue(r.applicant_xmobile); cell.CellStyle = dataCellStyle;
                cell = row.CreateCell(5); cell.SetCellValue(r.transID); cell.CellStyle = dataCellStyle;
                cell = row.CreateCell(6); cell.SetCellValue(r.TransactionDate); cell.CellStyle = dataCellStyle;
                cell = row.CreateCell(7); cell.SetCellValue(r.isw_amt); cell.CellStyle = dataCellStyle;
                cell = row.CreateCell(8); cell.SetCellValue(r.tech_amt); cell.CellStyle = dataCellStyle;
                cell = row.CreateCell(9); cell.SetCellValue(r.Qty); cell.CellStyle = dataCellStyle;
                cell = row.CreateCell(10); cell.SetCellValue(r.Cld_Fees); cell.CellStyle = dataCellStyle;
                cell = row.CreateCell(11); cell.SetCellValue(r.full_amt); cell.CellStyle = dataCellStyle;
                cell = row.CreateCell(12); cell.SetCellValue(DateTime.Now.ToString("yyyy-MM-dd")); cell.CellStyle = dataCellStyle;
                cell = row.CreateCell(13); cell.SetCellValue(r.payment_ref); cell.CellStyle = dataCellStyle;
                cell = row.CreateCell(14); cell.SetCellValue(r.item_code); cell.CellStyle = dataCellStyle;
                cell = row.CreateCell(15); cell.SetCellValue(r.item_description); cell.CellStyle = dataCellStyle;
              //  cell = row.CreateCell(14); cell.SetCellValue(r.used_status); cell.CellStyle = dataCellStyle;

                //   item.init_amt = string.Format("{0:n}", Convert.ToInt32(item.init_amt));

                sn++; rowIndex++;

            }
            // Auto-size each column
            for (var i = 0; i <= 14; i++)
            {
                sheet.AutoSizeColumn(i);

                // Bump up with auto-sized column width to account for bold headers
                sheet.SetColumnWidth(i, sheet.GetColumnWidth(i) + 1024);
            }
            // Add row indicating Grand total...
            sheet.CreateRow(rowIndex + 1).CreateCell(0).SetCellValue("Grand Total: " +g_tot);

            // Add row indicating date/time report was generated...
            sheet.CreateRow(rowIndex + 3).CreateCell(0).SetCellValue("Report generated on " + DateTime.Now.ToString());

            // NPOI Save the Excel spreadsheet to a file on the web server's file system
            //using (var fileData = new FileStream(filename, FileMode.Create))
            //{
            //    workbook.Write(fileData);
            //}
            // NPOI Save the Excel spreadsheet to a MemoryStream and return it to the client
            using (var exportData = new MemoryStream())
            {
                workbook.Write(exportData);
                pg.Response.ContentType = "application/vnd.ms-excel";
                pg.Response.AddHeader("Content-Disposition", string.Format("attachment;filename={0}", filename));
                pg.Response.Clear();
                pg.Response.BinaryWrite(exportData.GetBuffer());
                pg.Response.End();
            }
        }

        public void CreateReportExcel(System.Web.UI.Page pg, List<Classes.XObjs.ReportItem> ri, string filename, string sheetname)
        {
            // Create a new workbook
            var workbook = new HSSFWorkbook();
            var sheet = workbook.CreateSheet(sheetname);
            //Create Header Style
            var headerLabelCellStyle = workbook.CreateCellStyle();
            headerLabelCellStyle.Alignment = HorizontalAlignment.Center;
            headerLabelCellStyle.BorderBottom = BorderStyle.Thick;
            headerLabelCellStyle.FillForegroundColor = HSSFColor.SeaGreen.Index;
            headerLabelCellStyle.FillPattern = FillPattern.SolidForeground;

            var headerLabelFont = workbook.CreateFont();
            headerLabelFont.Boldweight = (short)FontBoldWeight.Normal;
            headerLabelFont.FontName = "Calibri";
            headerLabelFont.Color = HSSFColor.White.Index;
            headerLabelFont.FontHeightInPoints = Convert.ToInt16(8);
            headerLabelCellStyle.SetFont(headerLabelFont);

            //Create Data Style
            var dataCellStyle = workbook.CreateCellStyle();
            dataCellStyle.Alignment = HorizontalAlignment.Center;

            var dataFont = workbook.CreateFont();
            dataFont.Boldweight = (short)FontBoldWeight.Normal;
            dataFont.FontName = "Calibri";
            dataFont.FontHeightInPoints = Convert.ToInt16(8);
            dataCellStyle.SetFont(dataFont);
            // Add header labels
            var rowIndex = 0;
            var row = sheet.CreateRow(rowIndex);
            var cell = row.CreateCell(0); cell.SetCellValue("S/N"); cell.CellStyle = headerLabelCellStyle;

            cell = row.CreateCell(1); cell.SetCellValue("APPLICANT NAME"); cell.CellStyle = headerLabelCellStyle;
            cell = row.CreateCell(2); cell.SetCellValue("APPLICANT ADDRESS"); cell.CellStyle = headerLabelCellStyle;
            cell = row.CreateCell(3); cell.SetCellValue("APPLICANT E-MAIL"); cell.CellStyle = headerLabelCellStyle;
            cell = row.CreateCell(4); cell.SetCellValue("APPLICANT MOBILE"); cell.CellStyle = headerLabelCellStyle;

            //cell = row.CreateCell(5); cell.SetCellValue("AGENT NAME"); cell.CellStyle = headerLabelCellStyle;
            //cell = row.CreateCell(6); cell.SetCellValue("AGENT CODE"); cell.CellStyle = headerLabelCellStyle;
            //cell = row.CreateCell(7); cell.SetCellValue("AGENT E-MAIL"); cell.CellStyle = headerLabelCellStyle;
            //cell = row.CreateCell(8); cell.SetCellValue("AGENT MOBILE"); cell.CellStyle = headerLabelCellStyle;

            cell = row.CreateCell(5); cell.SetCellValue("TRANSACTION ID"); cell.CellStyle = headerLabelCellStyle;
            cell = row.CreateCell(6); cell.SetCellValue("CODE"); cell.CellStyle = headerLabelCellStyle;
            cell = row.CreateCell(7); cell.SetCellValue("DESCRIPTION"); cell.CellStyle = headerLabelCellStyle;
            cell = row.CreateCell(8); cell.SetCellValue("PAYMENT DATE"); cell.CellStyle = headerLabelCellStyle;

            cell = row.CreateCell(9); cell.SetCellValue("PAYMENT MODE"); cell.CellStyle = headerLabelCellStyle;
            cell = row.CreateCell(10); cell.SetCellValue("PAYMENT STATUS"); cell.CellStyle = headerLabelCellStyle;
            cell = row.CreateCell(11); cell.SetCellValue("INITIAL AMOUNT (NGN)"); cell.CellStyle = headerLabelCellStyle;
            cell = row.CreateCell(12); cell.SetCellValue("TECH FEES (NGN)"); cell.CellStyle = headerLabelCellStyle;
            cell = row.CreateCell(13); cell.SetCellValue("CONVENIENCE FEES(NGN)"); cell.CellStyle = headerLabelCellStyle;
            cell = row.CreateCell(14); cell.SetCellValue("TOTAL FEES(NGN)"); cell.CellStyle = headerLabelCellStyle;
            cell = row.CreateCell(15); cell.SetCellValue("CURRENT OFFICE"); cell.CellStyle = headerLabelCellStyle;
            cell = row.CreateCell(16); cell.SetCellValue("STATUS"); cell.CellStyle = headerLabelCellStyle;
            cell = row.CreateCell(17); cell.SetCellValue("DATE"); cell.CellStyle = headerLabelCellStyle;
            rowIndex++;
            int sn = 1;
            double g_tot = 0;
            foreach (XPay.Classes.XObjs.ReportItem r in ri)
            {
                double tot = 0;
                string n_init = r.init_amt.Trim().Replace(",", "");
                string n_tech = r.tech_amt.Trim();
                string n_isw = r.isw_amt.Trim();
                tot = Convert.ToDouble(n_init) + Convert.ToDouble(n_tech) + Convert.ToDouble(n_isw);
                g_tot = g_tot + tot;
                c_app = ret.getApplicantByID(r.applicantID);
                row = sheet.CreateRow(rowIndex);  //row.CreateCell(0).
                cell = row.CreateCell(0); cell.SetCellValue(sn.ToString()); cell.CellStyle = dataCellStyle;
                cell = row.CreateCell(1); cell.SetCellValue(c_app.xname); cell.CellStyle = dataCellStyle;
                cell = row.CreateCell(2); cell.SetCellValue(c_app.address); cell.CellStyle = dataCellStyle;
                cell = row.CreateCell(3); cell.SetCellValue(c_app.xemail); cell.CellStyle = dataCellStyle;
                cell = row.CreateCell(4); cell.SetCellValue(c_app.xmobile); cell.CellStyle = dataCellStyle;
                //cell = row.CreateCell(5); cell.SetCellValue(c_ai.xname); cell.CellStyle = dataCellStyle;
                //cell = row.CreateCell(6); cell.SetCellValue(c_ai.code); cell.CellStyle = dataCellStyle;
                //cell = row.CreateCell(7); cell.SetCellValue(c_ai.xemail); cell.CellStyle = dataCellStyle;
                //cell = row.CreateCell(8); cell.SetCellValue(c_ai.xmobile); cell.CellStyle = dataCellStyle;
                cell = row.CreateCell(5); cell.SetCellValue(r.newtransID); cell.CellStyle = dataCellStyle;
                cell = row.CreateCell(6); cell.SetCellValue(r.item_code); cell.CellStyle = dataCellStyle;
                cell = row.CreateCell(7); cell.SetCellValue(r.item_desc); cell.CellStyle = dataCellStyle;
                cell = row.CreateCell(8); cell.SetCellValue(r.payment_date); cell.CellStyle = dataCellStyle;
                cell = row.CreateCell(9); cell.SetCellValue(r.payment_mode); cell.CellStyle = dataCellStyle;
                cell = row.CreateCell(10); cell.SetCellValue(r.payment_status); cell.CellStyle = dataCellStyle;
                cell = row.CreateCell(11); cell.SetCellValue(r.init_amt); cell.CellStyle = dataCellStyle;
                cell = row.CreateCell(12); cell.SetCellValue(string.Format("{0:n}", Convert.ToDouble(r.tech_amt))); cell.CellStyle = dataCellStyle;
                cell = row.CreateCell(13); cell.SetCellValue(string.Format("{0:n}", Convert.ToDouble(r.isw_amt))); cell.CellStyle = dataCellStyle;
                cell = row.CreateCell(14); cell.SetCellValue(string.Format("{0:n}", Convert.ToDouble(tot))); cell.CellStyle = dataCellStyle;
                cell = row.CreateCell(15); cell.SetCellValue(r.office_status); cell.CellStyle = dataCellStyle;
                cell = row.CreateCell(16); cell.SetCellValue(r.data_status); cell.CellStyle = dataCellStyle;
                cell = row.CreateCell(17); cell.SetCellValue(DateTime.Now.ToString("yyyy-MM-dd")); cell.CellStyle = dataCellStyle;

                //   item.init_amt = string.Format("{0:n}", Convert.ToInt32(item.init_amt));

                sn++; rowIndex++;

            }
            // Auto-size each column
            for (var i = 0; i <= 11; i++)
            {
                sheet.AutoSizeColumn(i);

                // Bump up with auto-sized column width to account for bold headers
                sheet.SetColumnWidth(i, sheet.GetColumnWidth(i) + 1024);
            }
            // Add row indicating Grand total...
            sheet.CreateRow(rowIndex + 1).CreateCell(0).SetCellValue("Grand Total: " + string.Format("{0:n}", Convert.ToDouble(g_tot)));

            // Add row indicating date/time report was generated...
            sheet.CreateRow(rowIndex + 3).CreateCell(0).SetCellValue("Report generated on " + DateTime.Now.ToString());

            // NPOI Save the Excel spreadsheet to a file on the web server's file system
            //using (var fileData = new FileStream(filename, FileMode.Create))
            //{
            //    workbook.Write(fileData);
            //}
            // NPOI Save the Excel spreadsheet to a MemoryStream and return it to the client
            using (var exportData = new MemoryStream())
            {
                workbook.Write(exportData);
                pg.Response.ContentType = "application/vnd.ms-excel";
                pg.Response.AddHeader("Content-Disposition", string.Format("attachment;filename={0}", filename));
                pg.Response.Clear();
                pg.Response.BinaryWrite(exportData.GetBuffer());
                pg.Response.End();
            }
        }
        public void CreateReportExcelBank(System.Web.UI.Page pg, List<Classes.XObjs.ReportItem> ri, string filename, string sheetname)
        {
            // Create a new workbook
            var workbook = new HSSFWorkbook();
            var sheet = workbook.CreateSheet(sheetname);
            //Create Header Style
            var headerLabelCellStyle = workbook.CreateCellStyle();
            headerLabelCellStyle.Alignment = HorizontalAlignment.Center;
            headerLabelCellStyle.BorderBottom = BorderStyle.Thick;
            headerLabelCellStyle.FillForegroundColor = HSSFColor.SeaGreen.Index;
            headerLabelCellStyle.FillPattern = FillPattern.SolidForeground;

            var headerLabelFont = workbook.CreateFont();
            headerLabelFont.Boldweight = (short)FontBoldWeight.Normal;
            headerLabelFont.FontName = "Calibri";
            headerLabelFont.Color = HSSFColor.White.Index;
            headerLabelFont.FontHeightInPoints = Convert.ToInt16(8);
            headerLabelCellStyle.SetFont(headerLabelFont);

            //Create Data Style
            var dataCellStyle = workbook.CreateCellStyle();
            dataCellStyle.Alignment = HorizontalAlignment.Center;

            var dataFont = workbook.CreateFont();
            dataFont.Boldweight = (short)FontBoldWeight.Normal;
            dataFont.FontName = "Calibri";
            dataFont.FontHeightInPoints = Convert.ToInt16(8);
            dataCellStyle.SetFont(dataFont);
            // Add header labels
            var rowIndex = 0;
            var row = sheet.CreateRow(rowIndex);
            var cell = row.CreateCell(0); cell.SetCellValue("S/N"); cell.CellStyle = headerLabelCellStyle;
            cell = row.CreateCell(1); cell.SetCellValue("TRANSACTION ID"); cell.CellStyle = headerLabelCellStyle;
            cell = row.CreateCell(2); cell.SetCellValue("CODE"); cell.CellStyle = headerLabelCellStyle;
            cell = row.CreateCell(3); cell.SetCellValue("DESCRIPTION"); cell.CellStyle = headerLabelCellStyle;
            cell = row.CreateCell(4); cell.SetCellValue("PAYMENT DATE"); cell.CellStyle = headerLabelCellStyle;
            cell = row.CreateCell(5); cell.SetCellValue("INITIAL AMOUNT (NGN)"); cell.CellStyle = headerLabelCellStyle;
            cell = row.CreateCell(6); cell.SetCellValue("TECH FEES (NGN)"); cell.CellStyle = headerLabelCellStyle;
            cell = row.CreateCell(7); cell.SetCellValue("DATE"); cell.CellStyle = headerLabelCellStyle;
            rowIndex++;
            int sn = 1;
            foreach (XPay.Classes.XObjs.ReportItem r in ri)
            {
                row = sheet.CreateRow(rowIndex);  //row.CreateCell(0).
                cell = row.CreateCell(0); cell.SetCellValue(sn.ToString()); cell.CellStyle = dataCellStyle;
                cell = row.CreateCell(1); cell.SetCellValue(r.newtransID); cell.CellStyle = dataCellStyle;
                cell = row.CreateCell(2); cell.SetCellValue(r.item_code); cell.CellStyle = dataCellStyle;
                cell = row.CreateCell(3); cell.SetCellValue(r.item_desc); cell.CellStyle = dataCellStyle;
                cell = row.CreateCell(4); cell.SetCellValue(r.payment_date); cell.CellStyle = dataCellStyle;
                cell = row.CreateCell(5); cell.SetCellValue(r.init_amt); cell.CellStyle = dataCellStyle;
                cell = row.CreateCell(6); cell.SetCellValue(r.tech_amt); cell.CellStyle = dataCellStyle;
                cell = row.CreateCell(7); cell.SetCellValue(DateTime.Now.ToString("yyyy-MM-dd")); cell.CellStyle = dataCellStyle;
                sn++; rowIndex++;
            }
            // Auto-size each column
            for (var i = 0; i <= 11; i++)
            {
                sheet.AutoSizeColumn(i);

                // Bump up with auto-sized column width to account for bold headers
                sheet.SetColumnWidth(i, sheet.GetColumnWidth(i) + 1024);
            }
            // Add row indicating date/time report was generated...
            sheet.CreateRow(rowIndex + 1).CreateCell(0).SetCellValue("Report generated on " + DateTime.Now.ToString());

            // NPOI Save the Excel spreadsheet to a file on the web server's file system
            //using (var fileData = new FileStream(filename, FileMode.Create))
            //{
            //    workbook.Write(fileData);
            //}
            // NPOI Save the Excel spreadsheet to a MemoryStream and return it to the client
            using (var exportData = new MemoryStream())
            {
                workbook.Write(exportData);
                pg.Response.ContentType = "application/vnd.ms-excel";
                pg.Response.AddHeader("Content-Disposition", string.Format("attachment;filename={0}", filename));
                pg.Response.Clear();
                pg.Response.BinaryWrite(exportData.GetBuffer());
                pg.Response.End();
            }
        }
        public void CreateMemberExcel(System.Web.UI.Page pg, List<Classes.XObjs.Merchant> lt_x, string filename, string sheetname)
        {
            // Create a new workbook
            var workbook = new HSSFWorkbook();
            var sheet = workbook.CreateSheet(sheetname);
            //Create Header Style
            var headerLabelCellStyle = workbook.CreateCellStyle();
            headerLabelCellStyle.Alignment = HorizontalAlignment.Center;
            headerLabelCellStyle.BorderBottom = BorderStyle.Thick;
            headerLabelCellStyle.FillForegroundColor = HSSFColor.SeaGreen.Index;
            headerLabelCellStyle.FillPattern = FillPattern.SolidForeground;

            var headerLabelFont = workbook.CreateFont();
            headerLabelFont.Boldweight = (short)FontBoldWeight.Normal;
            headerLabelFont.FontName = "Calibri";
            headerLabelFont.Color = HSSFColor.White.Index;
            headerLabelFont.FontHeightInPoints = Convert.ToInt16(8);
            headerLabelCellStyle.SetFont(headerLabelFont);

            //Create Data Style
            var dataCellStyle = workbook.CreateCellStyle();
            dataCellStyle.Alignment = HorizontalAlignment.Center;

            var dataFont = workbook.CreateFont();
            dataFont.Boldweight = (short)FontBoldWeight.Normal;
            dataFont.FontName = "Calibri";
            dataFont.FontHeightInPoints = Convert.ToInt16(8);
            dataCellStyle.SetFont(dataFont);
            // Add header labels
            var rowIndex = 0;
            var row = sheet.CreateRow(rowIndex);
            var cell = row.CreateCell(0); cell.SetCellValue("S/N"); cell.CellStyle = headerLabelCellStyle;
            cell = row.CreateCell(1); cell.SetCellValue("CODE"); cell.CellStyle = headerLabelCellStyle;
            cell = row.CreateCell(2); cell.SetCellValue("NAME"); cell.CellStyle = headerLabelCellStyle;
            cell = row.CreateCell(3); cell.SetCellValue("COMPANY"); cell.CellStyle = headerLabelCellStyle;
            cell = row.CreateCell(4); cell.SetCellValue("E-MAIL"); cell.CellStyle = headerLabelCellStyle;
            cell = row.CreateCell(5); cell.SetCellValue("MOBILE No."); cell.CellStyle = headerLabelCellStyle;
            cell = row.CreateCell(6); cell.SetCellValue("STATUS"); cell.CellStyle = headerLabelCellStyle;
            cell = row.CreateCell(7); cell.SetCellValue("DATE"); cell.CellStyle = headerLabelCellStyle;
            rowIndex++;
            int sn = 1;
            foreach (XPay.Classes.XObjs.Merchant x in lt_x)
            {
                row = sheet.CreateRow(rowIndex);  //row.CreateCell(0).
                cell = row.CreateCell(0); cell.SetCellValue(sn.ToString()); cell.CellStyle = dataCellStyle;
                cell = row.CreateCell(1); cell.SetCellValue(x.sys_ID); cell.CellStyle = dataCellStyle;
                cell = row.CreateCell(2); cell.SetCellValue(x.xname); cell.CellStyle = dataCellStyle;
                cell = row.CreateCell(3); cell.SetCellValue(x.cname); cell.CellStyle = dataCellStyle;
                cell = row.CreateCell(4); cell.SetCellValue(x.xemail); cell.CellStyle = dataCellStyle;
                cell = row.CreateCell(5); cell.SetCellValue(x.xmobile); cell.CellStyle = dataCellStyle;
                if (x.xvisible == "1")
                {
                    cell = row.CreateCell(6); cell.SetCellValue("ACTIVE"); cell.CellStyle = dataCellStyle;
                }
                else
                {
                    cell = row.CreateCell(6); cell.SetCellValue("INACTIVE"); cell.CellStyle = dataCellStyle;
                }
                cell = row.CreateCell(7); cell.SetCellValue(DateTime.Now.ToString("yyyy-MM-dd")); cell.CellStyle = dataCellStyle;
                sn++; rowIndex++;
            }
            // Auto-size each column
            for (var i = 0; i <= 11; i++)
            {
                sheet.AutoSizeColumn(i);

                // Bump up with auto-sized column width to account for bold headers
                sheet.SetColumnWidth(i, sheet.GetColumnWidth(i) + 1024);
            }
            // Add row indicating date/time report was generated...
            sheet.CreateRow(rowIndex + 1).CreateCell(0).SetCellValue("Report generated on " + DateTime.Now.ToString());

            // NPOI Save the Excel spreadsheet to a file on the web server's file system
            //using (var fileData = new FileStream(filename, FileMode.Create))
            //{
            //    workbook.Write(fileData);
            //}
            // NPOI Save the Excel spreadsheet to a MemoryStream and return it to the client
            using (var exportData = new MemoryStream())
            {
                workbook.Write(exportData);
                pg.Response.ContentType = "application/vnd.ms-excel";
                pg.Response.AddHeader("Content-Disposition", string.Format("attachment;filename={0}", filename));
                pg.Response.Clear();
                pg.Response.BinaryWrite(exportData.GetBuffer());
                pg.Response.End();
            }
        }
        public void CreateItemsExcel(System.Web.UI.Page pg, List<Classes.XObjs.Fee_list> lt_x, string filename, string sheetname)
        {
            // Create a new workbook
            var workbook = new HSSFWorkbook();
            var sheet = workbook.CreateSheet(sheetname);
            //Create Header Style
            var headerLabelCellStyle = workbook.CreateCellStyle();
            headerLabelCellStyle.Alignment = HorizontalAlignment.Center;
            headerLabelCellStyle.BorderBottom = BorderStyle.Thick;
            headerLabelCellStyle.FillForegroundColor = HSSFColor.SeaGreen.Index;
            headerLabelCellStyle.FillPattern = FillPattern.SolidForeground;

            var headerLabelFont = workbook.CreateFont();
            headerLabelFont.Boldweight = (short)FontBoldWeight.Normal;
            headerLabelFont.FontName = "Calibri";
            headerLabelFont.Color = HSSFColor.White.Index;
            headerLabelFont.FontHeightInPoints = Convert.ToInt16(8);
            headerLabelCellStyle.SetFont(headerLabelFont);

            //Create Data Style
            var dataCellStyle = workbook.CreateCellStyle();
            dataCellStyle.Alignment = HorizontalAlignment.Center;

            var dataFont = workbook.CreateFont();
            dataFont.Boldweight = (short)FontBoldWeight.Normal;
            dataFont.FontName = "Calibri";
            dataFont.FontHeightInPoints = Convert.ToInt16(8);
            dataCellStyle.SetFont(dataFont);
            // Add header labels
            var rowIndex = 0;
            var row = sheet.CreateRow(rowIndex);
            var cell = row.CreateCell(0); cell.SetCellValue("S/N"); cell.CellStyle = headerLabelCellStyle;
            cell = row.CreateCell(1); cell.SetCellValue("CODE"); cell.CellStyle = headerLabelCellStyle;
            cell = row.CreateCell(2); cell.SetCellValue("DESCRIPTION"); cell.CellStyle = headerLabelCellStyle;
            cell = row.CreateCell(3); cell.SetCellValue("QT CODE"); cell.CellStyle = headerLabelCellStyle;
            cell = row.CreateCell(4); cell.SetCellValue("AMOUNT (NGN)"); cell.CellStyle = headerLabelCellStyle;
            cell = row.CreateCell(5); cell.SetCellValue("TECH FEES (NGN)"); cell.CellStyle = headerLabelCellStyle;
            cell = row.CreateCell(6); cell.SetCellValue("CATEGORY (NGN)"); cell.CellStyle = headerLabelCellStyle;
            cell = row.CreateCell(7); cell.SetCellValue("STATUS"); cell.CellStyle = headerLabelCellStyle;
            cell = row.CreateCell(8); cell.SetCellValue("DATE"); cell.CellStyle = headerLabelCellStyle;
            rowIndex++;
            int sn = 1;
            foreach (XPay.Classes.XObjs.Fee_list x in lt_x)
            {
                row = sheet.CreateRow(rowIndex);  //row.CreateCell(0).
                cell = row.CreateCell(0); cell.SetCellValue(sn.ToString()); cell.CellStyle = dataCellStyle;
                cell = row.CreateCell(1); cell.SetCellValue(x.item_code); cell.CellStyle = dataCellStyle;
                cell = row.CreateCell(2); cell.SetCellValue(x.item); cell.CellStyle = dataCellStyle;
                cell = row.CreateCell(3); cell.SetCellValue(x.qt_code); cell.CellStyle = dataCellStyle;
                cell = row.CreateCell(4); cell.SetCellValue(x.init_amt); cell.CellStyle = dataCellStyle;
                cell = row.CreateCell(5); cell.SetCellValue(x.tech_amt); cell.CellStyle = dataCellStyle;
                cell = row.CreateCell(6); cell.SetCellValue(x.xcategory); cell.CellStyle = dataCellStyle;
                if (x.xvisible == "1")
                {
                    cell = row.CreateCell(7); cell.SetCellValue("VISIBLE"); cell.CellStyle = dataCellStyle;
                }
                else
                {
                    cell = row.CreateCell(7); cell.SetCellValue("INVISIBLE"); cell.CellStyle = dataCellStyle;
                }
                cell = row.CreateCell(8); cell.SetCellValue(DateTime.Now.ToString("yyyy-MM-dd")); cell.CellStyle = dataCellStyle;
                sn++; rowIndex++;
            }
            // Auto-size each column
            for (var i = 0; i <= 11; i++)
            {
                sheet.AutoSizeColumn(i);

                // Bump up with auto-sized column width to account for bold headers
                sheet.SetColumnWidth(i, sheet.GetColumnWidth(i) + 1024);
            }
            // Add row indicating date/time report was generated...
            sheet.CreateRow(rowIndex + 1).CreateCell(0).SetCellValue("Report generated on " + DateTime.Now.ToString());

            // NPOI Save the Excel spreadsheet to a file on the web server's file system
            //using (var fileData = new FileStream(filename, FileMode.Create))
            //{
            //    workbook.Write(fileData);
            //}
            // NPOI Save the Excel spreadsheet to a MemoryStream and return it to the client
            using (var exportData = new MemoryStream())
            {
                workbook.Write(exportData);
                pg.Response.ContentType = "application/vnd.ms-excel";
                pg.Response.AddHeader("Content-Disposition", string.Format("attachment;filename={0}", filename));
                pg.Response.Clear();
                pg.Response.BinaryWrite(exportData.GetBuffer());
                pg.Response.End();
            }
        }

        public void CreateStructureExcel(System.Web.UI.Page pg, List<Classes.XObjs.PRatio> lt_x, string filename, string sheetname)
        {
            // Create a new workbook
            var workbook = new HSSFWorkbook();
            var sheet = workbook.CreateSheet(sheetname);
            //Create Header Style
            var headerLabelCellStyle = workbook.CreateCellStyle();
            headerLabelCellStyle.Alignment = HorizontalAlignment.Center;
            headerLabelCellStyle.BorderBottom = BorderStyle.Thick;
            headerLabelCellStyle.FillForegroundColor = HSSFColor.SeaGreen.Index;
            headerLabelCellStyle.FillPattern = FillPattern.SolidForeground;

            var headerLabelFont = workbook.CreateFont();
            headerLabelFont.Boldweight = (short)FontBoldWeight.Normal;
            headerLabelFont.FontName = "Calibri";
            headerLabelFont.Color = HSSFColor.White.Index;
            headerLabelFont.FontHeightInPoints = Convert.ToInt16(8);
            headerLabelCellStyle.SetFont(headerLabelFont);

            //Create Data Style
            var dataCellStyle = workbook.CreateCellStyle();
            dataCellStyle.Alignment = HorizontalAlignment.Center;

            var dataFont = workbook.CreateFont();
            dataFont.Boldweight = (short)FontBoldWeight.Normal;
            dataFont.FontName = "Calibri";
            dataFont.FontHeightInPoints = Convert.ToInt16(8);
            dataCellStyle.SetFont(dataFont);
            // Add header labels
            var rowIndex = 0;
            var row = sheet.CreateRow(rowIndex);
            var cell = row.CreateCell(0); cell.SetCellValue("S/N"); cell.CellStyle = headerLabelCellStyle;
            cell = row.CreateCell(1); cell.SetCellValue("PARTNER NAME"); cell.CellStyle = headerLabelCellStyle;
            cell = row.CreateCell(2); cell.SetCellValue("PARTNER COMPANY"); cell.CellStyle = headerLabelCellStyle;
            cell = row.CreateCell(3); cell.SetCellValue("RELATIONSHIP"); cell.CellStyle = headerLabelCellStyle;
            cell = row.CreateCell(4); cell.SetCellValue("RATIO VALUE (%)"); cell.CellStyle = headerLabelCellStyle;
            cell = row.CreateCell(5); cell.SetCellValue("RATIO TYPE"); cell.CellStyle = headerLabelCellStyle;
            cell = row.CreateCell(6); cell.SetCellValue("DATE"); cell.CellStyle = headerLabelCellStyle;
            rowIndex++;
            int sn = 1;
            foreach (XPay.Classes.XObjs.PRatio x in lt_x)
            {
                row = sheet.CreateRow(rowIndex);  //row.CreateCell(0).
                cell = row.CreateCell(0); cell.SetCellValue(sn.ToString()); cell.CellStyle = dataCellStyle;
                cell = row.CreateCell(1); cell.SetCellValue(x.xpartnerID); cell.CellStyle = dataCellStyle;
                cell = row.CreateCell(2); cell.SetCellValue(x.xvisible); cell.CellStyle = dataCellStyle;
                cell = row.CreateCell(3); cell.SetCellValue(x.p_type); cell.CellStyle = dataCellStyle;
                cell = row.CreateCell(4); cell.SetCellValue(x.xratio); cell.CellStyle = dataCellStyle;
                cell = row.CreateCell(5); cell.SetCellValue(x.r_type); cell.CellStyle = dataCellStyle;
                cell = row.CreateCell(6); cell.SetCellValue(DateTime.Now.ToString("yyyy-MM-dd")); cell.CellStyle = dataCellStyle;
                sn++; rowIndex++;
            }
            // Auto-size each column
            for (var i = 0; i <= 11; i++)
            {
                sheet.AutoSizeColumn(i);

                // Bump up with auto-sized column width to account for bold headers
                sheet.SetColumnWidth(i, sheet.GetColumnWidth(i) + 1024);
            }
            // Add row indicating date/time report was generated...
            sheet.CreateRow(rowIndex + 1).CreateCell(0).SetCellValue("Report generated on " + DateTime.Now.ToString());

            // NPOI Save the Excel spreadsheet to a file on the web server's file system
            //using (var fileData = new FileStream(filename, FileMode.Create))
            //{
            //    workbook.Write(fileData);
            //}
            // NPOI Save the Excel spreadsheet to a MemoryStream and return it to the client
            using (var exportData = new MemoryStream())
            {
                workbook.Write(exportData);
                pg.Response.ContentType = "application/vnd.ms-excel";
                pg.Response.AddHeader("Content-Disposition", string.Format("attachment;filename={0}", filename));
                pg.Response.Clear();
                pg.Response.BinaryWrite(exportData.GetBuffer());
                pg.Response.End();
            }
        }
    }
}

