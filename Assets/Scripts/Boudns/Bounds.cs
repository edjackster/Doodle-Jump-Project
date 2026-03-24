using System;
using UnityEngine;

public class Bounds : MonoBehaviour
{
    private const float Half = .5f;
    private const float Quarter = .25f;
    
    private void OnTriggerExit2D(Collider2D other)
    {
        if(other.TryGetComponent(out Player _) == false)
            return;

        var bounceX = transform.localScale.x * Half + Math.Abs(other.transform.localScale.x) * Quarter;
        var posX = Mathf.Clamp(-other.transform.position.x, -bounceX, bounceX); 
        
        other.transform.position = new Vector3(posX, other.transform.position.y, other.transform.position.z);
    }
}
