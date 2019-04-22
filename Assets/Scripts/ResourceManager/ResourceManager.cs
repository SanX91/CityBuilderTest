using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CityBuilderTest
{
    public class ResourceManager : MonoBehaviour, IInitializer<Currency>
    {
        public event EventHandler<ResourceUpdateEventArgs> OnResourceUpdate;
        Currency resourceData;

        void OnResourceUpdated()
        {
            OnResourceUpdate?.Invoke(this, new ResourceUpdateEventArgs(resourceData));
        }

        public bool HaveSufficientResources(Currency target)
        {
            return target != null
                && resourceData != null
                && resourceData.gold >= target.gold
                && resourceData.wood >= target.wood
                && resourceData.steel >= target.steel;
        }

        public void AdjustResources(Currency currency, bool isExpense = false)
        {
            if(!isExpense)
            {
                resourceData.Add(currency);
            }
            else
            {
                resourceData.Remove(currency);
            }

            OnResourceUpdated();
        }

        public void Initialize(Currency param)
        {
            resourceData = new Currency(param);
            OnResourceUpdated();
        }
    }

    public class ResourceUpdateEventArgs : EventArgs
    {
        public Currency ResourceData { get; private set; }

        public ResourceUpdateEventArgs(Currency resourceData)
        {
            ResourceData = new Currency(resourceData);
        }
    }
}

