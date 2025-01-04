using AutoMapper;
using CashFlow.Application.AutoMapper;
using CashFlow.Communication.Requests;
using CashFlow.Communication.Responses;
using CashFlow.Domain.Entities;
using CashFlow.Domain.Repositories;
using CashFlow.Domain.Repositories.Expenses;
using CashFlow.Exception.ExceptionsBase;

namespace CashFlow.Application.UseCases.Expenses.Register;
public class RegisterExpenseUseCase(IExpensesWriteOnlyRepository expensesRepository, IUnitOfWork unitOfWork, IMapper mapper) : IRegisterExpenseUseCase
{
    private readonly IExpensesWriteOnlyRepository _expensesRepository = expensesRepository;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IMapper _mapper = mapper;
    public async Task<ResponseRegisteredExpensiveJson> Execute(RequestRegisterExpenseJson request)
    {

        Validate(request);
        var entity = _mapper.Map<Expense>(request);
        await _expensesRepository.Add(entity);
        await _unitOfWork.Commmit();
        return _mapper.Map<ResponseRegisteredExpensiveJson>(entity);
    }

    private void Validate(RequestRegisterExpenseJson request)
    {
        var validator = new RegisterExpenseValidator();
        //devolve uma lista de erros
        var result = validator.Validate(request);
        

        if(!result.IsValid)
        {
            //seleciona em cada um dos elementos dessa lista, o error message
            // LINQ - um conjunto de recursos que estende as poderosas capacidades de consulta SQL para as linguagens c#
            var errorMessages = result.Errors.Select(f => f.ErrorMessage).ToList();
            throw new ErrorOnValidationException(errorMessages);
        }
    }
}
