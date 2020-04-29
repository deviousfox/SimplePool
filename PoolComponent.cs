using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
///   A simple pool component, you can use it separately from PoolController
/// </summary>
public class PoolComponent : MonoBehaviour
{
    public bool InitializeOnStart;
    public string Key;
    public Queue<GameObject> PooledObjects;
    public GameObject ObjectTemplate;
    public int ObjectsAmount;

    private void Start()
    {
        if (InitializeOnStart)
        {
            InitializePool();
        }
    }
    /// <summary>
    ///   Initialize pool component
    /// </summary>
    public void InitializePool()
    {
        PooledObjects = new Queue<GameObject>();
        for (int i = 0; i < ObjectsAmount; i++)
        {
            GameObject tempObject = Instantiate(ObjectTemplate);
            tempObject.SetActive(false);
            PooledObjects.Enqueue(tempObject);
        }
    }

    public GameObject InstantiateFromPool(Vector3 position, Quaternion rotation)
    {
        GameObject spawnObject = PooledObjects.Dequeue();
        spawnObject.transform.position = position;
        spawnObject.transform.rotation = rotation;
        spawnObject.SetActive(true);

        return spawnObject;
    }
    public void ReturnToPool(GameObject returnedObject)
    {
        returnedObject.SetActive(false);
        PooledObjects.Enqueue(returnedObject);
    }


    public PoolComponent() { }
    public PoolComponent(GameObject objectTemplate, int objectsAmount)
    {
        ObjectTemplate = objectTemplate;
        ObjectsAmount = objectsAmount;
        InitializePool();
    }
}
