using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Zenject;

public class PoolManager : MonoBehaviour
{
    [SerializeField]
    PoolObject[] objectSamples;

    [SerializeField]
    int increment;

    List<Pool> pools;
    private void Awake()
    {
        Initialize();
    }
    public void Initialize()
    {
        pools = new List<Pool>();

        foreach (PoolObject objectSample in objectSamples)
        {
            Pool tempPool = new Pool(objectSample, objectSample.Name, transform, increment);
            pools.Add(tempPool);
        }
    }
    public PoolObject GetPoolObject(string poolName, Transform transform) => pools.First(pool => pool.Name == poolName).GetObject(transform); 
    public PoolObject GetPoolObject(int index, Transform transform) => pools[index].GetObject(transform); 
    public void Dismiss(PoolObject obj) => pools.First(pool => pool.Name == obj.Name).Dismiss(obj); 
}
