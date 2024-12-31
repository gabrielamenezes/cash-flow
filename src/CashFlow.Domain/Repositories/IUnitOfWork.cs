namespace CashFlow.Domain.Repositories;

public interface IUnitOfWork
{
    Task Commmit();
}
