using System.Collections.Generic;
using UnityEngine;

namespace CityBuilderTest
{
    public class BuildingsPanel : MiniPanel, IInitializer<IBuildingContainer>
    {
        public RectTransform content;
        public BuildingButton buildingButtonPrefab;
        private IBuildingContainer buildingContainer;
        private List<BuildingButton> buildingButtons;

        public void Initialize(IBuildingContainer param)
        {
            buildingContainer = param;

            buildingButtons = new List<BuildingButton>();
            buildingButtons.AddRange(CreateButtons(param.ProductionBuildings()));
            buildingButtons.AddRange(CreateButtons(param.DecorationBuildings()));
        }

        private IEnumerable<BuildingButton> CreateButtons(IEnumerable<BuildingConfig> buildings)
        {
            foreach (BuildingConfig building in buildings)
            {
                BuildingButton button = Instantiate(buildingButtonPrefab, content);
                button.Initialize(building, new Currency(), null);
                yield return button;
            }
        }
    }
}