using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class EnemyGenerator : MonoBehaviour
{
    [SerializeField] private SpawnPositions _spawnPositions;
    [SerializeField] private DifficultyController _difficultyController;

    [Header("Tiles")]
    [SerializeField] private GameObject _tile;
    [SerializeField] private Canvas _tileCanvas;
    public Canvas TileCanvas
    {
        get { return _tileCanvas; }
    }

    [Header("Enemies")]
    [SerializeField] private List<EnemyBehaviour> enemyPrefabs = new List<EnemyBehaviour>();
    public List<EnemyBehaviour> EnemyPrefabs
    {
        get { return enemyPrefabs; }
    }

    [SerializeField] private Canvas _enemyCanvas;
    public Canvas EnemyCanvas
    {
        get { return _enemyCanvas; }
    }

    [Header("Vegetables")]
    [SerializeField] private List<VegetableBehaviour> vegetablePrefabs = new List<VegetableBehaviour>();
    public List<VegetableBehaviour> VegetablePrefabs
    {
        get { return vegetablePrefabs; }
    }                         
   
    [SerializeField] private Canvas _vegetableCanvas;
    public Canvas VegetableCanvas
    {
        get { return _vegetableCanvas; }
    }

    [Header("Active Worms")]
    private int _wormsActive = 0;
    public int WormsActive
    {
        get { return _wormsActive; }
        set
        {
            _wormsActive = value;

            if(WormsActive >= MaxWormsActive)
            {
                OnGameOver?.Invoke();
            }
        }
    }

    private int _maxWormsActive;
    public int MaxWormsActive
    {
        get { return _maxWormsActive; }
        set { _maxWormsActive = value; }
    }  
                                         
    [Header("Spawning")]
    [SerializeField] private int _wormsToSpawn = 1;
    public int WormsToSpawn
    {
        get { return _wormsToSpawn; }
        set { _wormsToSpawn = value; }
    }

    [SerializeField] private float _spawnTime = 1f;                                                 
    public float SpawnTime
    {
        get { return _spawnTime; }
        set { _spawnTime = value; }
    }

    private int _timePlaying = 0;
    public int TimePlaying
    {
        get { return _timePlaying; }
        set
        {
            _timePlaying = value;

            if(_difficultyController.NextTreshold == _timePlaying)
            {
                DifficultyController.OnTresholdReached?.Invoke();
            }

            OnTimeIncrease?.Invoke();
            OnTimeReached?.Invoke();
        }
    }

    private List<int> indexes = new List<int>();

    private List<VegetableBehaviour> activeVegetables = new List<VegetableBehaviour>();
    public List<VegetableBehaviour> ActiveVegetables 
    {
        get { return activeVegetables; }
    }

    private EnemyBehaviour _chosenEnemyPrefab;
    public EnemyBehaviour ChosenEnemyPrefab
    {
        get { return _chosenEnemyPrefab; }
        set { _chosenEnemyPrefab = value; }
    }

    private VegetableBehaviour _chosenVegetablePrefab;
    public VegetableBehaviour ChosenVegetablePrefab
    {
        get { return _chosenVegetablePrefab; }
        set { _chosenVegetablePrefab = value; }
    }

    private int _enemiesKilled = 0;
    public int EnemiesKilled
    {
        get { return _enemiesKilled; }
        set
        {
            _enemiesKilled = value;
            //OnWormsKilledChanged?.Invoke();
            OnWormKilled?.Invoke();
        }
    }

    //public static UnityEvent OnWormsKilledChanged = new UnityEvent();
    public static UnityEvent OnGameOver = new UnityEvent();

    public static UnityEvent OnCompletionGoalComplete = new UnityEvent();
    
    public static UnityEvent OnWormKilled = new UnityEvent();
    public static UnityEvent OnTimeReached = new UnityEvent();
    public static UnityEvent OnTimeIncrease = new UnityEvent();


    [Header("Completion Goals")]
    [SerializeField] private CompletionGoal _completionGoal = null;
    public CompletionGoal CompletionGoal
    {
        get { return _completionGoal; }
    }

    public IEnumerator GameTimer()
    {
        while(true)
        {
            yield return new WaitForSeconds(1);

            TimePlaying++;
        }
    }

    void Setup()
    {
        ChosenEnemyPrefab = EnemyPrefabs[0];
        ChosenVegetablePrefab = VegetablePrefabs[0];


        if(_spawnPositions.UseAutoGenerating == true)
        {
            _spawnPositions.AutoGenerateSpawnPositions();
        }
        else
        {
            _spawnPositions.ConvertPositionsToDifferentResolutions();
        }

        for(int i = 0; i < _spawnPositions.SpawnPositionsList.Count; i++)
        {
            
            indexes.Add(i);
            Instantiate(_tile, _spawnPositions.SpawnPositionsList[i], Quaternion.identity, TileCanvas.transform);
            
            GrowVegetable(indexes[i], i);
        }

        _difficultyController.MaxWormCount = _spawnPositions.SpawnPositionsList.Count - 5;
        MaxWormsActive = _spawnPositions.SpawnPositionsList.Count;

        DifficultyController.OnTresholdReached.AddListener( delegate { SpawnTime = _difficultyController.DifficultyIncreasing(TimePlaying, SpawnTime); });
        OnGameOver.AddListener( delegate { GameOver(); });

        _difficultyController.NextTreshold = _difficultyController.Tresholds[0];

        if(_completionGoal != null)
        {
            SetupLevelGoals();
       
            OnCompletionGoalComplete.AddListener( delegate
            {
                if(_completionGoal.IsCompleted == true)
                {
                    LevelWon();
                }
            });
        }

    }

    public void SetupLevelGoals()
    {
        switch(_completionGoal.Type)
        {
            case "KillEnemies":
            OnWormKilled.AddListener(delegate { _completionGoal.GoalCompletion(); });
            break;

            case "TimeReached":
            OnTimeReached.AddListener(delegate { _completionGoal.GoalCompletion(); });
            break;

            case "TargetScore":
            ScoreManager.OnScoreChanged.AddListener(delegate { _completionGoal.GoalCompletion(); });
            break;
        }
    }

    void Start()
    {        
        Setup();

        MaxWormsActive = indexes.Count;

        StartCoroutine(GameTimer());
        StartCoroutine(LateStart(1f, StartGeneratingEnemies()));
    }


    IEnumerator LateStart(float delay, IEnumerator Coroutine)
    {
        yield return new WaitForSeconds(delay);
        StartCoroutine(Coroutine);  
    }

    private IEnumerator StartGeneratingEnemies()
    {
        while(true)
        {
            WormsToSpawn = _difficultyController.NumberOfEnemiesToSpawn(WormsActive, WormsToSpawn);
            SpawnEnemies();
            
            yield return new WaitForSeconds(SpawnTime);
        }
    }

    public void EnemyKilled(int index)
    {
        WormsActive--;
        StartCoroutine(GrowVegetableBack(index));
        
    }
 
    public IEnumerator GrowVegetableBack(int index)
    {
        yield return new WaitForSeconds(0.16f);
        GrowVegetable(index, index);
        
        
        indexes.Add(index);
        yield return new WaitForSeconds(0.03f);
    }

    void GrowVegetable(int index, int positionIndex)
    {
        ChosenVegetablePrefab = ChooseRandomVegetable(ChosenVegetablePrefab, VegetablePrefabs);

        VegetableBehaviour vegetable = Instantiate(ChosenVegetablePrefab, _spawnPositions.SpawnPositionsList[positionIndex], Quaternion.identity, VegetableCanvas.transform);

        vegetable.Init(index, this);

        ActiveVegetables.Add(vegetable);  
    }

    public void SpawnEnemies()
    {
        for (int i = 0; i < _wormsToSpawn; i++)
        {
            if(enemyPrefabs!= null)
            {
                ChosenEnemyPrefab = _difficultyController.ChooseRandomEnemy(enemyPrefabs);
                
                int _randomIndex = indexes[Random.Range(0, indexes.Count)];

                Vector2 _wormPosition = (_spawnPositions.SpawnPositionsList[_randomIndex]);

                indexes.Remove(_randomIndex);

                for(int j = 0; j < ActiveVegetables.Count; j++)
                {
                    if(ActiveVegetables[j].VegetableIndex == _randomIndex)
                    {
                        Destroy(ActiveVegetables[j].gameObject);

                        ActiveVegetables.Remove(ActiveVegetables[j]);
                    }
                }

                EnemyBehaviour worm = Instantiate(ChosenEnemyPrefab, _wormPosition, Quaternion.identity, EnemyCanvas.transform);
                worm.Init(_randomIndex, this);

                WormsActive++;
            }
                 
        }
    }
    
    public VegetableBehaviour ChooseRandomVegetable(VegetableBehaviour chosenVegetablePrefab, List<VegetableBehaviour> vegetablePrefabs)
    {
        float chance = Random.Range(0f, 100f);

        if(vegetablePrefabs.Count >= 1)
        {
            if(chance < vegetablePrefabs[0].Chance)
            {
                chosenVegetablePrefab = vegetablePrefabs[0];
            }
            else if(chance < vegetablePrefabs[1].Chance)
            {
                chosenVegetablePrefab = vegetablePrefabs[1];
            }
            else if(chance < vegetablePrefabs[2].Chance)
            {
                chosenVegetablePrefab = vegetablePrefabs[2];
            }
        }
        else
        {
            Debug.LogError("No Vegetable Prefabs Available!");
        }

        return chosenVegetablePrefab;
    }

    public void GameOver()
    {
        if(SceneManager.GetActiveScene().buildIndex == 1)
        {
            ScoreManager.Instance.UpdateHighscore();
            ScoreManager.Instance.SaveHighscoreToFile();

            DatabaseManager.Instance.SaveToDatabase();
        }
        
        AudioManager.Instance.AudioControlMethod(10, "Play", AudioManager.Instance.Sounds);
        AudioManager.Instance.AudioControlMethod(0, "Stop", AudioManager.Instance.Music);
        AudioManager.Instance.AudioControlMethod(1, "Play", AudioManager.Instance.Sounds);

        MenuManager.Instance.InstantiateMenuIfValid(MenuManager.Instance.GameOverMenuPrefab);
        Time.timeScale = 0;
    }

    public void LevelWon()
    {
        MenuManager.Instance.UnlockNextLevel();

        AudioManager.Instance.AudioControlMethod(9, "Play", AudioManager.Instance.Sounds);
        AudioManager.Instance.AudioControlMethod(0, "Stop", AudioManager.Instance.Music);

        
        MenuManager.Instance.InstantiateMenuIfValid(MenuManager.Instance.LevelWonMenuPrefab);
        Time.timeScale = 0;

        MenuManager.Instance.SaveLevelProgressToFile();

    }
}



    



