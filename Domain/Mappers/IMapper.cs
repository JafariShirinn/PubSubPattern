namespace Domain.Mappers
{

    public interface IMapper<TSource, TTarget>
    {
        TTarget Map(TSource source);

        void Map(TSource source, TTarget target);
    }

}
