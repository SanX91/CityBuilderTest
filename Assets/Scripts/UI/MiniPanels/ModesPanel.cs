using System;

namespace CityBuilderTest
{
    /// <summary>
    /// The modes panel class.
    /// </summary>
    public class ModesPanel : MiniPanel
    {
        public event EventHandler OnBuild;
        public event EventHandler OnRegular;

        public void OnBuildMode()
        {
            OnBuild?.Invoke(this, EventArgs.Empty);
        }

        public void OnRegularMode()
        {
            OnRegular?.Invoke(this, EventArgs.Empty);
        }
    }
}