using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace CityBuilderTest
{
    /// <summary>
    /// The UI manager class.
    /// Responsible for initializing and toggling of UI panels.
    /// </summary>
    public class UIManager : MonoBehaviour, ISerializationCallbackReceiver, IInitializer<IGameManager>
    {
        public List<UIPanel> uiPanels;

        /// <summary>
        /// Gets any panel derived from UIPanel class.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        private T GetPanel<T>() where T : UIPanel
        {
            return (T)uiPanels.Single(x => x.GetType() == typeof(T));
        }

        public void OnBeforeSerialize() { }

        /// <summary>
        /// Since the UI panels can be directly dragged and dropped in the uiPanels list,
        /// makes sure that there are no duplicates.
        /// </summary>
        public void OnAfterDeserialize()
        {
            if (uiPanels.Count > uiPanels.Distinct().Count())
            {
                uiPanels = uiPanels.Distinct().ToList();
                Debug.LogError("Item already added");
            }
        }

        public void Initialize(IGameManager gameManager)
        {
            GetPanel<GameHUD>().Initialize(gameManager);
            GetPanel<GameHUD>().Open();
        }
    }
}