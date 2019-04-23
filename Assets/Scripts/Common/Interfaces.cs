using System;
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
        bool HasFired();
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
        IResourceManager ResourceManager();
        IGridSystem GridSystem();
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
        void OnSelect(Mode mode);
        void OnDeselect();
    }

    public interface IResourceManager
    {
        event EventHandler<ResourceUpdateEventArgs> OnResourceUpdate;
        void AdjustResources(Currency currency, bool isExpense = false);
        bool HaveSufficientResources(Currency target);
    }

    public interface IGridSystem
    {
        bool CanPlaceObject(IPlaceable placeable, Vector2 inputPosition, out Vector2 gridPosition);
        void TogglePlaceObject(IPlaceable placeable, bool isPlaced);
    }
}

