using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CityBuilderTest
{
    public class GameManager : MonoBehaviour
    {
        public UIManager uiManager;
        public BuildingContainer buildingContainer;

        private void Start()
        {
            uiManager.Initialize(buildingContainer);
        }
    }
}
