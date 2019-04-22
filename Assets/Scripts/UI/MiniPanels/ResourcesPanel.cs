using System;
using UnityEngine;
using UnityEngine.UI;

namespace CityBuilderTest
{
    public class ResourcesPanel : MiniPanel, IInitializer<ResourceManager>
    {
        public Text goldAmountText, woodAmountText, steelAmountText;
        ResourceManager resourceManager;

        public void Initialize(ResourceManager param)
        {
            resourceManager = param;
            resourceManager.OnResourceUpdate += OnResourceUpdate;
        }

        private void OnResourceUpdate(object sender, ResourceUpdateEventArgs e)
        {
            goldAmountText.text = $"[{e.ResourceData.gold}]";
            woodAmountText.text = $"[{e.ResourceData.wood}]";
            steelAmountText.text = $"[{e.ResourceData.steel}]";
        }
    }
}