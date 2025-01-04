using CashFlow.Domain.Entities;
using CashFlow.Domain.Repositories.Expenses;
using Microsoft.EntityFrameworkCore;

namespace CashFlow.Infrastructure.DataAccess.Repositories;

internal class ExpensesRepository(CashFlowDbContext dbContext) : IExpensesReadOnlyRepository, IExpensesWriteOnlyRepository
{
    private readonly CashFlowDbContext _dbContext = dbContext;
    public async Task Add(Expense expense)
    {
        await _dbContext.Expenses.AddAsync(expense);
    }

    public async Task<List<Expense>> GetAll() => await _dbContext.Expenses.AsNoTracking().ToListAsync();

    public async Task<Expense?> GetById(long id)
    {
        return await _dbContext.Expenses.AsNoTracking().FirstOrDefaultAsync(e => e.Id == id);
    }
}
