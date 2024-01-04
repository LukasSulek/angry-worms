using UnityEngine;

[System.Serializable]
public class Sound
{
    [SerializeField] private string _soundName;
    public string SoundName
    {
        get { return _soundName; }
    }

    [SerializeField] private AudioClip _audioClip;
    public AudioClip Clip
    {
        get {return _audioClip; }
    }

    [Range(0, 1)]
    [SerializeField] private int _volume;
    public int Volume
    {
        get { return _volume; }
        set { _volume = value; }
    }

    [Range(0.1f, 3f)]
    [SerializeField] private float _pitch = 1;
    public float Pitch
    {
        get { return _pitch; }
    }

    [SerializeField] private bool _loop;
    public bool Loop
    {
        get { return _loop; }
    }




    private AudioSource _source;

    public AudioSource Source
    {
        get { return _source; }
        set { _source = value; }
    }



}