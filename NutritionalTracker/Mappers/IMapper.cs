namespace NutritionalTracker.Mappers {
    public interface IMapper<TFrom, TTo> {
        TTo Map(TFrom fromObject);
    }
}