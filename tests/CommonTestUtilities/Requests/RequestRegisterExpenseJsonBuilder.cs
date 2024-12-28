using Bogus;
using CashFlow.Communication.Requests;

namespace CommonTestUtilities.Requests;

public class RequestRegisterExpenseJsonBuilder
{
    public static RequestRegisterExpenseJson Build()
    {
        var faker = new Faker("en");
        return new RequestRegisterExpenseJson
        {
            Title = faker.Commerce.ProductName(),
            Date = faker.Date.Past(),
            Description = faker.Lorem.Sentence(),
            Amount = faker.Random.Decimal(1, 1000),
            PaymentType = faker.PickRandom<CashFlow.Communication.Enums.PaymentType>()
        };
    }
}
