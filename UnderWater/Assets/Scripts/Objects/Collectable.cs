using Submarine;
using UnityEngine;

namespace Objects
{
    public class Collectable : MonoBehaviour, ICollectable
    {
        private Container _container;

        public float GetWeight()
        {
            return 250;
        }

        public void OnCollect(Container collector)
        {
            _container = collector;
            gameObject.SetActive(false);
        }

        public void OnDiscard(Vector2 location)
        {
            gameObject.SetActive(true);
            transform.position = location;
            _container = null;
        }

        public bool IsCollectable()
        {
            return true;
        }
    }
}
