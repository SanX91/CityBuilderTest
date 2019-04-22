using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CityBuilderTest
{
    public class ProductionBuilding : Building
    {
        bool canProduce, isProductionPaused;
        ProductionBuildingUI productionBuildingUI;

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

        void StartProduction()
        {
            StartCoroutine(Production());
            productionBuildingUI.ToggleProductionButton(false);
            productionBuildingUI.ToggleProgressBar(true);
            canProduce = true;
        }

        IEnumerator Production()
        {
            float productionTime = 0;
            ProductionBuildingConfig productionBuildingConfig = (ProductionBuildingConfig)config;

            while (isReady)
            {
                if(isProductionPaused)
                {
                    yield return null;
                    continue;
                }

                productionTime += Time.deltaTime;
                if (productionTime >= productionBuildingConfig.production.time)
                {
                    resourceManager.AdjustResources(productionBuildingConfig.production.produce);
                    productionTime = 0;
                }

                buildingUI.SetProgress(productionTime / productionBuildingConfig.production.time);
                yield return null;
            }
        }

        public override void OnSelect(Mode mode)
        {
            base.OnSelect(mode);
            if(!isReady)
            {
                return;
            }

            if (mode == modeSelection.BuildMode())
            {
                isProductionPaused = true;
                return;
            }

            if(canProduce)
            {
                buildingUI.ToggleProgressBar(true);
                return;
            }

            productionBuildingUI.ToggleProductionButton(true);
        }
    }
}

