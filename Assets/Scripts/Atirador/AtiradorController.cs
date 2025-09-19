using UnityEngine;

public class AtiradorController : MonoBehaviour
{
    [SerializeField] private GameObject flechaPrefab;
    [SerializeField] private Transform firePoint;

    public void Fire()
    {
        Instantiate(flechaPrefab, firePoint.position, firePoint.rotation);
    }

}
