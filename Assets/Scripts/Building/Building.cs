using System;
using System.Collections;
using UnityEngine;

namespace CityBuilderTest
{
    /// <summary>
    /// The base building class.
    /// Contains information and methods related to buildings or placeable objects.
    /// Responsible for building construction.
    /// Initializes the UI on the building.
    /// </summary>
    public abstract class Building : MonoBehaviour, IInitializer<BuildingConfig, IGameManager>, IPlaceable, ISelectable
    {
        public event EventHandler OnConstructionComplete;
        public BuildingUI buildingUI;
        protected BuildingConfig config;
        protected IGameManager gameManager;
        protected bool isReady;

        public Vector2Int StartGridId { get; set; }

        public void Initialize(BuildingConfig config, IGameManager gameManager)
        {
            this.config = config;
            this.gameManager = gameManager;
            InitializeUI();
        }

        protected virtual void InitializeUI()
        {
            buildingUI.Initialize(config.name);
        }

        public Vector2Int GridCost()
        {
            return config.gridCost;
        }

        /// <summary>
        /// Method to be called to being construction.
        /// Also called when the building has been moved and placed again.
        /// Does not cost any resource to move the building.
        /// </summary>
        public virtual void Construct()
        {
            if (isReady)
            {
                return;
            }

            StartCoroutine(BeginConstruction());
            gameManager.ResourceManager().AdjustResources(config.itemCost, true);
        }

        /// <summary>
        /// Begins the construction.
        /// The progress bar will be visible during this process, 
        /// even if the building is not selected in the regular mode.
        /// The lengthy construction process takes place just once, doesn't need to happen again for moving.
        /// Cannot move the building during construction.
        /// </summary>
        /// <returns></returns>
        private IEnumerator BeginConstruction()
        {
            float constructionTime = 0;
            buildingUI.ToggleProgressBar(true);

            while (!isReady)
            {
                constructionTime += Time.deltaTime;
                if (constructionTime >= config.constructionTime)
                {
                    isReady = true;
                }

                buildingUI.SetProgress(constructionTime / config.constructionTime);
                yield return null;
            }

            OnConstructionComplete?.Invoke(this, EventArgs.Empty);
            buildingUI.ToggleProgressBar(false);
        }
        
        /// <summary>
        /// In Regular mode, displays the building name upon selection.
        /// In Build mode, prepares the building to be moved(post construction).
        /// </summary>
        /// <param name="mode"></param>
        public virtual void OnSelect(Mode mode)
        {
            if (mode == gameManager.ModeSelection().BuildMode())
            {
                if (!isReady)
                {
                    return;
                }

                gameManager.ModeSelection().BuildMode().MoveBuilding(this);
                return;
            }

            buildingUI.ToggleNameText(true);
        }

        /// <summary>
        /// Switches off any UI displayed by the building.
        /// If construction is in process, the progress bar would still be displayed.
        /// </summary>
        public virtual void OnDeselect()
        {
            if (isReady)
            {
                buildingUI.ToggleUI(false);
                return;
            }

            buildingUI.ToggleNameText(false);
        }
    }
}


