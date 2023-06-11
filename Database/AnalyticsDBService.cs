using System.Globalization;
using System.Text;
using Classes;
using Dapper;
using Dapper.Contrib.Extensions;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Primitives;
// using Newtonsoft.Json;
// using Newtonsoft.Json.Linq;

namespace Database;
public class AnalyticsDBService
{
    private readonly ILogger<AnalyticsDBService> logger;
    private readonly AnalyticsDBContext dbContext;

    public AnalyticsDBService(
            ILogger<AnalyticsDBService> logger,
            AnalyticsDBContext dbContext
        )
    {
        this.logger = logger;
        this.dbContext = dbContext;
    }

    internal void TrackEvent(DtoAnalyticsEvent dtoEvent)
    {
        TrackEvent(
            Src: dtoEvent.Src,
            UserId: dtoEvent.Uid,
            Version: dtoEvent.Ver,
            EventName: dtoEvent.Evt,
            Attribute: dtoEvent.Atr,
            DType: dtoEvent.Typ,
            Value: dtoEvent.Val
        );
    }

    internal void TrackEvent(

        string Src,
        string UserId,
        int Version,
        string EventName,

        string Attribute = null,
        string DType = null,
        string Value = null
      )
    {
        Task.Run(async () =>
        {
            try
            {
                await CreateEvent(
                    Src,
                    UserId,
                    Version,
                    EventName,
                    Attribute,
                    DType,
                    Value);
            }
            catch (System.Exception ex)
            {
                logger.LogError(ex, "Analytics TrackEvent Failed");
            }
        });

    }


    private async Task CreateEvent(
        string Src,
        string UserId,
        int Version,
        string EventName,

        string Attribute = null,
        string DType = null,
        string Value = null
      )
    {
        using (var connection = dbContext.CreateConnection())
        {
            var newEvent = new RowAnalyticsEvent
            {
                CreatedAt = DateTime.Now,
                Src = Src,
                UserId = UserId,
                Version = Version,
                EventName = EventName,
                Attribute = Attribute,
                DType = DType,
                Value = Value
            };
            await connection.InsertAsync(newEvent);
        }
    }


}
