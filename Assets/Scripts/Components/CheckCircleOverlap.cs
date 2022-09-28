using System;
using UnityEngine;
using UnityEngine.Events;

namespace Components
{
    public class CheckCircleOverlap : MonoBehaviour
    {
        [SerializeField] private float _range;
        [SerializeField] private LayerMask _mask;
        [SerializeField] private OnOverlapEvent _event;

        private readonly Collider2D[] _interactionResult = new Collider2D[20];
        
        public void Check()
        {
            var size = Physics2D.OverlapCircleNonAlloc(transform.position, _range, _interactionResult, _mask);
            
            for (int i = 0; i < size; i++)
                _event?.Invoke(_interactionResult[i].gameObject);
        }

        [Serializable]
        public class OnOverlapEvent : UnityEvent<GameObject> { }
    }
}