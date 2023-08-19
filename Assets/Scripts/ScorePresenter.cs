using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using TMPro;
using UnityEngine;

public class ScorePresenter : MonoBehaviour
{
    [SerializeField] private OxygenHandler _oxygenHandler;
    [SerializeField] private TextMeshProUGUI _score;
    [SerializeField] private TextMeshProUGUI _bestScore;
    [SerializeField] private Manipulator _manipulator;
    [SerializeField] private string _nameScore = "BestScore";
    public int Score { get; private set; }
    public int BestScore { get; private set; }
    private AudioSource _audioSource;

    private void Start()
    {
        _audioSource = GetComponent<AudioSource>();
        BestScore = PlayerPrefs.GetInt(_nameScore);
    }

    private void AddScore(Item item)
    {
        if (item.GetType() != typeof(Fish)) return;

        _audioSource.Play();
        Score += item.GetResource();
        _score.text = "SCORE: " + Score;
    }

    private void SetBestScore()
    {
        if (Score > BestScore)
        {
            BestScore = Score;
            PlayerPrefs.SetInt(_nameScore, BestScore);
        }

        _bestScore.text = "BEST SCORE: " + BestScore;
    }

    private void OnEnable()
    {
        _manipulator.ItemRemoved += AddScore;
        _oxygenHandler.OnDead += SetBestScore;
    }

    private void OnDisable()
    {
        _manipulator.ItemRemoved -= AddScore;
        _oxygenHandler.OnDead -= SetBestScore;
    }
}
