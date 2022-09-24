using System;
using Components;
using UnityEngine;
using Utils;

namespace Creatures.Hero
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class Hero : MonoBehaviour
    {
        [Space][Header("Speed stats")]
        [SerializeField] private float _maxSpeed;
        [SerializeField] private float _maxAcceleration;
        [SerializeField] private float _maxDeceleration;    
        [SerializeField] private float _maxAirAcceleration;
        [SerializeField] private float _maxAirDeceleration;
        [SerializeField] private float _maxTurnSpeed;
        [SerializeField] private float _maxAirTurnSpeed;

        [Space] [Header("Checkers")] 
        [SerializeField] private LayerCheck _groundCheck;
        
        private float _maxSpeedChange;
        private float _acceleration;
        private float _deceleration;
        private float _turnSpeed;
        private Vector2 _velocity;
        
        private Rigidbody2D _rigidbody;
        private float _directionX;
        private float _directionY;
        private Vector2 _desiredVelocity;
        private bool _onGround;
        
        private const float DirectionTolerance = 0.1f; 

        public float DirectionX
        {
            get => _directionX;
            set => _directionX = value;
        }

        public float DirectionY
        {
            get => _directionY;
            set => _directionY = value;
        }

        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody2D>();
        }

        private void Update()
        {
            _desiredVelocity = new Vector2(_directionX, 0) * _maxSpeed;
        }

        private void FixedUpdate()
        {
            _onGround = _groundCheck.IsTouchingLayer;
            
            _acceleration = _onGround ? _maxAcceleration : _maxAirAcceleration;
            _deceleration = _onGround ? _maxDeceleration : _maxAirDeceleration;
            _turnSpeed = _onGround ? _maxTurnSpeed : _maxAirTurnSpeed;

            if (_directionX != 0)
            {
                UpdateSpriteDirection();
                if (Mathf.Sign(_directionX) != Mathf.Sign(_velocity.x))
                {
                    _maxSpeedChange = _turnSpeed * Time.deltaTime;
                }
                else
                {
                    _maxSpeedChange = _acceleration * Time.deltaTime;
                }
            }
            else
            {
                _maxSpeedChange = _deceleration * Time.deltaTime;
            }

            _velocity.x = Mathf.MoveTowards(_velocity.x, _desiredVelocity.x, _maxSpeedChange);
            _rigidbody.velocity = _velocity;
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
    }
}