
using System.Globalization;

namespace CashFlow.Api.Middleware;

public class CultureMiddleware
{
    private readonly RequestDelegate _next;
    public CultureMiddleware(RequestDelegate next)
    {
        _next = next;
    }
    public async Task Invoke(HttpContext context)
    {

        var supportedLanguages = CultureInfo.GetCultures(CultureTypes.AllCultures).ToList();
        //extraindo do header da minha requisição qual é a cultura de quem fez a requisição deseja
        var requestedCulture = context.Request.Headers.AcceptLanguage.FirstOrDefault();

        //instancio uma cultura padrão
        var cultureInfo = new CultureInfo("en");

        //se a cultura for preenchida no header da requisição, eu sobrescrevo o valor padrão para o valor que veio no header
        if (!string.IsNullOrWhiteSpace(requestedCulture)
            && supportedLanguages.Exists(l => l.Name.Equals(requestedCulture)))
        {
            cultureInfo = new CultureInfo(requestedCulture);
        }

        CultureInfo.CurrentCulture = cultureInfo;
        CultureInfo.CurrentUICulture = cultureInfo;
        await _next(context);
    }
}
