using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DifficultyController : MonoBehaviour
{
    [Header("Difficulty Tresholds")]
    [SerializeField] private List<int> tresholds = new List<int>();
    public List<int> Tresholds
    {
        get { return tresholds; }
    }

    private int _nextTreshold;
    public int NextTreshold
    {
        get { return _nextTreshold; }
        set { _nextTreshold = value; }
    }

    [SerializeField] private float _decrease = 0.1f;
    public float Decrease
    {
        get{ return _decrease; }
        set{ _decrease = value; }
    }

    [SerializeField] private float _minimumSpawnTime = 0.2f;
    public float MinimumSpawnTime
    {
        get{ return _minimumSpawnTime; }
        set{ _minimumSpawnTime = value; }
    }

    [SerializeField] private List<float> enemySpawnCountChances = new List<float>();
    public List<float> EnemySpawnCountChances
    {
        get { return enemySpawnCountChances; }
    }

    [SerializeField] private List<int> wormsToSpawnNumbers = new List<int>();
    public List<int> WormsToSpawnNumbers
    {
        get { return wormsToSpawnNumbers; }
    }

    private int _maxWormCount = 10;
    public int MaxWormCount
    {
        get { return _maxWormCount; }
        set { _maxWormCount = value; }
    }

    public static UnityEvent OnTresholdReached = new UnityEvent();

    public float DifficultyIncreasing(int time, float spawnTime)
    {
        if(NextTreshold == time)
        {
            spawnTime = ChangeSpawningSpeed(spawnTime);

            CalculateNextTreshold(time);
        }

        return spawnTime;
    }

    public void CalculateNextTreshold(int time)
    {
        for(int i = 0; i < Tresholds.Count; i++)
        {
            if(Tresholds[i] > time)
            {
                NextTreshold = Tresholds[i];
                break;
            }
        }
    }

    public float ChangeSpawningSpeed(float spawnTime)
    {
        spawnTime -= Decrease;

        if(spawnTime < MinimumSpawnTime)
        {
            spawnTime = MinimumSpawnTime;
        }

        return spawnTime;
    }

    //tuto metodu pridat
    public int NumberOfEnemiesToSpawn(int wormsActive, int wormsToSpawn)
    {
        float chance = Random.Range(0f, 100f);

        if(wormsActive >= _maxWormCount)
        {
            wormsToSpawn = WormsToSpawnNumbers[0];
            
        }
        else if(EnemySpawnCountChances.Count > 0 && WormsToSpawnNumbers.Count > 0)
        {
            if(chance < EnemySpawnCountChances[0])
            {
                wormsToSpawn = WormsToSpawnNumbers[0];
            }
            else if(chance < EnemySpawnCountChances[1])
            {
                wormsToSpawn = WormsToSpawnNumbers[1];
            }
            else if(chance < EnemySpawnCountChances[2])
            {
                wormsToSpawn = WormsToSpawnNumbers[2];
            }
        }
        else
        {
            Debug.LogError("EnemySpawnChances or WormsToSpawnNumbers is empty!");
        }

        return wormsToSpawn;
    }


    public EnemyBehaviour ChooseRandomEnemy(List<EnemyBehaviour> enemyPrefabs)
    {
        float[] prefixSum = new float[enemyPrefabs.Count];
        prefixSum[0] = enemyPrefabs[0].Chance;
        for (int i = 1; i < enemyPrefabs.Count; i++)
        {
            prefixSum[i] = prefixSum[i - 1] + enemyPrefabs[i].Chance;
        }

        float randomValue = Random.value * prefixSum[enemyPrefabs.Count - 1];
        for (int i = 0; i < enemyPrefabs.Count; i++)
        {
            if (randomValue < prefixSum[i])
            {
                return enemyPrefabs[i];
            }
        }

        return null;
    }

}
