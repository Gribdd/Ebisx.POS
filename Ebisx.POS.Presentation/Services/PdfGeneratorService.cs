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
            Paragraph header = new Paragraph("MAUI PDF Sample")
                .SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER)
                .SetFontSize(20);

            document.Add(header);
            Paragraph subheader = new Paragraph("Welcome to .NET Multi-platform App UI")
                .SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER)
                .SetFontSize(15);
            document.Add(subheader);
            LineSeparator ls = new LineSeparator(new SolidLine());
            document.Add(ls);

            Paragraph footer = new Paragraph("Don't forget to like and subscribe!")
                .SetTextAlignment(iText.Layout.Properties.TextAlignment.RIGHT)
                .SetFontColor(iText.Kernel.Colors.ColorConstants.LIGHT_GRAY)
                .SetFontSize(14);

            document.Add(footer);
            document.Close();
        }

#if ANDROID
        pdfview.Source = $"file:///android_asset/pdfjs/web/viewer.html?file=file://{WebUtility.UrlEncode(filePath)}";
#else
            pdfview.Source = filePath;
#endif
    }
}

