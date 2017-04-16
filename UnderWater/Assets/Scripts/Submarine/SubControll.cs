using UnityEngine;

public class SubControll : MonoBehaviour
{
    private Rigidbody2D _rigidbody2D;
    private SpriteRenderer _spriteRenderer;

    private void OnEnable()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void FixedUpdate()
    {
        var velocity = _rigidbody2D.velocity;
        if (velocity.x > 0)
        {
            _spriteRenderer.flipX = true;
        }
        else if(velocity.x < 0)
        {
            _spriteRenderer.flipX = false;
        }
    }
}