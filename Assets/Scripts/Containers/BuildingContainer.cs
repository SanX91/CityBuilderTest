using System;
using System.Collections.Generic;
using UnityEngine;

namespace CityBuilderTest
{
    /// <summary>
    /// The building container class.
    /// Stores all the building prefabs and their properties.
    /// </summary>
    [CreateAssetMenu(fileName = "BuildingContainer", menuName = "CityBuilderTest/BuildingContainer")]
    public class BuildingContainer : ScriptableObject, IBuildingContainer
    {
        public List<ProductionBuildingConfig> productionBuildings;
        public List<BuildingConfig> decorationBuildings;

        public IEnumerable<BuildingConfig> DecorationBuildings()
        {
            foreach (BuildingConfig building in decorationBuildings)
            {
                yield return building;
            }
        }

        public IEnumerable<ProductionBuildingConfig> ProductionBuildings()
        {
            foreach (ProductionBuildingConfig building in productionBuildings)
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

    /// <summary>
    /// ProductionBuildingConfig derives from BuildingConfig and adds a resource production field to it.
    /// </summary>
    [Serializable]
    public class ProductionBuildingConfig : BuildingConfig
    {
        public ResourceProduction production;
    }

    /// <summary>
    /// The Currency class.
    /// Can be used as a cost of an object.
    /// Also can be used to store resource data.
    /// </summary>
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

        public void Add(Currency currency)
        {
            gold += currency.gold;
            wood += currency.wood;
            steel += currency.steel;
        }

        public void Remove(Currency currency)
        {
            gold -= currency.gold;
            wood -= currency.wood;
            steel -= currency.steel;
        }
    }

    [Serializable]
    public class ResourceProduction
    {
        public Currency produce;
        [Tooltip("Time in seconds")]
        public float time;
    }
}
