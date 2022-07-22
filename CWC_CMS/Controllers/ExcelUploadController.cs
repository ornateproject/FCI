using CWC_CMS.Models;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;
using SIDCUL.Areas.Services.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CWC_CMS.Controllers
{
    public class ExcelUploadController : Controller
    {
        //
        // GET: /Home/
        public ActionResult Index()
        {
            if (TempData["SaveResult"] != null && TempData["SaveUpdateMessage"] != null)
            {
                ViewBag.SaveResult = Convert.ToInt32(TempData["SaveResult"]);
                ViewBag.SaveUpdateMessage = TempData["SaveUpdateMessage"].ToString();
            }

            if(TempData["IsExcelValidated"] != null)
            {
                ViewBag.IsExcelValidated = "Yes";
            }
            else
            {
                ViewBag.IsExcelValidated = "No";
            }
            var model = new MyViewModel();
            return View(model);
            
        }


        public ActionResult GenerateOfferLetter()
        {
            string[] details = null;
            if (Session["userdetailsCareer"] != null)
            {
                string userdetails = null;
                

                userdetails = Session["userdetailsCareer"].ToString();
                details = userdetails.Split('(', '@', '#', '$', ')');
                // _omChargesMasterModel.ActionPerformerEmpid = Convert.ToInt32(details[0]);

            }

            int ExamID = Convert.ToInt32(details[15]);
            string RollNo = details[5];

            string ExamName = Master.GetExamNameByExamID(ExamID);
            string PageAddressParam = Path.Combine(Server.MapPath("~/CareerPortalOfferLetterFormat/"), ExamName + ".html");
            string DesktopPath = Request.Url.Authority;
            ViewBag.DesktopPath = DesktopPath;
            string path = PageAddressParam;
            string content = System.IO.File.ReadAllText(path);
            SqlHelper osqlHelper = new SqlHelper();
            Hashtable ht = new Hashtable();
            ht.Add("@ExamID", ExamID);
            ht.Add("@RollNo", RollNo);
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();

            DataTable dtValues = new DataTable();
            ds = osqlHelper.ExecuteProcudere("PROC_GET_DATA_FOR_OFFER_LETTER_CAREER_PORTAL", ht);

            if (ds != null)
            {
                if (ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        dt = ds.Tables[0];
                        dtValues = ds.Tables[1];
                        for (int i = 0; i < dt.Rows.Count; i++ )
                        {
                            content = content.Replace("[" + dt.Rows[i][0].ToString() + "]", dtValues.Rows[0][i].ToString());
                        }
                    }
                }
            }

            CMSModel cmsModel = new CMSModel();
            cmsModel.PageAddress = PageAddressParam;
            cmsModel.InitialPageHTML = content;
            return View(cmsModel);

        }

        public ActionResult GenerateResult()
        {
            string[] details = null;
            if (Session["userdetailsCareer"] != null)
            {
                string userdetails = null;


                userdetails = Session["userdetailsCareer"].ToString();
                details = userdetails.Split('(', '@', '#', '$', ')');
                // _omChargesMasterModel.ActionPerformerEmpid = Convert.ToInt32(details[0]);

            }
            int ExamID = Convert.ToInt32(details[15]);
            string RollNo = details[5];
            string ExamName = Master.GetExamNameByExamID(ExamID);
            string PageAddressParam = Path.Combine(Server.MapPath("~/CareerPortalResultFormat/"), ExamName + ".html");
            string DesktopPath = Request.Url.Authority;
            ViewBag.DesktopPath = DesktopPath;
            string path = PageAddressParam;
            string content = System.IO.File.ReadAllText(path);
            SqlHelper osqlHelper = new SqlHelper();
            Hashtable ht = new Hashtable();
            ht.Add("@ExamID", ExamID);
            ht.Add("@RollNo", RollNo);
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();

            DataTable dtValues = new DataTable();
            ds = osqlHelper.ExecuteProcudere("PROC_GET_DATA_FOR_OFFER_LETTER_CAREER_PORTAL", ht);

            if (ds != null)
            {
                if (ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        dt = ds.Tables[0];
                        dtValues = ds.Tables[1];
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            content = content.Replace("[" + dt.Rows[i][0].ToString() + "]", dtValues.Rows[0][i].ToString());
                        }
                    }
                }
            }

            CMSModel cmsModel = new CMSModel();
            cmsModel.PageAddress = PageAddressParam;
            cmsModel.InitialPageHTML = content;

            ViewBag.IsResult = "Yes";
            return View("GenerateOfferLetter",cmsModel);

        }

        [HttpPost]
        [MultipleButton(Name = "action", Argument = "Index")]
        public ActionResult Index(MyViewModel model)
        {
            
            string ExamName = model.ExamName;

            int ExamNameRecordCount = Master.CheckIfExamNameIsAllowed(ExamName);
            


            if (ExamNameRecordCount > 0)
            {
                ViewBag.SaveResult = 1;
                ViewBag.SaveUpdateMessage = "Exam Already Created, Duplicate Entry Not Allowed.";
                return View(model);
            }


            //if (!ModelState.IsValid)
            //{
            //    return View(model);
            //}
            //string FileName = model.MyExcelFile.FileName;
            //System.IO.File.
            string Extension = System.IO.Path.GetExtension(model.MyExcelFile.FileName);
            string fileName = "Excel" + DateTime.Now.ToString("ddMMyyyyHHmmss") + Extension;

            string FileSavePath = Path.Combine(Server.MapPath("~/Temp/"), fileName);
            model.MyExcelFile.SaveAs(FileSavePath);


            //string WorkItemDocumentPath = SWCS_Integration.Upload_Single_ExcelFile(model.MyExcelFile, System.DateTime.Now.Date.ToString(), "Work_Documents", 1);


            DataTable dt = GetExcelData(FileSavePath, Extension);
            
            ExamMasterModel _ExamMasterModel = new ExamMasterModel();
            _ExamMasterModel.ExamID = 0;
            _ExamMasterModel.ExamName = ExamName;
            int ExamID = _ExamMasterModel.SaveUpdate("INSERT");
            string TableName = "TBL_TRAN_RESULT_" + ExamName;

            //GetDataTableFromSpreadsheet(model.MyExcelFile.InputStream, false);
            DataTable CreateTableDynamically = new DataTable();
            CreateTableDynamically.Columns.Add("ColumnName", typeof(string));
            CreateTableDynamically.Columns.Add("ColumnType", typeof(string));

            foreach (DataColumn column in dt.Columns)
            {
                //Console.Write(column.ColumnName);
                if (column.ColumnName.ToString().Contains("_DATE"))
                {
                    CreateTableDynamically.Rows.Add(column.ColumnName, "DATE");
                }
                else
                {
                    CreateTableDynamically.Rows.Add(column.ColumnName, "nvarchar(max)");
                }

                //CreateTableDynamically.Rows.Add(column.ColumnName, "nvarchar(max)");

            }

            SqlHelper osqlHelper = new SqlHelper();
            Hashtable ht = new Hashtable();
            ht.Add("@CreateTableDynamically", CreateTableDynamically);
            ht.Add("@TableName", TableName);
            int result = osqlHelper.ExecuteQuery("PROC_CREATE_TABLE_DYNAMICALLY", ht);



            string SQL = String.Empty;
            string Values = String.Empty;
            for(int i =0 ; i<dt.Rows.Count; i++){
                for (int j = 0; j < dt.Columns.Count; j++)
                {
                    if (j == 0)
                    {
                        Values = Values+"(" +ExamID+","+ "'" + dt.Rows[i][j].ToString().Trim() + "'";
                    }
                    else
                    {
                        Values = Values + "," + "'" + dt.Rows[i][j].ToString().Trim() + "'";
                    }
                }

                if (i != dt.Rows.Count - 1)
                {
                    Values = Values + "),";
                }
                else
                {
                    Values = Values + ")";
                }
                
                
            }

            SQL = "INSERT INTO " + TableName + " VALUES" + Values;
            MySearch _ms = new MySearch(SQL);
            _ms.ExecuteInsert();

            Master.InsertRollNoRegNoDateOfBirthForLoginFromExcel(ExamID);
            
            foreach (HttpPostedFileBase file in model.ResultImageFiles)
            {
                //Checking file is available to save.  
                if (file != null)
                {
                    var InputFileName = Path.GetFileName(file.FileName);
                    var ServerSavePath = Path.Combine(Server.MapPath("~/ImagesForCareerPortal/") + InputFileName);
                    //Save file to server folder  
                    Extension = System.IO.Path.GetExtension(file.FileName);
                    if(Extension == ".jpg" || Extension == ".png" || Extension == ".jpeg")
                    {
                        file.SaveAs(ServerSavePath);
                    }
                    
                }

            }

            string strContent = "<p>Thanks for uploading the file</p>" + ConvertDataTableToHTMLTable(dt);
            model.MSExcelTable = strContent;
            return View(model);
        }


        [HttpPost]
        [MultipleButton(Name = "action", Argument = "ValidateExcel")]
        public ActionResult ValidateExcel(MyViewModel model)
        {

            

            List<string> MandatoryFields = new List<string>();
            MandatoryFields.Add("ROLL_NO");
            MandatoryFields.Add("REGT_NO");
            MandatoryFields.Add("BIRTH_DATE");

            string Extension = System.IO.Path.GetExtension(model.MyExcelFile.FileName);
            string fileName = "Excel" + DateTime.Now.ToString("ddMMyyyyHHmmss") + Extension;

            string FileSavePath = Path.Combine(Server.MapPath("~/Temp/"), fileName);
            model.MyExcelFile.SaveAs(FileSavePath);


            //string WorkItemDocumentPath = SWCS_Integration.Upload_Single_ExcelFile(model.MyExcelFile, System.DateTime.Now.Date.ToString(), "Work_Documents", 1);


            DataTable dt = GetExcelData(FileSavePath, Extension);
            System.IO.File.Delete(FileSavePath);
            foreach (DataColumn column in dt.Columns)
            {
                if (column.ColumnName == "ROLL_NO")
                {
                    MandatoryFields.Remove("ROLL_NO");
                }
                if (column.ColumnName == "REGT_NO")
                {
                    MandatoryFields.Remove("REGT_NO");
                }
                if (column.ColumnName == "BIRTH_DATE")
                {
                    MandatoryFields.Remove("BIRTH_DATE");
                }
            }

            if ((!MandatoryFields.Any()))
            {

            }
            else
            {
                TempData["SaveResult"] = 1;
                TempData["SaveUpdateMessage"] = "Please include 'ROLL_NO', 'REGT_NO', 'BIRTH_DATE' in your excel as these are mandatory fields.";
                return RedirectToAction("Index");
            }

            

            //GetDataTableFromSpreadsheet(model.MyExcelFile.InputStream, false);
            DataTable CreateTableDynamically = new DataTable();
            CreateTableDynamically.Columns.Add("ColumnName", typeof(string));
            CreateTableDynamically.Columns.Add("ColumnType", typeof(string));
            int k = 0;
            List<int> IndexesOfDateValues = new List<int>();
            foreach (DataColumn column in dt.Columns)
            {
                //Console.Write(column.ColumnName);
                if (column.ColumnName.ToString().Contains("_DATE"))
                {
                    CreateTableDynamically.Rows.Add(column.ColumnName, "DATE");
                    IndexesOfDateValues.Add(k);
                }
                else
                {
                    CreateTableDynamically.Rows.Add(column.ColumnName, "nvarchar(max)");
                }
                k++;
                //CreateTableDynamically.Rows.Add(column.ColumnName, "nvarchar(max)");

            }

            int i = 0;
            foreach (DataRow dr in dt.Rows)
            {
                int j = 0;
                foreach (DataColumn dc in dt.Columns)
                {

                    
                    if(i == 0)
                    {

                    }
                    else
                    {
                        if (IndexesOfDateValues.Contains(j))
                        {
                            string[] DatePartsArray = dr[dc].ToString().Trim().Split('-');
                            if(DatePartsArray.Length != 3)
                            {
                                TempData["SaveResult"] = 1;
                                TempData["SaveUpdateMessage"] = "Date is not in a correct format in Excel . Kindly correct it and try to upload again.";
                                return RedirectToAction("Index");
                            }
                            else
                            {
                                if(DatePartsArray[0].Length!= 4 || DatePartsArray[1].Length != 2 || DatePartsArray[2].Length != 11)
                                {
                                    TempData["SaveResult"] = 1;
                                    TempData["SaveUpdateMessage"] = "Date is not in a correct format in Excel . Kindly correct it and try to upload again.";
                                    return RedirectToAction("Index");
                                }
                                else
                                {
                                    if(Convert.ToInt32(DatePartsArray[1]) > 12)
                                    {
                                        TempData["SaveResult"] = 1;
                                        TempData["SaveUpdateMessage"] = "Month cannot be greater than 12.";
                                        return RedirectToAction("Index");
                                    }
                                }
                            }
                        }
                    }


                    j++;
                }

                i++;
            }
            ViewBag.SaveResult = 1;
            ViewBag.SaveUpdateMessage = "Excel has been validated successfully. You can now successfully upload the excel.";
            ViewBag.IsExcelValidated = "Yes";

            return View("Index", model);
        }


        public static DataTable GetDataTableFromSpreadsheet(Stream MyExcelStream, bool ReadOnly)
        {
            DataTable dt = new DataTable();
            using (SpreadsheetDocument sDoc = SpreadsheetDocument.Open(MyExcelStream, ReadOnly))
            {
                WorkbookPart workbookPart = sDoc.WorkbookPart;
                IEnumerable<Sheet> sheets = sDoc.WorkbookPart.Workbook.GetFirstChild<Sheets>().Elements<Sheet>();
                string relationshipId = sheets.First().Id.Value;
                WorksheetPart worksheetPart = (WorksheetPart)sDoc.WorkbookPart.GetPartById(relationshipId);
                Worksheet workSheet = worksheetPart.Worksheet;
                SheetData sheetData = workSheet.GetFirstChild<SheetData>();
                IEnumerable<Row> rows = sheetData.Descendants<Row>();
                int k = 0;
                List<int> IndexesOfDateValues = new List<int>();
                List<string> TableColumnNames = new List<string>();
                foreach (Cell cell in rows.ElementAt(0))
                {
                    dt.Columns.Add(GetCellValue(sDoc, cell));
                    if (GetCellValue(sDoc, cell).Contains("_DATE"))
                    {
                        IndexesOfDateValues.Add(k);
                    }
                    k++;
                }

                int VarToSkipFirstItemOfForeachLoop = 0;
                foreach (Row row in rows) //this will also include your header row...
                {
                    DataRow tempRow = dt.NewRow();

                    for (int i = 0; i < row.Descendants<Cell>().Count(); i++)
                    {
                        Cell cell = row.Descendants<Cell>().ElementAt(i);
                        int actualCellIndex = CellReferenceToIndex(cell);
                        try
                        {
                            if (IndexesOfDateValues.Contains(i))
                            {

                                if (VarToSkipFirstItemOfForeachLoop == 0)
                                {
                                    tempRow[actualCellIndex] = GetCellValue(sDoc, cell);
                                }
                                else
                                {
                                    if (row.Descendants<Cell>().ElementAt(i).CellValue != null)
                                    {
                                        tempRow[actualCellIndex] = DateTime.FromOADate(Convert.ToDouble(GetCellValue(sDoc, cell))).Date;
                                    }
                                    else
                                    {
                                        tempRow[actualCellIndex] = "NA";
                                    }
                                }
                                VarToSkipFirstItemOfForeachLoop++;
                            }
                            else
                            {
                                tempRow[actualCellIndex] = GetCellValue(sDoc, cell);
                            }
                        }
                        catch (Exception)
                        {
                            
                            throw;
                        }
                        finally
                        {
                            
                        }
                        

                        
                        
                    }

                    dt.Rows.Add(tempRow);
                }
            }
            dt.Rows.RemoveAt(0);
            return dt;
        }

        public DataTable GetExcelData(string filePath, string extension)
        {
            try
            {
                //String strConnection = "server=localhost;User Id=root;Persist Security Info=False;database=new_prabhha1;password=P@ssw0rd";
                string oledbConnectionString = string.Empty;
                OleDbConnection conn = null;
                //oledbConnectionString = "Provider=Microsoft.Jet.OLEDB.4.0; Data Source=" + filePath + "; Extended Properties=Excel 8.0;";
                if (extension == ".xls" || extension == ".XLS")
                    oledbConnectionString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + filePath + ";" + "Extended Properties=\"Excel 8.0;HDR=YES;IMEX=1\";";
                if (extension == ".xlsx" || extension == ".XLSX")
                    oledbConnectionString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + filePath + ";" + "Extended Properties='Excel 12.0;HDR={1};'";

                // oledbConnectionString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + "http://49.50.101.77/projectmanagement/Temp/DPR Surface Hole Drilling DPR    19.04.2019 (1).xlsx" + ";" + "Extended Properties='Excel 12.0;HDR={1};'";

                // oledbConnectionString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + filePath + ";" + "Extended Properties='Excel 12.0 Xml;HDR=YES;'";
                conn = new OleDbConnection(oledbConnectionString);
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
                OleDbCommand command = new OleDbCommand("Select * from [Sheet1$]", conn);
                OleDbDataAdapter objAdapter = new OleDbDataAdapter();
                objAdapter.SelectCommand = command;
                //DataSet objDataset = new DataSet();            
                //objAdapter.Fill(ds);
                DataTable dt = new DataTable();
                objAdapter.Fill(dt);
                objAdapter.Dispose();
                conn.Close();
                conn.Dispose();
                //lbllll.Visible = true;
                //lbllll.Text = oledbConnectionString;
                return dt;
                //return objDataset.Tables[0];
                //return ds;

            }
            catch (Exception ex)
            {
                DataTable dt = new DataTable();
                //lbllll.Visible = true;
                //lbllll.Text = Convert.ToString(ex);
                return dt;

            }

        }

        private static int CellReferenceToIndex(Cell cell)
        {
            int index = 0;
            string reference = cell.CellReference.ToString().ToUpper();
            foreach (char ch in reference)
            {
                if (Char.IsLetter(ch))
                {
                    int value = (int)ch - (int)'A';
                    index = (index == 0) ? value : ((index + 1) * 26) + value;
                }
                else
                    return index;
            }
            return index;
        }



        public static string GetCellValue(SpreadsheetDocument document, Cell cell)
        {
            SharedStringTablePart stringTablePart = document.WorkbookPart.SharedStringTablePart;

            if (cell.CellValue != null)
            {
                string value = cell.CellValue.InnerXml;

                if (cell.DataType != null && cell.DataType.Value == CellValues.SharedString)
                {
                    return stringTablePart.SharedStringTable.ChildElements[Int32.Parse(value)].InnerText;
                }
                else
                {
                    return value;
                }
            }
            else
            {
                return "NA";
            }
            
        }
        public static string ConvertDataTableToHTMLTable(DataTable dt)
        {
            string ret = "";
            ret = "<table id=" + (char)34 + "tblExcel" + (char)34 + ">";
            ret += "<tr>";
            foreach (DataColumn col in dt.Columns)
            {
                ret += "<td class=" + (char)34 + "tdColumnHeader" + (char)34 + ">" + col.ColumnName + "</td>";
            }
            ret += "</tr>";
            foreach (DataRow row in dt.Rows)
            {
                ret += "<tr>";
                for (int i = 0; i < dt.Columns.Count; i++)
                {
                    ret += "<td class=" + (char)34 + "tdCellData" + (char)34 + ">" + row[i].ToString() + "</td>";
                }
                ret += "</tr>";
            }
            ret += "</table>";
            return ret;
        }
	}
}