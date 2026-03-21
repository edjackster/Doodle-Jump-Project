using UnityEngine;

public class PlatformBreakingMover : MonoBehaviour
{
    [SerializeField] private float _fallSpeed;

    private bool _isFalling;

    private void Update()
    {
        if (_isFalling)
            transform.position += Vector3.down * (_fallSpeed * Time.deltaTime);
    }

    public void Fall()
    {
        _isFalling = true;
    }

    private void OnDisable()
    {
        _isFalling = false;
    }
}