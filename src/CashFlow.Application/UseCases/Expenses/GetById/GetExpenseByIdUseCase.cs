using AutoMapper;
using CashFlow.Communication.Responses;
using CashFlow.Domain.Repositories.Expenses;
using CashFlow.Domain.Repositories;

namespace CashFlow.Application.UseCases.Expenses.GetById;

public class GetExpenseByIdUseCase(IExpensesRepository expensesRepository, IMapper mapper) : IGetExpenseByIdUseCase
{
    private readonly IExpensesRepository _expensesRepository = expensesRepository;
    private readonly IMapper _mapper = mapper;
    public Task<ResponseExpenseJson> Execute(long id)
    {
        
    }
}
