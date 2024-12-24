using CashFlow.Application.UseCases.Expenses.Register;
using CashFlow.Communication.Requests;
using CashFlow.Exception.ExceptionsBase;
using Microsoft.AspNetCore.Mvc;

namespace CashFlow.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ExpensesController : ControllerBase
{
    [HttpPost]
    public IActionResult Register([FromBody] RequestRegisterExpenseJson request)
    {
        try
        {
            var useCase = new RegisterExpenseUseCase();
            var response = useCase.Execute(request);
            return Created(string.Empty, response);
        }
        catch(ErrorOnValidationException ex)
        {
            var errorResponse = new Communication.Responses.ResponseErrorJson(ex.Errors);
            return BadRequest(ex.Message);
        }
        catch
        {
            var errorResponse = new Communication.Responses.ResponseErrorJson(errorMessage: "Unkown error");
            return StatusCode(StatusCodes.Status500InternalServerError, errorResponse);
        }
        
    }
}
