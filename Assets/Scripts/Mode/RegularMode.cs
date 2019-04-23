using System;
using UnityEngine;

namespace CityBuilderTest
{
    /// <summary>
    /// The Regular Mode class.
    /// Has methods for selecting and deselecting a building. when regular mode is active.
    /// </summary>
    public class RegularMode : Mode
    {
        private Building selectedBuilding;

        public override void OnExit()
        {
            DeselectBuilding();
        }

        private void DeselectBuilding()
        {
            if (selectedBuilding == null)
            {
                return;
            }

            selectedBuilding.OnConstructionComplete -= OnConstructionComplete;
            selectedBuilding.OnDeselect();
            selectedBuilding = null;
        }

        /// <summary>
        /// Selects the building if the user clicks on it.
        /// Deselects any previously selected buildings.
        /// </summary>
        public override void OnUpdate()
        {
            if (!controller.HasFired())
            {
                return;
            }

            if (eventSystem.IsPointerOverGameObject())
            {
                return;
            }

            Ray ray = Camera.main.ScreenPointToRay(controller.Position());

            if (!Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, buildingMask.value))
            {
                return;
            }

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

        private void OnConstructionComplete(object sender, EventArgs e)
        {
            selectedBuilding.OnSelect(this);
        }
    }
}
