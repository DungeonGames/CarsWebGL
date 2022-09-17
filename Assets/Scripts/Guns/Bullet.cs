using System.Collections;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float _moveSpeed;

    private int _damage = 1;
    private Enemy _target;

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Enemy enemy))
        {
            if (enemy.IsAlive)
                enemy.TakeDamage(_damage);

            Destroy(gameObject);
        }
    }

    public void Init(Enemy tartget)
    {
        _target = tartget;
        StartCoroutine(MoveToTarget(tartget));
    }

    private IEnumerator MoveToTarget(Enemy tartget)
    {
        while (enabled)
        {
            transform.position = Vector3.MoveTowards(transform.position, tartget.transform.position, _moveSpeed * Time.deltaTime);
            transform.LookAt(tartget.transform.position);

            if (_target.gameObject.activeSelf == false)
            {
                Destroy(gameObject);
            }

            yield return null;
        }
    }
}
