using CashFlow.Domain.Repositories;

namespace CashFlow.Infrastructure.DataAccess;

internal class UnitOfWork(CashFlowDbContext dbContext) : IUnitOfWork
{
    private readonly CashFlowDbContext _dbContext = dbContext;
    public void Commmit() => _dbContext.SaveChanges();
}
