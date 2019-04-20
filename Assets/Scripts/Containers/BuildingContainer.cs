using System;
using System.Collections.Generic;
using UnityEngine;

namespace CityBuilderTest
{
    [CreateAssetMenu(fileName = "BuildingContainer", menuName = "CityBuilderTest/BuildingContainer")]
    public class BuildingContainer : ScriptableObject
    {
        public List<ProductionBuildingConfig> productionBuildings;
        public List<BuildingConfig> decorationBuildings;
    }

    [Serializable]
    public class BuildingConfig
    {
        public string name;
        public GameObject prefab;
        public Vector2 gridSize;
        public ResourcePile itemCost;
    }

    [Serializable]
    public class ProductionBuildingConfig : BuildingConfig
    {
        public ResourceProduction production;
    }

    [Serializable]
    public class ResourcePile
    {
        public int gold, wood, steel;

        public bool IsGreaterThanOrEqual(ResourcePile resourceData)
        {
            return resourceData != null
                && resourceData.gold >= gold
                && resourceData.wood >= wood
                && resourceData.steel >= steel;
        }
    }

    [Serializable]
    public class ResourceProduction
    {
        public ResourceTypes resourceType;
        public int amount;
        [Tooltip("Time in seconds")]
        public float time;
    }

    public enum ResourceTypes
    {
        Gold = 0,
        Wood = 1,
        Steel = 2
    } 
}
