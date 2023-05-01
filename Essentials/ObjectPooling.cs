using System.Collections.Generic;
using UnityEngine;

public class ObjectPooling : MonoBehaviour
{
    public static ObjectPooling instance;
    [SerializeField] List<Pooling> poolings;

    void Awake() 
    {
        instance = this;
        foreach (Pooling pool in poolings)
        {
            Create(pool);
        }
    }

    void Create(Pooling pool) 
    {
        pool.queue = new Queue<GameObject>();
        Transform parent = new GameObject(pool.id).transform;
        parent.parent = transform;

        for (int i = 0; i < pool.maxObjectCount; i++)
        {
            GameObject obj = Instantiate(pool._object, transform.position, Quaternion.identity, parent);
            pool.queue.Enqueue(obj);
            obj.SetActive(false);
        }
    }

    public Pooling GetPool(string id)
    {
        Pooling pool = null;
        foreach (var item in poolings)
        {
            if (item.id == id)
            {
                pool = item;
                break;
            }
        }

        return pool;
    }
}

[System.Serializable]
public class Pooling 
{
    public string id;
    public GameObject _object;
    [Min(1)] public int maxObjectCount = 1;

    public Queue<GameObject> queue;

    public GameObject getItem
    {
        get
        {
            GameObject obj = queue.Dequeue();
            queue.Enqueue(obj);

            return obj;
        }
    }
}
