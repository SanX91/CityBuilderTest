using System.Collections;
using UnityEngine;

namespace CityBuilderTest
{
    public abstract class Building : MonoBehaviour, IInitializer<BuildingConfig>, IPlaceable
    {
        public BuildingUI buildingUI;
        protected BuildingConfig config;
        bool isReady;

        public Vector2Int StartGridId { get; set; }

        public void Initialize(BuildingConfig param)
        {
            config = param;
            buildingUI.Initialize(config.name);
        }

        public Vector2Int GridCost()
        {
            return config.gridCost;
        }

        public void Construct()
        {
            StartCoroutine(BeginConstruction());
        }

        IEnumerator BeginConstruction()
        {
            float constructionTime = 0;
            buildingUI.ToggleProgressBar(true);

            while(!isReady)
            {
                constructionTime += Time.deltaTime;
                if(constructionTime>=config.constructionTime)
                {
                    isReady = true;
                }

                buildingUI.SetProgress(constructionTime/config.constructionTime);
                yield return null;
            }

            buildingUI.ToggleProgressBar(false);
        }
    }
}


