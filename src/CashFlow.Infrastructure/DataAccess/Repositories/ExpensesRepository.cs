using CashFlow.Domain.Entities;
using CashFlow.Domain.Repositories.Expenses;
using Microsoft.EntityFrameworkCore;

namespace CashFlow.Infrastructure.DataAccess.Repositories;

internal class ExpensesRepository(CashFlowDbContext dbContext) : IExpensesReadOnlyRepository, IExpensesWriteOnlyRepository, IExpensesUpdateOnlyRepository
{
    private readonly CashFlowDbContext _dbContext = dbContext;
    public async Task Add(Expense expense)
    {
        await _dbContext.Expenses.AddAsync(expense);
    }

    public async Task<List<Expense>> GetAll() => await _dbContext.Expenses.AsNoTracking().ToListAsync();

    async Task<Expense?> IExpensesReadOnlyRepository.GetById(long id)
    {
        return await _dbContext.Expenses.AsNoTracking().FirstOrDefaultAsync(e => e.Id == id);
    }

    async Task<Expense?> IExpensesUpdateOnlyRepository.GetById(long id)
    {
        return await _dbContext.Expenses.FirstOrDefaultAsync(e => e.Id == id);
    }

    public async Task<bool> Delete(long id)
    {
        var result = await _dbContext.Expenses.FirstOrDefaultAsync(e => e.Id == id);
        if(result is null)
        {
            return false;
        }
        _dbContext.Expenses.Remove(result);
        return true;
    }

    public void Update(Expense expense)
    {
        _dbContext.Expenses.Update(expense);
    }

    public async Task<List<Expense>> FilterByMonth(DateOnly month)
    {
        var startDate = new DateTime(year: month.Year, month: month.Month, day: 1).Date;
        var daysInMonth = DateTime.DaysInMonth(year: month.Year, month: month.Month);
        var endDate = new DateTime(year: month.Year, month: month.Month, day: daysInMonth, hour: 23, minute: 59, second: 59);
        return await _dbContext
            .Expenses
            .AsNoTracking()
            .Where(e => e.Date >= startDate && e.Date <= endDate)
            .OrderBy(e => e.Date)
            .ToListAsync();
    }
}
