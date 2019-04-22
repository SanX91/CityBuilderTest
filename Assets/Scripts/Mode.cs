using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace CityBuilderTest
{
    public abstract class Mode : MonoBehaviour
    {
        public EventSystem eventSystem;
        protected IController controller;
    }
}

