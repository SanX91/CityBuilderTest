using System;
using System.Collections.Generic;
using UnityEngine;

namespace CityBuilderTest
{
    public class BuildingsPanel : MiniPanel, IInitializer<IGameManager>
    {
        public RectTransform content;
        public BuildingButton buildingButtonPrefab;
        private IBuildingContainer buildingContainer;
        private List<BuildingButton> buildingButtons;

        public void Initialize(IGameManager param)
        {
            buildingContainer = param.GameConfig().BuildingContainer();

            buildingButtons = new List<BuildingButton>();
            buildingButtons.AddRange(CreateButtons(buildingContainer.ProductionBuildings(), param.GetResourceManager(), param.ModeSelection().BuildMode().CreateBuilding));
            buildingButtons.AddRange(CreateButtons(buildingContainer.DecorationBuildings(), param.GetResourceManager(), param.ModeSelection().BuildMode().CreateBuilding));
        }

        private IEnumerable<BuildingButton> CreateButtons(IEnumerable<BuildingConfig> buildings, ResourceManager resourceManager, Action<BuildingConfig> onClick)
        {
            foreach (BuildingConfig building in buildings)
            {
                BuildingButton button = Instantiate(buildingButtonPrefab, content);
                button.Initialize(building, resourceManager, ()=> {
                    onClick(building);
                });
                yield return button;
            }
        }
    }
}