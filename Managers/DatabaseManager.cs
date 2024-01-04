using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase.Database;
using System;
using TMPro;
using System.Linq;
using UnityEngine.UI;

public class DatabaseManager : MonoBehaviour
{
    private static DatabaseManager _instance;
    #region DatabaseManagerGetter
    public static DatabaseManager Instance
    {
        get 
        {
            if(_instance == null)
            {
                _instance = FindObjectOfType<DatabaseManager>();
            }

            return _instance; 
        }
    }
    #endregion DatabaseManagerGetter

    private DatabaseReference _dbReference;
    public DatabaseReference DbReference
    {
        get { return _dbReference; }
        set { _dbReference = value; }
    }

    private string _userID;

    private string _playerName;
    public string PlayerName
    {
        get { return _playerName; }
        set { _playerName = value; }
    }

    [SerializeField] private LeaderboardElement _leaderboardElement;

    [SerializeField] private int _maxPlayersInLeaderboard;
    public int MaxPlayersInLeaderboard
    {
        get { return _maxPlayersInLeaderboard; }
        set { _maxPlayersInLeaderboard = value; }
    }

    /*
    private List<LeaderboardElement> leaderboardElements;
    public List<LeaderboardElement> LeaderboardElements
    {
        get { return leaderboardElements; }
    }
    */


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

        _userID = SystemInfo.deviceUniqueIdentifier;
        DbReference = FirebaseDatabase.DefaultInstance.RootReference;
    }

    void Start()
    {
        if(PlayerName == null)
        {
            LoadPlayerNameData();

            if(PlayerName == null)
            {
                MenuManager.Instance.SelectName();
            }
        }

        SaveToDatabase();
    }

    public void SaveToDatabase()
    {
        User newPlayerData = new User(PlayerName, ScoreManager.Instance.Highscore);
        string json = JsonUtility.ToJson(newPlayerData);

        _dbReference.Child("users").Child(_userID).SetRawJsonValueAsync(json);

        UpdateDatabase();
    }

    public IEnumerator GetNameFromDatabase(Action<string> OnCallback)
    {
        var userNameData = _dbReference.Child("users").Child(_userID).Child("name").GetValueAsync();
        yield return new WaitUntil(predicate: () => userNameData.IsCompleted);

        if(userNameData != null)
        {
            DataSnapshot snapshot = userNameData.Result;

            OnCallback?.Invoke(snapshot.Value.ToString());
        }
    }

    public IEnumerator GetHighscoreFromDatabase(Action<int> OnCallback)
    {
        var userHighscoreData = _dbReference.Child("users").Child(_userID).Child("highscore").GetValueAsync();
        yield return new WaitUntil(predicate: () => userHighscoreData.IsCompleted);

        if(userHighscoreData != null)
        {
            DataSnapshot snapshot = userHighscoreData.Result;

            OnCallback?.Invoke(int.Parse(snapshot.Value.ToString()));
        }
    }


    private void UpdateDatabase()
    {
        if(PlayerName != null)
        {
            _dbReference.Child("users").Child(_userID).Child("name").SetValueAsync(PlayerName);
            UpdateHighscore();
        }
    }

    private void UpdateHighscore()
    {
        _dbReference.Child("users").Child(_userID).Child("highscore").SetValueAsync(ScoreManager.Instance.Highscore);
    }

    public void LoadPlayerNameData()
    {
        PlayerData data = SaveSystem.LoadPlayerNameData(this);

        PlayerName = data.PlayerName; 
    }
    
    public void SavePlayerNameToFile()
    {
        SaveSystem.SavePlayerNameToFile(this);
        
    }

    void OnApplicationQuit()
    {
        SavePlayerNameToFile();
        DatabaseManager.Instance.SaveToDatabase();
    }

    public void GetName()
    {
        StartCoroutine(GetNameFromDatabase((string name) => { PlayerName = name; }));
    }

    public void GetHighscore()
    {
        StartCoroutine(GetHighscoreFromDatabase((int highscore) => { ScoreManager.Instance.Highscore = highscore; }));
    }

    public IEnumerator LoadLeaderboardData()
    {
        Transform parent = FindObjectOfType<VerticalLayoutGroup>().gameObject.transform;

        var leaderboardData = _dbReference.Child("users").OrderByChild("highscore").GetValueAsync();
        yield return new WaitUntil(predicate: () => leaderboardData.IsCompleted);

        DataSnapshot dataSnapshot = leaderboardData.Result;
        
        int playerCount = 1;
        
        foreach(DataSnapshot childSnapshot in dataSnapshot.Children.Reverse<DataSnapshot>())
        {
            string playerName = childSnapshot.Child("name").Value.ToString();
            int highscore = Convert.ToInt32(childSnapshot.Child("highscore").Value.ToString());

            LeaderboardElement leaderboardElement = Instantiate(_leaderboardElement, new Vector2(parent.position.x, parent.position.y), Quaternion.identity, parent);

            leaderboardElement.PositionText.text = playerCount.ToString();
            leaderboardElement.PlayerNameText.text = playerName;
            leaderboardElement.PlayerHighscoreText.text = highscore.ToString();

            playerCount++;

            if(playerCount > MaxPlayersInLeaderboard && MaxPlayersInLeaderboard != 0)
            {
                break;
            }   
        }
    }

}
