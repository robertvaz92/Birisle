using System.Collections.Generic;
using UnityEngine;

public class ObjectPool<T> where T : Component
{
    private T prefab;
    private List<T> activeObjects;
    private List<T> inactiveObjects;
    private Transform parent;
    private string nameSuffix;
    private int counter;
    public ObjectPool(T prefab, int initialSize, Transform _parent, string _nameSuffix)
    {
        this.prefab = prefab;
        parent = _parent;
        nameSuffix = _nameSuffix;
        counter = 0;

        activeObjects = new List<T>(initialSize);
        inactiveObjects = new List<T>(initialSize);

        for (int i = 0; i < initialSize; i++)
        {
            T obj = GameObject.Instantiate(prefab, parent);
            UpdateName(obj);
            obj.gameObject.SetActive(false);
            inactiveObjects.Add(obj);
        }
    }

    // Get an object from the pool
    public T Get()
    {
        T obj;
        if (inactiveObjects.Count > 0)
        {
            obj = inactiveObjects[inactiveObjects.Count - 1];
            inactiveObjects.RemoveAt(inactiveObjects.Count - 1);
        }
        else
        {
            obj = GameObject.Instantiate(prefab, parent);
            UpdateName(obj);
        }

        obj.gameObject.SetActive(true);
        activeObjects.Add(obj);
        return obj;
    }

    // Return an object to the pool
    public void ReturnToPool(T obj)
    {
        if (activeObjects.Remove(obj))
        {
            obj.gameObject.SetActive(false);
            inactiveObjects.Add(obj);
        }
        else
        {
            Debug.LogWarning("Trying to return object not in active list.");
        }
    }

    // Optional: Clear all objects
    public void Clear()
    {
        foreach (var obj in activeObjects)
            GameObject.Destroy(obj.gameObject);

        foreach (var obj in inactiveObjects)
            GameObject.Destroy(obj.gameObject);

        activeObjects.Clear();
        inactiveObjects.Clear();
        counter = 0;
    }

    private void UpdateName(T obj)
    {
        counter++;
        obj.gameObject.name = $"{nameSuffix}_{counter}";
    }
}
