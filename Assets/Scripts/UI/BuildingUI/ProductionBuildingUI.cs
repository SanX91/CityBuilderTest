using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace CityBuilderTest
{
    public class ProductionBuildingUI : BuildingUI, IInitializer<Action>
    {
        [SerializeField]
        Button productionButton;

        public void Initialize(Action param)
        {
            productionButton.onClick.AddListener(()=>
            {
                param?.Invoke();
            });
        }

        public void ToggleProductionButton(bool isActive)
        {
            productionButton.gameObject.SetActive(isActive);
        }

        public override void ToggleUI(bool isActive)
        {
            base.ToggleUI(isActive);
            ToggleProductionButton(isActive);
        }
    }
}

