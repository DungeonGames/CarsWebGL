using UnityEngine;

public class Turret : Gun
{
    [SerializeField] private Transform[] _shootPoints;

    public override void Shoot(Enemy enemy)
    {
        for (int i = 0; i < _shootPoints.Length; i++)
        {
            var bullet = Instantiate(BulletTemplate, _shootPoints[i].position, Quaternion.identity);
            bullet.Init(enemy);
            var shootParticle = Instantiate(ShootParticle, _shootPoints[i].position, Quaternion.identity);
            shootParticle.transform.SetParent(_shootPoints[i]);
        }       
    }
}
