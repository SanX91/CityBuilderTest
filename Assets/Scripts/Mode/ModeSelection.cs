using UnityEngine;

namespace CityBuilderTest
{
    /// <summary>
    /// The mode selection class.
    /// Responsible for choosing between the different modes.
    /// Also initializes and updates the different modes.
    /// </summary>
    public class ModeSelection : MonoBehaviour, IModeSelection, IInitializer<IGameManager, IController>, IUpdateable
    {
        [SerializeField]
        private BuildMode buildMode;
        [SerializeField]
        private RegularMode regularMode;
        private Mode currentMode;

        //If IsBusy=true, cannot change modes.
        public bool IsBusy { get; set; }

        public BuildMode BuildMode()
        {
            return buildMode;
        }

        public RegularMode RegularMode()
        {
            return regularMode;
        }

        public void SwitchMode(Mode mode)
        {
            if (currentMode != mode)
            {
                currentMode.OnExit();
            }

            currentMode = mode;
        }

        public void Initialize(IGameManager gameManager, IController controller)
        {
            regularMode.Initialize(controller);
            buildMode.Initialize(gameManager, controller);
            currentMode = regularMode;
        }

        public void OnUpdate()
        {
            if (currentMode == null)
            {
                return;
            }

            currentMode.OnUpdate();
        }
    }
}

