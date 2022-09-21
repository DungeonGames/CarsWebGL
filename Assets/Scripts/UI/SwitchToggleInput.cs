using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class SwitchToggleInput : MonoBehaviour
{
    [SerializeField] private Toggle _toggle;
    [SerializeField] private Image _handleOn;
    [SerializeField] private Image _handleOff;

    private bool _isKeyboardInput;
    private const string KeyboardInput = "KeyboardInput";

    public event UnityAction<bool> KeyboardOn;

    private void Start()
    {
        Load();

        if (_isKeyboardInput)
        {
            _toggle.isOn = true;
        }
        else
        {
            _toggle.isOn = false;
        }

        ChangeState();
    }

    public void ChangeState()
    {
        if (_toggle.isOn)
        {
            _handleOn.gameObject.SetActive(false);
            _handleOff.gameObject.SetActive(true);
            _isKeyboardInput = true;
        }
        else
        {
            _handleOn.gameObject.SetActive(true);
            _handleOff.gameObject.SetActive(false);
            _isKeyboardInput = false;
        }

        KeyboardOn?.Invoke(_isKeyboardInput);
        Save();
    }

    private void Load()
    {
        var dataInput = SaveSystem.Load<SaveData.PlayerData>(KeyboardInput);
        _isKeyboardInput = dataInput.IsKeyboardInput;
    }

    private void Save()
    {
        SaveSystem.Save(KeyboardInput, GetSaveSnapshot());
    }

    private SaveData.PlayerData GetSaveSnapshot()
    {
        var data = new SaveData.PlayerData()
        {
            IsKeyboardInput = _isKeyboardInput,
        };

        return data;
    }
}
