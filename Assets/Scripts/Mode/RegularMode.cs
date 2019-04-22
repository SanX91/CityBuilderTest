using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CityBuilderTest
{
    public class RegularMode : Mode
    {
        Building selectedBuilding;

        public override void OnExit()
        {
            DeselectBuilding();
        }

        void DeselectBuilding()
        {
            if (selectedBuilding == null)
            {
                return;
            }

            selectedBuilding.OnConstructionComplete -= OnConstructionComplete;
            selectedBuilding.OnDeselect();
            selectedBuilding = null;
        }

        public override void OnUpdate()
        {
            if (eventSystem.IsPointerOverGameObject())
            {
                return;
            }

            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (!Physics.Raycast(ray, out hit, Mathf.Infinity, buildingMask.value))
            {
                return;
            }

            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                Building building = hit.collider.gameObject.GetComponent<Building>();
                if (selectedBuilding == building)
                {
                    return;
                }

                DeselectBuilding();
                selectedBuilding = building;
                selectedBuilding.OnSelect(this);
                selectedBuilding.OnConstructionComplete += OnConstructionComplete;
            }
        }

        void OnConstructionComplete(object sender, EventArgs e)
        {
            selectedBuilding.OnSelect(this);
        }
    }
}
