using System.Collections;
using UnityEngine;

namespace Creatures.Security.Patrol
{
    public class PointPatrol : Patrol
    {
        [SerializeField] private Transform[] _points;
        [SerializeField] private float _threshold = 0.5f;
        [SerializeField] private Security _security;
        
        private int _destinationPointIndex;


        public override IEnumerator DoPatrol()
        {
            while (enabled)
            {
                if (IsOnPatrol()) _destinationPointIndex = (int) Mathf.Repeat(_destinationPointIndex + 1, _points.Length);

                var direction = _points[_destinationPointIndex].position - transform.position;
                _security.DirectionX = direction.x;

                yield return null;
            }
        }

        private bool IsOnPatrol()
        {
            return (_points[_destinationPointIndex].position - transform.position).magnitude < _threshold;
        }
    }
}