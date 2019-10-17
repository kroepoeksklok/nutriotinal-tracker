namespace NutritionalTracker.Mappers {
    public interface IMapperProcessor {
        TTo Map<TFrom, TTo>(TFrom from);
    }
}