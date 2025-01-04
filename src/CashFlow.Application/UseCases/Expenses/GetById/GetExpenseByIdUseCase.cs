using AutoMapper;
using CashFlow.Communication.Responses;
using CashFlow.Domain.Repositories.Expenses;
using CashFlow.Domain.Repositories;
using CashFlow.Exception.ExceptionsBase;
using CashFlow.Exception;

namespace CashFlow.Application.UseCases.Expenses.GetById;

public class GetExpenseByIdUseCase(IExpensesRepository expensesRepository, IMapper mapper) : IGetExpenseByIdUseCase
{
    private readonly IExpensesRepository _expensesRepository = expensesRepository;
    private readonly IMapper _mapper = mapper;
    public async Task<ResponseExpenseJson> Execute(long id)
    {
        //Validate(id);
        var result = await _expensesRepository.GetById(id);
        if (result is null)
        {
            throw new NotFoundException(ResourceErrorMessages.EXPENSE_NOT_FOUND);
        }
        return _mapper.Map<ResponseExpenseJson>(result);
    }
}
