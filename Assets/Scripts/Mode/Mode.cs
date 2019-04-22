using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace CityBuilderTest
{
    public abstract class Mode : MonoBehaviour, IUpdateable
    {
        public LayerMask buildingMask;
        public EventSystem eventSystem;
        protected IController controller;

        public abstract void OnUpdate();
        public abstract void OnExit();
    }
}

