using UnityEngine;

namespace CityBuilderTest
{
    /// <summary>
    /// The Build Mode class.
    /// Has methods to be used when the build mode is active.
    /// Has methods to create, move, select and place buildings.
    /// </summary>
    public class BuildMode : Mode, IInitializer<IGameManager, IController>
    {
        public LayerMask gridMask;
        private IGameManager gameManager;
        private Building building;

        /// <summary>
        /// Creates a new building with the buildingConfig data.
        /// </summary>
        /// <param name="buildingConfig"></param>
        public void CreateBuilding(BuildingConfig buildingConfig)
        {
            if (building != null)
            {
                return;
            }

            building = Instantiate(buildingConfig.prefab).GetComponent<Building>();
            building.Initialize(buildingConfig, gameManager);

            gameManager.ModeSelection().IsBusy = true;
        }

        public void Initialize(IGameManager gameManager, IController controller)
        {
            this.gameManager = gameManager;
            this.controller = controller;
        }

        /// <summary>
        /// Moves a building.
        /// Unregisters the building from the grid system.
        /// </summary>
        /// <param name="building"></param>
        public void MoveBuilding(Building building)
        {
            gameManager.GridSystem().TogglePlaceObject(building, false);
            this.building = building;

            gameManager.ModeSelection().IsBusy = true;
        }

        public override void OnExit()
        {

        }

        /// <summary>
        /// Selects the building if the user clicks on it.
        /// </summary>
        private void SelectBuilding()
        {
            if (!controller.HasFired())
            {
                return;
            }

            if (eventSystem.IsPointerOverGameObject())
            {
                return;
            }

            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (!Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, buildingMask.value))
            {
                return;
            }

            hit.collider.gameObject.GetComponent<Building>().OnSelect(this);
        }

        /// <summary>
        /// Checks if the user can place the building on the grid.
        /// If it's possible to place the building, and the user clicks, the building is placed on the grid.
        /// </summary>
        private void PlaceBuilding()
        {
            Ray ray = Camera.main.ScreenPointToRay(controller.Position());

            if (!Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, gridMask.value))
            {
                return;
            }

            bool canPlaceBuilding = gameManager.GridSystem().CanPlaceObject(building, new Vector2(hit.point.x, hit.point.z), out Vector2 position);
            building.transform.position = new Vector3(position.x, 0, position.y);

            if (eventSystem.IsPointerOverGameObject())
            {
                return;
            }

            if (!canPlaceBuilding)
            {
                return;
            }

            if (controller.HasFired())
            {
                building.Construct();
                gameManager.GridSystem().TogglePlaceObject(building, true);
                gameManager.ModeSelection().IsBusy = false;
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
