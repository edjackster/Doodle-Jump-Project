using System;
using UnityEngine;
using Zenject;

public class DesktopInput : IInput, ITickable
{
    private const string HorizontalAxisName = "Horizontal";

    public event Action<float> Moved;

    public void Tick()
    {
        Moved?.Invoke(Input.GetAxis(HorizontalAxisName));
    }
}
