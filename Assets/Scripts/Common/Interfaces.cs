using System.Collections.Generic;
using UnityEngine;

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

    public interface IGameConfig
    {
        IBuildingContainer BuildingContainer();
        Currency StartUpFunds();
    }

    public interface IBuildingContainer
    {
        IEnumerable<ProductionBuildingConfig> ProductionBuildings();
        IEnumerable<BuildingConfig> DecorationBuildings();
    }

    public interface IController
    {
        Vector2 Position();
        bool HasClicked();
    }

    public interface IPlaceable
    {
        Vector2Int StartGridId { get; set; }
        Vector2Int GridCost();
    }

    public interface IUpdateable
    {
        void OnUpdate();
    }

    public interface IGameManager
    {
        IGameConfig GameConfig();
        IModeSelection ModeSelection();
        ResourceManager GetResourceManager();
    }

    public interface IModeSelection
    {
        BuildMode BuildMode();
        RegularMode RegularMode();
        void SwitchMode(Mode mode);
        bool IsBusy { get; set; }
    }

    public interface ISelectable
    {
        void OnSelect();
        void OnDeselect();
    }
}

