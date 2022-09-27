using System.Collections;
using UnityEngine;

namespace Creatures.Security.Patrol
{
    public abstract class Patrol : MonoBehaviour
    {
        public abstract IEnumerator DoPatrol();
    }
}