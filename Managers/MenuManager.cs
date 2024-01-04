using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;
using TMPro;
using UnityEngine.UI;


public class MenuManager : MonoBehaviour
{
    private static MenuManager _instance;
    #region MenuManagerGetter
    public static MenuManager Instance
    {
        get 
        {
            if(_instance == null)
            {
                _instance = FindObjectOfType<MenuManager>();
            }

            return _instance; 
        }
    }
    #endregion MenuManagerGetter


    [Header("Main Menu")]
    [SerializeField] private GameObject _mainMenuPrefab;
    public GameObject MainMenuPrefab
    {
        get { return _mainMenuPrefab; }
    }

    [Header("Level Select Menu")]
    [SerializeField] private GameObject _levelSelectMenuPrefab;
    public GameObject LevelSelectMenuPrefab
    {
        get { return _levelSelectMenuPrefab; }
    }

    [Header("Settings Menu")]
    [SerializeField] private GameObject _settingsMenuPrefab;
    public GameObject SettingsMenuPrefab
    {
        get { return _settingsMenuPrefab; }
    }

    [Header("Leaderboard Menu")]
    [SerializeField] private GameObject _leaderboardMenuPrefab;
    public GameObject LeaderboardMenuPrefab
    {
        get { return _leaderboardMenuPrefab; }
    }

    [Header("Player Interface")]
    [SerializeField] private GameObject _playerInterfaceMenuPrefab;
    public GameObject PlayerInterfaceMenuPrefab
    {
        get { return _playerInterfaceMenuPrefab; }
    }

    [Header("Pause Menu")]
    [SerializeField] private GameObject _pauseMenuPrefab;
    public GameObject PauseMenuPrefab
    {
        get { return _pauseMenuPrefab; }
    }

    [Header("Game Over Menu")]
    [SerializeField] private GameObject _gameOverMenuPrefab;
    public GameObject GameOverMenuPrefab
    {
        get { return _gameOverMenuPrefab; }
    }

    [Header("Level Won Menu")]
    [SerializeField] private GameObject _levelWonMenuPrefab;
    public GameObject LevelWonMenuPrefab
    {
        get { return _levelWonMenuPrefab; }
    }

    [Header("Name Selection Menu")]
    [SerializeField] private GameObject _nameSelectionMenuPrefab;
    public GameObject NameSelectionMenu
    {
        get { return _nameSelectionMenuPrefab; }
    }


