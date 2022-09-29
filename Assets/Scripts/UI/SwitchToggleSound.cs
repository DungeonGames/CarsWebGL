using UnityEngine;
using UnityEngine.UI;

public class SwitchToggleSound : MonoBehaviour
{
    [SerializeField] private Toggle _toggle;
    [SerializeField] private Image _handleOff;

    private AudioResources _audioResources;

    private void Start()
    {
        _audioResources = FindObjectOfType<AudioResources>();
    }

    public void ChangeState()
    {
        if (_toggle.isOn)
        {
            _handleOff.gameObject.SetActive(false);
            _audioResources.UnMute();
        }
        else
        {
            _handleOff.gameObject.SetActive(true);
            _audioResources.Mute();
        }
    }
}
