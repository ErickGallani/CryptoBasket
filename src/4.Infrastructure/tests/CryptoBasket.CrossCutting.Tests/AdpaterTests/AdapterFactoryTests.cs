namespace CryptoBasket.CrossCutting.Tests.AdpaterTests
{
    using CryptoBasket.CrossCutting.Adapters;
    using System;
    using System.Threading.Tasks;
    using Xunit;

    public class AdapterFactoryTests
    {
        private readonly IAdapterFactory adapterFactory;

        public AdapterFactoryTests()
        {
            adapterFactory = new AdapterFactory();
        }

        [Fact]
        public void Given_AValidAdapterRequested_When_TheFactoryBuildsIt_Then_TheRightAdapterIsReturned()
        {
            // act
            var adapter = this.adapterFactory.Create<AdapterFake>();

            // assert
            Assert.NotNull(adapter);
            Assert.True(adapter.GetType() == typeof(AdapterFake));
        }

        [Fact]
        public void Given_MultipleAdapterAreRequestedAsync_When_TheFactoryBuildsIt_Then_NoErrorsOccursTheRightAdapterIsReturned()
        {
            // act
            var task1 = BuildAdapter();
            var task2 = BuildAdapter();
            var task3 = BuildAdapter();
            var task4 = BuildAdapter();

            Task.WaitAll(task1, task2, task3, task4);

            // assert
            Assert.NotNull(task1.Result);
            Assert.NotNull(task2.Result);
            Assert.NotNull(task3.Result);
            Assert.NotNull(task4.Result);

            Assert.True(task1.Result.GetType() == typeof(AdapterFake));
            Assert.True(task2.Result.GetType() == typeof(AdapterFake));
            Assert.True(task3.Result.GetType() == typeof(AdapterFake));
            Assert.True(task4.Result.GetType() == typeof(AdapterFake));
        }

        private Task<AdapterFake> BuildAdapter() =>
           Task.FromResult(this.adapterFactory.Create<AdapterFake>());

        private class AdapterFake : IAdapter<FakeClassA, FakeClassB>
        {
            public FakeClassB Adapt(FakeClassA source) =>
                throw new NotImplementedException();
        }

        private class FakeClassA
        { }

        private class FakeClassB
        { }
    }
}
