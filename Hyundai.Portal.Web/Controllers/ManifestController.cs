using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HyundaiPortal.Business.Service;
using HyundaiPortal.Business.Model;
using CrystalDecisions.CrystalReports.Engine;
using System.IO;
using HyundaiPortal.Business;
using System.Data;
using HyundaiPortal.Business.Util;


namespace Hyundai.Controllers
{
    [Authorize]
    public class ManifestController : BaseController
    {
        //
        // GET: /Item/
        BLService blService;

        public ManifestController()
        {
            blService = new BLService();
        }

        public ActionResult List()
        {
            return View();
        }

        public ActionResult DownloadPDF(int midx)
        {
            KoreanRomanizer converter = new KoreanRomanizer();

            var param = new ParameterModel();
            param.midx = midx;
            param.searchKey = "midx";
            param.searchText = midx.ToString();
            var mblModel = blService.getMBL(param);
            var hblModel = blService.getHBLList(param);
            var mblTable = new DataTable();
            var hblTable = new DataTable();
            mblTable.Columns.Add("FltNo");
            mblTable.Columns.Add("mblNo");
            mblTable.Columns.Add("FltDate");
            mblTable.Columns.Add("ArrivalDate");
            mblTable.Columns.Add("DptrCd");
            mblTable.Columns.Add("DestCd");
            mblTable.Columns.Add("ShipperAddress1");
            mblTable.Columns.Add("ShipperName");
            mblTable.Columns.Add("CneeName");
            mblTable.Columns.Add("CneePhone");
            var dataRow = mblTable.NewRow();
            dataRow["FltNo"] = mblModel.FltNo;
            dataRow["mblNo"] = mblModel.mblNo;
            dataRow["FltDate"] = mblModel.FltDate;
            dataRow["ArrivalDate"] = mblModel.ArrivalDate;
            dataRow["DptrCd"] = mblModel.DptrCd;
            dataRow["DestCd"] = mblModel.DestCd;
            dataRow["ShipperAddress1"] = mblModel.CUSTOMER.CustFullAddress;
            dataRow["ShipperName"] = mblModel.CUSTOMER.CustEngName;
            dataRow["CneeName"] = mblModel.CUSTOMER1.CustEngName;
            dataRow["CneePhone"] = mblModel.CUSTOMER1.WPhone;
            mblTable.Rows.Add(dataRow);
            //dataRow
            hblTable.Columns.Add("HblNo");
            hblTable.Columns.Add("ShipperName");
            hblTable.Columns.Add("ShipperAddress");
            hblTable.Columns.Add("ConsigneeName");
            hblTable.Columns.Add("ConsigneeAddress");
            hblTable.Columns.Add("Carton", typeof(int));
            hblTable.Columns.Add("Value", typeof(decimal));
            hblTable.Columns.Add("Weight", typeof(decimal));
            hblTable.Columns.Add("Memo");
            foreach (var item in hblModel)
            {
                var conEngName = item.ConsigneeEngName;
                if (string.IsNullOrEmpty(conEngName))
                {
                    conEngName = converter.romanize(item.ConsigneeName);
                }
 
                var newRow = hblTable.NewRow();
                newRow["HblNo"] = item.HblNo;
                newRow["ShipperName"] = item.ShipperName;
                newRow["ShipperAddress"] = item.ShipperAddress + item.ShipperZipAddress + "\r" + ((item.ShipperZipCode??"").Length == 4?"0":"") + item.ShipperZipCode;
                newRow["ConsigneeName"] = conEngName;
                newRow["ConsigneeAddress"] = item.EngAddress + item.EngZipaddress + "\r" + item.ConsigneeZipCode;
                newRow["Carton"] = item.Carton;
                newRow["Value"] = item.PRODUCTITEM.Sum(i=>i.ItemTotalAmt);
                newRow["Weight"] = item.Weight;
                newRow["Memo"] = string.Join("\r", item.PRODUCTITEM.Select(i => i.itemName).Take(3)) + (item.PRODUCTITEM.Count() > 3 ? "\r..." : "");
                hblTable.Rows.Add(newRow);
            }

            ReportDocument rd = new ReportDocument();
            rd.Load(Path.Combine(Server.MapPath("~/Reports"), "Manifest.rpt"));
            //rd.SetDataSource(getHblList(param));
            rd.Database.Tables[0].SetDataSource(hblTable);
            rd.Database.Tables[1].SetDataSource(mblTable);
            Response.Buffer = false;
            Response.ClearContent();
            Response.ClearHeaders();


            try
            {
                Stream stream = rd.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
                stream.Seek(0, SeekOrigin.Begin);
                return File(stream, "application/pdf", "Manifest.pdf");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
