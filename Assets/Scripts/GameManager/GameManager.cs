using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CityBuilderTest
{
    public class GameManager : MonoBehaviour, IGameManager
    {
        public GridSystem gridSystem;
        public ModeSelection modeSelection;
        public UIManager uiManager;
        public ResourceManager resourceManager;
        public GameConfig gameConfig;

        public IGameConfig GameConfig()
        {
            return gameConfig;
        }

        public IModeSelection ModeSelection()
        {
            return modeSelection;
        }

        public ResourceManager GetResourceManager()
        {
            return resourceManager;
        }

        private void Start()
        {
            gridSystem.Initialize();
            modeSelection.Initialize(gridSystem,resourceManager);
            uiManager.Initialize(this);
            resourceManager.Initialize(gameConfig.startUpFunds);
        }

        void Update()
        {
            modeSelection.OnUpdate();
        }
    }
}
