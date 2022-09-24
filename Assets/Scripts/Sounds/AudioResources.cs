using UnityEngine;

public class AudioResources : MonoBehaviour
{
    public static AudioResources Instance;

    [SerializeField] private Sound[] _sounds;

    private const string MainTheme = "MainTheme";

    private void Awake()
    {
        if(Instance == null)
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
        Sound sound = GetSoundByName(name);

        if (sound == null)
        {
            return;
        }

        sound.Source.Play();
    }

    private Sound GetSoundByName(string name)
    {
        for (int i = 0; i < _sounds.Length; i++)
        {
            if(_sounds[i].Name == name)
            {
                return _sounds[i];
            }
        }

        return null;
    }
}