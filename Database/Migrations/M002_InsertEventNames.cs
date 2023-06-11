using FluentMigrator;
using TagsAttribute = FluentMigrator.TagsAttribute;

namespace Database.Migrations;
[Tags("Analytics")]
[Migration(2)]
public class M002_InsertEventNames : AutoReversingMigration
{

    public override void Up()
    {
        var EventNames = new List<string>{
            "Reg",
            "Login",
            "Logout",
            "Inst",
            "Run",
            "Upd",
            "Uninst",
            "Ping",
            "Dox",
            "Loc",
            "Info",
            "Play",
            "Pause",
            "Stop",
            "Buy",
            "Sell",
            "Click",
            "Touch",
            "Drag",
            "Drop",
            "Sos",
            "Sus"
            };
        EventNames.ForEach(EventName => Insert.IntoTable("EventName").Row(new
        {
            CreatedAt = DateTime.Now,
            Version = 1,
            EventName = EventName
        }));
    }
}