using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CityBuilderTest
{
    public class ResourceManager : MonoBehaviour, IInitializer<Currency>
    {
        Currency resourceData;

        public void AdjustResource(ResourceTypes type, int amount)
        {
            switch(type)
            {
                case ResourceTypes.Wood:
                    resourceData.wood += amount;
                    break;
                case ResourceTypes.Steel:
                    resourceData.steel += amount;
                    break;
                default:
                    resourceData.gold += amount;
                    break;
            }
        }

        public void Initialize(Currency param)
        {
            resourceData = new Currency(param);
        }
    }
}

