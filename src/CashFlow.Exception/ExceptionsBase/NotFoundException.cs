namespace CashFlow.Exception.ExceptionsBase;

public class NotFoundException : CashFlowException
{
    //se eu vou lançar um notfound exception, eu preciso lançar com a mensagem de erro que aconteceu (adicionar no resource)

    public NotFoundException(string message) : base(message) // repassando para o construtor da classe base a mensagem que eu recebi
    {
    }
}
