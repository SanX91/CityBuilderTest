using System.Collections;
using UnityEngine;

namespace CityBuilderTest
{
    /// <summary>
    /// Class for production buildings.
    /// Apart from the added functionality of Production, 
    /// the remaining functionalities are the same as any other Building.
    /// </summary>
    public class ProductionBuilding : Building
    {
        private bool canProduce, isProductionPaused;
        private ProductionBuildingUI productionBuildingUI;

        /// <summary>
        /// Production is paused when the building is in the process of being moved.
        /// </summary>
        public override void Construct()
        {
            base.Construct();
            isProductionPaused = false;
        }

        protected override void InitializeUI()
        {
            buildingUI.Initialize(config.name);
            productionBuildingUI = (ProductionBuildingUI)buildingUI;
            productionBuildingUI.Initialize(StartProduction);
        }

        /// <summary>
        /// Method to be called to being production.
        /// </summary>
        private void StartProduction()
        {
            StartCoroutine(Production());
            productionBuildingUI.ToggleProductionButton(false);
            productionBuildingUI.ToggleProgressBar(true);
            canProduce = true;
        }

        /// <summary>
        /// The production once started will go on forever.
        /// However, production will be paused when the building is being moved.
        /// Generates resources after each production cycle.
        /// </summary>
        /// <returns></returns>
        private IEnumerator Production()
        {
            float productionTime = 0;
            ProductionBuildingConfig productionBuildingConfig = (ProductionBuildingConfig)config;

            while (isReady)
            {
                if (isProductionPaused)
                {
                    yield return null;
                    continue;
                }

                productionTime += Time.deltaTime;
                if (productionTime >= productionBuildingConfig.production.time)
                {
                    gameManager.ResourceManager().AdjustResources(productionBuildingConfig.production.produce);
                    productionTime = 0;
                }

                buildingUI.SetProgress(productionTime / productionBuildingConfig.production.time);
                yield return null;
            }
        }

        /// <summary>
        /// Production buildings will also display the production progress bar upon selection(Post construction).
        /// If production hasn't started it will display a start production button(Post construction).
        /// </summary>
        /// <param name="mode"></param>
        public override void OnSelect(Mode mode)
        {
            base.OnSelect(mode);
            if (!isReady)
            {
                return;
            }

            if (mode == gameManager.ModeSelection().BuildMode())
            {
                isProductionPaused = true;
                return;
            }

            if (canProduce)
            {
                buildingUI.ToggleProgressBar(true);
                return;
            }

            productionBuildingUI.ToggleProductionButton(true);
        }
    }
}

