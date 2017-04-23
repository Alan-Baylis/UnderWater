using System.Collections.Generic;
using System.Linq;
using Objects;
using UnityEngine;

namespace Submarine
{
    public class Container : MonoBehaviour
    {

        private ICollection<ICollectable> _objects;

        private void OnEnable()
        {
            _objects = new List<ICollectable>();
        }

        private void FixedUpdate()
        {
            GetComponent<SubmarineState>().CargoWeight = _objects.Sum(o => o.GetWeight());
        }

        public void Add(ICollectable collectable)
        {
            collectable.OnCollect(this);
            _objects.Add(collectable);
        }
    }
}
