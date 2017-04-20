using UnityEngine;

namespace Controller
{
    public interface IControllable
    {
        void UpdateAxis(Vector2 controllVector);
    }
}