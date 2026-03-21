using System.Collections.Generic;
using UnityEngine;

public class TrapPool: MonoBehaviour
{
    [SerializeField] private Hole _prefab;
    
    private Queue<Hole> _pool = new();

    public Hole Get()
    {
        Hole hole;
        
        if (_pool.Count > 0)
        {
            hole = _pool.Dequeue();
            hole.gameObject.SetActive(true);
            return hole;
        }
        else
        {
            hole = Instantiate(_prefab);
            hole.gameObject.SetActive(true);
            return hole;
        }
    }

    public void Return(Hole hole)
    {
        hole.gameObject.SetActive(false);
        _pool.Enqueue(hole);
    }
}