using UnityEngine.UI;

namespace CityBuilderTest
{
    /// <summary>
    /// The resources panel class.
    /// </summary>
    public class ResourcesPanel : MiniPanel, IInitializer<IResourceManager>
    {
        public Text goldAmountText, woodAmountText, steelAmountText;
        private IResourceManager resourceManager;

        public void Initialize(IResourceManager resourceManager)
        {
            this.resourceManager = resourceManager;
            this.resourceManager.OnResourceUpdate += OnResourceUpdate;
        }

        private void OnResourceUpdate(object sender, ResourceUpdateEventArgs e)
        {
            goldAmountText.text = $"[{e.ResourceData.gold}]";
            woodAmountText.text = $"[{e.ResourceData.wood}]";
            steelAmountText.text = $"[{e.ResourceData.steel}]";
        }
    }
}