using FluentValidation;

namespace CashFlow.Application.UseCases.Expenses.GetById;

public class GetExpenseByIdValidator : AbstractValidator<long>
{
    public GetExpenseByIdValidator()
    {
        RuleFor(x => x).GreaterThan(0).WithMessage("Id must be greater than 0");
        RuleFor(x => x).NotEmpty().WithMessage("Id is required");
    }
}
