using System;

namespace Domain.Mappers
{
    public abstract class SimpleMapper<TSource, TTarget> : IMapper<TSource, TTarget>
        where TTarget : new()
    {
        public TTarget Map(TSource source)
        {
            if (source == null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            var target = new TTarget();

            Map(source, target);

            return target;
        }

        public abstract void Map(TSource source, TTarget target);
    }
}