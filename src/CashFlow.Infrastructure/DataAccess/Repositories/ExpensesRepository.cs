using CashFlow.Domain.Entities;
using CashFlow.Domain.Repositories.Expenses;

namespace CashFlow.Infrastructure.DataAccess.Repositories;

internal class ExpensesRepository(CashFlowDbContext dbContext) : IExpensesRepository
{
    private readonly CashFlowDbContext _dbContext = dbContext;
    public void Add(Expense expense)
    {
        _dbContext.Expenses.Add(expense);
        _dbContext.SaveChanges();
    }
}
