using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class GunProjectile : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Rigidbody m_rb;
    [SerializeField] public int enemyLayer;

    [Header("Values")]
    [SerializeField] private float m_speed = 10.0f;
    [SerializeField] private Vector2 m_speedMultiplier;
    [SerializeField] private Vector2 m_sizeMultiplier;
    [SerializeField] private float m_shootingDelay = 0.2f;
    [SerializeField] private float m_chargeDuration = 1;
    [SerializeField] private float m_lifeTime = 3;

    [Header("SFX")]
    [SerializeField] private ParticleSystem Hit_VFX;
    [SerializeField] private ParticleSystem HitEnemy_VFX;

    private float m_currentChargeTime = 0;
    private bool m_hasReachedMaxCharge = false; 

    public float ChargeDuration { get => m_chargeDuration; set => m_chargeDuration = value; }
    public float CurrentChargeTime { get => m_currentChargeTime; set => m_currentChargeTime = value; }

    public void Shoot(Vector3 dir)
    {
        float ratio = m_currentChargeTime / m_chargeDuration;
        float speed = Mathf.Lerp(m_speed * m_speedMultiplier.x, m_speed * m_speedMultiplier.y, ratio);
        m_rb.AddForce(speed * dir, ForceMode.Impulse);
        StartCoroutine(C_LifeTime());
    }

    private IEnumerator C_LifeTime()
    {
        yield return new WaitForSeconds(m_lifeTime);
        DestroyProjectile();
    }

    public void DestroyProjectile()
    {
        Destroy(gameObject);
    }

    public void SetCharge(float newTimer)
    {

        m_currentChargeTime = newTimer;
        float ratio = m_currentChargeTime / m_chargeDuration;
        transform.localScale = Vector3.Lerp(m_sizeMultiplier.x * Vector3.one, m_sizeMultiplier.y * Vector3.one, m_currentChargeTime / m_chargeDuration);

        if(m_currentChargeTime >= m_chargeDuration)
        {
            OnReachMaxCharge();
        }
    }

    private void OnReachMaxCharge()
    {
        m_hasReachedMaxCharge = true;
    }

    private void OnImpact()
    {
        DestroyProjectile();
    }

    private void OnCollisionEnter(Collision other)
    {
        OnImpact();
        if (other.gameObject.layer == enemyLayer)
        {
            HitEnemy_VFX.transform.parent = null;
            HitEnemy_VFX.Play();
        }

        else
        {
            Hit_VFX.transform.parent = null;
            Hit_VFX.Play();
        }
    }
}
