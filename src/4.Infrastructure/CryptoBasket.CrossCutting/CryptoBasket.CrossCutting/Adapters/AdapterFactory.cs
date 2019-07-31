namespace CryptoBasket.CrossCutting.Adapters
{
    using System;
    using System.Collections.Concurrent;

    public class AdapterFactory : IAdapterFactory
    {
        private readonly ConcurrentDictionary<Type, IAdapter> adapters;

        public AdapterFactory() =>
            adapters = new ConcurrentDictionary<Type, IAdapter>();

        public TAdapter Create<TAdapter>() where TAdapter : class, IAdapter, new() =>
            adapters.GetOrAdd(typeof(TAdapter), new TAdapter()) as TAdapter;
    }
}
