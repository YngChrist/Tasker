// See https://aka.ms/new-console-template for more information

using System.ComponentModel;
using System.Reflection.Metadata;
using QuestPDF.Companion;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
using QuestPDF.Previewer;
using Document = QuestPDF.Fluent.Document;
using IContainer = QuestPDF.Infrastructure.IContainer;

QuestPDF.Settings.License = LicenseType.Community;
Document.Create(container =>
{
    container.Page(page =>
    {
        page.Size(PageSizes.A4);
        page.Margin(2, Unit.Centimetre);
        page.PageColor(Colors.White);
        page.DefaultTextStyle(x => x.FontFamily("Aptos").FontSize(12).LineHeight(1.5f));

        // Заголовок страницы
        page.Header()
            .Padding(10)
            .AlignCenter()
            .Text($"Итоги за день [{DateTime.Today.ToShortDateString()}]")
            .SemiBold().FontSize(24).FontColor(Colors.Black);

        // Контент страницы
        page.Content()
            .Column(column =>
            {
                column.Spacing(8); // Меньше расстояние между элементами списка

                // Подзаголовок
                column.Item()
                    .Text("Список выполненных задач")
                    .FontSize(16).FontColor(Colors.Grey.Darken2).Bold();

                column.Item()
                    .Text("Связанные с работой:")
                    .FontSize(12).FontColor(Colors.Grey.Darken2).Bold();
                var itemFontSize = 9;
                column.Item()
                    .Border(0.5f)
                    .BorderColor(Colors.Black)
                    .Padding(10)
                    .Column(innerColumn =>
                    {
                        innerColumn.Spacing(4); // Меньше расстояние между задачами
                        innerColumn.Item().Text("- Разработка отчета").FontSize(itemFontSize).FontColor(Colors.Black);
                        innerColumn.Item().Text("- Собрание команды").FontSize(itemFontSize).FontColor(Colors.Black);
                        innerColumn.Item().Text("- Тестирование").FontSize(itemFontSize).FontColor(Colors.Black);
                        innerColumn.Item().Text("- Ревизия кода").FontSize(itemFontSize).FontColor(Colors.Black);
                        innerColumn.Item().Text("- Подготовка релиза").FontSize(itemFontSize).FontColor(Colors.Black);
                        
                        innerColumn.Item().Text("Итого: 5 задач").FontSize(itemFontSize+3).FontColor(Colors.Green.Medium);
                    });
                
                column.Item()
                    .Text("Связанные с домом:")
                    .FontSize(12).FontColor(Colors.Grey.Darken2).Bold();
                column.Item()
                    .Border(0.5f)
                    .BorderColor(Colors.Black)
                    .Padding(10)
                    .Column(innerColumn =>
                    {
                        innerColumn.Spacing(4); // Меньше расстояние между задачами
                        innerColumn.Item().Text("- Разработка отчета").FontSize(itemFontSize).FontColor(Colors.Black);
                        innerColumn.Item().Text("- Собрание команды").FontSize(itemFontSize).FontColor(Colors.Black);
                        innerColumn.Item().Text("- Тестирование").FontSize(itemFontSize).FontColor(Colors.Black);
                        innerColumn.Item().Text("- Ревизия кода").FontSize(itemFontSize).FontColor(Colors.Black);
                        innerColumn.Item().Text("- Подготовка релиза").FontSize(itemFontSize).FontColor(Colors.Black);
                        
                        innerColumn.Item().Text("Итого: 5 задач").FontSize(itemFontSize+3).FontColor(Colors.Green.Medium);
                    });
                
                column.Item().Text("Общее количество задач: 17!").FontSize(itemFontSize+3).FontColor(Colors.Green.Medium);
                
                column.Item()
                    .Text("Дополнительные события и новое состояние по задачам:")
                    .FontSize(12).FontColor(Colors.Grey.Darken2).Bold();
                column.Item()
                    .Text(Placeholders.LoremIpsum())
                    .FontSize(itemFontSize).FontColor(Colors.Black);
                
                column.Item()
                    .Text("Действия завтра:")
                    .FontSize(12).FontColor(Colors.Grey.Darken2).Bold();
                column.Item()
                    .Text(Placeholders.LoremIpsum())
                    .FontSize(itemFontSize).FontColor(Colors.Black);
            });
    });
}).ShowInCompanion();
Console.ReadKey();
return;

IContainer CellStyle(IContainer container) =>
    container.Padding(5).Border(1).BorderColor(Colors.Grey.Lighten2).AlignMiddle();

void AddTaskRow(TableDescriptor table, string task, string status, string time)
{
    table.Cell().Element(CellStyle).Text(task);
    table.Cell().Element(CellStyle).Text(status)
        .FontColor(status switch
        {
            "Выполнена" => Colors.Green.Darken2,
            "Ожидает" => Colors.Red.Darken2,
            "В процессе" => Colors.Orange.Darken2,
            _ => Colors.Black
        });
    table.Cell().Element(CellStyle).Text(time);
}