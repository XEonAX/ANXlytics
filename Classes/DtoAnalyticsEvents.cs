
using Dapper.Contrib.Extensions;

namespace Classes;

public class DtoAnalyticsEvent
{
    public string Src { get; set; }
    public string Uid { get; set; }
    public Int16 Ver { get; set; }
    public string Evt { get; set; }
    public string Atr { get; set; }
    public string Typ { get; set; }
    public string Val { get; set; }

}