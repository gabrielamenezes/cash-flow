
using CashFlow.Application.UseCases.Expenses.Reports.Pdf.Fonts;
using CashFlow.Domain.Reports;
using CashFlow.Domain.Repositories.Expenses;
using MigraDoc.DocumentObjectModel;
using MigraDoc.Rendering;
using PdfSharp.Fonts;

namespace CashFlow.Application.UseCases.Expenses.Reports.Pdf;
public class GenerateExpensesReportPdfUseCase : IGenerateExpensesReportPdfUseCase
{
    private const string CURRENCY_SYMBOL = "R$";
    private readonly IExpensesReadOnlyRepository _repository;

    public GenerateExpensesReportPdfUseCase(IExpensesReadOnlyRepository repository)
    {
        _repository = repository;
        // ensinando a nossa biblioteca a como resolver as fontes
        GlobalFontSettings.FontResolver = new ExpensesReportFontResolver();
    }

    public async Task<byte[]> Execute(DateOnly month)
    {
        var expenses = await _repository.FilterByMonth(month);
        if (expenses.Count == 0)
        {
            return [];
        }

        var document = CreateDocument(month);
        var page = CreatePage(document);

        var table = page.AddTable();
        table.AddColumn();
        table.AddColumn();

        var row = table.AddRow();
        row.Cells[0].AddImage("C:\\Users\\gabriela.mendes\\Downloads\\modo-serio.jpg");
        row.Cells[1].AddParagraph("Oi, Gabriela!");
        row.Cells[1].Format.Font = new Font { Name = FontHelper.RALEWAY_BLACK, Size = 16 };

        var paragraph = page.AddParagraph();
        var title = string.Format(ResourceReportGenerationMessage.TOTAL_SPENT_IN, month.ToString("Y"));
        paragraph.AddFormattedText(title, new Font { Name = FontHelper.RALEWAY_REGULAR, Size = 15 });
        paragraph.AddLineBreak();

        var totalExpenses = expenses.Sum(expense => expense.Amount);
        paragraph.AddFormattedText($"{CURRENCY_SYMBOL} {totalExpenses}", new Font { Name = FontHelper.WORKSANS_BLACK, Size = 50 });
        return RenderDocument(document);
    }

    private Document CreateDocument(DateOnly month)
    {
        var document = new Document();
        document.Info.Title = $"{ResourceReportGenerationMessage.EXPENSES_FOR} {month:Y}";
        document.Info.Author = "Gabriela Menezes";

        var style = document.Styles["Normal"];
        style!.Font.Name = FontHelper.RALEWAY_REGULAR;
        return document;
    }

    private Section CreatePage(Document document)
    {
        var section = document.AddSection();
        section.PageSetup = document.DefaultPageSetup.Clone();

        section.PageSetup.PageFormat = PageFormat.A4;
        section.PageSetup.LeftMargin = 40;
        section.PageSetup.RightMargin = 40;
        section.PageSetup.TopMargin = 80;
        section.PageSetup.BottomMargin = 80;
        return section;
    }

    private byte[] RenderDocument(Document document)
    {
        var renderer = new PdfDocumentRenderer
        {
            Document = document
        };
        renderer.RenderDocument();
        using var file = new MemoryStream();
        renderer.PdfDocument.Save(file);
        return file.ToArray();
    }
}
