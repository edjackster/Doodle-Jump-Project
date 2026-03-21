using System;
using UnityEngine;

public class BreakDetector : MonoBehaviour
{
    public event Action OnBreak;
    
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.TryGetComponent(out Player _) == false)
            return;
        
        foreach (ContactPoint2D point in other.contacts)
        {
            if (point.normal.y < -0.5f)
            {
                OnBreak?.Invoke();
                return;
            }
        }
    }
}