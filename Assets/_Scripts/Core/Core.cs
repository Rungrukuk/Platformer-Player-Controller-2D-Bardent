using System.Collections.Generic;
using System.Linq;
using _Scripts.Core.CoreComponents;
using UnityEngine;

namespace _Scripts.Core
{
    public class Core : MonoBehaviour
    {

        private readonly List<CoreComponent> coreComponents = new List<CoreComponent>();
        public void LogicUpdate()
        {
            foreach (CoreComponent component in coreComponents)
            {
                component.LogicUpdate();
            }
        }
        public void AddComponent(CoreComponent component)
        {
            if (!coreComponents.Contains(component))
            {
                coreComponents.Add(component);
            }
        }

        private T GetCoreComponent<T>() where T : CoreComponent
        {
            var comp =  coreComponents.OfType<T>().FirstOrDefault();
            if (comp)
                return comp;
            comp = GetComponentInChildren<T>();
            if (comp)
                return comp;
            Debug.LogWarning($"{typeof(T)} Not Found On {transform.parent.name}");
            return null;
        }

        public T GetCoreComponent<T>(ref T value) where T : CoreComponent
        {
            value = GetCoreComponent<T>();
            return value;
        }

    }
}
