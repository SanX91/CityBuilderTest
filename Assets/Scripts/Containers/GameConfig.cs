using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CityBuilderTest
{
    [CreateAssetMenu(fileName = "GameConfig", menuName = "CityBuilderTest/GameConfig")]
    public class GameConfig : ScriptableObject, IGameConfig
    {
        public BuildingContainer buildingContainer;

        public IBuildingContainer BuildingContainer()
        {
            return buildingContainer;
        }
    }
}

