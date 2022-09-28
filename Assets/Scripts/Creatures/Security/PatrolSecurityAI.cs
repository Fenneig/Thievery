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
            if (_patrolZone.CurrentSuspiciousLevel != ZoneSuspiciousLevel.SuspiciousLevel.NoThreat) StartState(AgroToHero());
        }

        protected override IEnumerator AgroToHero()
        {
            yield return new WaitForSeconds(_patrolZone.CurrentSuspiciousLevel == ZoneSuspiciousLevel.SuspiciousLevel.Threat ? _alarmDelayWhileThreat : _alarmDelay);
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
            if (IsDead) yield break;

            yield return new WaitForSeconds(_patrolZone.CurrentSuspiciousLevel == ZoneSuspiciousLevel.SuspiciousLevel.Threat ? _missHeroCooldownWhileThreat : _missHeroCooldown);
            StartState(_patrol.DoPatrol());
        }

        private IEnumerator Attack()
        {
            while (_canAttack.IsTouchingLayer)
            {
                StopMoving();
                _security.Attack();
                _patrolZone.ChangeSuspiciousLevelToThreat();
                
                yield return new WaitForSeconds(_patrolZone.CurrentSuspiciousLevel == ZoneSuspiciousLevel.SuspiciousLevel.Threat ? _attackCooldownWhileThreat : _attackCooldown);
            }

            StartState(GoToHero());
        }
    }
}