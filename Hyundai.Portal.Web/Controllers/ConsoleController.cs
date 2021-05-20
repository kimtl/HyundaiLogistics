using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using System.IO;
using System.Web.UI;
using HyundaiPortal.Business.Service;
using HyundaiPortal.Business.Model;
using HyundaiPortal.Business.Util;
using System.Data;

namespace Hyundai.Controllers
{
    public class ConsoleController : Controller
    {
        //
        // GET: /Console/
        public BLService blService;
        public ConsoleService consoleService;

        public ConsoleController()
        {
            blService = new BLService();
            consoleService = new ConsoleService();
        }

        public ActionResult Manage()
        {
            return View();
        }

        public ActionResult HblExport(int midx)
        {
            var hblList = blService.getHBLList(new ParameterModel { searchKey = "midx", searchText = midx.ToString() });

            GridView gv = new GridView();
            var excelData = new DataTable();
            excelData.Columns.Add("No");
            excelData.Columns.Add("MAWBNo");
            excelData.Columns.Add("HAWBNo");
            excelData.Columns.Add("주문번호");
            excelData.Columns.Add("발송일");
            excelData.Columns.Add("업체코드");
            excelData.Columns.Add("발송인이름");
            excelData.Columns.Add("주소");
            excelData.Columns.Add("city");
            excelData.Columns.Add("state");
            excelData.Columns.Add("우편번호");
            excelData.Columns.Add("전화번호");
            excelData.Columns.Add("수취인이름");
            excelData.Columns.Add("수취인우편번호");
            excelData.Columns.Add("주소1");
            excelData.Columns.Add("주소2");
            excelData.Columns.Add("수취인전화번호");
            excelData.Columns.Add("휴대폰");
            excelData.Columns.Add("메모");
            excelData.Columns.Add("주민번호");
            excelData.Columns.Add("통관고유번호");
            excelData.Columns.Add("통관구분");
            excelData.Columns.Add("항목구분");
            excelData.Columns.Add("HS코드");
            excelData.Columns.Add("무게");
            excelData.Columns.Add("Carton");
            excelData.Columns.Add("품명");
            excelData.Columns.Add("URL");
            excelData.Columns.Add("수량");
            excelData.Columns.Add("단가");
            excelData.Columns.Add("합계");
            int cnt = 0;
            int hblidx = 0;
            foreach(var hbl in hblList)
            {
                hblidx++;
                DataRow row = excelData.NewRow();
                row["No"] = hblidx.ToString();
                row["MAWBNo"] = hbl.MBL.mblNo;
                row["HAWBNo"] = hbl.HblNo;
                row["주문번호"] = "";
                row["발송일"] = hbl.OnBoardDate;
                row["업체코드"] = hbl.ShipperCd;
                row["발송인이름"] = hbl.ShipperName;
                row["주소"] = hbl.ShipperAddress;
                row["city"] = hbl.ShipperCity;
                row["state"] = hbl.ShipperState;
                row["우편번호"] = hbl.ShipperZipCode;
                row["전화번호"] = hbl.ShipperPhone;
                row["수취인이름"] = hbl.ConsigneeName;
                row["수취인우편번호"] = hbl.ConsigneeZipCode;
                row["주소1"] = hbl.ConsigneeZipAddress;
                row["주소2"] = hbl.ConsigneeAddress;
                row["수취인전화번호"] = hbl.ConsigneePhone;
                row["휴대폰"] = hbl.ConsigneeCellPhone;
                row["메모"] = hbl.Memo;
                row["주민번호"] = hbl.juminNo;
                row["통관고유번호"] = "";
                row["통관구분"] = hbl.CODE2.CD;
                row["항목구분"] = "";
                row["HS코드"] = "";
                row["무게"] = hbl.Weight;
                row["Carton"] = hbl.Carton;
                row["품명"] = "";
                row["URL"] = "";
                row["수량"] = "";
                row["단가"] = "";
                row["합계"] = "";
                excelData.Rows.Add(row);
                int itemCnt = 0;
                foreach (var item in hbl.PRODUCTITEM)
                {
                    if (itemCnt == 0)
                    {
                        excelData.Rows[cnt]["품명"] = item.itemName;
                        excelData.Rows[cnt]["URL"] = item.url ;
                        excelData.Rows[cnt]["수량"] = item.itemQty;
                        excelData.Rows[cnt]["단가"] = item.itemAmt;
                        excelData.Rows[cnt]["합계"] = item.ItemTotalAmt;
                    }
                    else
                    {
                        var itemRow = excelData.NewRow();
                        itemRow["품명"] = item.itemName;
                        itemRow["URL"] = item.url;
                        itemRow["수량"] = item.itemQty;
                        itemRow["단가"] = item.itemAmt;
                        itemRow["합계"] = item.ItemTotalAmt;
                        excelData.Rows.InsertAt(itemRow, cnt + 1);
                        cnt++;
                    }
                    itemCnt++;
                }
                cnt++;
            }
            gv.DataSource = excelData;
            gv.DataBind();

            Response.ClearContent();
            Response.Buffer = true;
            Response.AddHeader("content-disposition", "attachment; filename=ConsoleList.xls");
            Response.ContentType = "application/ms-excel";
            Response.Charset = "euc-kr";
            Response.ContentEncoding = System.Text.Encoding.GetEncoding("euc-kr");
            StringWriter sw = new StringWriter();
            HtmlTextWriter htw = new HtmlTextWriter(sw);
            gv.RenderControl(htw);
            Response.Output.Write(sw.ToString());
            Response.Flush();
            Response.End();

            return View();
        }

