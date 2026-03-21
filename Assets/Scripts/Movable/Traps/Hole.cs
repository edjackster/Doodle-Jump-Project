using UnityEngine;

[RequireComponent(typeof(PlayerDetector))]
public class Hole : Movable
{
    private PlayerDetector _playerDetector;
    
    private void Awake()
    {
        _playerDetector  = GetComponent<PlayerDetector>();
    }

    private void OnEnable()
    {
        _playerDetector.PlayerDetected += OnPlayerDetected;
    }

    private void OnDisable()
    {
        _playerDetector.PlayerDetected -= OnPlayerDetected;
    }

    private void OnPlayerDetected(Player player)
    {
        player.FallInHall();
    }
}
