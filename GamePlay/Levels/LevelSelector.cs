using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelSelector : MonoBehaviour
{
    [SerializeField] private LevelButton _levelButton;
    public LevelButton LevelButton
    {
        get { return _levelButton; }
    }


    private List<LevelButton> levelButtons = new List<LevelButton>();
    public List<LevelButton> LevelButtons 
    {
        get { return levelButtons; }
    }

    [SerializeField] private List<string> levelNames = new List<string>();
    public List<string> LevelNames
    {
        get { return levelNames; }
    }

   

    [SerializeField] private GameObject _levelButtonsParent;
    
    public void InstantiateLevelButtons()
    {
        int levelIndex = 0;

        foreach(string levelName in LevelNames)
        {
            LevelButton levelButton = Instantiate(_levelButton, transform.position, Quaternion.identity, _levelButtonsParent.transform);
            if(levelIndex + 1 <= MenuManager.Instance.MaxLevelReached)
            {

                levelButton.LevelNumberText.text = (levelIndex + 1).ToString();
                levelButton.LockImage.SetActive(false);
                levelButton.ButtonComponent.onClick.AddListener( delegate
                {
                    MenuManager.Instance.LoadScene(levelName, 1);
                    AudioManager.Instance.AudioControlMethod(6, "Play", AudioManager.Instance.Sounds);
                });
            }
            else
            {
                levelButton.LevelNumberText.gameObject.SetActive(false);
                levelButton.ButtonComponent.onClick.AddListener( delegate { AudioManager.Instance.AudioControlMethod(8, "Play", AudioManager.Instance.Sounds); });
            }

            levelIndex++;
        }
    }

   





}
