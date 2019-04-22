using System;
using UnityEngine;

namespace CityBuilderTest
{
    public class GameHUD : UIPanel, IInitializer<IGameManager>
    {
        IModeSelection modeSelection;

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

        void OnBuild(object sender, EventArgs e)
        {
            if (modeSelection.IsBusy)
            {
                Debug.Log("Cannot change mode now!");
                return;
            }

            GetMiniPanel<BuildingsPanel>().Open();
            modeSelection.SwitchMode(modeSelection.BuildMode());
        }

        void OnRegular(object sender, EventArgs e)
        {
            if(modeSelection.IsBusy)
            {
                Debug.Log("Cannot change mode now!");
                return;
            }

            GetMiniPanel<BuildingsPanel>().Close();
            modeSelection.SwitchMode(modeSelection.RegularMode());
        }

        public void Initialize(IGameManager param)
        {
            modeSelection = param.ModeSelection();
            GetMiniPanel<ResourcesPanel>().Initialize(param.GetResourceManager());
            GetMiniPanel<BuildingsPanel>().Initialize(param);
        }
    }
}