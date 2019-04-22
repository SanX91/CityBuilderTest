using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

namespace CityBuilderTest
{
    public class UIManager : MonoBehaviour, ISerializationCallbackReceiver, IInitializer<IGameManager>
    {
        public List<UIPanel> uiPanels;

        T GetPanel<T>() where T:UIPanel
        {
            return (T)uiPanels.Single(x => x.GetType() == typeof(T));
        }

        public void OnBeforeSerialize() { }

        public void OnAfterDeserialize()
        {
            if (uiPanels.Count > uiPanels.Distinct().Count())
            {
                uiPanels = uiPanels.Distinct().ToList();
                Debug.LogError("Item already added");
            }
        }

        public void Initialize(IGameManager param)
        {
            GetPanel<GameHUD>().Initialize(param);
            GetPanel<GameHUD>().Open();
        }
    }
}