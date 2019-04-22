using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CityBuilderTest
{
    public class BuildMode : Mode, IUpdateable, IMode, IInitializer<GridSystem>
    {
        public LayerMask gridMask;
        GridSystem gridSystem;
        Building building;

        public void CreateBuilding(BuildingConfig buildingConfig)
        {
            if(building!=null)
            {
                Destroy(building.gameObject);
                building = null;
            }

            building = Instantiate(buildingConfig.prefab).GetComponent<Building>();
            building.Initialize(buildingConfig);
        }

        public void Initialize(GridSystem param)
        {
            gridSystem = param;
        }

        public void OnClick(BuildingConfig buildingConfig)
        {
            CreateBuilding(buildingConfig);
        }

        public void OnUpdate()
        {
            if(building == null)
            {
                return;
            }

            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if(!Physics.Raycast(ray,out hit,Mathf.Infinity,gridMask.value))
            {
                return;
            }

            Vector2 position;
            bool canPlaceBuilding = gridSystem.CanPlaceObject(building, new Vector2(hit.point.x,hit.point.z), out position);
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
                gridSystem.PlaceObject(building);
                building = null;
            }   
        }
    }
}
