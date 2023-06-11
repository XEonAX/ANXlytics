
using Dapper.Contrib.Extensions;

namespace Classes;

[Table("Events")]

public class RowAnalyticsEvent
{
    public DateTime CreatedAt { get; set; }
    public string Src { get; set; }
    public string UserId { get; set; }
    public int Version { get; set; }
    public string EventName { get; set; }
    public string Attribute { get; set; }
    public string DType { get; set; }
    public string Value { get; set; }

}