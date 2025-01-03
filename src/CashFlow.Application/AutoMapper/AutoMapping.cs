using AutoMapper;
using CashFlow.Communication.Requests;
using CashFlow.Communication.Responses;
using CashFlow.Domain.Entities;

namespace CashFlow.Application.AutoMapper;
public class AutoMapping : Profile
{
    public AutoMapping()
    {
        RequestToEntity();
        EntityToResponse();
    }
    //mapeamento de requisição para entidade a ser persistida
    private void RequestToEntity()
    {
        CreateMap<RequestRegisterExpenseJson, Expense>();
    }
    // mapeamento de entidade para resposta
    private void EntityToResponse()
    {
        CreateMap<Expense, ResponseRegisteredExpensiveJson>();
        CreateMap<Expense, ResponseShortExpenseJson>();
        CreateMap<Expense, ResponseExpenseJson>();
    }
}
