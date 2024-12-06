using UnityEngine;
using UnityEngine.Splines;

public class Rail_Chara : MonoBehaviour
{
    [SerializeField] private SplineAnimate m_splineAnimate;

    void Start()
    {
        m_splineAnimate.Play();   
    }
}
