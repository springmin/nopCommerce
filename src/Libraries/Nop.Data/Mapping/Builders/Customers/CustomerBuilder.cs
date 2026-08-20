using System.Data;
using FluentMigrator.Builders.Create.Table;
using Nop.Core.Domain.Common;
using Nop.Core.Domain.Customers;
using Nop.Core.Domain.Directory;
using Nop.Core.Domain.Localization;
using Nop.Data.Extensions;

namespace Nop.Data.Mapping.Builders.Customers;

/// <summary>
/// Represents a customer entity builder
/// </summary>
public partial class CustomerBuilder : NopEntityBuilder<Customer>
{
    #region Methods

    /// <summary>
    /// Apply entity configuration
    /// </summary>
    /// <param name="table">Create table expression builder</param>
    public override void MapEntity(CreateTableExpressionBuilder table)
    {
        table
            // Username/Email are indexed columns: on utf8mb4 databases (MySQL 8,
            // TiDB) an index key is limited to 3072 bytes, so a VARCHAR(1000) column
            // (4000 bytes) would exceed it. 255 chars matches NewsLetterSubscription.Email.
            .WithColumn(nameof(Customer.Username)).AsString(255).Nullable()
            .WithColumn(nameof(Customer.Email)).AsString(255).Nullable()
            .WithColumn(nameof(Customer.EmailToRevalidate)).AsString(255).Nullable()
            .WithColumn(nameof(Customer.FirstName)).AsString(1000).Nullable()
            .WithColumn(nameof(Customer.LastName)).AsString(1000).Nullable()
            .WithColumn(nameof(Customer.Gender)).AsString(1000).Nullable()
            .WithColumn(nameof(Customer.Company)).AsString(1000).Nullable()
            .WithColumn(nameof(Customer.StreetAddress)).AsString(1000).Nullable()
            .WithColumn(nameof(Customer.StreetAddress2)).AsString(1000).Nullable()
            .WithColumn(nameof(Customer.ZipPostalCode)).AsString(1000).Nullable()
            .WithColumn(nameof(Customer.City)).AsString(1000).Nullable()
            .WithColumn(nameof(Customer.County)).AsString(1000).Nullable()
            .WithColumn(nameof(Customer.Phone)).AsString(1000).Nullable()
            .WithColumn(nameof(Customer.Fax)).AsString(1000).Nullable()
            .WithColumn(nameof(Customer.VatNumber)).AsString(1000).Nullable()
            .WithColumn(nameof(Customer.TimeZoneId)).AsString(1000).Nullable()
            .WithColumn(nameof(Customer.CustomCustomerAttributesXML)).AsString(int.MaxValue).Nullable()
            .WithColumn(nameof(Customer.DateOfBirth)).AsDateTime2().Nullable()
            .WithColumn(nameof(Customer.SystemName)).AsString(400).Nullable()
            .WithColumn(nameof(Customer.LastIpAddress)).AsString(100).Nullable()
            .WithColumn(nameof(Customer.CurrencyId)).AsInt64().ForeignKey<Currency>(onDelete: Rule.SetNull).Nullable()
            .WithColumn(nameof(Customer.LanguageId)).AsInt64().ForeignKey<Language>(onDelete: Rule.SetNull).Nullable()
            .WithColumn(NameCompatibilityManager.GetColumnName(typeof(Customer), nameof(Customer.BillingAddressId))).AsInt32().ForeignKey<Address>(onDelete: Rule.None).Nullable()
            .WithColumn(NameCompatibilityManager.GetColumnName(typeof(Customer), nameof(Customer.ShippingAddressId))).AsInt32().ForeignKey<Address>(onDelete: Rule.None).Nullable();
    }

    #endregion
}