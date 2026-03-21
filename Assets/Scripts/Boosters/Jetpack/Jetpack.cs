using UnityEngine;

public class Jetpack : PickUp
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.TryGetComponent(out Player player) == false)
            return;
        
        gameObject.SetActive(false);
    }
}
