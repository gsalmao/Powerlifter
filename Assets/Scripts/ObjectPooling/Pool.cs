using System.Collections.Generic;
using UnityEngine;

namespace ZHMQ.ObjectPooling
{
    internal class Pool
    {
        public string Key { get; set; }

        public List<Component> InactiveObjects = new();

        public T GetInactiveObject<T>() where T : Component
        {
            foreach(T obj in InactiveObjects)
            {
                if (obj == null)
                {
                    InactiveObjects.Remove(obj);
                    continue;
                }

                if (!obj.gameObject.activeSelf)
                    return obj;
            }

            return null;
        }
    }
}
