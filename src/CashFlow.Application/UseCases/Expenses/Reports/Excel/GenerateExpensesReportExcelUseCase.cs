using CashFlow.Domain.Extensions;
using CashFlow.Domain.Reports;
using CashFlow.Domain.Repositories.Expenses;
using ClosedXML.Excel;

namespace CashFlow.Application.UseCases.Expenses.Reports.Excel;

public class GenerateExpensesReportExcelUseCase(IExpensesReadOnlyRepository expensesRepository) : IGenerateExpensesReportExcelUseCase
{
    private const string CURRENCY_SYMBOL = "R$";
    private readonly IExpensesReadOnlyRepository _expensesRepository = expensesRepository;
    public async Task<byte[]> Execute(DateOnly month)
    {
        var expenses = await _expensesRepository.FilterByMonth(month);
        if (expenses.Count == 0)
        {
            return [];
        }
        using var workbook = new XLWorkbook(); //gerando um arquivo em branco
        workbook.Author = "Gabriela Menezes";
        workbook.Style.Font.FontSize = 12;
        workbook.Style.Font.FontName = "Times New Roman";

        var worksheet = workbook.Worksheets.Add(month.ToString("Y"));
        InsertHeader(worksheet);

        var aux = 2;
        foreach (var expense in expenses)
        {
            worksheet.Cell($"A{aux}").Value = expense.Title;
            worksheet.Cell($"B{aux}").Value = expense.Date;
            worksheet.Cell($"C{aux}").Value = expense.PaymentType.PaymentTypeToString();

            worksheet.Cell($"D{aux}").Value = expense.Amount;
            worksheet.Cell($"D{aux}").Style.NumberFormat.Format = $"{CURRENCY_SYMBOL} #, ##0.00";

            worksheet.Cell($"E{aux}").Value = expense.Description;

            aux++;
        }

        worksheet.Columns().AdjustToContents();

        var file = new MemoryStream(); // a fonte desses dados é um arquivo em memória
        workbook.SaveAs(file);

        return file.ToArray(); //retornando o arquivo em bytes
    }
    private void InsertHeader(IXLWorksheet worksheet)
    {
        worksheet.Cell("A1").Value = ResourceReportGenerationMessage.TITLE;
        worksheet.Cell("B1").Value = ResourceReportGenerationMessage.DATE;
        worksheet.Cell("C1").Value = ResourceReportGenerationMessage.PAYMENT_TYPE;
        worksheet.Cell("D1").Value = ResourceReportGenerationMessage.AMOUNT;
        worksheet.Cell("E1").Value = ResourceReportGenerationMessage.DESCRIPTION;

        worksheet.Cells("A1:E1").Style.Font.Bold = true;
        //worksheet.Cells("A1:E1").Style.Fill.BackgroundColor = XLColor.FromHtml("#OC65EE");

        //centralizando o texto
        worksheet.Cell("A1").Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
        worksheet.Cell("B1").Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
        worksheet.Cell("C1").Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
        worksheet.Cell("E1").Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
        worksheet.Cell("D1").Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Right);
    }
}
