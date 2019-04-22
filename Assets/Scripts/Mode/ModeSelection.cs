using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CityBuilderTest
{
    public class ModeSelection : MonoBehaviour, IModeSelection, IInitializer<GridSystem, ResourceManager>, IUpdateable
    {
        [SerializeField]
        BuildMode buildMode;
        [SerializeField]
        RegularMode regularMode;

        Mode currentMode;
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
            if(currentMode != mode)
            {
                currentMode.OnExit();
            }

            currentMode = mode;
        }

        public void Initialize(GridSystem param1, ResourceManager param2)
        {
            buildMode.Initialize(param1, param2, this);
            currentMode = regularMode;
        }

        public void OnUpdate()
        {
            if(currentMode == null)
            {
                return;
            }

            currentMode.OnUpdate();
        }
    }
}

