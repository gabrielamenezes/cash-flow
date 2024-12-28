using CashFlow.Communication.Requests;

namespace Validators.Tests.Expenses.Register;

public class RegisterExpenseValidatorTests
{
    [Fact]
    public void ShouldReturnSuccessWhenRequestIsValid()
    {
        //Arrange
        var validator = new CashFlow.Application.UseCases.Expenses.Register.RegisterExpenseValidator();
        var request = new RequestRegisterExpenseJson
        {
            Title = "Apple",
            Date = DateTime.Now.AddDays(-1),
            Description = "Description",
            Amount = 100,
            PaymentType = CashFlow.Communication.Enums.PaymentType.Cash
        };
        //Act
        var result = validator.Validate(request);

        //Assert
        Assert.True(result.IsValid);
    }
}
