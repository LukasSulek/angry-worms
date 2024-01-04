using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Events;

public class PowerUpSlider : MonoBehaviour
{
    /*
    private EnemyGenerator _enemyGenerator;
    [SerializeField] private Slider _wormCountSlider;
    public Slider WormCountSlider
    {
        get { return _wormCountSlider; }
    }

    [SerializeField] private TextMeshProUGUI _currentValueText;
    [SerializeField] private TextMeshProUGUI _targetValueText;


    private int _currentValue = 0;
    public int CurrentValue
    {
        get { return _currentValue; }
        set
        {
            _currentValue = Mathf.Min(value, TargetValue);

            OnCurrentValueChanged?.Invoke();
        }
    }

    [SerializeField] private int _targetValue = 50;
    public int TargetValue
    {
        get { return _targetValue; }
        set { _targetValue = value; }
    }

    private bool _powerUpInteractable = false;
    public bool PowerUpInteractable
    {
        get { return _powerUpInteractable; } 
        set { _powerUpInteractable = value; }
    }

    public static UnityEvent OnCurrentValueChanged = new UnityEvent();

    void Awake()
    {
        _enemyGenerator = FindObjectOfType<EnemyGenerator>().GetComponent<EnemyGenerator>();
    }
    
    void Start()
    {
        WormCountSlider.value = 0;
        _wormCountSlider.maxValue = TargetValue;
        UpdateValueText(_targetValueText, TargetValue);

        EnemyGenerator.OnWormsKilledChanged.AddListener( delegate { CurrentValue++; });
        PowerUpSlider.OnCurrentValueChanged.AddListener( delegate { CompareValues(); UpdateSlider(); });
    }

    public void UpdateSlider()
    {
        WormCountSlider.value = CurrentValue;

        UpdateValueText(_currentValueText, CurrentValue);
    }

    public void UpdateValueText(TextMeshProUGUI text, int value)
    {
        text.text = value.ToString();
    }
    
    public void CompareValues()
    {
        if(CurrentValue >= TargetValue)
        {
            PowerUpInteractable = true;
        }
        else 
        {
            PowerUpInteractable = false;
        }
    }
*/

}
