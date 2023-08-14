using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using TMPro;
using UnityEngine;

public class Wallet : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _textMeshPro;
    [SerializeField] private Manipulator _manipulator;

    public int Score { get; private set; }


    private void AddScore(int amount)
    {
        Score += amount;
        _textMeshPro.text = "Score: " + Score;
    }

    private void OnEnable()
    {
        _manipulator.FishSold += AddScore;
    }

    private void OnDisable()
    {
        _manipulator.FishSold -= AddScore;
    }
}
