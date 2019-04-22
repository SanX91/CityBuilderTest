using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CityBuilderTest
{
    public class BuildMode : Mode, IInitializer<GridSystem, ResourceManager, IModeSelection>
    {
        public LayerMask gridMask;
        GridSystem gridSystem;
        ResourceManager resourceManager;
        IModeSelection modeSelection;
        List<Building> buildings;
        Building building;

        public void CreateBuilding(BuildingConfig buildingConfig)
        {
            if(building!=null)
            {
                return;
            }

            building = Instantiate(buildingConfig.prefab).GetComponent<Building>();
            building.Initialize(buildingConfig,resourceManager,modeSelection);

            modeSelection.IsBusy = true;
        }

        public void Initialize(GridSystem param1, ResourceManager param2, IModeSelection param3)
        {
            gridSystem = param1;
            resourceManager = param2;
            modeSelection = param3;

            buildings = new List<Building>();
        }

        public void MoveBuilding(Building building)
        {
            gridSystem.TogglePlaceObject(building, false);
            this.building = building;

            modeSelection.IsBusy = true;
        }

        public override void OnExit()
        {
            
        }

        void SelectBuilding()
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
                hit.collider.gameObject.GetComponent<Building>().OnSelect(this);
            }
        }

        void PlaceBuilding()
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (!Physics.Raycast(ray, out hit, Mathf.Infinity, gridMask.value))
            {
                return;
            }

            Vector2 position;
            bool canPlaceBuilding = gridSystem.CanPlaceObject(building, new Vector2(hit.point.x, hit.point.z), out position);
            building.transform.position = new Vector3(position.x, 0, position.y);

            if (eventSystem.IsPointerOverGameObject())
            {
                return;
            }

            if (!canPlaceBuilding)
            {
                return;
            }

            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                building.Construct();
                gridSystem.TogglePlaceObject(building, true);
                buildings.Add(building);
                modeSelection.IsBusy = false;
                building = null;
            }
        }

        public override void OnUpdate()
        {
            if (building == null)
            {
                SelectBuilding();
                return;
            }

            PlaceBuilding();
        }
    }
}
