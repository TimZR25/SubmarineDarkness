using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(AudioSource))]
public class OxygenHandler : MonoBehaviour
{
    [SerializeField] private GameObject _restartUI;

    [SerializeField] private Manipulator _manipulator;
    [SerializeField] private Image timerImage;

    [SerializeField] private float _maxOxygen;
    public float CurrentOxygen { get; private set; }

    public Action OnDead;

    private AudioSource _audioSource;

    private void Start()
    {
        _audioSource = GetComponent<AudioSource>();

        CurrentOxygen = _maxOxygen;
        StartCoroutine(RemoveOxygen());
    }

    private void AddOxygen(Item item)
    {
        if (item.GetType() != typeof(Oxygen)) return;

        _audioSource.Play();
        if (item.GetResource() + CurrentOxygen >= _maxOxygen)
        {
            CurrentOxygen = _maxOxygen;
        }
        else
        {
            CurrentOxygen += item.GetResource();
        }
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

        if (CurrentOxygen <= 0)
        {
            OnDead?.Invoke();
            _restartUI.SetActive(true);
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
