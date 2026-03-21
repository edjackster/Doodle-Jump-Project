using System;

public interface IInput
{
    public event Action<float> Moved;
}