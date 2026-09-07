using FluentMigrator;
using Nop.Core.Domain.Common;
using Nop.Core.Domain.Customers;
using Nop.Data.Extensions;
using Nop.Data.Mapping;

namespace Nop.Data.Migrations.UpgradeTo500;

[NopSchemaMigration("2026-03-01 00:00:01", "SearchTerm migration")]
public class SearchTermMigration : ForwardOnlyMigration
{
    /// <summary>
    /// Collect the UP migration expressions
    /// </summary>
    public override void Up()
    {
        //truncate via the migration's own connection (same transaction). Using a second
        //connection here (e.g. the data provider) races with the migration transaction and
        //SQLite reports 'database is locked'
        var tableName = NameCompatibilityManager.GetTableName(typeof(SearchTerm));
        Execute.Sql($"DELETE FROM \"{tableName}\"");

        this.DeleteColumnsIfExists<SearchTerm>(["Count"]);

        this.AddOrAlterColumnFor<SearchTerm>(t => t.CreatedOnUtc)
            .AsDateTime2();

        this.AddOrAlterForeignKeyColumnFor<SearchTerm, Customer>(t => t.CustomerId);

        this.AddOrAlterColumnFor<SearchTerm>(t => t.Deleted)
            .AsBoolean();
    }
}
