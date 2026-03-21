using System;
using UnityEngine;

public class Bounds : MonoBehaviour
{
    private void OnTriggerExit2D(Collider2D other)
    {
        if(other.TryGetComponent(out Player _) == false)
            return;

        var bounceX = transform.localScale.x / 2 + Math.Abs(other.transform.localScale.x) / 4;
        var posX = Mathf.Clamp(-other.transform.position.x, -bounceX, bounceX); 
        
        other.transform.position = new Vector3(posX, other.transform.position.y, other.transform.position.z);
    }
}
