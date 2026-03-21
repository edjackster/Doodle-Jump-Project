using System;
using UnityEngine;
using Zenject;

public class MobileInput : IInput, ITickable
{
    private const float MaxTiltToMaxInput  = 0.5f;
    private const float DeadZone = 0.05f;

    public event Action<float> Moved;
    
    public void Tick()
    {
        float raw = Input.acceleration.x; 

        if (Mathf.Abs(raw) < DeadZone)
        {
            Moved?.Invoke(0f);
            return;
        }

        float normalized = Mathf.Clamp(raw / MaxTiltToMaxInput, -1f, 1f);

        Moved?.Invoke(normalized);
    }
}
