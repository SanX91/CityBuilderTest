using UnityEngine;

namespace CityBuilderTest
{
    public abstract class MiniPanel : MonoBehaviour, IPanel
    {
        protected bool isInitialized;

        public virtual void Close()
        {
            gameObject.SetActive(false);
        }

        public virtual void Open()
        {
            gameObject.SetActive(true);
        }
    }
}

