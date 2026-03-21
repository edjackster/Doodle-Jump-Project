using UnityEngine;
using Random = UnityEngine.Random;

public class MovingPlatform : Platform
{
    [SerializeField] private float _maxX = 2f;
    [SerializeField] private float _maxSpeed = 2f;
    [SerializeField] private float _minSpeed = 2f;
    
    private int _direction = 1;
    private float _speed = 1;
    
    public override PlatformType Type { get; protected set; } = PlatformType.Moving;

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        
        Vector3 a = new Vector3(_maxX, transform.position.y, 0);
        Vector3 b = new Vector3(-_maxX, transform.position.y, 0);
        
        Gizmos.DrawLine(a,b);
    }

    private void FixedUpdate()
    {
        transform.Translate(_direction * _speed * Time.fixedDeltaTime, 0, 0);
        
        if (transform.position.x * _direction > _maxX)
            _direction *= -1;
    }

    public override void Reload()
    {
        base.Reload();
        _speed = Random.Range(_minSpeed, _maxSpeed);
    }
}
