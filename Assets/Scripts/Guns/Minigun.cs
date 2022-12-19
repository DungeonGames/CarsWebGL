using UnityEngine;

public class Minigun : Gun
{
    [SerializeField] private Transform _shootPoint;
    [SerializeField] private Transform _shellPoint;
    [SerializeField] private ParticleSystem _shellParticle;

    private const string MinigunShoot = "MinigunShoot";

    public override void Shoot(Enemy enemy)
    {
        CameraShake.Shake();
        AudioResources.PlaySound(MinigunShoot);
        var bullet = Instantiate(BulletTemplate, _shootPoint.position, Quaternion.identity);
        bullet.Init(enemy);
        var shootParticle = Instantiate(ShootParticle, _shootPoint.position, _shootPoint.rotation);
        shootParticle.transform.SetParent(_shootPoint);
        Instantiate(_shellParticle, _shellPoint.position, Quaternion.identity);
    }
}
