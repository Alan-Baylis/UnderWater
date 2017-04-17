using System;
using UnityEngine;
using UnityEngine.UI;

namespace Submarine
{
    public class SubControll : MonoBehaviour
    {
        private Rigidbody2D _rigidbody2D;
        private SpriteRenderer _spriteRenderer;

        public Text SpeedText;

        private void OnEnable()
        {
            _rigidbody2D = GetComponent<Rigidbody2D>();
            _spriteRenderer = GetComponent<SpriteRenderer>();
        }

        private void FixedUpdate()
        {
            AdjustSpriteDirection();

            var velocity = _rigidbody2D.velocity;
            SpeedText.text = string.Format("Speed: {0}m/s", Mathf.Round(velocity.magnitude * 100)/100f);
        }

        private void AdjustSpriteDirection()
        {
            var velocity = _rigidbody2D.velocity;
            if (velocity.x > 0)
            {
                _spriteRenderer.flipX = true;
            }
            else if (velocity.x < 0)
            {
                _spriteRenderer.flipX = false;
            }
        }
    }
}