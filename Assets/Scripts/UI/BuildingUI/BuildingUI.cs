using UnityEngine;
using UnityEngine.UI;

namespace CityBuilderTest
{
    /// <summary>
    /// The building UI class.
    /// Has all the UI elements which can show up on a building.
    /// </summary>
    public class BuildingUI : MonoBehaviour, IInitializer<string>
    {
        [SerializeField]
        private Text nameText;
        [SerializeField]
        private ProgressBar progressBar;

        public void Initialize(string name)
        {
            nameText.text = name;
            ToggleUI(false);
        }

        public void SetProgress(float progress)
        {
            progressBar.fillBar.fillAmount = progress;
        }

        public void ToggleProgressBar(bool isActive)
        {
            progressBar.gameObject.SetActive(isActive);
        }

        public void ToggleNameText(bool isActive)
        {
            nameText.gameObject.SetActive(isActive);
        }

        public virtual void ToggleUI(bool isActive)
        {
            ToggleNameText(isActive);
            ToggleProgressBar(isActive);
        }
    }
}

