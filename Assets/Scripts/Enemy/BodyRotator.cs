using UnityEngine;

public class BodyRotator : MonoBehaviour
{
    private float _axisRotationSpeed = 10f;
    private float _randomRotationZ;
    private float _rangeRotationZ = 180f;
    private Vector3 _rotationEuler = new Vector3(0, 0, 20);

    private void Start()
    {
        _randomRotationZ = Random.Range(-_rangeRotationZ, _rangeRotationZ);
        transform.rotation = Quaternion.Euler(0, 180, _randomRotationZ);
    }

    private void Update()
    {
        transform.Rotate(_rotationEuler * _axisRotationSpeed * Time.deltaTime);
    }
}
