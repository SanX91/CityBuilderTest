using System;
using UnityEngine;
using UnityEngine.UI;

namespace CityBuilderTest
{
    public class BuildingButton : MonoBehaviour, IInitializer<BuildingConfig, ResourceManager, Action>
    {
        [SerializeField]
        private Button button;
        [SerializeField]
        private Text buttonNameText, costText;
        private Action onClick;

        Currency itemCost;
        ResourceManager resourceManager;

        void OnEnable()
        {
            ToggleInteractable();
        }

        public void Initialize(BuildingConfig buildingConfig, ResourceManager resourceManager, Action onClick)
        {
            buttonNameText.text = buildingConfig.name;
            itemCost = buildingConfig.itemCost;
            this.resourceManager = resourceManager;
            this.onClick = onClick;

            resourceManager.OnResourceUpdate += OnResourceUpdate;

            costText.text = $"G: {itemCost.gold}, W: {itemCost.wood}, S: {itemCost.steel}";
            ToggleInteractable();
        }

        private void OnResourceUpdate(object sender, ResourceUpdateEventArgs e)
        {
            ToggleInteractable();
        }

        void ToggleInteractable()
        {
            if(resourceManager == null)
            {
                return;
            }

            button.interactable = resourceManager.HaveSufficientResources(itemCost);
        }

        public void OnClick()
        {
            onClick?.Invoke();
        }
    }
}

