using System;
using UnityEngine;

namespace CityBuilderTest
{
    /// <summary>
    /// The resource manager class.
    /// Maintains the available resources(Currency).
    /// Has a method to adjust the resources.
    /// Has a method to check if there are sufficient resources for a purchase.
    /// </summary>
    public class ResourceManager : MonoBehaviour, IInitializer<Currency>, IResourceManager
    {
        public event EventHandler<ResourceUpdateEventArgs> OnResourceUpdate;
        private Currency resourceData;

        private void OnResourceUpdated()
        {
            OnResourceUpdate?.Invoke(this, new ResourceUpdateEventArgs(resourceData));
        }

        /// <summary>
        /// Determines if the available resources are sufficient for a potential purchase.
        /// </summary>
        /// <param name="target"></param>
        /// <returns></returns>
        public bool HaveSufficientResources(Currency target)
        {
            return target != null
                && resourceData != null
                && resourceData.gold >= target.gold
                && resourceData.wood >= target.wood
                && resourceData.steel >= target.steel;
        }

        /// <summary>
        /// Can add further resources.
        /// Can remove resources.
        /// </summary>
        /// <param name="currency"></param>
        /// <param name="isExpense"></param>
        public void AdjustResources(Currency currency, bool isExpense = false)
        {
            if (!isExpense)
            {
                resourceData.Add(currency);
            }
            else
            {
                resourceData.Remove(currency);
            }

            OnResourceUpdated();
        }

        public void Initialize(Currency funds)
        {
            resourceData = new Currency(funds);
            OnResourceUpdated();
        }
    }

    /// <summary>
    /// Event arguments for a resource update event.
    /// </summary>
    public class ResourceUpdateEventArgs : EventArgs
    {
        public Currency ResourceData { get; private set; }

        public ResourceUpdateEventArgs(Currency resourceData)
        {
            ResourceData = new Currency(resourceData);
        }
    }
}

