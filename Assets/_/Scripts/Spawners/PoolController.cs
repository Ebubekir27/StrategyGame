using System.Collections;
using System.Collections.Generic;
using TowerGame;
using UnityEngine;
using UnityEngine.Pool;


 
public class PoolController :MonoBehaviour
{
    public static PoolController Instance;
   
    private Dictionary<GameObject, List<GameObject>> _pools;
 
    private void Awake()
    {
        Instance = this;
        _pools = new Dictionary<GameObject, List<GameObject>>();
    }
    public GameObject PullFromPool(GameObject prefab)
    {
        if (_pools.ContainsKey(prefab) == false) _pools.Add(prefab, new List<GameObject>());
        if (_pools[prefab].Count <= 0)
        {
            var newInstance = Instantiate(prefab, transform);
           
            return newInstance;
        }
       

        var pool = _pools[prefab];
       
        var instance = pool[pool.Count - 1];
         pool.RemoveAt(pool.Count - 1);
        

        return instance;
    }

    public void ReturnToPool(GameObject prefab, GameObject instance)
    {
        if (_pools.ContainsKey(prefab) == false) _pools.Add(prefab, new List<GameObject>());

        instance.SetActive(false);
        instance.transform.SetParent(transform);

        var pool = _pools[prefab];
        pool.Add(instance);
    }
}