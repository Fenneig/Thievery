using System.Collections;
using Components;
using UnityEngine;

namespace Creatures.Security.Patrol
{
    public class PlatformPatrol : Patrol
    {
        [SerializeField] private LayerCheck _endOfPlatformCheck;
        [SerializeField] private LayerCheck _wallCheck;
        [SerializeField] private Security _security;
        
        private float _lastFramePositionX;
        private int _checkStuckCount;

        public override IEnumerator DoPatrol()
        {
            while (enabled)
            {
                var direction = (_endOfPlatformCheck.gameObject.transform.position - transform.position);
                if (IsNeedToTurnBack())
                {
                    direction.x *= -1;
                    yield return new WaitForSeconds(0.1f);
                }
                
                _security.DirectionX = direction.x;
                yield return null;
            }
        }

        private bool IsNeedToTurnBack()
        {
            return !_endOfPlatformCheck.IsTouchingLayer || _wallCheck.IsTouchingLayer;
        }
    }
}