    private int _maxLevelReached = 1;
    public int MaxLevelReached
    {
        get { return _maxLevelReached;}
        set { _maxLevelReached = value; }
    }


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
        
    }

    void Start()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;

        if(ScoreManager.Instance.Highscore == 0)
        {
            ScoreManager.Instance.LoadHighscoreData();
        }

        LoadLevelProgressData();


        InstantiateMenuIfValid(MenuManager.Instance.MainMenuPrefab);
    }

    public void LoadScene(string sceneName, float time)
    {
        SceneManager.LoadScene(sceneName);
        Time.timeScale = time;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        switch(scene.buildIndex)
        {
            case 0:
                InstantiateMenuIfValid(MenuManager.Instance.MainMenuPrefab);
            break;

            default:
                InstantiateMenuIfValid(MenuManager.Instance.PlayerInterfaceMenuPrefab);
                AudioManager.Instance.AudioControlMethod(0, "Play", AudioManager.Instance.Music);
            break;
        }
    }

    public void SelectName()
    {
        GameObject nameSelectionMenu = Instantiate(_nameSelectionMenuPrefab, transform.position, Quaternion.identity);
        PlayerNameSelection playerNameSelection = nameSelectionMenu.GetComponent<PlayerNameSelection>();

        playerNameSelection.SavePlayerNameButton.onClick.AddListener( delegate { playerNameSelection.SavePlayerName(); });
    }

    public void UnlockNextLevel()
    {
        if(SceneManager.GetActiveScene().buildIndex - 1 == MaxLevelReached && MaxLevelReached <= 5)
        {
            MaxLevelReached++;
        }
    }

    public void AddMainMenuFunctionality(MainMenu mainMenu)
    {
        ScoreManager.Instance.ResetScore();
        ScoreManager.Instance.DisplayHighscore(mainMenu.HighscoreText);

        mainMenu.PlayEndlessButton.onClick.AddListener( delegate
        {
            AudioManager.Instance.AudioControlMethod(6, "Play", AudioManager.Instance.Sounds);
            LoadScene("Endless", 1);
        });

        mainMenu.LevelSelectMenuButton.onClick.AddListener( delegate
        {
            AudioManager.Instance.AudioControlMethod(7, "Play", AudioManager.Instance.Sounds);
            InstantiateMenuIfValid(LevelSelectMenuPrefab);
            FindObjectOfType<LevelSelectMenu>().GetComponentInChildren<LevelSelector>().InstantiateLevelButtons();

        });

        mainMenu.SettingsMenuButton.onClick.AddListener( delegate
        {
            AudioManager.Instance.AudioControlMethod(7, "Play", AudioManager.Instance.Sounds);
            InstantiateMenuIfValid(SettingsMenuPrefab);
        });

        mainMenu.LeaderboardMenuButton.onClick.AddListener( delegate
        {
            AudioManager.Instance.AudioControlMethod(7, "Play", AudioManager.Instance.Sounds);
            InstantiateMenuIfValid(LeaderboardMenuPrefab);
        });
    }


    public void AddLevelSelectMenuFunctionality(LevelSelectMenu levelSelectMenu)
    {
        levelSelectMenu.MainMenuButton.onClick.AddListener( delegate
        {
            AudioManager.Instance.AudioControlMethod(7, "Play", AudioManager.Instance.Sounds);
            DestroyMenu(levelSelectMenu);
        }); 
    }


    public void AddSettingsMenuFunctionality(SettingsMenu settingsMenu)
    {
        
        settingsMenu.MainMenuButton.onClick.AddListener( delegate
        {
            AudioManager.Instance.AudioControlMethod(7, "Play", AudioManager.Instance.Sounds);

            if(SceneManager.GetActiveScene().buildIndex > 0)
            {
                InstantiateMenuIfValid(PauseMenuPrefab);
                DestroyMenu(settingsMenu);
            }
            
            AudioManager.Instance.SaveVolumeSettingsToFile();
            DestroyMenu(settingsMenu);
        });

        settingsMenu.MusicButton.onClick.AddListener(delegate
        {
            AudioManager.Instance.AudioControlMethod(7, "Play", AudioManager.Instance.Sounds);

            AudioManager.Instance.ToggleMusic();
            AudioManager.Instance.ChangeMusicVolume();
        });

        settingsMenu.SoundButton.onClick.AddListener(delegate
        {
            AudioManager.Instance.AudioControlMethod(7, "Play", AudioManager.Instance.Sounds);

            AudioManager.Instance.ToggleSound();
            AudioManager.Instance.ChangeSoundVolume();
        });
    
    }

    public void AddLeaderboardMenuFunctionality(LeaderboardMenu leaderboardMenu)
    {
        leaderboardMenu.MainMenuButton.onClick.AddListener( delegate
        {
            AudioManager.Instance.AudioControlMethod(7, "Play", AudioManager.Instance.Sounds);

            DestroyMenu(leaderboardMenu);
        });
    }
    

    public void AddPlayerInterfaceMenuFunctionality(PlayerInterfaceMenu playerInterfaceMenu)
    {
        EnemyGenerator enemyGenerator = FindObjectOfType<EnemyGenerator>().GetComponent<EnemyGenerator>();

        CompletionGoal completionGoal = enemyGenerator.CompletionGoal;

        var type = "";

        if(completionGoal != null)
        {
            type = completionGoal.Type;
        }
        else
        {
            type = null;
        }

        switch(type)
        {
            case null:
            TextMeshProUGUI scoreText = Instantiate(playerInterfaceMenu.EndlessScoreText, playerInterfaceMenu.transform);

            scoreText.text = ScoreManager.Instance.Score.ToString();

            ScoreManager.OnScoreChanged.AddListener( delegate
            {
                ScoreManager.Instance.DisplayScore(scoreText);
            });
            break;

            case "KillEnemies":
            EnemyKillCounter enemyKillCounter = Instantiate(playerInterfaceMenu.EnemyKillCounter, playerInterfaceMenu.transform);

            enemyKillCounter.TargetEnemiesKilled.text = completionGoal.Amount.ToString();


            EnemyGenerator.OnWormKilled.AddListener( delegate 
            {
                enemyKillCounter.CurrentEnemiesKilled.text = enemyGenerator.EnemiesKilled.ToString();
            });
            break;

            case "TimeReached":
            TimeCounter timeCounter = Instantiate(playerInterfaceMenu.TimeCounter, playerInterfaceMenu.transform);

            timeCounter.TargetTime.text = completionGoal.Amount.ToString();


            EnemyGenerator.OnTimeIncrease.AddListener( delegate 
            {
                timeCounter.CurrentTime.text = enemyGenerator.TimePlaying.ToString();
            });
            break;

            case "TargetScore":
            ScoreCounter scoreCounter = Instantiate(playerInterfaceMenu.ScoreCounter, playerInterfaceMenu.transform);

            scoreCounter.TargetScore.text = completionGoal.Amount.ToString();


            ScoreManager.OnScoreChanged.AddListener( delegate
            {
                ScoreManager.Instance.DisplayScore(scoreCounter.CurrentScore);
            });

            break;

        }


        playerInterfaceMenu.PauseMenuButton.onClick.AddListener( delegate
        {
            AudioManager.Instance.AudioControlMethod(7, "Play", AudioManager.Instance.Sounds);
            InstantiateMenuIfValid(MenuManager.Instance.PauseMenuPrefab);

            Time.timeScale = 0;
            AudioManager.Instance.AudioControlMethod(0, "Pause", AudioManager.Instance.Music);
        });

        
    }
   

    public void AddPauseMenuFunctionality(PauseMenu pauseMenu)
    {
        pauseMenu.ContinueButton.onClick.AddListener( delegate
        {
            AudioManager.Instance.AudioControlMethod(7, "Play", AudioManager.Instance.Sounds);
            DestroyMenu(pauseMenu);


            Time.timeScale = 1;
            AudioManager.Instance.AudioControlMethod(0, "Resume", AudioManager.Instance.Music);
        });

        pauseMenu.RestartButton.onClick.AddListener( delegate
        {
            AudioManager.Instance.AudioControlMethod(7, "Play", AudioManager.Instance.Sounds);
            AudioManager.Instance.AudioControlMethod(0, "Stop", AudioManager.Instance.Music);
            LoadScene(SceneManager.GetActiveScene().name, 1);
            ScoreManager.Instance.ResetScore();
        });

        pauseMenu.MainMenuButton.onClick.AddListener(delegate
        {
            AudioManager.Instance.AudioControlMethod(7, "Play", AudioManager.Instance.Sounds);
            LoadScene("MainMenu", 0);
            AudioManager.Instance.AudioControlMethod(0, "Stop", AudioManager.Instance.Music);
        });

        pauseMenu.SettingsMenuButton.onClick.AddListener( delegate 
        {
            AudioManager.Instance.AudioControlMethod(7, "Play", AudioManager.Instance.Sounds);
            InstantiateMenuIfValid(MenuManager.Instance.SettingsMenuPrefab);
            DestroyMenu(pauseMenu);
        });
    }


    public void AddGameOverMenuFunctionality(GameOverMenu gameOverMenu)
    {
        EnemyGenerator enemyGenerator = FindObjectOfType<EnemyGenerator>().GetComponent<EnemyGenerator>();

        CompletionGoal completionGoal = enemyGenerator.CompletionGoal;

        var type = "";

        if(completionGoal != null)
        {
            type = completionGoal.Type;
        }
        else
        {
            type = null;
        }

        switch(type)
        {
            case null:
            ScoreCounter scoreCounter = Instantiate(gameOverMenu.EndlessCounter, gameOverMenu.transform);
            scoreCounter.CurrentScore.text = ScoreManager.Instance.Score.ToString();
            scoreCounter.TargetScore.text = ScoreManager.Instance.Highscore.ToString();
            break;

            case "KillEnemies":
            EnemyKillCounter enemyKillCounter = Instantiate(gameOverMenu.LevelKillCounter, gameOverMenu.transform);
            enemyKillCounter.TargetEnemiesKilled.text = completionGoal.Amount.ToString();
            enemyKillCounter.CurrentEnemiesKilled.text = enemyGenerator.EnemiesKilled.ToString();
            break;

            case "TimeReached":
            TimeCounter timeCounter = Instantiate(gameOverMenu.LevelTimeCounter, gameOverMenu.transform);
            timeCounter.TargetTime.text = completionGoal.Amount.ToString();
            timeCounter.CurrentTime.text = enemyGenerator.TimePlaying.ToString();
            break;

            case "TargetScore":
            ScoreCounter levelScoreCounter = Instantiate(gameOverMenu.LevelScoreCounter, gameOverMenu.transform);
            levelScoreCounter.CurrentScore.text = ScoreManager.Instance.Score.ToString();
            levelScoreCounter.TargetScore.text = completionGoal.Amount.ToString();
            break;
        }
        
        gameOverMenu.RetryButton.onClick.AddListener( delegate
        {
            AudioManager.Instance.AudioControlMethod(7, "Play", AudioManager.Instance.Sounds);
            LoadScene(SceneManager.GetActiveScene().name, 1);
            ScoreManager.Instance.ResetScore();
        });

        gameOverMenu.MainMenuButton.onClick.AddListener( delegate
        {
            AudioManager.Instance.AudioControlMethod(7, "Play", AudioManager.Instance.Sounds);
            LoadScene("MainMenu", 0);
            ScoreManager.Instance.LoadHighscoreData();
            AudioManager.Instance.AudioControlMethod(0, "Stop", AudioManager.Instance.Music);
        });
        
    }

    public void AddLevelWonMenuFunctionality(LevelWonMenu levelWonMenu)
    {
        levelWonMenu.RetryButton.onClick.AddListener( delegate 
        {
            AudioManager.Instance.AudioControlMethod(7, "Play", AudioManager.Instance.Sounds);
            FindObjectOfType<CompletionGoal>().IsCompleted = false;
            LoadScene(SceneManager.GetActiveScene().name, 1);
        });

        levelWonMenu.MainMenuButton.onClick.AddListener( delegate
        {
            AudioManager.Instance.AudioControlMethod(7, "Play", AudioManager.Instance.Sounds);
            LoadScene("MainMenu", 0);
            AudioManager.Instance.AudioControlMethod(0, "Stop", AudioManager.Instance.Music);
        });
    }



    public void DestroyMenu(GameMenu menu)
    {
        if(menu != null)
        {
            Destroy(menu.gameObject);
        }
    }
    
    public void CreateMenu(GameMenu menu) 
    {
        GameObject newMenu = Instantiate(menu.gameObject);
        GameMenu menuComponent = newMenu.GetComponent<GameMenu>();

        switch(menuComponent.GameMenuName)
        {
            case "MainMenu":
            AddMainMenuFunctionality((MainMenu) menuComponent);
            break;

            case "LevelSelectMenu": 
            AddLevelSelectMenuFunctionality((LevelSelectMenu) menuComponent);
            break;

            case "SettingsMenu": 
            AddSettingsMenuFunctionality((SettingsMenu) menuComponent);
            break;

            case "LeaderboardMenu": 
            AddLeaderboardMenuFunctionality((LeaderboardMenu) menuComponent);
            break;

            case "AchievementMenu": 
            //AddAchievementMenuFunctionality((AchievementMenu) menuComponent);
            break;

            case "PlayerInterfaceMenu": 
            AddPlayerInterfaceMenuFunctionality((PlayerInterfaceMenu) menuComponent);
            break;

            case "PauseMenu": 
            AddPauseMenuFunctionality((PauseMenu) menuComponent);
            break;

            case "GameOverMenu": 
            AddGameOverMenuFunctionality((GameOverMenu) menuComponent);
            break;

            case "LevelWonMenu":
            AddLevelWonMenuFunctionality((LevelWonMenu) menuComponent);
            break;

            default: 
            Debug.LogError("The name of the Menu is not matching!");
            break;
        }     
    } 

    public void InstantiateMenuIfValid(GameObject prefab)
    {
        GameMenu menu = prefab.GetComponent<GameMenu>();
 
        if(menu != null)
        {
            CreateMenu(menu);
        }
        else
        {
            Debug.LogError("MenuPrefab has no GameMenu script attached to it!");
        }
    }  

    public void LoadLevelProgressData()
    {
        PlayerData data = SaveSystem.LoadLevelProgressData(this);

        MaxLevelReached = data.MaxLevelReached; 
    }
    
    public void SaveLevelProgressToFile()
    {
        SaveSystem.SaveLevelProgress(MenuManager.Instance);
        
    }





}