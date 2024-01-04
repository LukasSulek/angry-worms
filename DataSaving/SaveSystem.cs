using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public static class SaveSystem
{
    public static void SaveHighscoreToFile(ScoreManager scoreManager)
    {
        BinaryFormatter binaryFormatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/highscoreData.dat";
        FileStream fileStream = new FileStream(path, FileMode.Create);

        PlayerData data = new PlayerData(scoreManager);

        binaryFormatter.Serialize(fileStream, data);
        fileStream.Close();
    }

    public static void SaveVolumeSettingsToFile(AudioManager audioManager)
    {
        BinaryFormatter binaryFormatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/volumeSettingsData.dat";
        FileStream fileStream = new FileStream(path, FileMode.Create);

        PlayerData data = new PlayerData(audioManager);

        binaryFormatter.Serialize(fileStream, data);
        fileStream.Close();
    }
	
    public static void SavePlayerNameToFile(DatabaseManager databaseManager)
    {
        string path = Application.persistentDataPath + "/playerNameData.dat";

        BinaryFormatter binaryFormatter = new BinaryFormatter();
        FileStream fileStream = new FileStream(path, FileMode.Create);

        PlayerData data = new PlayerData(databaseManager);

        binaryFormatter.Serialize(fileStream, data);
        fileStream.Close();
    }

    public static void SaveLevelProgress(MenuManager menuManager)
    {
        string path = Application.persistentDataPath + "/levelProgress.dat";

        BinaryFormatter binaryFormatter = new BinaryFormatter();
        FileStream fileStream = new FileStream(path, FileMode.Create);

        PlayerData data = new PlayerData(menuManager);

        binaryFormatter.Serialize(fileStream, data);
        fileStream.Close();
    }


    



    public static PlayerData LoadHighscoreData(ScoreManager scoreManager)
    {
        string path = Application.persistentDataPath + "/highscoreData.dat";

        if(File.Exists(path))
        {
            BinaryFormatter binaryFormatter = new BinaryFormatter();
            FileStream fileStream = new FileStream(path, FileMode.Open);

            PlayerData data = binaryFormatter.Deserialize(fileStream) as PlayerData;

            fileStream.Close();

            return data;
        }
        else
        {
            PlayerData data = new PlayerData(scoreManager);
            return data;
        }
    }

    public static PlayerData LoadVolumeSettingsData(AudioManager audioManager)
    {
        string path = Application.persistentDataPath + "/volumeSettingsData.dat";

        if(File.Exists(path))
        {
            BinaryFormatter binaryFormatter = new BinaryFormatter();
            FileStream fileStream = new FileStream(path, FileMode.Open);

            PlayerData data = binaryFormatter.Deserialize(fileStream) as PlayerData;

            fileStream.Close();

            return data;
        }
        else
        {
            PlayerData data = new PlayerData(audioManager);
            return data;
        }
    }


    public static PlayerData LoadPlayerNameData(DatabaseManager databaseManager)
    {
        string path = Application.persistentDataPath + "/playerNameData.dat";

        if(File.Exists(path))
        {
            BinaryFormatter binaryFormatter = new BinaryFormatter();
            FileStream fileStream = new FileStream(path, FileMode.Open);

            PlayerData data = binaryFormatter.Deserialize(fileStream) as PlayerData;

            fileStream.Close();

            return data;
        }
        else
        {
            PlayerData data = new PlayerData(databaseManager);
            return data;
        }
    }
    
    public static PlayerData LoadLevelProgressData(MenuManager menuManager)
    {
        string path = Application.persistentDataPath + "/levelProgress.dat";

        if(File.Exists(path))
        {
            BinaryFormatter binaryFormatter = new BinaryFormatter();
            FileStream fileStream = new FileStream(path, FileMode.Open);

            PlayerData data = binaryFormatter.Deserialize(fileStream) as PlayerData;

            fileStream.Close();

            return data;
        }
        else
        {
            PlayerData data = new PlayerData(menuManager);
            return data;
        }
    }
    
}
