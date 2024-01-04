using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class EnemyBehaviour : MonoBehaviour
{
    [Header("Enemy Button Component")]
    [SerializeField] private Button _enemy;

    [Header("Enemy parameters")]
    [SerializeField] private string _name;
    [SerializeField] private int _scoreValue; 
    [SerializeField] private int _health;     
    [SerializeField] private float _chance;
    public float Chance
    {
        get { return _chance; }
        set { _chance = value; }
    }

    [Header("Enemy Tap Sounds")]
    [SerializeField] private List<int> tapSoundsList = new List<int>();
    public List<int> TapSoundsList
    {
        get { return tapSoundsList; }
    }

    [Header("Enemy Death Sounds")]
    [SerializeField] private List<int> deathSoundsList = new List<int>();
    public List<int> DeathSoundsList
    {
        get { return deathSoundsList; }
    }

    [Header("Particle Effect")]
    //[SerializeField] private ParticleSystem _particleEffect;

    private int _enemyIndex;                                            
    private EnemyGenerator _enemyGenerator;                           

    public void Init(int index, EnemyGenerator enemyGenerator)
    {            
        _enemyIndex = index;
        _enemyGenerator = enemyGenerator;
        _enemy.onClick.AddListener(EnemyTapped);
        AudioManager.Instance.AudioControlMethod(1, "Play", AudioManager.Instance.Sounds);
        AudioManager.Instance.AudioControlMethod(4, "Play", AudioManager.Instance.Sounds);
    }
    
    public void EnemyTapped()
    {
        PlayRandomEnemySound(TapSoundsList);
        _health--;

        if(_health == 0)
        {
            PlayRandomEnemySound(DeathSoundsList);
            Destroy(gameObject);  
            _enemyGenerator.EnemyKilled(_enemyIndex);
            ScoreManager.Instance.AddScore(_scoreValue);

            _enemyGenerator.EnemiesKilled++;

            //PlayParticleEffect();
        }                                                  
    }
    
/*
    public void PlayParticleEffect()
    {
        ParticleSystem particleEffect = Instantiate(_particleEffect, this.transform.position, Quaternion.identity);
        particleEffect.Play();
        Destroy(particleEffect, particleEffect.main.duration);
    }
*/
    public void PlayRandomEnemySound(List<int> enemySoundsList)
    {
        if(enemySoundsList.Count != 0)
        {
            int randomIndex = Random.Range(0, enemySoundsList.Count);
            int index = enemySoundsList[randomIndex];

            AudioManager.Instance.AudioControlMethod(index, "Play", AudioManager.Instance.Sounds);
        }
        
    }

}
