using System.Reflection;
using Dapper;
using Database;
using Extensions;
using FluentMigrator.Runner;

namespace Configurations
{
    public static class Configurator
    {

        public static void Configure(WebApplicationBuilder builder)
        {
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            // #region TGBOT
            // if (builder.Environment.IsProduction())
            //     builder.Services.AddHostedService<ConfigureTelegram>();
            // builder.Services.AddHttpClient("anxtelegram")
            //         .AddTypedClient<ITelegramBotClient>(httpClient
            //             => new TelegramBotClient(builder.Configuration.GetSection("BotConfiguration").Get<TelegramBotConfiguration>().BotToken, httpClient));

            // builder.Services.AddScoped<TelegramService>();
            // #endregion


            builder.Services.Configure<AnalyticsDBConfiguration>(builder.Configuration.GetSection("AnalyticsDBConfiguration"));
            SqlMapper.AddTypeHandler(new SqliteGuidTypeHandler());
            SqlMapper.RemoveTypeMap(typeof(Guid));
            SqlMapper.RemoveTypeMap(typeof(Guid?));

            builder.Services.AddSingleton<AnalyticsDBContext>();
            builder.Services.AddTransient<AnalyticsDBService>();

            // builder.Services.AddTransient<FireForgetTelegramSnitcher>();


            // Add services to the container.
            // // The Telegram.Bot library heavily depends on Newtonsoft.Json library to deserialize
            // // incoming webhook updates and send serialized responses back.
            // // Read more about adding Newtonsoft.Json to ASP.NET Core pipeline:
            // //   https://docs.microsoft.com/en-us/aspnet/core/web-api/advanced/formatting?view=aspnetcore-5.0#add-newtonsoftjson-based-json-format-support
            builder.Services.AddControllers();
            //         .AddNewtonsoftJson(option =>
            //         {
            //             option.UseMemberCasing();
            //         });


            MigrationExtension.Migrate(
                builder.Configuration.GetSection("AnalyticsDBConfiguration")
                    .Get<AnalyticsDBConfiguration>().ConnectionString
            );
        }

    }
}