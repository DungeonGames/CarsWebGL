using UnityEngine;

public class Minigun : Gun
{
    [SerializeField] private Transform _shootPoint;

    private const string MinigunShoot = "MinigunShoot";

    public override void Shoot(Enemy enemy)
    {
        AudioResources.PlaySound(MinigunShoot);
        var bullet = Instantiate(BulletTemplate, _shootPoint.position, Quaternion.identity);
        bullet.Init(enemy);
        var shootParticle = Instantiate(ShootParticle, _shootPoint.position, Quaternion.identity);
        shootParticle.transform.SetParent(_shootPoint);
    }
}
