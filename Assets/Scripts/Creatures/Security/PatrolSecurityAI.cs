using System.Collections;
using Components;
using UnityEngine;

namespace Creatures.Security
{
    public class PatrolSecurityAI : SecurityAI
    {
        [SerializeField] private LayerCheck _canAttack;
        [SerializeField] private float _targetReachThreshold;

        public override void OnHeroInVision(GameObject go)
        {
            if (IsDead) return;
            Target = go;

            StartState(AgroToHero());
        }

        protected override IEnumerator AgroToHero()
        {
            yield return new WaitForSeconds(1);
            StartState(GoToHero());
        }

        private IEnumerator GoToHero()
        {
            while (_vision.IsTouchingLayer)
            {
                if (_canAttack.IsTouchingLayer)
                {
                    StartState(Attack());
                }
                else
                {
                    var horizontalDelta = Mathf.Abs(Target.transform.position.x - transform.position.x);
                    if (horizontalDelta <= _targetReachThreshold) _security.DirectionX = 0;
                    else SetDirectionToTarget();
                }

                yield return null;
            }
            Debug.Log("Miss Hero!");
            if (IsDead) yield break;

            yield return new WaitForSeconds(_missHeroCooldown);
            StartState(_patrol.DoPatrol());
        }

        private IEnumerator Attack()
        {
            while (_canAttack.IsTouchingLayer)
            {
                StopMoving();
                _security.Attack();
                yield return new WaitForSeconds(_attackCooldown);
            }

            StartState(GoToHero());
        }
    }
}