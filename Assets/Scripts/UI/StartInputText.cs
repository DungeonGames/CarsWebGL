using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class StartInputText : MonoBehaviour
{
    [SerializeField] private SwitchToggleInput _inputToggle;
    [SerializeField] private TMP_Text _toStartText;

    private const string ClickText = "CLICK TO START";
    private const string TapText = "TAP TO START";

    private void OnEnable()
    {
        _inputToggle.KeyboardOn += ChangeText;
    }

    private void ChangeText(bool isKeyboard)
    {
        if (isKeyboard)
        {
            _toStartText.text = ClickText;
        }
        else
        {
            _toStartText.text = TapText;
        }
    }
}
