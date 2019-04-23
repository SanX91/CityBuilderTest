using System;
using System.Collections.Generic;
using UnityEngine;

namespace CityBuilderTest
{
    /// <summary>
    /// The buildings mini panel.
    /// Responsible for creating all the building buttons.
    /// </summary>
    public class BuildingsPanel : MiniPanel, IInitializer<IGameManager>
    {
        public RectTransform content;
        public BuildingButton buildingButtonPrefab;
        private IBuildingContainer buildingContainer;
        private List<BuildingButton> buildingButtons;

        public void Initialize(IGameManager gameManager)
        {
            buildingContainer = gameManager.GameConfig().BuildingContainer();

            buildingButtons = new List<BuildingButton>();
            buildingButtons.AddRange(CreateButtons(buildingContainer.ProductionBuildings(), 
                gameManager.ResourceManager(), 
                gameManager.ModeSelection().BuildMode().CreateBuilding));
            buildingButtons.AddRange(CreateButtons(buildingContainer.DecorationBuildings(), 
                gameManager.ResourceManager(), 
                gameManager.ModeSelection().BuildMode().CreateBuilding));
        }

        private IEnumerable<BuildingButton> CreateButtons(IEnumerable<BuildingConfig> buildings, IResourceManager resourceManager, Action<BuildingConfig> onClick)
        {
            foreach (BuildingConfig building in buildings)
            {
                BuildingButton button = Instantiate(buildingButtonPrefab, content);
                button.Initialize(building, resourceManager, () =>
                {
                    onClick(building);
                });
                yield return button;
            }
        }
    }
}