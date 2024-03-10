using System.Collections.Generic;
using UnityEngine;

namespace ZHMQ.ObjectPooling
{
    public class ObjectPool : MonoBehaviour
    {
        private static List<Pool> Pools = new();

        #region Spawn

        public static T Spawn<T>(T prefab, Vector3 position = default, Quaternion rotation = default) where T : Component
        {
            Pool pool = Pools.Find(p => p.Key == prefab.name);

            if (pool == null)
            {
                pool = new Pool() { Key = prefab.name };
                Pools.Add(pool);
            }

            T spawnableObj = pool.GetInactiveObject<T>();
            
            if(spawnableObj == null)
            {
                spawnableObj = Instantiate(prefab, position, rotation);
            }
            else
            {
                spawnableObj.transform.SetPositionAndRotation(position, rotation);
                pool.InactiveObjects.Remove(spawnableObj);
                spawnableObj.gameObject.SetActive(true);
            }

            return spawnableObj;

        }

        public static GameObject Spawn(GameObject prefab, Vector3 position = default, Quaternion rotation = default)
        {
            Transform obj = Spawn(prefab.transform, position, rotation);
            return obj.gameObject;
        }

        #endregion

        #region Return

        public static void ReturnToPool<T>(T obj) where T : Component
        {
            string goName = obj.gameObject.name.Substring(0, obj.name.Length - 7);
            Pool pool = Pools.Find(p => p.Key == goName);

            if(pool == null)
            {
                Debug.LogError($"Trying to release an object that is not pooled: {obj.name}");
            }
            else
            {
                obj.gameObject.SetActive(false);
                pool.InactiveObjects.Add(obj);
            }
        }

        public static void ReturnToPool(GameObject obj)
        {
            ReturnToPool(obj.transform);
        }

        #endregion
    }
}