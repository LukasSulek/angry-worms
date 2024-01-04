using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerNameSelection : MonoBehaviour
{
    [SerializeField] private TMP_InputField _playerNameInputField;
    public TMP_InputField PlayerNameInputField
    {
        get { return _playerNameInputField; }
    }

    [SerializeField] private Button _savePlayerNameButton;
    public Button SavePlayerNameButton
    {
        get { return _savePlayerNameButton; }
    }

    [SerializeField] private TextMeshProUGUI _wrongNameText;
    public TextMeshProUGUI WrongNameText
    {
        get { return _wrongNameText; }
    }

    public void SavePlayerName()
    {
        if(PlayerNameInputField.text.Length >= 4 && PlayerNameInputField.text.Length <= 12)
        {
            DatabaseManager.Instance.PlayerName = PlayerNameInputField.text;
            DatabaseManager.Instance.SaveToDatabase();
            DatabaseManager.Instance.SavePlayerNameToFile();
            Destroy(this.gameObject);
        }
        else if(PlayerNameInputField.text.Length < 4 || PlayerNameInputField.text.Length > 12)
        {
            StartCoroutine(DisplayMessage("The name must contain 4 - 12 characters!"));
            return;
        }
    }

    public IEnumerator DisplayMessage(string message)
    {
        WrongNameText.text = message;

        yield return new WaitForSeconds(3f);

        WrongNameText.text = "";
    }




}
