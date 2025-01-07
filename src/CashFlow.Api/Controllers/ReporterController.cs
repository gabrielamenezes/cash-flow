using Microsoft.AspNetCore.Mvc;
using System.Net.Mime;

namespace CashFlow.Api.Controllers;
[Route("api/[controller]")]
[ApiController]
public class ReporterController : ControllerBase
{
    [HttpGet("excel")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> GetExcel()
    {
        byte[] file = new byte[1];
        if(file.Length == 0)
        {
            return NoContent();
        }
        return File(file, MediaTypeNames.Application.Octet, "report.xlsx");
    }
}
