using FluentMigrator;
using TagsAttribute = FluentMigrator.TagsAttribute;

namespace Database.Migrations;
[Tags("Analytics")]
[Migration(1)]
public class M001_CreateCoreTables : AutoReversingMigration
{

    public override void Up()
    {
        Create.Table("Users")
            .WithColumn("UserId").AsString().NotNullable().PrimaryKey().Unique()
            .WithColumn("DisplayName").AsString().NotNullable()
            .WithColumn("Role").AsString().NotNullable()
            .WithColumn("Version").AsInt16().Nullable()
            .WithColumn("CreatedAt").AsDateTime().NotNullable()
            .WithColumn("ModifiedAt").AsDateTime().NotNullable();

        Create.Table("EventName")
            .WithColumn("EventName").AsString().NotNullable().PrimaryKey().Unique()
            .WithColumn("CreatedAt").AsDateTime().NotNullable()
            .WithColumn("Version").AsInt16()
            ;

        Create.Table("Events")
            .WithColumn("CreatedAt").AsDateTime().NotNullable()
            .WithColumn("Src").AsString().Nullable()
            .WithColumn("UserId").AsString().NotNullable()
            .WithColumn("Version").AsInt16()
            .WithColumn("EventName").AsString().NotNullable().ForeignKey("EventName", "EventName").OnDelete(System.Data.Rule.Cascade)
            .WithColumn("Attribute").AsString().Nullable()
            .WithColumn("DType").AsString().Nullable()
            .WithColumn("Value").AsString().Nullable()
            ;


        Create.Table("Locations")
            .WithColumn("CreatedAt").AsDateTime().NotNullable()
            .WithColumn("Src").AsString().Nullable()
            .WithColumn("UserId").AsString().NotNullable()
            .WithColumn("Version").AsInt16()
            .WithColumn("IP").AsString().Nullable()
            .WithColumn("Location").AsString().Nullable()
            ;
    }
}