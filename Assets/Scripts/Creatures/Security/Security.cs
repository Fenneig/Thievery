using Components;
using UnityEngine;

namespace Creatures.Security
{
    public class Security : MonoBehaviour
    {
        [SerializeField] private float _speed;
        [SerializeField] private LayerCheck _groundLayerCheck;
        [SerializeField] private Rigidbody2D _rigidbody;
        [SerializeField] private Animator _animator;

        private float _directionX;
        public float DirectionX
        {
            get => _directionX;
            set => _directionX = value;
        }
        private bool _isGrounded;

        private static readonly int WalkingKey = Animator.StringToHash("is-walking");
        private static readonly int HitKey = Animator.StringToHash("hit");
        private static readonly int GroundKey = Animator.StringToHash("is-ground");
        private static readonly int AttackKey = Animator.StringToHash("attack");

        private void Update()
        {
            _isGrounded = _groundLayerCheck.IsTouchingLayer;
        }

        private void FixedUpdate()
        {
            _rigidbody.velocity  = CalculateVelocity();

            SetAnimatorSettings();

            UpdateSpriteDirection();
        }

        private Vector2 CalculateVelocity()
        {
            if (_directionX < 0) _directionX = -1;
            else if (_directionX > 0) _directionX = 1;
            else _directionX = 0;
            Debug.Log($"direction = {_directionX}, speed = {_speed}");
            return new Vector2(_directionX * _speed, 0);
        }
        
        private void UpdateSpriteDirection()
        {
            if (_directionX < 0)
            {
                transform.localScale = new Vector3(-1, 1, 1);
            }
            else if (_directionX > 0)
            {
                transform.localScale = Vector3.one;
            }
        }

        public void Attack()
        {
            _animator.SetTrigger(AttackKey);
        }

        private void SetAnimatorSettings()
        {
            _animator.SetBool(GroundKey, _isGrounded);
            _animator.SetBool(WalkingKey, _rigidbody.velocity.x != 0);
        }
    }
}