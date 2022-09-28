using UnityEngine;
using UnityEngine.Events;

namespace Components
{
    public class HealthComponent : MonoBehaviour
    {
        [SerializeField] private UnityEvent _onDie;

        public void ApplyDamage()
        {
            Debug.Log(gameObject.name + " got hit");
            _onDie?.Invoke();
        }
    }
}