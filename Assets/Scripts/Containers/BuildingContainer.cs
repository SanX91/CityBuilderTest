using System;
using System.Collections.Generic;
using UnityEngine;

namespace CityBuilderTest
{
    [CreateAssetMenu(fileName = "BuildingContainer", menuName = "CityBuilderTest/BuildingContainer")]
    public class BuildingContainer : ScriptableObject, IBuildingContainer
    {
        public List<ProductionBuildingConfig> productionBuildings;
        public List<BuildingConfig> decorationBuildings;

        public IEnumerable<BuildingConfig> DecorationBuildings()
        {
            foreach(var building in decorationBuildings)
            {
                yield return building;
            }
        }

        public IEnumerable<ProductionBuildingConfig> ProductionBuildings()
        {
            foreach (var building in productionBuildings)
            {
                yield return building;
            }
        }
    }

    [Serializable]
    public class BuildingConfig
    {
        public string name;
        public GameObject prefab;
        public Vector2Int gridCost;
        public Currency itemCost;
        [Tooltip("Time in seconds")]
        public float constructionTime = 10;
    }

    [Serializable]
    public class ProductionBuildingConfig : BuildingConfig
    {
        public ResourceProduction production;
    }

    [Serializable]
    public class Currency
    {
        public int gold, wood, steel;

        public Currency(Currency target)
        {
            gold = target.gold;
            wood = target.wood;
            steel = target.steel;
        }

        public Currency()
        {

        }

        public bool IsGreaterThanOrEqual(Currency target)
        {
            return target != null
                && gold >= target.gold
                && wood >= target.wood
                && steel >= target.steel;
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
