namespace CryptoBasket.CrossCutting.Adapters
{
    public interface IAdapterFactory
    {
        TAdapter Create<TAdapter>() where TAdapter : class, IAdapter, new();
    }
}
