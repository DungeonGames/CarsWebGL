using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoketLauncher : Gun
{
    [SerializeField] private Transform _shootPoint;

    public override void Shoot(Enemy enemy)
    {
        var bullet = Instantiate(BulletTemplate, _shootPoint.position, Quaternion.identity);
        bullet.Init(enemy);
        var shootParticle = Instantiate(ShootParticle, _shootPoint.position, Quaternion.identity);
        shootParticle.transform.SetParent(_shootPoint);
    }
}
