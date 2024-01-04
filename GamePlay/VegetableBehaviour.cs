using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VegetableBehaviour : MonoBehaviour
{
    [SerializeField] private Button _vegetableButtonComponent;
    [SerializeField] private string _name; 

    [SerializeField] private float _chance;
    public float Chance
    {
        get { return _chance; }
        set { _chance = value; }
    }




    [SerializeField] private List<int> vegetableTapSounds = new List<int>();
    public List<int> VegetableTapSounds
    {
        get { return vegetableTapSounds; }
    }
    
    [SerializeField] private List<int> vegetableEatenSounds = new List<int>();
    public List<int> VegetableEatenSounds
    {
        get { return vegetableEatenSounds; }
    }
    
    private int _vegetableIndex;
    public int VegetableIndex
    {
        get { return _vegetableIndex; }
    }                                      
    private EnemyGenerator _enemyGenerator;                           


    public void Init(int index, EnemyGenerator enemyGenerator)
    {            
        _vegetableIndex = index;
        _enemyGenerator = enemyGenerator;


        _vegetableButtonComponent.onClick.AddListener(VegetableTapped);

    }

    public void VegetableTapped()
    {
        PlayRandomVegetableSound(VegetableTapSounds); 
        EnemyGenerator.OnGameOver?.Invoke();
        Destroy(gameObject);  
    }

    public void PlayRandomVegetableSound(List<int> enemySoundsList)
    {
        int randomIndex = Random.Range(0, enemySoundsList.Count);

        int index = enemySoundsList[randomIndex];

        AudioManager.Instance.AudioControlMethod(index, "Play", AudioManager.Instance.Sounds);
    }


}


    