        public ActionResult AsianaExport(int midx)
        {
            var hblList = blService.getHBLList(new ParameterModel { searchKey = "midx", searchText = midx.ToString() });

            GridView gv = new GridView();
            var excelData = new DataTable();
            excelData.Columns.Add("AWPR No.");
            excelData.Columns.Add("MAWB No.");
            excelData.Columns.Add("HAWB No.");
            excelData.Columns.Add("Origin.");
            excelData.Columns.Add("Dest.");
            excelData.Columns.Add("Piece");
            excelData.Columns.Add("Weight (KG)");
            excelData.Columns.Add("Commodity");
            excelData.Columns.Add("Remarks");
            excelData.Columns.Add("Name");
            excelData.Columns.Add("Address");
            excelData.Columns.Add("Place");
            excelData.Columns.Add("Country code");
            excelData.Columns.Add("State");
            excelData.Columns.Add("Post/Zip Code");
            excelData.Columns.Add("Tel No.");
            excelData.Columns.Add("Fax No.");
            excelData.Columns.Add("Name1");
            excelData.Columns.Add("Address1");
            excelData.Columns.Add("Place1");
            excelData.Columns.Add("State1");
            excelData.Columns.Add("Country code1");
            excelData.Columns.Add("Post/Zip Code1");
            excelData.Columns.Add("Tel No.1");
            excelData.Columns.Add("Fax No.1");
            
            int cnt = 0;
            int hblidx = 0;
            foreach (var hbl in hblList)
            {
                hblidx++;
                DataRow row = excelData.NewRow();
                row[1] = StringUtil.TruncateNumeric(hbl.MBL.mblNo, 8);
                row[2] = StringUtil.TruncateNumeric(hbl.HblNo, 12);
                row[3] = StringUtil.Truncate(hbl.MBL.DptrCd, 3);
                row[4] = StringUtil.Truncate(hbl.MBL.DestCd, 3);
                row[5] = hbl.Carton;
                row[6] = StringUtil.Ceiling((decimal)hbl.Weight);
                row[7] = StringUtil.Truncate(StringUtil.RemoveSpecial(hbl.PRODUCTITEM.FirstOrDefault() == null ? "" : hbl.PRODUCTITEM.FirstOrDefault().itemName), 40);
                row[9] = hbl.ShipperEngName == null ? hbl.ShipperName : hbl.ShipperName;
                row[10] = StringUtil.Truncate(StringUtil.RemoveSpecial(hbl.ShipperAddress), 35);
                row[11] = (StringUtil.Truncate(hbl.ShipperCity, 15) ?? "").ToUpper();
                row[12] = "US";
                row[13] = hbl.ShipperState == null ? "" : StringUtil.ConvertState(hbl.ShipperState);
                row[14] = StringUtil.RemoveSpecial(hbl.ShipperZipCode);
                row[15] = StringUtil.RemoveSpecial(hbl.ShipperPhone);
                row[17] = StringUtil.Truncate(StringUtil.RemoveSpecial(hbl.ConsigneeEngName), 35);
                row[18] = StringUtil.Truncate(StringUtil.RemoveSpecial(hbl.EngAddress + hbl.EngZipaddress),35);
                row[19] = StringUtil.RemoveSpecial(hbl.EngZipaddress.Split(' ').Last());
                row[21] = "KR";
                row[22] = StringUtil.RemoveSpecial(hbl.ConsigneeZipCode);
                row[23] = StringUtil.RemoveSpecial(hbl.ConsigneePhone);
                
                excelData.Rows.Add(row);
                cnt++;
            }
            gv.DataSource = excelData;
            gv.DataBind();

            Response.ClearContent();
            Response.Buffer = true;
            Response.AddHeader("content-disposition", "attachment; filename=AsianaList.xls");
            Response.ContentType = "application/ms-excel";
            Response.Charset = "euc-kr";
            Response.ContentEncoding = System.Text.Encoding.GetEncoding("euc-kr");
            StringWriter sw = new StringWriter();
            HtmlTextWriter htw = new HtmlTextWriter(sw);
            gv.RenderControl(htw);
            Response.Output.Write(sw.ToString());
            Response.Flush();
            Response.End();

            return View();
        }

        public ActionResult setRelease(int midx)
        {
            ResultModel result = new ResultModel();
            try
            {
                result = consoleService.setRelease(midx);
            }
            catch (Exception ex)
            {
                result.ResultCode = -1;
                result.ResultMessage = ex.Message;
                throw ex;
            }

            return Json(result);
        }
    }
}
