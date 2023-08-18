using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class OxygenHandler : MonoBehaviour
{
    [SerializeField] private Manipulator _manipulator;
    [SerializeField] private Image timerImage;

    [SerializeField] private float _maxOxygen;
    public float CurrentOxygen { get; private set; }


    private void Start()
    {
        CurrentOxygen = _maxOxygen;
        StartCoroutine(RemoveOxygen());
    }

    private void AddOxygen(Item item)
    {
        if (item.GetType() != typeof(Oxygen)) return;

        CurrentOxygen += item.GetResource();
    }

    private IEnumerator RemoveOxygen()
    {
        while (CurrentOxygen > 0)
        {
            CurrentOxygen -= Time.deltaTime;
            var normalizedValue = Mathf.Clamp(CurrentOxygen / _maxOxygen, 0.0f, 1.0f);
            timerImage.fillAmount = normalizedValue;
            yield return null;
        }
    }

    private void OnEnable()
    {
        _manipulator.ItemRemoved += AddOxygen;
    }

    private void OnDisable()
    {
        _manipulator.ItemRemoved -= AddOxygen;
    }
}
