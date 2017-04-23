using Submarine;
using UnityEngine;

namespace Objects
{
    public class Collector : MonoBehaviour
    {
        public Container Container;
        private void OnTriggerEnter2D(Collider2D other)
        {
            var collectable = other.gameObject.GetComponent<ICollectable>();

            if (collectable != null && collectable.IsCollectable())
            {
                Debug.Log("Found " + other.name);
                Container.Add(collectable);
            }
        }
    }
}
