using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Boid : MonoBehaviour
{
    [SerializeField] private BoidSettings _settings;
    [SerializeField] private float _minSpeed;
    [SerializeField] private float _maxSpeed;

    [HideInInspector]
    public Vector3 position;
    [HideInInspector]
    public Vector3 forward;
    Vector3 velocity;

    [HideInInspector]
    public Vector3 avgFlockHeading;
    [HideInInspector]
    public Vector3 avgAvoidanceHeading;
    [HideInInspector]
    public Vector3 centreOfFlockmates;
    [HideInInspector]
    public int numPerceivedFlockmates;


    private float _minSpeedOnStart = 5f;
    private float _maxSpeedOnStart = 10f;
    private bool _isHoleTrapCath = false;
    private Rigidbody _rigidbody;
    private Transform cachedTransform;
    private Transform _target;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        cachedTransform = transform;
        SetMinMaxSpeed(_minSpeedOnStart, _maxSpeedOnStart);
    }

    public void Initialize(Transform target)
    {
        this._target = target;

        position = cachedTransform.position;
        forward = cachedTransform.forward;

        float startSpeed = (_settings.minSpeed + _settings.maxSpeed) / 2;
        velocity = transform.forward * startSpeed;
    }

    public void SetTarget(Transform target)
    {
        _target = target;
        SetMinMaxSpeed(_minSpeed, _maxSpeed);
    }

    public void HoleTrapCath(Transform target)
    {
        _rigidbody.isKinematic = true;
        _isHoleTrapCath = true;
        SetTarget(target);
    }

    public void UpdateBoid()
    {
        Vector3 acceleration = Vector3.zero;

        if (_target != null)
        {
            Vector3 offsetToTarget = (_target.position - position);
            acceleration = SteerTowards(offsetToTarget) * _settings.targetWeight;
        }

        if (numPerceivedFlockmates != 0)
        {
            centreOfFlockmates /= numPerceivedFlockmates;

            Vector3 offsetToFlockmatesCentre = (centreOfFlockmates - position);

            var alignmentForce = SteerTowards(avgFlockHeading) * _settings.alignWeight;
            var cohesionForce = SteerTowards(offsetToFlockmatesCentre) * _settings.cohesionWeight;
            var seperationForce = SteerTowards(avgAvoidanceHeading) * _settings.seperateWeight;

            acceleration += alignmentForce;
            acceleration += cohesionForce;
            acceleration += seperationForce;
        }

        if (IsHeadingForCollision())
        {
            Vector3 collisionAvoidDir = ObstacleRays();
            Vector3 collisionAvoidForce = SteerTowards(collisionAvoidDir) * _settings.avoidCollisionWeight;
            acceleration += collisionAvoidForce;
        }

        velocity += acceleration * Time.deltaTime;
        float speed = velocity.magnitude;
        Vector3 dir = velocity / speed;
        speed = Mathf.Clamp(speed, _settings.minSpeed, _settings.maxSpeed);
        velocity = dir * speed;

        if (_isHoleTrapCath == false)
        {
            cachedTransform.position += velocity * Time.deltaTime;
            cachedTransform.forward = dir;
            position = cachedTransform.position;
            forward = dir;
        }
        else
        {
            transform.position = Vector3.MoveTowards(transform.position, _target.position, speed * Time.deltaTime);
        }
    }

    private void SetMinMaxSpeed(float minSpeed, float maxSpeed)
    {
        _settings.minSpeed = minSpeed;
        _settings.maxSpeed = maxSpeed;
    }

    private Vector3 SteerTowards(Vector3 vector)
    {
        Vector3 v = vector.normalized * _settings.maxSpeed - velocity;
        return Vector3.ClampMagnitude(v, _settings.maxSteerForce);
    }

    private bool IsHeadingForCollision()
    {
        RaycastHit hit;

        return Physics.SphereCast(position, _settings.boundsRadius, forward, out hit, _settings.collisionAvoidDst, _settings.obstacleMask);
    }

    private Vector3 ObstacleRays()
    {
        Vector3[] rayDirections = BoidHelper.directions;

        for (int i = 0; i < rayDirections.Length; i++)
        {
            Vector3 dir = cachedTransform.TransformDirection(rayDirections[i]);
            Ray ray = new Ray(position, dir);
            if (!Physics.SphereCast(ray, _settings.boundsRadius, _settings.collisionAvoidDst, _settings.obstacleMask))
            {
                return dir;
            }
        }

        return forward;
    }
}