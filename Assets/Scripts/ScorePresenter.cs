using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using TMPro;
using UnityEngine;

public class ScorePresenter : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _textMeshPro;
    [SerializeField] private Manipulator _manipulator;

    public int Score { get; private set; }


    private void AddScore(Item item)
    {
        if (item.GetType() != typeof(Fish)) return;

        Score += item.GetResource();
        _textMeshPro.text = "Score: " + Score;
    }

    private void OnEnable()
    {
        _manipulator.ItemRemoved += AddScore;
    }

    private void OnDisable()
    {
        _manipulator.ItemRemoved -= AddScore;
    }
}
