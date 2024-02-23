using UnityEngine;

public class MinigunBullet : Bullet
{
    private float _damage = 1;

    public void Init(float damage)
    {
        _damage = damage;
    }
    
    private void OnTriggerEnter(Collider other)
    {
        Destroy(gameObject);

        if (other.TryGetComponent(out Enemy enemy))
        {
            if (enemy.IsAlive)
            {
                enemy.TakeDamage(_damage);
                enemy.Discard();
            }
        }
    }
}
