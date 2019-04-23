using UnityEngine;

namespace CityBuilderTest
{
    /// <summary>
    /// The Game Config class.
    /// Stores data for initializing the game.
    /// Also stores various prefabs and their properties.
    /// </summary>
    [CreateAssetMenu(fileName = "GameConfig", menuName = "CityBuilderTest/GameConfig")]
    public class GameConfig : ScriptableObject, IGameConfig
    {
        public BuildingContainer buildingContainer;
        public Currency startUpFunds;

        /// <summary>
        /// Contains prefabs and properties of various buildings.
        /// </summary>
        /// <returns></returns>
        public IBuildingContainer BuildingContainer()
        {
            return buildingContainer;
        }

        /// <summary>
        /// The base funds for the gameplay.
        /// </summary>
        /// <returns></returns>
        public Currency StartUpFunds()
        {
            return startUpFunds;
        }
    }
}

