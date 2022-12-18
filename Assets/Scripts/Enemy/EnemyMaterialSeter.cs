using System.Collections;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Enemy))]
public class EnemyMaterialSeter : MonoBehaviour
{
    [SerializeField] private SkinnedMeshRenderer _skinnedMeshRenderer;
    [SerializeField] private MeshRenderer[] _meshRenderers;
    [SerializeField] private Material _defaultMaterial;
    [SerializeField] private Material _blackParttMaterial;
    [SerializeField] private Material _dieMaterial;

    private Enemy _enemy;
    private float _delay = 0.4f;

    public event UnityAction SwitchEnded;

    private void Awake()
    {
        _enemy = GetComponent<Enemy>();
    }

    private void OnEnable()
    {
        _enemy.Hit += OnHit;
        _enemy.PrepareToDie += OnPrepareToDie;
    }

    private void OnDisable()
    {
        _enemy.Hit -= OnHit;
        _enemy.PrepareToDie -= OnPrepareToDie;
    }

    private void OnHit()
    {
        StartCoroutine(SwithHitMaterial());
    }

    private IEnumerator SwithHitMaterial()
    {
        Change(_dieMaterial, _blackParttMaterial);

        yield return new WaitForSeconds(_delay);

        Change(_defaultMaterial, _blackParttMaterial);
    }

    private void OnPrepareToDie(Enemy enemy)
    {
        StartCoroutine(SwitchDieMaterial());
    }

    private IEnumerator SwitchDieMaterial()
    {
        Change(_dieMaterial, _blackParttMaterial);

        yield return new WaitForSeconds(_delay);

        Change(_dieMaterial, _blackParttMaterial);

        SwitchEnded?.Invoke();
    }

    private void Change(Material mainMaterial, Material partMaterial)
    {
        _skinnedMeshRenderer.material = mainMaterial;

        for (int i = 0; i < _meshRenderers.Length; i++)
        {
            _meshRenderers[i].material = partMaterial;
        }
    }
}
