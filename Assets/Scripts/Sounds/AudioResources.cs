using UnityEngine;

public class AudioResources : MonoBehaviour
{
    public static AudioResources Instance;

    [SerializeField] private Sound[] _sounds;

    private bool _isMute = false;
    private const string MainTheme = "MainTheme";

    public bool IsMute => _isMute;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);

        foreach (Sound sound in _sounds)
        {
            sound.Source = gameObject.AddComponent<AudioSource>();
            sound.Source.clip = sound.AudioClip;
            sound.Source.volume = sound.Volume;
            sound.Source.pitch = sound.Pitch;
            sound.Source.loop = sound.Loop;
        }
    }

    private void Start()
    {
        PlaySound(MainTheme);
    }

    public void PlaySound(string name)
    {
        if (_isMute == false)
        {
            Sound sound = GetSoundByName(name);

            if (sound == null)
            {
                return;
            }

            sound.Source.Play();
        }
    }

    public void Mute()
    {
        _isMute = true;
        Sound sound = GetSoundByName(MainTheme);
        sound.Source.Stop();
    }

    public void UnMute()
    {
        _isMute = false;
        PlaySound(MainTheme);
    }

    private Sound GetSoundByName(string name)
    {
        for (int i = 0; i < _sounds.Length; i++)
        {
            if (_sounds[i].Name == name)
            {
                return _sounds[i];
            }
        }

        return null;
    }
}