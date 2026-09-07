using FluentMigrator.Expressions;
using FluentMigrator.Runner.Generators.SQLite;
using FluentMigrator.Runner.Initialization;
using FluentMigrator.Runner.Processors;
using FluentMigrator.Runner.Processors.SQLite;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Nop.Data.Migrations;

/// <summary>
/// SQLite migration processor that tolerates ALTER COLUMN expressions.
/// </summary>
/// <remarks>
/// SQLite does not support the ALTER COLUMN statement, so the stock
/// <see cref="SQLiteProcessor"/> throws for any <see cref="AlterColumnExpression"/>.
/// During a fresh installation the full table schema is already created with the final
/// column types by the schema migration, so altering a column is a no-op here. The same
/// applies when upgrade migrations run against an already-installed SQLite database whose
/// schema matches the entity model.
/// </remarks>
public partial class NopSqliteProcessor : SQLiteProcessor
{
    /// <summary>
    /// Initializes a new instance of the <see cref="NopSqliteProcessor"/> class.
    /// </summary>
    public NopSqliteProcessor(SQLiteDbFactory dbFactory, SQLiteGenerator generator,
        ILogger<SQLiteProcessor> logger, IOptionsSnapshot<ProcessorOptions> options,
        IConnectionStringAccessor connectionStringAccessor, IServiceProvider serviceProvider,
        SQLiteQuoter quoter)
        : base(dbFactory, generator, logger, options, connectionStringAccessor, serviceProvider, quoter)
    {
    }

    /// <inheritdoc />
    public override void Process(AlterColumnExpression expression)
    {
        //SQLite does not support altering a column; the schema is created with the
        //final types by the schema migration, so there is nothing to do
    }

    /// <inheritdoc />
    public override void Process(AlterDefaultConstraintExpression expression)
    {
        //SQLite does not support altering default constraints; the schema is created
        //with the final constraints by the schema migration, so there is nothing to do
    }
}
