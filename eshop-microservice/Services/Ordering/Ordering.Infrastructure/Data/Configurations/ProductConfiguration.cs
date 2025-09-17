namespace Ordering.Infrastructure.Data.Configurations
{
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.HasKey(p => p.Id);

            builder.Property(p => p.Id)
                    .HasConversion(productId => productId.Value,
                    dbId => ProductId.Of(dbId));

            builder.Property(p => p.Name).HasMaxLength(200).IsRequired();

            builder.Property(p => p.Price).IsRequired().HasColumnType("decimal(18,2)");

        }
    }
}
