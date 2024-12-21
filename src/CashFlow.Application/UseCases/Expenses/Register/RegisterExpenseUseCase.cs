using CashFlow.Communication.Enums;
using CashFlow.Communication.Requests;
using CashFlow.Communication.Responses;

namespace CashFlow.Application.UseCases.Expenses.Register;
public class RegisterExpenseUseCase
{
    public ResponseRegisteredExpensiveJson Execute(RequestRegisterExpenseJson request)
    {
        Validate(request);
        return new ResponseRegisteredExpensiveJson();
    }

    private void Validate(RequestRegisterExpenseJson request)
    {
        var titleIsEmpty = string.IsNullOrWhiteSpace(request.Title);
        if(titleIsEmpty)
        {
            throw new ArgumentException("Title is required");
        }

        if(request.Amount <= 0)
        {
            throw new ArgumentException("Amount must be greater than zero");
        }

        var result = DateTime.Compare(request.Date, DateTime.Now);

        if(result > 0)
        {
            throw new ArgumentException("Date must be less than or equal to the current date");
        }

        var paymentTypeIsValid = Enum.IsDefined(typeof(PaymentType), request.PaymentType);

        if(!paymentTypeIsValid)
        {
            throw new ArgumentException("Payment type is invalid");
        }
    }
}
