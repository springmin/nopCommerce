using FluentMigrator.Expressions;


namespace Nop.Data.Migrations;

/// <summary>
/// SQLite generator that tolerates ALTER COLUMN expressions.
/// </summary>
/// <remarks>
/// Microsoft.Data.Sqlite / SQLite does not support the ALTER COLUMN statement, so the
/// stock <see cref="SqliteGenerator"/> throws
/// <see cref="FluentMigrator.Exceptions.DatabaseOperationNotSupportedException"/> for any
/// <see cref="AlterColumnExpression"/>. During a fresh installation the full table schema
/// is already created with the final column types by the schema migration, so altering a
/// column is a no-op here. The same applies when upgrade migrations run against an
/// already-installed SQLite database whose schema matches the entity model.
/// </remarks>
public partial class NopSqliteGenerator : FluentMigrator.Runner.Generators.SQLite.SQLiteGenerator
{
    /// <inheritdoc />
    public override string Generate(AlterColumnExpression expression)
    {
        //SQLite does not support altering a column; the schema is created with the
        //final types by the schema migration, so there is nothing to do
        return string.Empty;
    }

    /// <inheritdoc />
    public override string Generate(AlterDefaultConstraintExpression expression)
    {
        //SQLite does not support altering default constraints; the schema is created
        //with the final constraints by the schema migration, so there is nothing to do
        return string.Empty;
    }
}
