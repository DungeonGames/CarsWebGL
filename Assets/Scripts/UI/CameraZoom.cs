using System.Collections;
using UnityEngine;
using Cinemachine;

[RequireComponent(typeof(CinemachineVirtualCamera))]
public class CameraZoom : MonoBehaviour
{
    [SerializeField] private GameUiHandler _gameStartHandler;
    [SerializeField] private float _speed;

    private CinemachineVirtualCamera _virtualCamera;
    private CinemachineTransposer _transposer;
    private CinemachineComposer _composer;

    private float _newFieldOfView = 44f;
    private float _newFollowOffsetY = 60f;
    private float _newTrackedObjectOffset = 5f;

    private void OnEnable()
    {
        _gameStartHandler.GameStart += OnZoom;
    }

    private void Start()
    {
        _virtualCamera = GetComponent<CinemachineVirtualCamera>();
        _transposer = _virtualCamera.GetCinemachineComponent<CinemachineTransposer>();
        _composer = _virtualCamera.GetCinemachineComponent<CinemachineComposer>();
    }

    private void OnDisable()
    {
        _gameStartHandler.GameStart -= OnZoom;
    }

    private void OnZoom()
    {
        StartCoroutine(MoveCamera(_newFieldOfView, _newFollowOffsetY, _newTrackedObjectOffset));
    }

    private IEnumerator MoveCamera(float fieldOfView, float followOffsetY, float TrackedObjectOffset)
    {
        while (_virtualCamera.m_Lens.FieldOfView != fieldOfView)
        {
            _virtualCamera.m_Lens.FieldOfView = GetSmoothChangedValue(_virtualCamera.m_Lens.FieldOfView, fieldOfView);
            _transposer.m_FollowOffset.y = GetSmoothChangedValue(_transposer.m_FollowOffset.y, followOffsetY);
            _composer.m_TrackedObjectOffset.y = GetSmoothChangedValue(_composer.m_TrackedObjectOffset.y, TrackedObjectOffset);

            yield return null;
        }
    }

    private float GetSmoothChangedValue(float currentValue, float newValue)
    {
        float changedValue = Mathf.MoveTowards(currentValue, newValue, _speed * Time.deltaTime);

        return changedValue;
    }
}
