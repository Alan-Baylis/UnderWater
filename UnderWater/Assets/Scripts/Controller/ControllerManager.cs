using UnityEngine;
using Utility;

namespace Controller
{
    internal class ControllerManager : SingeltonBehaviour<ControllerManager>
    {
        private GameObject _target;

        private void Update()
        {
            if (!_target)
                return;

            var controllables = _target.GetComponents<IControllable>();

            foreach (var controllable in controllables)
                controllable.UpdateAxis(GetControllVector());
        }

        private static Vector2 GetControllVector()
        {
            var x = Input.GetAxis("Horizontal");
            var y = Input.GetAxis("Vertical");

            var controllAxis = new Vector2(x, y);
            return controllAxis;
        }

        public static GameObject Target
        {
            get
            {
                return Instance._target;
            }
            set
            {
                Instance._target = value;
            }
        }
    }
}