using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float _moveSpeed;

    private Enemy _target;

    private void Update()
    {
        if(_target != null)
        {
            transform.position = Vector3.MoveTowards(transform.position, _target.transform.position, _moveSpeed * Time.deltaTime);
            transform.LookAt(_target.transform.position);

            if (_target.gameObject.activeSelf == false)
            {
                Destroy(gameObject);
            }
        }
    }

    public void Init(Enemy target) => _target = target;
}
