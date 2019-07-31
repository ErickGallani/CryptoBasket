namespace CryptoBasket.CrossCutting.Adapters
{
    public interface IAdapter<in TSource, out TDestination> : IAdapter
        where TSource : class
        where TDestination : class
    {
        TDestination Adapt(TSource source);
    }

    public interface IAdapter { }
}
