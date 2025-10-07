using Unity.Cinemachine;
using UnityEngine;

[RequireComponent(typeof(CinemachineVirtualCamera))]
public class CameraShake : MonoBehaviour
{
    [SerializeField] private NoiseSettings _noiseSettings;
    [SerializeField] private float _timeRemaining;
    [SerializeField] private float _shakeDuration = 0.3f;
    [SerializeField] private float _shakeAmplitude = 1.0f; // настройка амплитуды
    [SerializeField] private float _shakeFrequency = 10f; // настройка частоты
    
    private CinemachineVirtualCamera _virtualCamera;
    private CinemachineBasicMultiChannelPerlin _channelPerlin;
    
    private float _shakeTime = 0.3f;
    private bool _isShake = false; 

    private void Start()
    {
        _virtualCamera = GetComponent<CinemachineVirtualCamera>();
        _channelPerlin = _virtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
        _timeRemaining = _shakeTime;
    }

    private void Update()
    {
        // if (_isShake)
        // {
        //     if(_timeRemaining > 0)
        //     {
        //         SetNoise();
        //     }
        //     else
        //     {
        //         SetDefaultNoise();
        //     }
        // }
    }

    public void Shake()
    {
        _isShake = true;
    }

    // private void SetNoise()
    // {
    //     _channelPerlin.m_NoiseProfile = _noiseSettings;
    //     _timeRemaining -= Time.deltaTime;
    // }
    //
    // private void SetDefaultNoise()
    // {
    //     _isShake = false;
    //     _channelPerlin.m_NoiseProfile = null;
    //     _timeRemaining = _shakeTime;
    // }
}
