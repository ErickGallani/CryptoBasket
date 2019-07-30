namespace CryptoBasket.Repository.Tests.Repositories
{
    using CryptoBasket.Domain.Entities;
    using CryptoBasket.Domain.ValueObjects;
    using CryptoBasket.Repository.Context;
    using CryptoBasket.Repository.Repositories;
    using Microsoft.EntityFrameworkCore;
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using Xunit;

    public class PurchaseRepositoryTests
    {
        [Fact]
        public async Task Given_AValidPurchase_When_TryToInsert_Then_IsInsertedWithSuccess()
        {
            // arrange
            var dbContextOptions = new DbContextOptionsBuilder<CryptoBasketContext>()
                .UseInMemoryDatabase(databaseName: "InsertTestDatabase")
                .Options;

            var product = new Product(
                123456,
                "My Coin",
                "MCN",
                245.45D);

            Purchase purchase = new Purchase(product, quantity: 2);

            // act
            using (var context = new CryptoBasketContext(dbContextOptions))
            {
                var repository = new PurchaseRepository(context);
                await repository.SaveAsync(purchase);
            }

            // assert
            using (var context = new CryptoBasketContext(dbContextOptions))
            {
                Assert.Equal(1, context.Purchase.Count());
                Assert.NotNull(context.Purchase.Single());

                var purchaseFromDatabase = context.Purchase.Single();

                Assert.NotEqual(default, purchaseFromDatabase.Id);

                Assert.Equal(purchase.Id, purchaseFromDatabase.Id);
                Assert.Equal(purchase.Quantity, purchaseFromDatabase.Quantity);
                Assert.Equal(product, purchaseFromDatabase.Product);
            }
        }

        [Fact]
        public async Task Given_PurchaseId_When_ThePurchaseOnTheDataBase_Then_ReturnsThePurchaseFromDatabase()
        {
            // arrange
            var dbContextOptions = new DbContextOptionsBuilder<CryptoBasketContext>()
                .UseInMemoryDatabase(databaseName: "GetTestDatabase")
                .Options;

            var product = new Product(
                123456,
                "My Coin",
                "MCN",
                245.45D);

            Purchase purchase = new Purchase(product, quantity: 2);

            // act
            using (var context = new CryptoBasketContext(dbContextOptions))
            {
                var repository = new PurchaseRepository(context);
                await repository.SaveAsync(purchase);
            }

            // assert
            using (var context = new CryptoBasketContext(dbContextOptions))
            {
                var repository = new PurchaseRepository(context);

                var purchaseFromDatabase = await repository.GetByIdAsync(purchase.Id);

                Assert.NotNull(purchaseFromDatabase);
                Assert.Equal(purchase.Id, purchaseFromDatabase.Id);
                Assert.Equal(purchase.Quantity, purchaseFromDatabase.Quantity);
                Assert.Equal(product, purchaseFromDatabase.Product);
            }
        }

        [Fact]
        public async Task Given_PurchaseId_When_ThePurchaseDoesNotExistsOnTheDataBase_Then_NothingReturns()
        {
            // arrange
            var dbContextOptions = new DbContextOptionsBuilder<CryptoBasketContext>()
                .UseInMemoryDatabase(databaseName: "GetNothingExistsTestDatabase")
                .Options;

            // act
            var purchaseId = Guid.NewGuid();

            // assert
            using (var context = new CryptoBasketContext(dbContextOptions))
            {
                var repository = new PurchaseRepository(context);

                var purchaseFromDatabase = await repository.GetByIdAsync(purchaseId);

                Assert.Null(purchaseFromDatabase);
            }
        }
    }
}
