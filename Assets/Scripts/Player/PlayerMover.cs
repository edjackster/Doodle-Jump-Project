using UnityEngine;
using Zenject;

public class PlayerMover : MonoBehaviour
{ 
    [SerializeField] private float moveSpeed;
    
    private IInput _input;

    [Inject]
    private void Construct(IInput input)
    {
        _input =  input;
    }
    
    private void OnEnable()
    {
        _input.Moved += Move;
    }

    private void OnDisable()
    {
        _input.Moved -= Move;
    }

    private void Move(float direction)
    {
        transform.Translate(Vector2.right * (direction * moveSpeed * Time.deltaTime), Space.World);
    }
}