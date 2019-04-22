using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CityBuilderTest
{
    public class GameManager : MonoBehaviour, IGameManager
    {
        public GridSystem gridSystem;
        public BuildMode buildMode;
        public UIManager uiManager;
        public ResourceManager resourceManager;
        public GameConfig gameConfig;

        public IGameConfig GameConfig()
        {
            return gameConfig;
        }

        public IMode BuildMode()
        {
            return buildMode;
        }

        private void Start()
        {
            gridSystem.Initialize();
            //resourceManager.Initialize();
            buildMode.Initialize(gridSystem);
            uiManager.Initialize(this);
        }

        void Update()
        {
            buildMode.OnUpdate();
        }
    }
}
