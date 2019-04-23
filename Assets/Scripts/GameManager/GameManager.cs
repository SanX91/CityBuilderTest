using UnityEngine;

namespace CityBuilderTest
{
    /// <summary>
    /// Game manager.
    /// Responsible for filling up dependencies of most systems.
    /// The main MonoBehaviour class for Starting and Updating the game.
    /// </summary>
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

        public IResourceManager ResourceManager()
        {
            return resourceManager;
        }

        public IGridSystem GridSystem()
        {
            return gridSystem;
        }

        private void Start()
        {
            gridSystem.Initialize();
            modeSelection.Initialize(this, new MouseController());
            uiManager.Initialize(this);
            resourceManager.Initialize(gameConfig.startUpFunds);
        }

        private void Update()
        {
            modeSelection.OnUpdate();
        }
    }
}
