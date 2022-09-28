using UnityEngine;

namespace Components
{
    public class DamageComponent : MonoBehaviour
    {
        public void DealDamage(GameObject target)
        {
            var healthComponent = target.GetComponent<HealthComponent>();
            if (healthComponent != null) healthComponent.ApplyDamage();
        }
    }
}