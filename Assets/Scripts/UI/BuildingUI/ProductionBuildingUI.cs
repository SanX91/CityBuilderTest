using System;
using UnityEngine;
using UnityEngine.UI;

namespace CityBuilderTest
{
    /// <summary>
    /// The production building UI class.
    /// Derives from the BuildingUI class.
    /// Apart from all the elements from the BuildingUI class, adds a production button.
    /// </summary>
    public class ProductionBuildingUI : BuildingUI, IInitializer<Action>
    {
        [SerializeField]
        private Button productionButton;

        public void Initialize(Action onClick)
        {
            productionButton.onClick.AddListener(() =>
            {
                onClick?.Invoke();
            });
        }

        public void ToggleProductionButton(bool isActive)
        {
            productionButton.gameObject.SetActive(isActive);
        }

        public override void ToggleUI(bool isActive)
        {
            base.ToggleUI(isActive);
            ToggleProductionButton(isActive);
        }
    }
}

