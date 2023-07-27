using TMPro;
using System;
using UnityEngine;
using DG.Tweening;

public class ScoreHandler : MonoBehaviour
{
    [SerializeField] private GameHandler gameHandler;
    [SerializeField] private GameController gameController;
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private TextMeshProUGUI scoreToAddText;
    private int previousScore;
    private int currentScore = 0;
    [Header("Animation settings")]
    [SerializeField] private float duration;
    [SerializeField] private float startAlpha;
    [SerializeField] private float endAlpha;
    [SerializeField] private Transform endPosition;
    private Sequence animSequence;

    private void Awake()
    {
        gameController = new GameController();

    }

    private void Start()
    {
        previousScore = gameController.GetScore();
        currentScore = previousScore;
        UpdateScoreText();
    }

    private void Update()
    {
        CheckIfScoreChanged();
    }
    private void CheckIfScoreChanged()
    {
        int newScore = gameController.GetScore();
        if (newScore == currentScore)
            return;
        previousScore = currentScore;
        currentScore = newScore;
        int scoreChange = currentScore - previousScore;
        AddScoreAnimSequence(scoreChange);

    }
    private void UpdateScoreText()
    {
        scoreText.text = currentScore.ToString();
        scoreToAddText.transform.position = Vector3.zero;
    }

    private void AddScoreAnimSequence(int amountToAdd)
    {
        scoreToAddText.text = amountToAdd.ToString();
        animSequence = DOTween.Sequence();
        animSequence.Append(scoreToAddText.DOFade(1f, duration / 2f))
            .Join(scoreToAddText.transform.DOMove(endPosition.position, duration, false))
            .Append(scoreToAddText.DOFade(0f, duration / 2f))
            .OnComplete(UpdateScoreText);
    }
}
