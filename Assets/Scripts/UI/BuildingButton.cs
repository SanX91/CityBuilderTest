using System;
using UnityEngine;
using UnityEngine.UI;

namespace CityBuilderTest
{
    public class BuildingButton : MonoBehaviour, IInitializer<BuildingConfig, Currency, Action>
    {
        [SerializeField]
        private Button button;
        [SerializeField]
        private Text buttonNameText, costText;
        private Action onClick;

        Currency itemCost, currentResources;

        public void Initialize(BuildingConfig buildingConfig, Currency currentResources, Action onClick)
        {
            buttonNameText.text = buildingConfig.name;
            itemCost = buildingConfig.itemCost;
            this.currentResources = currentResources;
            this.onClick = onClick;

            costText.text = $"G: {itemCost.gold}, W: {itemCost.wood}, S: {itemCost.steel}";
            ToggleInteractable();
        }

        public void ToggleInteractable()
        {
            button.interactable = currentResources.IsGreaterThanOrEqual(itemCost);
        }

        public void OnClick()
        {
            onClick?.Invoke();
        }
    }
}

