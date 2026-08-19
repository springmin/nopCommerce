using FluentMigrator.Builders.Create.Table;
using Nop.Core.Domain.Common;

namespace Nop.Data.Mapping.Builders.Common;

/// <summary>
/// Represents a generic attribute entity builder
/// </summary>
public partial class GenericAttributeBuilder : NopEntityBuilder<GenericAttribute>
{
    #region Methods

    /// <summary>
    /// Apply entity configuration
    /// </summary>
    /// <param name="table">Create table expression builder</param>
    public override void MapEntity(CreateTableExpressionBuilder table)
    {
        table
            // KeyGroup + Key are covered by the combined index
            // IX_GenericAttribute_EntityId_KeyGroup_and_Key: on utf8mb4 databases
            // (MySQL 8, TiDB) an index key is limited to 3072 bytes, so two
            // VARCHAR(400) columns (1600+1600=3200 bytes) would exceed it.
            .WithColumn(nameof(GenericAttribute.KeyGroup)).AsString(255).NotNullable()
            .WithColumn(nameof(GenericAttribute.Key)).AsString(255).NotNullable()
            .WithColumn(nameof(GenericAttribute.Value)).AsString(int.MaxValue).NotNullable();
    }

    #endregion
}