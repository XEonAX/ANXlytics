using Classes;
using Database;
using Microsoft.AspNetCore.Mvc;

namespace ANXlytics.Controllers
{

    [ApiController]
    [Route("sinkhole")]
    public class SinkholeController : ControllerBase
    {

        private readonly ILogger<SinkholeController> logger;
        private readonly AnalyticsDBService analyticsDBService;

        public SinkholeController(ILogger<SinkholeController> logger, AnalyticsDBService analyticsDBService)
        {
            this.logger = logger;
            this.analyticsDBService = analyticsDBService;
        }

        [HttpGet("burp")]
        public ActionResult Blurb()
        {
            return Ok("burp");
        }

        [HttpGet("barf")]
        public ActionResult<DtoAnalyticsEvent> Barf()
        {
            return Ok(new DtoAnalyticsEvent
            {
                Atr = "Test",
                Evt = "Reg",
                Src = "Swagger",
                Typ = "string",
                Uid = "1",
                Val = "test",
                Ver = 1
            });
        }

        [HttpPost("slurp")]
        public ActionResult Slurp(DtoAnalyticsEvent dtoAnalyticsEvent)
        {
            logger.LogInformation($"{nameof(Slurp)}");

            analyticsDBService.TrackEvent(dtoAnalyticsEvent);
            // if (dtoAnalyticsEvent.EventName == EventNames.OpenANXStudiosDiscord)
            // {
            //     _FireForgetTelegramSnitcher.Execute($"{user.UserId} aka {user.DisplayName} OpenANXStudios Discord", true);
            // }
            // else if (dtoAnalyticsEvent.EventName == EventNames.OpenANXStudiosTG)
            // {
            //     _FireForgetTelegramSnitcher.Execute($"{user.UserId} aka {user.DisplayName} OpenANXStudios Telegram", true);
            // }
            // else if (dtoAnalyticsEvent.EventName == EventNames.Achievement)
            // {
            //     _FireForgetTelegramSnitcher.Execute($"{user.DisplayName} achieved {dtoAnalyticsEvent.Value1}", false);
            // }
            return Ok();
        }
    }
}