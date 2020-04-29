using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PoolController : MonoBehaviour
{
    public bool InitializeOnAwake;
    public bool FindAllPools;
    Dictionary<string,PoolComponent> poolDictionary;

    public static PoolController Instance;

    private void Awake()
    {
        Instance = this; 
		if (InitializeOnAwake)
        {
            Initialize();
        }
    }


    public void Initialize()
    {
        poolDictionary = new Dictionary<string, PoolComponent>();
        if (FindAllPools)
        {
            PoolComponent[] tempComponents = FindObjectsOfType<PoolComponent>();
            foreach (PoolComponent component in tempComponents)
            {
                poolDictionary.Add(component.Key, component);
            }
        }
    }

    public void InitializeAllPools()
    {
        List<PoolComponent> tempComponnents = new List<PoolComponent>();
        tempComponnents = poolDictionary.Values.ToList();
        tempComponnents.Union(FindObjectsOfType<PoolComponent>());
        foreach (PoolComponent component in tempComponnents)
        {
            component.InitializePool();
        }
    }

    public void InitializePools()
    {
        List<PoolComponent> tempComponnents = new List<PoolComponent>();
        tempComponnents = poolDictionary.Values.ToList();

        foreach (PoolComponent component in tempComponnents)
        {
            component.InitializePool();
        }
    }

    public GameObject InstantiateFromPool(string key, Vector3 position, Quaternion rotation)
    {
        return poolDictionary[key].InstantiateFromPool(position, rotation);
    }

    public void ReturnToPool(string key, GameObject returnedObject)
    {
        poolDictionary[key].ReturnToPool(returnedObject);
    }

    public PoolComponent GetPool(string key)
    {
        if (poolDictionary.ContainsKey(key))
        {
            return poolDictionary[key];
        }
        else
        {
            Debug.LogError("Key " + key + " does not exist.");
            return null;
        }
    }

    public PoolComponent CreatePool(string key, GameObject objectTemplate, int objectsAmount)
    {
        if (!poolDictionary.ContainsKey(key))
        {
            PoolComponent tempComponent = new PoolComponent(objectTemplate, objectsAmount);
            poolDictionary.Add(key, tempComponent);
        }
        return GetPool(key);
    }


}
