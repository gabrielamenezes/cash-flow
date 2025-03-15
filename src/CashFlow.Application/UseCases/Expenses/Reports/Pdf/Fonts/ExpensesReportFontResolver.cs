using PdfSharp.Charting;
using PdfSharp.Fonts;
using System.Reflection;

namespace CashFlow.Application.UseCases.Expenses.Reports.Pdf.Fonts;
public class ExpensesReportFontResolver : IFontResolver
{
    public byte[]? GetFont(string faceName)
    {
        var stream = ReadFontFile(faceName) ?? ReadFontFile(FontHelper.DEFAULT_FONT);
        var length = (int)stream!.Length;
        var data = new byte[length];
        stream.Read(buffer: data, offset: 0, length);

        return data;
    }

    public FontResolverInfo? ResolveTypeface(string familyName, bool bold, bool italic)
    {
        return new FontResolverInfo(familyName);
    }


    private Stream? ReadFontFile(string faceName)
    {
        // tudo que é necessário pro projeto funcionar vai estar aqui
        // pedindo a plataforma do .net, para devolver a referencia do projeto que está sendo executado agora
        var assembly = Assembly.GetExecutingAssembly();
        // vou ler como stream esse arquivo de faceName
        return assembly.GetManifestResourceStream($"CashFlow.Application.UseCases.Reports.Pdf.Fonts.{faceName}.ttf");
    }
}
