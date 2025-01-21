using UnityEngine;
using UnityEngine.Splines;

public class Rail_Enemy : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private SplineAnimate m_splineAnimate;

    [Header("Values")]
    [SerializeField] private float m_health;

    [Header("Effects")]
    [SerializeField] private ParticleSystem m_onDeathVFX;

    private SplineContainer m_splineContainer;

    public void Init(SplineContainer spline)
    {
        m_splineContainer = spline;
        m_splineAnimate.Container = m_splineContainer;
        m_splineAnimate.Play();
    }
    public void TakeDamage(float damage, float knockback, Transform damageSource)
    {
        m_health -= damage;

        if(m_health <= 0)
        {
            m_health = 0;
            DestroyEnemy();
            ScoreManager.Instance.ScoreEnhance();
        }

    }

    public void DestroyEnemy()
    {
        if (m_onDeathVFX) 
        {
            m_onDeathVFX.transform.parent = null;
            m_onDeathVFX.Play();
        }
        Destroy(gameObject);
    }
}
