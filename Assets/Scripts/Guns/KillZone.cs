using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class KillZone : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Enemy enemy))
        {
            enemy.CathHoleTrap();
        }
    }
}
