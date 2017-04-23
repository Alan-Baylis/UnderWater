using UnityEngine;
using UnityEngine.UI;

namespace Submarine
{
    public class SubmarineState : MonoBehaviour
    {
        private Rigidbody2D _rigidbody2D;
        public float BaseWeight = 1000;
        public float CargoWeight;

        public Text SpeedText;

        private void OnEnable()
        {
            _rigidbody2D = GetComponent<Rigidbody2D>();
        }

        private void FixedUpdate()
        {
            AdjustSpriteDirection();

            _rigidbody2D.mass = BaseWeight + CargoWeight;

            var velocity = _rigidbody2D.velocity;
            SpeedText.text = string.Format("Speed: {0}m/s", Mathf.Round(velocity.magnitude * 100) / 100f);
        }

        private void AdjustSpriteDirection()
        {
            var scale = transform.localScale;

            var velocity = _rigidbody2D.velocity;
            if (velocity.x > 0)
                scale.x = -1;
            else if (velocity.x < 0)
                scale.x = 1;

            transform.localScale = scale;
        }
    }
}