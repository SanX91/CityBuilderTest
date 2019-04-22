using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace CityBuilderTest
{
    public class BuildingUI : MonoBehaviour, IInitializer<string>
    {
        [SerializeField]
        Text nameText;
        [SerializeField]
        ProgressBar progressBar;

        public void Initialize(string param)
        {
            nameText.text = param;
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

