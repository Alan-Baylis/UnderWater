using Submarine;
using UnityEngine;

namespace Objects
{
    public interface ICollectable
    {
        float GetWeight();
        void OnCollect(Container collector);
        void OnDiscard(Vector2 location);
        bool IsCollectable();
    }
}