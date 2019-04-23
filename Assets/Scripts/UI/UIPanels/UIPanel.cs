using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace CityBuilderTest
{
    /// <summary>
    /// The base UI panel class.
    /// </summary>
    public abstract class UIPanel : MonoBehaviour, IPanel, ISerializationCallbackReceiver
    {
        [SerializeField]
        private List<MiniPanel> miniPanels;

        public virtual void Close()
        {
            gameObject.SetActive(false);
        }

        public virtual void Open()
        {
            gameObject.SetActive(true);
        }

        /// <summary>
        /// Gets any mini panel derived from MiniPanel class.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        protected T GetMiniPanel<T>() where T : MiniPanel
        {
            return (T)miniPanels.Single(x => x.GetType() == typeof(T));
        }

        /// <summary>
        /// Since the mini panels can be directly dragged and dropped in the miniPanels list,
        /// makes sure that there are no duplicates.
        /// </summary>
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

