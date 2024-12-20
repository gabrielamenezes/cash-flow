using CashFlow.Communication.Enums;

namespace CashFlow.Communication.Requests;
public class RequestRegisterExpenseJson
{
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public DateTime Date { get; set; } = default;
    public decimal Amount { get; set; } = default;
    public PaymentType PaymentType { get; set; } = default;
}
