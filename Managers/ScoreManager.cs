using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using UnityEngine.Events;

public class ScoreManager : MonoBehaviour
{
    private static ScoreManager _instance;
    public static ScoreManager Instance
    {
        get 
        {
            if(_instance == null)
            {
                _instance = FindObjectOfType<ScoreManager>();
            }

            return _instance; 
        }
    }
    
    private int _score = 0;
    public int Score
    {
        get { return _score; }
        set 
        { 
            _score = value;
            OnScoreChanged?.Invoke();
        }
    }

    private int _scoreMultiplier = 1;
    public int ScoreMultiplier
    {
        get { return _scoreMultiplier; }
        set { _scoreMultiplier = value; }
    }
    
    private int _highscore = 0;
    public int Highscore
    {
        get {return _highscore; }
        set { _highscore = value; }
    }

    public static UnityEvent OnScoreChanged = new UnityEvent();

    void Awake()
    {
        #region Singleton
        if(_instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(Instance);
        }
        else
        {
            Destroy(gameObject);
        }
        #endregion Singleton

        ResetScore();
    }
   
    public void AddScore(int scoreValue)
    {
        Score += scoreValue * _scoreMultiplier;
    }

    public void UpdateHighscore()
    {
        if(Highscore < Score)
        {
            Highscore = Score;
            SaveHighscoreToFile();
            DatabaseManager.Instance.SaveToDatabase();       
        }
    }

    public void DisplayScore(TextMeshProUGUI scoreText)
    {
        scoreText.text = ScoreManager.Instance.Score.ToString();
    }

    public void DisplayHighscore(TextMeshProUGUI highscoreText)
    {
        highscoreText.text = ScoreManager.Instance.Highscore.ToString();
    }


    public void ResetScore()
    {
        UpdateHighscore();
        ScoreManager.Instance.Score = 0;
    }

   public void LoadHighscoreData()
    {
        PlayerData data = SaveSystem.LoadHighscoreData(this);

        ScoreManager.Instance.Highscore = data.Highscore; 
    }

    public void SaveHighscoreToFile()
    {
        SaveSystem.SaveHighscoreToFile(ScoreManager.Instance);
    }

    void OnApplicationQuit()
    {
        SaveHighscoreToFile();
    }

}


