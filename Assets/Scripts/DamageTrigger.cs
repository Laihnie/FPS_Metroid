using UnityEngine;

public class DamageTrigger : MonoBehaviour
{
    [Header("Values")]
    [SerializeField] private float m_damageAmount = 5;
    [SerializeField] private float m_knockbackAmount = 5;

    [Header("Effects")]
    [SerializeField] private ParticleSystem m_hitVFX;
    private void OnTriggerEnter(Collider other)
    {
        if (other.isTrigger)
        {
            Rail_Enemy enemy = other.GetComponentInParent<Rail_Enemy>();

            if (enemy)
            {
                enemy.TakeDamage(m_damageAmount, m_knockbackAmount, transform);
            }
        }
    }

}
