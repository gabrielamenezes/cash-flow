using AutoMapper;
using CashFlow.Communication.Responses;
using CashFlow.Domain.Repositories.Expenses;
using CashFlow.Domain.Repositories;
using CashFlow.Exception.ExceptionsBase;

namespace CashFlow.Application.UseCases.Expenses.GetById;

public class GetExpenseByIdUseCase(IExpensesRepository expensesRepository, IMapper mapper) : IGetExpenseByIdUseCase
{
    private readonly IExpensesRepository _expensesRepository = expensesRepository;
    private readonly IMapper _mapper = mapper;
    public async Task<ResponseExpenseJson> Execute(long id)
    {
        Validate(id);
        var result = await _expensesRepository.GetById(id);
        return _mapper.Map<ResponseExpenseJson>(result);
    }

    private static void Validate(long id)
    {
        var validator = new GetExpenseByIdValidator();
        var result = validator.Validate(id);

        if (!result.IsValid)
        {
            var errorMessages = result.Errors.Select(f => f.ErrorMessage).ToList();
            throw new ErrorOnValidationException(errorMessages);
        }

    }
}
