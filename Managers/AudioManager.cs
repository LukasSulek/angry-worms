using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class AudioManager : MonoBehaviour
{  
    private static AudioManager _instance;
    #region MenuManagerGetter
    public static AudioManager Instance
    {
        get 
        {
            if(_instance == null)
            {
                _instance = FindObjectOfType<AudioManager>();
            }

            return _instance; 
        }
    }
    #endregion MenuManagerGetter

    private int _musicValue = 1;
    public int MusicValue
    {
        get { return _musicValue; }
        set { _musicValue = value; }
    }

    private int _soundValue = 1;
    public int SoundValue
    {
        get { return _soundValue; }
        set { _soundValue = value; }
    }

    [Header("Sounds")]
    [SerializeField] private List<Sound> sounds = new List<Sound>();
    public List<Sound> Sounds
    {
        get { return sounds; }
    }

    [Header("Music")]
    [SerializeField] private List<Sound> music = new List<Sound>();
    public List<Sound> Music
    {
        get { return music; }
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

        LoadVolumeSettingsData();

        AddAudioSource(Sounds);
        AddAudioSource(Music);
        
    }

    public void AddAudioSource(List<Sound> list)
    {   
        foreach(Sound sound in list)
        {
            sound.Source = gameObject.AddComponent<AudioSource>();
            sound.Source.clip = sound.Clip;

            sound.Source.volume = MusicValue;
            sound.Source.pitch = sound.Pitch;
            sound.Source.loop = sound.Loop;
        }
    }

    public void ToggleSound()
    {
        if (SoundValue == 1)
        {
            SoundValue = 0;
        }
        else if(SoundValue == 0)
        {
            SoundValue = 1;
        }
    }

    public void ToggleMusic()
    {
        if (MusicValue == 1)
        {
            MusicValue = 0;
        }
        else if(MusicValue == 0)
        {
            MusicValue = 1;
        }
    }


    public void ChangeSoundVolume()
    {
        foreach(Sound sound in Sounds)
        {
            sound.Source.volume = SoundValue;
        }
    }

    public void ChangeMusicVolume()
    {
        foreach(Sound sound in Music)
        {
            sound.Source.volume = MusicValue;
        }
    }

    public void AudioControlMethod(int index, string function, List<Sound> list)
    {
        Sound audio = list[index];

        switch(function)
        {
            case "Play":
                audio.Source.Play();
            break;

            case "Stop":
                audio.Source.Stop();
            break;

            case "Pause":
                audio.Source.Pause();
            break;

            case "Resume":
                audio.Source.UnPause();
            break;

            default:
                Debug.LogError("No audio function specified");
            break;
        }
    }

    public void SaveVolumeSettingsToFile()
    {
        SaveSystem.SaveVolumeSettingsToFile(AudioManager.Instance);
    }

    public void LoadVolumeSettingsData()
    {
        PlayerData volumeData = SaveSystem.LoadVolumeSettingsData(this);

        MusicValue = volumeData.MusicValue;
        SoundValue = volumeData.SoundValue;
    }

    void OnApplicationQuit()
    {
        SaveVolumeSettingsToFile();
    }

    

}
