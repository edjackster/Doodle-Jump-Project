using System;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class PlayerDetector : MonoBehaviour
{
    public event Action<Player> PlayerDetected;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.TryGetComponent(out Player player) == false)
            return;

        PlayerDetected?.Invoke(player);
    }
}
