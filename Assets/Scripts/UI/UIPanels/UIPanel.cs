using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace CityBuilderTest
{
    public abstract class UIPanel : MonoBehaviour, IPanel, ISerializationCallbackReceiver
    {
        [SerializeField]
        List<MiniPanel> miniPanels;

        public virtual void Close()
        {
            gameObject.SetActive(false);
        }

        public virtual void Open()
        {
            gameObject.SetActive(true);
        }

        protected T GetMiniPanel<T>() where T : MiniPanel
        {
            return (T)miniPanels.Single(x => x.GetType() == typeof(T));
        }

        public void OnAfterDeserialize()
        {
            if (miniPanels.Count > miniPanels.Distinct().Count())
            {
                miniPanels = miniPanels.Distinct().ToList();
                Debug.LogError("Item already added");
            }
        }

        public void OnBeforeSerialize() { }
    }
}

