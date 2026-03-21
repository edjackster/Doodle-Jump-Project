using System;
using UnityEngine;
using Zenject;

public class PlayerFlipper : MonoBehaviour
{
    private IInput _input;
    private int _direction = 1;

    [Inject]
    private void Construct(IInput input)
    {
        _input =  input;
    }
    
    private void OnEnable()
    {
        _input.Moved += Flip;
    }

    private void OnDisable()
    {
        _input.Moved -= Flip;
    }

    private void Flip(float direction)
    {
        if(direction < 0)
            _direction = -1;
        else if(direction > 0)
            _direction = 1;
        
        transform.localScale = new Vector3(Math.Abs(transform.localScale.x) * _direction, transform.localScale.y, transform.localScale.z);
    }
}
