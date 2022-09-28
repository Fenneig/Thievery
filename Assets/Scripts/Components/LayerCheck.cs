using System;
using UnityEditor;
using UnityEngine;

namespace Components
{
    [RequireComponent(typeof(Collider2D))]
    public class LayerCheck : MonoBehaviour
    {
        [SerializeField] protected LayerMask _layer;
        [SerializeField] protected bool _isTouchingLayer;

        private Collider2D _collider;
        public bool IsTouchingLayer => _isTouchingLayer;

        private void Start()
        {
            _collider = GetComponent<Collider2D>();
        }
        
        private void OnTriggerStay2D(Collider2D collision)
        {
            if (_collider != null) _isTouchingLayer = _collider.IsTouchingLayers(_layer);
        }
        
        private void OnTriggerExit2D(Collider2D collision)
        {
            if (_collider != null) _isTouchingLayer = _collider.IsTouchingLayers(_layer);
        }

        private void OnDrawGizmos()
        {
            Handles.color = _isTouchingLayer ? Color.green : Color.red;
            Handles.DrawSolidDisc(transform.position, Vector3.forward, 0.2f);
        }
    }
}