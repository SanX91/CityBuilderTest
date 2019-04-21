using System.Collections.Generic;

namespace CityBuilderTest
{
    public interface IPanel
    {
        void Open();
        void Close();
    }

    public interface IInitializer
    {
        void Initialize();
    }

    public interface IInitializer<T>
    {
        void Initialize(T param);
    }

    public interface IInitializer<T, U>
    {
        void Initialize(T param1, U param2);
    }

    public interface IInitializer<T, U, V>
    {
        void Initialize(T param1, U param2, V param3);
    }

    public interface IBuildingContainer
    {
        IEnumerable<ProductionBuildingConfig> ProductionBuildings();
        IEnumerable<BuildingConfig> DecorationBuildings();
    }
}

