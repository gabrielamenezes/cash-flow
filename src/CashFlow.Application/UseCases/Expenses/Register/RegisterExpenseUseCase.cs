﻿using CashFlow.Communication.Requests;
using CashFlow.Communication.Responses;
using CashFlow.Domain.Entities;
using CashFlow.Domain.Repositories;
using CashFlow.Domain.Repositories.Expenses;
using CashFlow.Exception.ExceptionsBase;

namespace CashFlow.Application.UseCases.Expenses.Register;
public class RegisterExpenseUseCase(IExpensesRepository expensesRepository, IUnitOfWork unitOfWork) : IRegisterExpenseUseCase
{
    private readonly IExpensesRepository _expensesRepository = expensesRepository;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    public ResponseRegisteredExpensiveJson Execute(RequestRegisterExpenseJson request)
    {

        Validate(request);
        var entity = new Expense
        {
            Title = request.Title,
            Description = request.Description,
            Date = request.Date,
            Amount = request.Amount,
            PaymentType = (Domain.Enums.PaymentType)request.PaymentType
        };
        _expensesRepository.Add(entity);
        _unitOfWork.Commmit();
        return new ResponseRegisteredExpensiveJson();
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
