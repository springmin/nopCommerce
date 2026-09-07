using FluentMigrator.Builders.Create.Table;
using Nop.Core.Domain.Catalog;
using Nop.Core.Domain.Shipping;
using Nop.Data.Extensions;

namespace Nop.Data.Mapping.Builders.Catalog;

/// <summary>
/// Represents a product warehouse inventory entity builder
/// </summary>
public partial class ProductWarehouseInventoryBuilder : NopEntityBuilder<ProductWarehouseInventory>
{
    #region Methods

    /// <summary>
    /// Apply entity configuration
    /// </summary>
    /// <param name="table">Create table expression builder</param>
    public override void MapEntity(CreateTableExpressionBuilder table)
    {
        table
            .WithColumn(nameof(ProductWarehouseInventory.ProductId)).AsInt64().ForeignKey<Product>()
            .WithColumn(nameof(ProductWarehouseInventory.WarehouseId)).AsInt64().ForeignKey<Warehouse>();
    }

    #endregion
}