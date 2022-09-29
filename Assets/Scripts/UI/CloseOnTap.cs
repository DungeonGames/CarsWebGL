using UnityEngine.EventSystems;
using UnityEngine;
using System.Collections.Generic;

public class CloseOnTap : MonoBehaviour
{
    [SerializeField] private GameObject _panelWithControlElements;
    [SerializeField] private GameObject _tapToPlay;

    private void Start()
    {
        _panelWithControlElements.SetActive(false);
    }

    public void CloseSelf()
    {
        _panelWithControlElements.gameObject.SetActive(!_panelWithControlElements.gameObject.activeSelf);

        if (_panelWithControlElements.gameObject.activeSelf == false)
        {
            if (_tapToPlay != null)
                _tapToPlay.gameObject.SetActive(true);

            Time.timeScale = 1;
        }
        else
        {
            if (_tapToPlay != null)
                _tapToPlay.gameObject.SetActive(false);
        }
    }
}
