using Components;
using UnityEngine;

namespace Creatures.Hero
{
    [RequireComponent(typeof(Rigidbody2D))]
    [RequireComponent(typeof(Animator))]
    public class Hero : MonoBehaviour
    {
        [Space] [Header("Speed stats")] [SerializeField]
        private float _maxSpeed;

        [SerializeField] private float _maxAcceleration;
        [SerializeField] private float _maxDeceleration;
        [SerializeField] private float _maxAirAcceleration;
        [SerializeField] private float _maxAirDeceleration;
        [SerializeField] private float _maxTurnSpeed;
        [SerializeField] private float _maxAirTurnSpeed;

        private float _maxSpeedChange;
        private float _acceleration;
        private float _deceleration;
        private float _turnSpeed;
        private Vector2 _velocity;

        [Space] [Header("CalculateJumpVelocity stats")] [SerializeField]
        private float _jumpHeight;

        [SerializeField] private float _timeToJumpApex;
        [SerializeField] private float _upwardMovementMultiplier;
        [SerializeField] private float _downwardMovementMultiplier;
        [SerializeField] private float _jumpCutOff;
        [SerializeField] private float _coyoteTime;

        private float _jumpSpeed;
        private float _gravityMultiplier;
        private float _coyoteTimeCounter = 0;

        private bool _desiredJump;
        private bool _pressingJump;
        private bool _currentlyJumping;

        public bool DesiredJump
        {
            set => _desiredJump = value;
        }

        public bool PressingJump
        {
            set => _pressingJump = value;
        }
        public bool IsCrouch
        {
            set => _isCrouch = value;
        }

        private const float DefaultGravityScale = 1f;


        [Space] [Header("Checkers")] [SerializeField]
        private LayerCheck _groundCheck;

        private Rigidbody2D _rigidbody;
        private float _directionX;
        private Vector2 _horizontalVelocity;
        private bool _onGround;
        private bool _isCrouch;

        private static readonly int JumpKey = Animator.StringToHash("jump");
        private static readonly int WalkingKey = Animator.StringToHash("is-walking");
        private static readonly int HitKey = Animator.StringToHash("hit");
        private static readonly int VerticalVelocityKey = Animator.StringToHash("vertical-velocity");
        private static readonly int CrouchKey = Animator.StringToHash("is-crouch");
        private static readonly int GroundKey = Animator.StringToHash("is-ground");
        private static readonly int AttackKey = Animator.StringToHash("attack");

        private Animator _animator;

        public float DirectionX
        {
            get => _directionX;
            set => _directionX = value;
        }

        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody2D>();
            _animator = GetComponent<Animator>();
        }

        private void Update()
        {
            var maxSpeed = _isCrouch && !_currentlyJumping ? 0 : _maxSpeed;
            _horizontalVelocity = new Vector2(_directionX, 0) * maxSpeed;

            Vector2 newGravity = new Vector2(0, -2 * _jumpHeight / (_timeToJumpApex * _timeToJumpApex));
            _rigidbody.gravityScale = newGravity.y / Physics2D.gravity.y * _gravityMultiplier;
        }

        private void FixedUpdate()
        {
            _onGround = _groundCheck.IsTouchingLayer;

            CalculateVelocity();

            SetAnimatorSettings();
        }

        private void CalculateVelocity()
        {
            float velocityX = CalculateVelocityX();
            float velocityY = CalculateVelocityY();
            _rigidbody.velocity = new Vector2(velocityX, velocityY);
        }

        private float CalculateVelocityX()
        {
            
            _acceleration = _onGround ? _maxAcceleration : _maxAirAcceleration;
            _deceleration = _onGround ? _maxDeceleration : _maxAirDeceleration;
            _turnSpeed = _onGround ? _maxTurnSpeed : _maxAirTurnSpeed;

            
            if (_directionX != 0)
            {
                UpdateSpriteDirection();
                if (Mathf.Sign(_directionX) != Mathf.Sign(_velocity.x))
                {
                    _maxSpeedChange = _turnSpeed * Time.fixedDeltaTime;
                }
                else
                {
                    _maxSpeedChange = _acceleration * Time.fixedDeltaTime;
                }
            }
            else
            {
                _maxSpeedChange = _deceleration * Time.fixedDeltaTime;
            }

            _velocity.x = Mathf.MoveTowards(_velocity.x, _horizontalVelocity.x, _maxSpeedChange);
            return _velocity.x;
        }

        private float CalculateVelocityY()
        {
            var velocityY = _rigidbody.velocity.y;
            if (_currentlyJumping || _isCrouch) _desiredJump = false; 
            if (_desiredJump) return CalculateJumpVelocity();

            CalculateGravity();
            return velocityY;
        }

        private void CalculateGravity()
        {
            if (_rigidbody.velocity.y > 0.01f)
            {
                if (_onGround)
                {
                    _gravityMultiplier = DefaultGravityScale;
                }
                else
                {
                    if (_pressingJump && _currentlyJumping)
                    {
                        _gravityMultiplier = _upwardMovementMultiplier;
                    }
                    else
                    {
                        _gravityMultiplier = _jumpCutOff;
                    }
                }
            }
            else if (_rigidbody.velocity.y < -0.01f)
                {

                    if (_onGround)
                    {
                        _gravityMultiplier = DefaultGravityScale;
                    }
                    else
                    {
                        _gravityMultiplier = _downwardMovementMultiplier;
                    }

                }
                else
                {
                    if (_onGround)
                    {
                        _currentlyJumping = false;
                    }

                    _gravityMultiplier = DefaultGravityScale;
                }

            }

        private float CalculateJumpVelocity()
        {
            var velocityY = _velocity.y;
            if (_onGround || (_coyoteTimeCounter > 0.03f && _coyoteTimeCounter < _coyoteTime))
            {
                _animator.SetTrigger(JumpKey);

                _desiredJump = false;
                _coyoteTimeCounter = 0;

                _jumpSpeed = Mathf.Sqrt(-2f * Physics2D.gravity.y * _rigidbody.gravityScale * _jumpHeight);

                velocityY += _jumpSpeed;
                _currentlyJumping = true;
            }

            return velocityY;
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

        private void SetAnimatorSettings()
        {
            _animator.SetBool(GroundKey, _onGround);
            _animator.SetBool(WalkingKey, _rigidbody.velocity.x != 0);
            _animator.SetBool(CrouchKey, _isCrouch);
            _animator.SetFloat(VerticalVelocityKey, _rigidbody.velocity.y);
        }

        public void Attack()
        {
            _animator.SetTrigger(AttackKey);
        }

        public void Interact()
        {
            
        }
    }
}