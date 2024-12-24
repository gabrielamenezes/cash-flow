using CashFlow.Communication.Enums;
using CashFlow.Communication.Requests;
using CashFlow.Communication.Responses;
using CashFlow.Exception.ExceptionsBase;

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
        var validator = new RegisterExpenseValidator();
        //devolve uma lista de erros
        var result = validator.Validate(request);
        

        if(!result.IsValid)
        {
            //seleciona em cada um dos elementos dessa lista, o error message
            // LINQ - um conjunto de recursos que estende as poderosas capacidades de consulta SQL para as linguagens c#
            var errorMessages = result.Errors.Select(f => f.ErrorMessage).ToList();
            throw new ErrorOnValidationException(errorMessages);
        }
    }
}
