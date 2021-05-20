using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.Collections.Generic;
using System.Configuration;
using System.Web.Mvc;
namespace HyundaiPortal.Business.Utility
{
    public class GeneratePDF
    {
        public string sourcePdfPath = string.Empty;
        public string savePdfPath = string.Empty;
        public MBL mbl;
        public IList<OTHERCHARGE> otherChargeList;
        private Document doc;
        private PdfWriter writer;
        private PdfContentByte cb;


        public GeneratePDF(MBL Mbl, IList<OTHERCHARGE>  OCList)
        {
            mbl = Mbl;
            otherChargeList = OCList;
        }

        public void GenerateMBLPDF()
        {

            // Source 파일 불러올 파일경로
            string SOURCE_FILE_NAME = "MBL_SOURCE.pdf";
            string SOURCE_PATH_FILENAME = sourcePdfPath + SOURCE_FILE_NAME;
            // 저장경로 생성
            Util.CreateFolder(savePdfPath);

            string _pdfFile = mbl.mblNo.Trim()+".pdf";
            string _PDF = savePdfPath + _pdfFile;

            try
            {
                // MASTER BL
                MemoryStream pdfMBL = setFormMBL(SOURCE_PATH_FILENAME);
                PdfReader reader1 = new PdfReader(pdfMBL.GetBuffer());

                doc = new Document(PageSize.LETTER, 0, 0, 0, 0);
                Document.Compress = true;
                writer = PdfWriter.GetInstance(doc, new FileStream(_PDF, FileMode.Create));
                doc.AddDocListener(writer);
                doc.Open();

                //PdfContentByte cb = writer.DirectContent;
                cb = writer.DirectContent;
                PdfImportedPage page;

                #region MASTER BL
                doc.NewPage();
                page = writer.GetImportedPage(reader1, 1);
                cb.AddTemplate(page, 0, 0);
                #endregion

                // Set additional information
                doc.AddCreationDate();
                doc.AddSubject("");
                doc.AddCreator("");
                doc.AddAuthor("");

                doc.Close();
                reader1.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        // 08212014 YONGHAK KIM
        // ######################################################################
        //
        //                          HYUNDAI FREIGHT MASTER BL PRINT
        //
        // ######################################################################

        public MemoryStream setFormMBL(string _file)
        {
            MemoryStream ms = new MemoryStream();

            //BINDING DATA
            PdfReader reader = new PdfReader(_file);
            PdfStamper stamper = new PdfStamper(reader, ms);

            AcroFields fields = stamper.AcroFields;

            fields.SetField("mbl1", mbl.mblNo.Substring(0,3));
            fields.SetField("mbl2", "JFK");
            fields.SetField("mbl3", mbl.mblNo.Substring(3, 4) + " " + mbl.mblNo.Substring(7));

            fields.SetField("mbl21", mbl.mblNo.Substring(0, 3) + " -");
            fields.SetField("mbl22", mbl.mblNo.Substring(3, 4) +" "+ mbl.mblNo.Substring(7));

            fields.SetField("mbl31", mbl.mblNo.Substring(0, 3) + " -");
            fields.SetField("mbl32", mbl.mblNo.Substring(3, 4) +" "+ mbl.mblNo.Substring(7));


            fields.SetField("ShipperName", mbl.ShipperFullAddress);
            fields.SetField("ShipperName4", mbl.ShipperName);

            fields.SetField("CneeName", mbl.CneeFullAddress);
            fields.SetField("IssuingCarrierName", mbl.IssuingCarrierName);

            fields.SetField("IATA", mbl.IATA);
            fields.SetField("AccountNo", mbl.AccountNo);


            fields.SetField("DptrName", mbl.DptrName);
            fields.SetField("DestCd", mbl.DestCd);
            fields.SetField("DestName", mbl.DestName);
            fields.SetField("FltNo", mbl.FltNo);
            fields.SetField("FltNo2", mbl.FltNo);
            fields.SetField("FltDate", String.Format("{0:MMM/dd/yyyy}", mbl.FltDate));
            fields.SetField("FltDate2", String.Format("{0:MMM/dd/yyyy}", mbl.FltDate));

            fields.SetField("NotNetiable", mbl.NotNetiableText);

            fields.SetField("AccountingInfo", mbl.AccountingInfo);

            fields.SetField("By1", mbl.By1);
            fields.SetField("By2", mbl.By2);
            fields.SetField("To1", mbl.To1);
            fields.SetField("To2", mbl.To2);
            fields.SetField("Currency", mbl.Currency);
            fields.SetField("ChagsCode", mbl.ChagsCode);

            fields.SetField("WTVALPPD", mbl.WTVALPPD);
            fields.SetField("OTHERPPD", mbl.OTHERPPD);
            
            fields.SetField("WTVALCOL", mbl.WTVALCOL);
            fields.SetField("OTHERCOL", mbl.OTHERCOL);
            fields.SetField("DeclaredValueForCarriage", mbl.DeclaredValueForCarriage);
            fields.SetField("DeclaredValueForCustoms", mbl.DeclaredValueForCustoms);

            fields.SetField("AmountOfInsurance", "NIL");
            fields.SetField("HandlingInfo", mbl.HandlingInfo);

            fields.SetField("NatureQuantityods", "" + mbl.NatureQuantityods);   

            fields.SetField("PKGS", ""+mbl.PKGS);   
            fields.SetField("PKGS2", ""+mbl.PKGS);
            fields.SetField("GrossWT", String.Format("{0:N}", mbl.GrossWT));
            fields.SetField("GrossWT2", String.Format("{0:N}", mbl.GrossWT));
            fields.SetField("WTType", "" + mbl.WTType != null ? (mbl.WTType==5 ? "K" : "L"):"K");
            fields.SetField("RateClass", "" + mbl.RateClass);
            fields.SetField("CWT", "" + String.Format("{0:N}", mbl.CWT));
            fields.SetField("RateChange", String.Format("{0:N}", mbl.RateChange));  
            fields.SetField("FltTotalAmt", String.Format("{0:N}", mbl.FltTotalAmt));
            fields.SetField("FltTotalAmt2", String.Format("{0:N}", mbl.FltTotalAmt));
            fields.SetField("FltTotalAmt3", String.Format("{0:N}", mbl.FltTotalAmt));
            
            fields.SetField("SignatureOfShipperOrAgent", "" + mbl.SignatureOfShipperOrAgent);
            fields.SetField("SignatureOfIssuingCarrierOrAgent", "" + mbl.SignatureOfIssuingCarrierOrAgent);

            int idx = 1;
            decimal sum = 0;
            foreach (var list in mbl.OTHERCHARGE)
            {
                if (!string.IsNullOrEmpty(list.chargeCd))
                {
                    fields.SetField("otherChargeCd" + idx, "" + list.chargeCd);
                    fields.SetField("otherChargeAmt" + idx, String.Format("{0:N}", list.ChargeAmt == null ? 0 : (decimal)list.ChargeAmt));
                    sum = sum + list.ChargeAmt == null ? 0 : (decimal)list.ChargeAmt;
                    idx++;
                }
            }
            fields.SetField("OtherChargeSum", String.Format("{0:N}", sum));
            fields.SetField("TotalPrepaid", String.Format("{0:N}", (sum+mbl.FltTotalAmt)));
            stamper.FormFlattening = true;
            stamper.Close();
            reader.Close();
            return ms;
        }
    }
}
