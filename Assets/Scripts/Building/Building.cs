using System;
using System.Collections;
using UnityEngine;

namespace CityBuilderTest
{
    public abstract class Building : MonoBehaviour, IInitializer<BuildingConfig, ResourceManager, IModeSelection>, IPlaceable
    {
        public event EventHandler OnConstructionComplete;
        public Collider collider;
        public BuildingUI buildingUI;
        protected BuildingConfig config;
        protected ResourceManager resourceManager;
        protected IModeSelection modeSelection;
        protected bool isReady;

        public Vector2Int StartGridId { get; set; }

        public void Initialize(BuildingConfig param1, ResourceManager param2, IModeSelection param3)
        {
            config = param1;
            resourceManager = param2;
            modeSelection = param3;
            StartCoroutine(ToggleCollider(false));
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

        public virtual void Construct()
        {
            StartCoroutine(ToggleCollider(true));

            if (isReady)
            {
                return;
            }

            StartCoroutine(BeginConstruction());
            resourceManager.AdjustResources(config.itemCost, true);
        }

        IEnumerator ToggleCollider(bool isActive)
        {
            if(isActive)
            {
                yield return new WaitForEndOfFrame();
            }
           
            collider.enabled = isActive;
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

            OnConstructionComplete?.Invoke(this,EventArgs.Empty);
            buildingUI.ToggleProgressBar(false);
        }

        public virtual void OnSelect(Mode mode)
        {
            if(mode == modeSelection.BuildMode())
            {
                if(!isReady)
                {
                    return;
                }

                StartCoroutine(ToggleCollider(false));
                modeSelection.BuildMode().MoveBuilding(this);
                return;
            }

            buildingUI.ToggleNameText(true);
        }

        public virtual void OnDeselect()
        {
            if(isReady)
            {
                buildingUI.ToggleUI(false);
                return;
            }

            buildingUI.ToggleNameText(false);
        }
    }
}


