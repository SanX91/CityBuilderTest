using UnityEngine;
using UnityEngine.EventSystems;

namespace CityBuilderTest
{
    /// <summary>
    /// The base Mode class.
    /// </summary>
    public abstract class Mode : MonoBehaviour, IUpdateable, IInitializer<IController>
    {
        public LayerMask buildingMask;
        public EventSystem eventSystem;
        protected IController controller;

        public abstract void OnUpdate();
        public abstract void OnExit();

        public void Initialize(IController controller)
        {
            this.controller = controller;
        }
    }
}

