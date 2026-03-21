using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class SpringAnimationController : MonoBehaviour
{
    [SerializeField] private Sprite _compressed;
    [SerializeField] private Sprite _decompressed;
    
    private SpriteRenderer _spriteRenderer;

    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void Release()
    {
        _spriteRenderer.sprite = _decompressed;
    }

    public void Reload()
    {
        _spriteRenderer.sprite = _compressed;
    }
}
