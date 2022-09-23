using UnityEngine;

public class MinigunBullet : Bullet
{
    private int _damage = 1;

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Enemy enemy))
        {
            if (enemy.IsAlive)
            {
                enemy.TakeDamage(_damage);
            }
        }

        Destroy(gameObject);
    }
}
