using System;
using UnityEngine;

[Serializable]
public class GameScreen
{
    [SerializeField] private GameObject[] _activate;
    [SerializeField] private GameObject[] _deactivate;
    [SerializeField] private bool _showed = false;

    public void ChangeState()
    {
        _showed = !_showed;

        for (int i = 0; i < _activate.Length; i++)
        {
            _activate[i].SetActive(_showed);
        }

        for (int i = 0; i < _deactivate.Length; i++)
        {
            _deactivate[i].SetActive(!_showed);
        }
    }
}