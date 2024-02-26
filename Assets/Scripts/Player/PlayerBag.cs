using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerBag : MonoBehaviour
{
    [SerializeField] private List<Car> _cars;
    [SerializeField] private List<Gun> _guns;

    private Car _currentCar;
    private Gun _currentGun;
    private int _currentCarIndex = 0;
    private int _currentGunIndex = 0;

    private const string CurrentCar = "CurrentCar";
    private const string CurrentGun = "CurrentGun";

    public event UnityAction<Car> CarChanged;
    public event UnityAction<Gun> GunChanged;

    private void Start()
    {
        _currentCar = _cars[_currentCarIndex];
        _currentCar.gameObject.SetActive(true);
        CarChanged?.Invoke(_currentCar);

        _currentGun = _guns[_currentGunIndex];
        _currentGun.gameObject.SetActive(true);
        GunChanged?.Invoke(_currentGun);
    }

    public void ActivateNewCar(Car newCar)
    {
        _currentCar.gameObject.SetActive(false);
        _currentCar = newCar;
        _currentCar.gameObject.SetActive(true);
        CarChanged?.Invoke(_currentCar);

        for (int i = 0; i < _cars.Count; i++)
        {
            if(_cars[i] == newCar)
            {
                _currentCarIndex = i;
            }
        }
    }

    public void ActivateNewGun(Gun newGun)
    {
        _currentGun.gameObject.SetActive(false);
        _currentGun = newGun;
        _currentGun.gameObject.SetActive(true);
        GunChanged?.Invoke(_currentGun);

        for (int i = 0; i < _guns.Count; i++)
        {
            if (_guns[i] == newGun)
            {
                _currentGunIndex = i;
            }
        }
    }
}
