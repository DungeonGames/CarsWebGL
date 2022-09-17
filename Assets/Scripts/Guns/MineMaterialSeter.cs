using System.Collections;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(BoxCollider))]
[RequireComponent(typeof(MeshRenderer))]
public class MineMaterialSeter : MonoBehaviour
{
    [SerializeField] private Material _defaultMaterial;
    [SerializeField] private Material _activatedMaterial;

    private MeshRenderer _meshRenderer;
    private float _delay = 0.5f;

    public event UnityAction Ready;

    private void Start()
    {
        _meshRenderer = GetComponent<MeshRenderer>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Car car))
        {
            StartCoroutine(PrepareToExplosion());
        }
    }

    private IEnumerator PrepareToExplosion()
    {
        yield return new WaitForSeconds(_delay);
        SetMaterial(_activatedMaterial);

        yield return new WaitForSeconds(_delay);
        SetMaterial(_defaultMaterial);

        yield return new WaitForSeconds(_delay);
        SetMaterial(_activatedMaterial);

        yield return new WaitForSeconds(_delay);
        SetMaterial(_defaultMaterial);

        yield return new WaitForSeconds(_delay);
        SetMaterial(_activatedMaterial);
        Ready?.Invoke();
    }

    private void SetMaterial(Material material) => _meshRenderer.material = material;
}
