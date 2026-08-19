using FluentMigrator;
using Nop.Data.Extensions;

namespace Nop.Data.Migrations.Installation;

/// <summary>
/// Adds the segment table used by the Tinyid id generator
/// (https://github.com/didi/tinyid)
/// </summary>
[NopMigration("2026-08-19 00:00:00", "Add nop_tiny_id_info table for the Tinyid id generator")]
public class TinyIdInfoTableMigration : ForwardOnlyMigration
{
    /// <summary>
    /// Collect the UP migration expressions
    /// </summary>
    public override void Up()
    {
        if (!Schema.Table("nop_tiny_id_info").Exists())
        {
            Create.Table("nop_tiny_id_info")
                .WithColumn("biz_type").AsString(50).PrimaryKey()
                .WithColumn("max_id").AsInt64().NotNullable()
                .WithColumn("step").AsInt32().NotNullable();
        }
    }
}
