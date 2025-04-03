using iText.Kernel.Pdf.Canvas.Draw;
using iText.Kernel.Pdf;
using iText.Layout.Element;
using System.Net;
using iText.Layout;

namespace Ebisx.POS.Presentation.Services;

public class PdfGeneratorService : IPdfGeneratorService
{
    public void GeneratePdf(
        WebView pdfview,
        BusinessInfo businessInfo,
        MachineInfo machineInfo,
        User user)
    {
        string fileName = "mauidotnet.pdf";

#if ANDROID
        var docsDirectory = Android.App.Application.Context.GetExternalFilesDir(Android.OS.Environment.DirectoryDocuments);
        var filePath = Path.Combine(docsDirectory.AbsoluteFile.Path, fileName);
#else
            var filePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), fileName);
#endif
        using (PdfWriter writer = new PdfWriter(filePath))
        {
            PdfDocument pdf = new PdfDocument(writer);
            Document document = new Document(pdf);

            #region businessinfo
            Paragraph businessName = new Paragraph(
                businessInfo.RegistedName)
                .SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER)
                .SetFontSize(12);
            document.Add(businessName);

            Paragraph businessAddress= new Paragraph(
                businessInfo.Address)
                .SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER)
                .SetFontSize(12);
            document.Add(businessAddress);

            Paragraph businessVat = new Paragraph(
                $"VAT Reg TIN: {businessInfo.VatTinNumber}")
                .SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER)
                .SetFontSize(12);
            document.Add(businessVat);
            #endregion

            #region machineinfo
            Paragraph machinePosNumber = new Paragraph(
                machineInfo.PosSerialNumber)
                .SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER)
                .SetFontSize(12);
            document.Add(machinePosNumber);

            Paragraph machineIdNumber = new Paragraph(
               $"MIN#{machineInfo.MinNumber}")
               .SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER)
               .SetFontSize(12);
            document.Add(machinePosNumber);
            #endregion

            #region employeeinfo
            document.Add(new Paragraph(
               "SALES INVOICE")
               .SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER)
               .SetFontSize(12));

            document.Add(new Paragraph(
               $"Cashier: {user.PublicId}-{user.FName} {user.LName}")
               .SetTextAlignment(iText.Layout.Properties.TextAlignment.LEFT)
               .SetFontSize(12));
            #endregion

            #region machineinfo
            document.Add(new Paragraph(
               "This serves as a SALES INVOICE")
               .SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER)
               .SetFontSize(12));

            document.Add(new Paragraph(
               $"ACCREDITATION NO.{machineInfo.AccreditationNumber}")
               .SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER)
               .SetFontSize(12));

            document.Add(new Paragraph(
               $"Date Issued: {machineInfo.DateIssued}")
               .SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER)
               .SetFontSize(12));

            document.Add(new Paragraph(
               $"Date Issued: {machineInfo.ValidUntil}")
               .SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER)
               .SetFontSize(12));
            
            document.Add(new Paragraph(
               $"PTU No.: {machineInfo.PtuNumber}")
               .SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER)
               .SetFontSize(12));
            #endregion

            document.Close();
        }

#if ANDROID
        pdfview.Source = $"file:///android_asset/pdfjs/web/viewer.html?file=file://{WebUtility.UrlEncode(filePath)}";
#else
            pdfview.Source = filePath;
#endif
    }
}

