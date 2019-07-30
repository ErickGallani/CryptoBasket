namespace CryptoBasket.Repository.Context
{
    using CryptoBasket.Domain.Entities;
    using CryptoBasket.Repository.Mappings;
    using Microsoft.EntityFrameworkCore;

    public class CryptoBasketContext : DbContext
    {
        public CryptoBasketContext(DbContextOptions<CryptoBasketContext> options)
            : base(options)
        { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Purchase>(new ProductMap().Configure);
        }

        //DBSets
        public DbSet<Purchase> Purchase { get; set; }
    }
}
