using System;
using UnityEngine;
using UnityEngine.UI;

namespace CityBuilderTest
{
    /// <summary>
    /// The building button class.
    /// Displays the right information and cost of a building.
    /// Should create a building when clicked.
    /// </summary>
    public class BuildingButton : MonoBehaviour, IInitializer<BuildingConfig, IResourceManager, Action>
    {
        [SerializeField]
        private Button button;
        [SerializeField]
        private Text buttonNameText, costText;
        private Action onClick;
        private Currency itemCost;
        private IResourceManager resourceManager;

        private void OnEnable()
        {
            ToggleInteractable();
        }

        public void Initialize(BuildingConfig buildingConfig, IResourceManager resourceManager, Action onClick)
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

        private void ToggleInteractable()
        {
            if (resourceManager == null)
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

