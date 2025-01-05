using CashFlow.Communication.Responses;
using CashFlow.Domain.Repositories;
using CashFlow.Domain.Repositories.Expenses;
using CashFlow.Exception;
using CashFlow.Exception.ExceptionsBase;

namespace CashFlow.Application.UseCases.Expenses.Delete;

public class DeleteExpenseUseCase(IExpensesWriteOnlyRepository expensesRepository, IUnitOfWork unitOfWork) : IDeleteExpenseUseCase
{
    private readonly IExpensesWriteOnlyRepository _expensesRepository = expensesRepository;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    public async Task Execute(long id)
    {
        var result = await _expensesRepository.Delete(id);
        if (!result)
        {
            throw new NotFoundException(ResourceErrorMessages.EXPENSE_NOT_FOUND);
        }
        await _unitOfWork.Commmit();
    }
}
