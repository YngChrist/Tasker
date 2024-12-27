using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;

namespace ConsoleTest;

public class PdfReportGenerator
{
    public void GenerateReport(string filePath)
    {
        QuestPDF.Settings.License = LicenseType.Community;
        var document = new ReportDocument();
        document.GeneratePdf(filePath);
    }
}

public class ReportDocument : IDocument
{
    public DocumentMetadata GetMetadata() => DocumentMetadata.Default;

    public void Compose(IDocumentContainer container)
    {
        container
            .Page(page =>
            {
                page.Size(PageSizes.A4);
                page.Margin(1, Unit.Centimetre);
                page.PageColor(Colors.White);
                page.DefaultTextStyle(x => x.FontSize(12));

                page.Header()
                    .Text("Отчёт по задачам")
                    .SemiBold().FontSize(20).AlignCenter();

                page.Content()
                    .Column(column =>
                    {
                        column.Spacing(20);

                        column.Item().Text("Статистика выполнения задач").FontSize(14);

                        column.Item().Table(table =>
                        {
                            table.ColumnsDefinition(columns =>
                            {
                                columns.RelativeColumn(1);
                                columns.RelativeColumn(2);
                            });

                            table.Header(header =>
                            {
                                header.Cell().Element(CellStyle).Text("Задача");
                                header.Cell().Element(CellStyle).Text("Статус");
                            });

                            table.Cell().Element(CellStyle).Text("Задача 1");
                            table.Cell().Element(CellStyle).Text("Выполнена");
                            table.Cell().Element(CellStyle).Text("Задача 2");
                            table.Cell().Element(CellStyle).Text("Ожидает");
                        });
                    });

                page.Footer()
                    .AlignCenter()
                    .Text(x =>
                    {
                        x.Span("Страница ");
                        x.CurrentPageNumber();
                        x.Span(" из ");
                        x.TotalPages();
                    });
            });
    }

    private static IContainer CellStyle(IContainer container) =>
        container.Padding(5).Border(1).BorderColor(Colors.Grey.Lighten2);
}