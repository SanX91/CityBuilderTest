using System;
using UnityEngine;

namespace CityBuilderTest
{
    public class GameHUD : UIPanel, IInitializer<IGameManager>
    {
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
            GetMiniPanel<BuildingsPanel>().Open();
        }

        void OnRegular(object sender, EventArgs e)
        {
            GetMiniPanel<BuildingsPanel>().Close();
        }

        public void Initialize(IGameManager param)
        {
            GetMiniPanel<BuildingsPanel>().Initialize(param);
        }
    }
}