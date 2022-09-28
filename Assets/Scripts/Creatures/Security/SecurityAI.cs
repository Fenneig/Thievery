using System.Collections;
using Components;
using UnityEngine;

namespace Creatures.Security
{
    public class SecurityAI : MonoBehaviour
    {
        [Space] [Header("Cooldowns")]
        [SerializeField] protected float _alarmDelay = 1;
        [SerializeField] protected float _attackCooldown = 1;
        [SerializeField] protected float _missHeroCooldown = 1;

        [Space] [Header("Layer Checks")] [SerializeField]
        protected LayerCheck _vision;

        [Space] [Header("Components")] 
        [SerializeField] protected Animator _animator;
        [SerializeField] protected Security _security;
        [SerializeField] protected Patrol.Patrol _patrol;
        
        protected GameObject Target;
        protected bool IsDead;

        private Coroutine _current;
        private int _corpseLayer;

        private static readonly int HitKey = Animator.StringToHash("hit");

        private void Start()
        {
            _corpseLayer = LayerMask.NameToLayer("Corpse");
            StartState(_patrol.DoPatrol());
        }

        public virtual void OnHeroInVision(GameObject go)
        {
        }

        protected virtual IEnumerator AgroToHero()
        {
            yield return null;
        }

        public void OnDie()
        {
            IsDead = true;
            _animator.SetTrigger(HitKey);
            
            StopMoving();
            if (_current != null) StopCoroutine(_current);
            gameObject.layer = _corpseLayer;
        }

        protected void SetDirectionToTarget()
        {
            var direction = Target.transform.position - transform.position;
            _security.DirectionX = direction.x;
        }

        protected void StartState(IEnumerator coroutine)
        {
            StopMoving();
            
            if (_current != null) StopCoroutine(_current);

            _current = StartCoroutine(coroutine);
        }

        protected void StopMoving()
        {
            _security.DirectionX = 0;
        }
    }
}