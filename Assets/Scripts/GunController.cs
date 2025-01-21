using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Splines;

public class GunController : MonoBehaviour
{

    [Header("References")]
    [SerializeField] private GunProjectile m_ProjectilePrefab;
    [SerializeField] private Transform m_shootingSpot;
    [SerializeField] private Transform m_rotationPoint;

    [Header("Aim")]
    [SerializeField] private float m_rotationSpeed = 10;
    [SerializeField] private LayerMask m_hitLayers;

    private GunProjectile m_currentProjectile;
    private bool m_isCharging = false;
    private Vector3 m_targetPos;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            OnMouseButtonDown();
        }

        if (Input.GetMouseButtonUp(0))
        {
            OnMouseButtonUp();
        }

        if (m_isCharging && m_currentProjectile)
        {
            float newChargeTime = m_currentProjectile.CurrentChargeTime + Time.deltaTime;
            m_currentProjectile.SetCharge(newChargeTime);
        }

        Vector3 shootDirection = m_targetPos - m_shootingSpot.position;
        m_rotationPoint.forward = Vector3.RotateTowards(m_rotationPoint.forward, shootDirection.normalized, m_rotationSpeed * Time.deltaTime, 10 );
    }

    private void FixedUpdate()
    {
        AimHandler();
    }

    private void AimHandler()
    {
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = Camera.main.transform.position.z;

        Ray ray = Camera.main.ScreenPointToRay(mousePos);
        RaycastHit hit;
        if(Physics.Raycast(ray, out hit, 100, m_hitLayers))
        {
            m_targetPos = hit.point;
        }
        else
        {
            m_targetPos = ray.GetPoint(60);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(m_targetPos, 1);
    }

    private void OnMouseButtonDown()
    {
        m_isCharging = true;
        m_currentProjectile = InstantiateProjectile();
    }
    
    private void OnMouseButtonUp()
    {
        m_isCharging = false;
        ShootProjectile(m_currentProjectile, m_shootingSpot.forward); // à retoucher
        m_currentProjectile = null;
    }
    private GunProjectile InstantiateProjectile()
    {
        GunProjectile projectile = Instantiate(m_ProjectilePrefab, m_shootingSpot);
        projectile.transform.localPosition = Vector3.zero;
        return projectile;
    }

    private void ShootProjectile(GunProjectile projectile, Vector3 dir)
    {
        projectile.transform.parent = null;
        projectile.Shoot(dir);
    }
}
