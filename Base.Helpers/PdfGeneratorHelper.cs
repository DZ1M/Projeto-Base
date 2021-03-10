using DinkToPdf;
using System;
using System.IO;

namespace Base.Helpers
{
    public static class PdfGeneratorHelper
    {
        public static HtmlToPdfDocument Generate(string html, Orientation orientacao, out string nome)
        {
            nome = String.Format("Relatorio_{0}.pdf", Guid.NewGuid());

            var globalSettings = new GlobalSettings
            {
                ColorMode = ColorMode.Color,
                Orientation = orientacao,
                PaperSize = PaperKind.A4,
                Margins = new MarginSettings { Top = 10 },
                DocumentTitle = nome
            };

            var objectSettings = new ObjectSettings
            {
                PagesCount = true,
                HtmlContent = html,
                WebSettings = { DefaultEncoding = "utf-8", UserStyleSheet = Path.Combine(Directory.GetCurrentDirectory(), "assets", "reports.css") },
                HeaderSettings = { FontName = "Roboto", FontSize = 6, Right = "Página [page] de [toPage]", Line = false, Spacing = 4 },
            };

            return new HtmlToPdfDocument()
            {
                GlobalSettings = globalSettings,
                Objects = { objectSettings }
            };
        }
    }
}
