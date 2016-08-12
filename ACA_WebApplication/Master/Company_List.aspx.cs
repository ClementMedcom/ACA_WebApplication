using BLL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Text;
using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.text.html;
using iTextSharp.text.html.simpleparser;
using System.Diagnostics;
namespace ACA_WebApplication.Master
{
    public partial class Company_List : System.Web.UI.Page
    {
        Master_BLL objMaster = new Master_BLL();
        
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Company_list(null, 1, "", 20);
            }
            txtsearch.Focus();
        }

        #region Display Company_List            
        public void Company_list(string companyTaxId,int pageIndex, string search, int PageSize)
        {
            try
            {
                DataSet ds = objMaster.list_Company(companyTaxId);
                DataTable dt = ds.Tables[0];
                IEnumerable<DataRow> query1 = from all_data in dt.AsEnumerable()
                                               where all_data.Field<string>("taxid").ToLower().StartsWith(search.ToLower()) || all_data.Field<string>("name").ToLower().StartsWith(search.ToLower())
                                               orderby all_data.Field<string>("name")
                                               select all_data;
                if (query1.Any())
                {
                    DataTable dt1 = query1.CopyToDataTable<DataRow>();
                    dt1.Columns.Add("RowNumber", typeof(System.Int32));
                    int ColumnValue = 0;
                    foreach (DataRow dr in dt1.Rows)
                    {
                        ColumnValue = ColumnValue + 1;
                        dr["RowNumber"] = ColumnValue.ToString();
                    }
                    DataTable dt_temp = (from all_data in dt1.AsEnumerable()
                                         where all_data.Field<string>("taxid").ToLower().StartsWith(search.ToLower()) || all_data.Field<string>("name").ToLower().StartsWith(search.ToLower())
                                         orderby all_data.Field<string>("name")
                                         select all_data).Skip((pageIndex - 1) * Convert.ToInt32(drp_count.Text)).Take(PageSize).CopyToDataTable<DataRow>();
                    int total_rows = dt1.Rows.Count;
                    rptCompany.DataSource = dt_temp;
                    rptCompany.DataBind();
                    hid_rowcount.Value = total_rows.ToString();
                    lbl_pagenum.Text = pageIndex.ToString();
                    int start_record = ((pageIndex - 1) * Convert.ToInt32(drp_count.Text)) + 1;
                    int End_record = ((pageIndex - 1) * Convert.ToInt32(drp_count.Text)) + Convert.ToInt32(drp_count.Text);
                    if (End_record > total_rows)
                    {
                        End_record = total_rows;
                    }
                    lbl_result.Text = "Showing Results " + start_record + "-" + End_record + " Out of " + total_rows + " Records";
                }
                else
                {
                    rptCompany.DataSource = null;
                    rptCompany.DataBind();
                    hid_rowcount.Value = "0";
                    lbl_pagenum.Text = "1";
                    lbl_result.Text = "Showing Results " + 0 + "-" + 0 + " Out of " + 0 + " Records";
                }
            }
            catch (Exception ex)
            {
                
            }
        }
        protected void rptCompany_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName == "View")
            {
                string tax_id = e.CommandArgument.ToString();
                Label lbl_name = (Label)e.Item.FindControl("lbl_name");
                Session["Tax_Id"] = tax_id;
                Session["Company_Name"] = lbl_name.Text;
                Response.Redirect("~/Master/Employer_Details.aspx");
            }
        }
        protected void txtsearch_TextChanged(object sender, EventArgs e)
        {
            Company_list(null, 1, txtsearch.Text, Convert.ToInt32(drp_count.Text));
        }
        #endregion

        #region Navigation
        protected void drp_count_SelectedIndexChanged(object sender, EventArgs e)
        {
            Company_list(null, 1, txtsearch.Text, Convert.ToInt32(drp_count.Text));
        }

        protected void btn_next_Click(object sender, EventArgs e)
        {
            double dblPageCount = (double)((decimal)Convert.ToInt32(hid_rowcount.Value) / Convert.ToDecimal(drp_count.Text));
            int pageCount = (int)Math.Ceiling(dblPageCount);
            int next_page = Convert.ToInt32(lbl_pagenum.Text) + 1;
            if (pageCount >= next_page)
                Company_list(null, Convert.ToInt32(lbl_pagenum.Text) + 1, txtsearch.Text, Convert.ToInt32(drp_count.Text));
        }

        protected void btn_previous_Click(object sender, EventArgs e)
        {
            int prev_page = Convert.ToInt32(lbl_pagenum.Text) - 1;
            if (prev_page != 0)
                Company_list(null, Convert.ToInt32(lbl_pagenum.Text) - 1, txtsearch.Text, Convert.ToInt32(drp_count.Text));
        }
        protected void btn_first_Click(object sender, EventArgs e)
        {
            Company_list(null, 1, txtsearch.Text, Convert.ToInt32(drp_count.Text));
        }

        protected void btn_last_Click(object sender, EventArgs e)
        {
            double dblPageCount = (double)((decimal)Convert.ToInt32(hid_rowcount.Value) / Convert.ToDecimal(drp_count.Text));
            int pageCount = (int)Math.Ceiling(dblPageCount);
            Company_list(null, pageCount, txtsearch.Text, Convert.ToInt32(drp_count.Text));
        }
        #endregion

        #region Export to PDF and Excel                         Developed by James.S
        //Export Displayed records to PDF or Excel
        protected void ExportToExcel(object sender, EventArgs e)
        {
            try
            {
                //Get the data from database into datatable
                DataSet ds = objMaster.list_Company(null);
                DataTable dt1 = ds.Tables[0];
                IEnumerable<DataRow> query1 = from all_data in dt1.AsEnumerable()
                                              where all_data.Field<string>("taxid").ToLower().StartsWith(txtsearch.Text.ToLower()) || all_data.Field<string>("name").ToLower().StartsWith(txtsearch.Text.ToLower())
                                              orderby all_data.Field<string>("name")
                                              select all_data;
                if (query1.Any())
                {
                    DataTable table = query1.CopyToDataTable<DataRow>();

                    if (table.Rows.Count > 0)
                    {
                        HttpContext.Current.Response.Clear();
                        HttpContext.Current.Response.ClearContent();
                        HttpContext.Current.Response.ClearHeaders();
                        HttpContext.Current.Response.Buffer = true;
                        HttpContext.Current.Response.ContentType = "application/ms-excel";
                        HttpContext.Current.Response.Write(@"<!DOCTYPE HTML PUBLIC ""-//W3C//DTD HTML 4.0 Transitional//EN"">");
                        HttpContext.Current.Response.AddHeader("Content-Disposition", "attachment;filename=Company_List.xls");
                        HttpContext.Current.Response.Charset = "utf-8";
                        HttpContext.Current.Response.ContentEncoding = System.Text.Encoding.GetEncoding("windows-1250");
                        //sets font
                        HttpContext.Current.Response.Write("<font style='font-size:10.0pt; font-family:Calibri;'>");
                        HttpContext.Current.Response.Write("<BR><BR><BR>");
                        //sets the table border, cell spacing, border color, font of the text, background, foreground, font height
                        HttpContext.Current.Response.Write("<Table border='1' bgColor='#ffffff' " +
                          "borderColor='#000000' cellSpacing='0' cellPadding='0' " +
                          "style='font-size:10.0pt; font-family:Calibri; background:white;'> <TR>");
                        HttpContext.Current.Response.Write("<Td><B>");
                        HttpContext.Current.Response.Write("Tax Id");
                        HttpContext.Current.Response.Write("</B></Td>");

                        HttpContext.Current.Response.Write("<Td><B>");
                        HttpContext.Current.Response.Write("Name");
                        HttpContext.Current.Response.Write("</B></Td>");
                        HttpContext.Current.Response.Write("<Td><B>");
                        HttpContext.Current.Response.Write("City");
                        HttpContext.Current.Response.Write("</B></Td>");
                        HttpContext.Current.Response.Write("<Td><B>");
                        HttpContext.Current.Response.Write("State");
                        HttpContext.Current.Response.Write("</B></Td>");
                        HttpContext.Current.Response.Write("<Td><B>");
                        HttpContext.Current.Response.Write("Zip Code");
                        HttpContext.Current.Response.Write("</B></Td>");
                        HttpContext.Current.Response.Write("<Td><B>");
                        HttpContext.Current.Response.Write("Country");
                        HttpContext.Current.Response.Write("</B></Td>");
                        HttpContext.Current.Response.Write("<Td><B>");
                        HttpContext.Current.Response.Write("Phone No");
                        HttpContext.Current.Response.Write("</B></Td>");
                        HttpContext.Current.Response.Write("<Td><B>");
                        HttpContext.Current.Response.Write("Contact Name");
                        HttpContext.Current.Response.Write("</B></Td>");
                        HttpContext.Current.Response.Write("</TR>");
                        foreach (DataRow row in table.Rows)
                        {
                            //write in new row
                            HttpContext.Current.Response.Write("<TR><Td>");
                            HttpContext.Current.Response.Write(row[1].ToString());
                            HttpContext.Current.Response.Write("</Td><Td>");
                            HttpContext.Current.Response.Write(row[2].ToString());
                            HttpContext.Current.Response.Write("</Td><Td>");
                            HttpContext.Current.Response.Write(row[6].ToString());
                            HttpContext.Current.Response.Write("</Td><Td>");
                            HttpContext.Current.Response.Write(row[7].ToString());
                            HttpContext.Current.Response.Write("</Td><Td>");
                            HttpContext.Current.Response.Write(row[8].ToString());
                            HttpContext.Current.Response.Write("</Td><Td>");
                            HttpContext.Current.Response.Write(row[9].ToString());
                            HttpContext.Current.Response.Write("</Td><Td>");
                            HttpContext.Current.Response.Write(row[10].ToString());
                            HttpContext.Current.Response.Write("</Td><Td>");
                            HttpContext.Current.Response.Write(row[11].ToString());
                            HttpContext.Current.Response.Write("</Td>");
                            HttpContext.Current.Response.Write("</TR>");
                        }
                        HttpContext.Current.Response.Write("</Table>");
                        HttpContext.Current.Response.Write("</font>");
                        HttpContext.Current.Response.Flush();
                        HttpContext.Current.Response.End();
                    }
                    else
                    {
                        lightDiv.Visible = true;
                        fadeDiv.Visible = true;
                        lbl_msg.Text = "No record found";
                    }
                }
            }
            catch (Exception)
            {

            }
        }
        protected void ExportToPDF(object sender, EventArgs e)
        {
            try
            {
                //Get the data from database into datatable
                DataSet ds = objMaster.list_Company(null);
                DataTable dt1 = ds.Tables[0];
                IEnumerable<DataRow> query1 = from all_data in dt1.AsEnumerable()
                                              where all_data.Field<string>("taxid").ToLower().StartsWith(txtsearch.Text.ToLower()) || all_data.Field<string>("name").ToLower().StartsWith(txtsearch.Text.ToLower())
                                              orderby all_data.Field<string>("name")
                                              select all_data;
                //DataTable dt = table;
                if (query1.Any())
                {
                    DataTable table = query1.CopyToDataTable<DataRow>();
                    table.Columns.Add("RowNumber", typeof(System.Int32));
                    int ColumnValue = 0;
                    foreach (DataRow dr in table.Rows)
                    {
                        ColumnValue = ColumnValue + 1;
                        dr["RowNumber"] = ColumnValue.ToString();
                    }
                    if (table.Rows.Count > 0)
                    {

                        Document pdfDoc = new Document(PageSize.A4, 5, 5, 2, 5);
                        try
                        {

                            PdfWriter.GetInstance(pdfDoc, System.Web.HttpContext.Current.Response.OutputStream);
                            pdfDoc.Open();
                            BaseFont bf = BaseFont.CreateFont(BaseFont.HELVETICA, BaseFont.CP1252, BaseFont.NOT_EMBEDDED);
                            //PDF Header table start here
                            PdfPTable pdftable2 = new PdfPTable(3);
                            //No border on the PDF table
                            pdftable2.DefaultCell.Border = Rectangle.NO_BORDER;
                            //Table width set in percentage  
                            pdftable2.WidthPercentage = 96;
                            float[] widths = new float[] { 100f, 60f, 100f };
                            pdftable2.SetWidths(widths);
                            //Image display on first row first column
                            string path = Server.MapPath("~\\img\\logo.png");
                            iTextSharp.text.Image jpg = iTextSharp.text.Image.GetInstance(path);
                            PdfPCell cell = new PdfPCell(jpg);
                            cell.HorizontalAlignment = 0; cell.Border = 0;
                            jpg.ScaleAbsolute(140f, 25f);
                            cell.PaddingLeft = 5f;
                            pdftable2.AddCell(cell);
                            //companies second column
                            cell = new PdfPCell(new Phrase("COMPANIES LIST", new Font(Font.FontFamily.TIMES_ROMAN, 12, Font.BOLD)));
                            cell.HorizontalAlignment = 0;  //0=Left, 1=Centre, 2=Right
                            cell.Border = 0;
                            pdftable2.AddCell(cell);
                            //Listed by is third column
                            if (txtsearch.Text == string.Empty)
                                txtsearch.Text = "All";
                            cell = new PdfPCell(new Phrase("Listed By :" + txtsearch.Text, new Font(Font.FontFamily.TIMES_ROMAN, 9, Font.BOLD)));
                            cell.HorizontalAlignment = 2; /*cell.Colspan = 3;*/ cell.Border = 0;
                            pdftable2.AddCell(cell);
                            //Second rows with colspan
                            cell = new PdfPCell(new Phrase("Total Records : " + table.Rows.Count, new Font(Font.FontFamily.TIMES_ROMAN, 9, Font.BOLD)));
                            cell.Border = 0; cell.Colspan = 2;
                            pdftable2.AddCell(cell);
                            //second row last column
                            cell = new PdfPCell(new Phrase(System.DateTime.Now.ToString(), new Font(Font.FontFamily.TIMES_ROMAN, 9, Font.BOLD)));
                            cell.HorizontalAlignment = 2; cell.Border = 0;
                            pdftable2.AddCell(cell);
                            pdftable2.SpacingBefore = 5f;
                            pdfDoc.Add(pdftable2);

                            //Company List PDF table start here
                            Font font8 = FontFactory.GetFont("ARIAL", 7);
                            BaseColor bc1 = new BaseColor(2, 148, 165);
                            BaseColor fc1 = new BaseColor(255, 255, 255);
                            //Craete instance of the pdf table and set the number of column in that table  
                            PdfPTable PdfTable = new PdfPTable(8);
                            PdfTable.WidthPercentage = 98;
                            PdfPCell PdfPCell = null;
                            PdfTable.DefaultCell.Padding = 8;
                            PdfTable.SpacingBefore = 10;
                            PdfTable.SpacingAfter = 10;
                            //column width set here
                            float[] widthee = new float[] { 20f, 32f, 140f, 55f, 20f, 22f, 40f, 80f };

                            PdfTable.SetWidths(widthee);

                            //PdfTable.DefaultCell.setFixedHeight(70);

                            //Table header
                            PdfPCell S_No = new PdfPCell(new Phrase("S.No", new Font(Font.FontFamily.TIMES_ROMAN, 11, Font.BOLD, fc1)));
                            S_No.BackgroundColor = bc1;
                            S_No.HorizontalAlignment = Element.ALIGN_CENTER;
                            PdfTable.AddCell(S_No);

                            PdfPCell Tax_Id = new PdfPCell(new Phrase("Tax ID", new Font(Font.FontFamily.TIMES_ROMAN, 11, Font.BOLD, fc1)));
                            Tax_Id.HorizontalAlignment = Element.ALIGN_CENTER;
                            Tax_Id.BackgroundColor = bc1;
                            PdfTable.AddCell(Tax_Id);

                            PdfPCell Company_Name = new PdfPCell(new Phrase("Company Name", new Font(Font.FontFamily.TIMES_ROMAN, 11, Font.BOLD, fc1)));
                            Company_Name.HorizontalAlignment = Element.ALIGN_CENTER;
                            Company_Name.BackgroundColor = bc1;
                            PdfTable.AddCell(Company_Name);

                            PdfPCell city = new PdfPCell(new Phrase("City", new Font(Font.FontFamily.TIMES_ROMAN, 11, Font.BOLD, fc1)));
                            city.HorizontalAlignment = Element.ALIGN_CENTER;
                            city.BackgroundColor = bc1;
                            PdfTable.AddCell(city);

                            PdfPCell state = new PdfPCell(new Phrase("State", new Font(Font.FontFamily.TIMES_ROMAN, 11, Font.BOLD, fc1)));
                            state.HorizontalAlignment = Element.ALIGN_CENTER;
                            state.BackgroundColor = bc1;
                            PdfTable.AddCell(state);
                            PdfPCell zip = new PdfPCell(new Phrase("Zip", new Font(Font.FontFamily.TIMES_ROMAN, 11, Font.BOLD, fc1)));
                            zip.HorizontalAlignment = Element.ALIGN_CENTER;
                            zip.BackgroundColor = bc1;
                            PdfTable.AddCell(zip);

                            //PdfPCell Country = new PdfPCell(new Phrase("Country", new Font(Font.FontFamily.TIMES_ROMAN, 11, Font.BOLD, fc1)));
                            //Country.HorizontalAlignment = Element.ALIGN_CENTER;
                            //Country.BackgroundColor = bc1;
                            //PdfTable.AddCell(Country);

                            PdfPCell Phone_No = new PdfPCell(new Phrase("Phone No", new Font(Font.FontFamily.TIMES_ROMAN, 11, Font.BOLD, fc1)));
                            Phone_No.HorizontalAlignment = Element.ALIGN_CENTER;
                            Phone_No.BackgroundColor = bc1;
                            PdfTable.AddCell(Phone_No);

                            PdfPCell Contact_Name = new PdfPCell(new Phrase("Contact Name", new Font(Font.FontFamily.TIMES_ROMAN, 11, Font.BOLD, fc1)));
                            Contact_Name.HorizontalAlignment = Element.ALIGN_CENTER;
                            Contact_Name.BackgroundColor = bc1;
                            PdfTable.AddCell(Contact_Name);

                            foreach (DataRow r in table.Rows)
                            {
                                PdfPCell = new PdfPCell(new Phrase(new Chunk(r["RowNumber"].ToString(), font8)));
                                PdfTable.AddCell(PdfPCell);

                                PdfPCell = new PdfPCell(new Phrase(new Chunk(r["taxid"].ToString(), font8)));
                                PdfTable.AddCell(PdfPCell);
                                PdfPCell = new PdfPCell(new Phrase(new Chunk(r["name"].ToString(), font8)));
                                PdfTable.AddCell(PdfPCell);
                                PdfPCell = new PdfPCell(new Phrase(new Chunk(r["city"].ToString(), font8)));
                                PdfTable.AddCell(PdfPCell);
                                PdfPCell = new PdfPCell(new Phrase(new Chunk(r["state"].ToString(), font8)));
                                PdfTable.AddCell(PdfPCell);
                                PdfPCell = new PdfPCell(new Phrase(new Chunk(r["zip"].ToString(), font8)));
                                PdfTable.AddCell(PdfPCell);
                                //PdfPCell = new PdfPCell(new Phrase(new Chunk(r["country"].ToString(), font8)));
                                //PdfTable.AddCell(PdfPCell);
                                PdfPCell = new PdfPCell(new Phrase(new Chunk(r["phoneNumber"].ToString(), font8)));
                                PdfTable.AddCell(PdfPCell);
                                PdfPCell = new PdfPCell(new Phrase(new Chunk(r["contactName"].ToString(), font8)));
                                PdfTable.AddCell(PdfPCell);

                            }

                            pdfDoc.Add(PdfTable); // add pdf table to the document   

                            pdfDoc.Close();
                            Response.ContentType = "application/pdf";
                            Response.AddHeader("content-disposition", "attachment; filename=Company_List.pdf");
                            System.Web.HttpContext.Current.Response.Write(pdfDoc);
                            Response.Flush();
                            Response.End();
                            HttpContext.Current.ApplicationInstance.CompleteRequest();
                        }
                        catch (DocumentException de)
                        {
                            System.Web.HttpContext.Current.Response.Write(de.Message);
                        }
                        catch (IOException ioEx)
                        {
                            System.Web.HttpContext.Current.Response.Write(ioEx.Message);
                        }
                        catch (Exception ex)
                        {
                            System.Web.HttpContext.Current.Response.Write(ex.Message);
                        }

                    }
                    else
                    {
                        lightDiv.Visible = true;
                        fadeDiv.Visible = true;
                        lbl_msg.Text = "No record found";
                    }
                }
            }
            catch (Exception)
            {

            }

        }
        protected void lbl_close_Click(object sender, EventArgs e)
        {
            lightDiv.Visible = false;
            fadeDiv.Visible = false;
        }
        #endregion

        #region Refresh                                 Developed by James.S
        //Reload the display details
        protected void Refresh(object sender, EventArgs e)
        {
            Company_list(null, 1, "", 20);
            txtsearch.Text = "";
            txtsearch.Focus();
        }
        #endregion
    }
}