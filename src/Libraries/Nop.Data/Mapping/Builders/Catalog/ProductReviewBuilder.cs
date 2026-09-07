using FluentMigrator.Builders.Create.Table;
using Nop.Core.Domain.Catalog;
using Nop.Core.Domain.Customers;
using Nop.Core.Domain.Stores;
using Nop.Data.Extensions;

namespace Nop.Data.Mapping.Builders.Catalog;

/// <summary>
/// Represents a product review entity builder
/// </summary>
public partial class ProductReviewBuilder : NopEntityBuilder<ProductReview>
{
    #region Methods

    /// <summary>
    /// Apply entity configuration
    /// </summary>
    /// <param name="table">Create table expression builder</param>
    public override void MapEntity(CreateTableExpressionBuilder table)
    {
        table
            .WithColumn(nameof(ProductReview.CustomerId)).AsInt64().ForeignKey<Customer>()
            .WithColumn(nameof(ProductReview.ProductId)).AsInt64().ForeignKey<Product>()
            .WithColumn(nameof(ProductReview.StoreId)).AsInt64().ForeignKey<Store>();
    }

    #endregion
}