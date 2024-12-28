using CashFlow.Communication.Requests;
using CashFlow.Exception;
using CommonTestUtilities.Requests;
using FluentAssertions;

namespace Validators.Tests.Expenses.Register;

public class RegisterExpenseValidatorTests
{
    [Fact]
    public void ShouldReturnSuccessWhenRequestIsValid()
    {
        //Arrange
        var validator = new CashFlow.Application.UseCases.Expenses.Register.RegisterExpenseValidator();
        var request = RequestRegisterExpenseJsonBuilder.Build();

        //Act
        var result = validator.Validate(request);

        //Assert
        result.IsValid.Should().BeTrue();
    }

    [Theory]
    [InlineData("      ")]
    [InlineData("")]
    [InlineData(null)]
    public void ShouldReturnErrorWhenTitleIsEmpty(string title)
    {
        //Arrange
        var validator = new CashFlow.Application.UseCases.Expenses.Register.RegisterExpenseValidator();
        var request = RequestRegisterExpenseJsonBuilder.Build();
        request.Title = title;
        //Act
        var result = validator.Validate(request);
        //Assert
        result.IsValid.Should().BeFalse();
        result.Errors.Should().ContainSingle().And.Contain(e => e.ErrorMessage == ResourceErrorMessages.TITLE_REQUIRED) ;
    }

    [Fact]
    public void ShouldReturnErrorWhenAmountIsZero()
    {
        //Arrange
        var validator = new CashFlow.Application.UseCases.Expenses.Register.RegisterExpenseValidator();
        var request = RequestRegisterExpenseJsonBuilder.Build();
        request.Amount = 0;

        //Act
        var result = validator.Validate(request);
        //Assert
        result.IsValid.Should().BeFalse();
        result.Errors.Should().ContainSingle(). And.Contain(e => e.ErrorMessage == ResourceErrorMessages.AMOUNT_MUST_BE_GREATER_THAN_ZERO);
    }
    [Fact]
    public void ShouldReturnErrorWhenDateIsInTheFuture()
    {
        //Arrange
        var validator = new CashFlow.Application.UseCases.Expenses.Register.RegisterExpenseValidator();
        var request = RequestRegisterExpenseJsonBuilder.Build();
        request.Date = DateTime.UtcNow.AddDays(1);
        //Act
        var result = validator.Validate(request);
        //Assert
        result.IsValid.Should().BeFalse();
        result.Errors.Should().ContainSingle().And.Contain(e => e.ErrorMessage == ResourceErrorMessages.EXPENSES_CANNOT_BE_FOR_THE_FUTURE);
    }

    [Fact]
    public void ShouldReturnErrorWhenPaymentTypeIsInvalid()
    {
        //Arrange
        var validator = new CashFlow.Application.UseCases.Expenses.Register.RegisterExpenseValidator();
        var request = RequestRegisterExpenseJsonBuilder.Build();
        request.PaymentType = (CashFlow.Communication.Enums.PaymentType)int.MaxValue;
        //Act
        var result = validator.Validate(request);
        //Assert
        result.IsValid.Should().BeFalse();
        result.Errors.Should().ContainSingle().And.Contain(e => e.ErrorMessage == ResourceErrorMessages.PAYMENT_TYPE_INVALID);
    }

    [Theory]
    [InlineData(-1)]
    [InlineData(0)]
    public void ShouldReturnErrorWhenAmountIsNegativeOrZero(decimal amount)
    {
        //Arrange
        var validator = new CashFlow.Application.UseCases.Expenses.Register.RegisterExpenseValidator();
        var request = RequestRegisterExpenseJsonBuilder.Build();
        request.Amount = amount;
        //Act
        var result = validator.Validate(request);
        //Assert
        result.IsValid.Should().BeFalse();
        result.Errors.Should().ContainSingle().And.Contain(e => e.ErrorMessage == ResourceErrorMessages.AMOUNT_MUST_BE_GREATER_THAN_ZERO);
    }
}
