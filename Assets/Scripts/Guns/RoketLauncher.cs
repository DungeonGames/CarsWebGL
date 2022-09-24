using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoketLauncher : Gun
{
    [SerializeField] private Transform _shootPoint;

    private const string RocketLauncherShoot = "RocketLauncherShoot";

    public override void Shoot(Enemy enemy)
    {
        AudioResources.PlaySound(RocketLauncherShoot);
        var bullet = Instantiate(BulletTemplate, _shootPoint.position, Quaternion.identity);
        bullet.Init(enemy);
        var shootParticle = Instantiate(ShootParticle, _shootPoint.position, Quaternion.identity);
        shootParticle.transform.SetParent(_shootPoint);
    }
}
