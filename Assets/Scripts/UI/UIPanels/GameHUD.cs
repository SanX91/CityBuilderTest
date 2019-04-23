using System;
using UnityEngine;

namespace CityBuilderTest
{
    /// <summary>
    /// The Game HUD class.
    /// Responsible for displaying the resources mini panel and the modes mini panel.
    /// Also displays the buildings mini panel when Build Mode is active.
    /// </summary>
    public class GameHUD : UIPanel, IInitializer<IGameManager>
    {
        private IModeSelection modeSelection;

        private void OnEnable()
        {
            GetMiniPanel<ModesPanel>().OnBuild += OnBuild;
            GetMiniPanel<ModesPanel>().OnRegular += OnRegular;
        }

        private void OnDisable()
        {
            GetMiniPanel<ModesPanel>().OnBuild -= OnBuild;
            GetMiniPanel<ModesPanel>().OnRegular -= OnRegular;
        }

        public override void Open()
        {
            base.Open();
            GetMiniPanel<ResourcesPanel>().Open();
            GetMiniPanel<ModesPanel>().Open();
            GetMiniPanel<BuildingsPanel>().Close();
        }

        private void OnBuild(object sender, EventArgs e)
        {
            if (modeSelection.IsBusy)
            {
                Debug.Log("Cannot change mode now!");
                return;
            }

            GetMiniPanel<BuildingsPanel>().Open();
            modeSelection.SwitchMode(modeSelection.BuildMode());
        }

        private void OnRegular(object sender, EventArgs e)
        {
            if (modeSelection.IsBusy)
            {
                Debug.Log("Cannot change mode now!");
                return;
            }

            GetMiniPanel<BuildingsPanel>().Close();
            modeSelection.SwitchMode(modeSelection.RegularMode());
        }

        public void Initialize(IGameManager gameManager)
        {
            modeSelection = gameManager.ModeSelection();
            GetMiniPanel<ResourcesPanel>().Initialize(gameManager.ResourceManager());
            GetMiniPanel<BuildingsPanel>().Initialize(gameManager);
        }
    }
}