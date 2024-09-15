using UnityEngine;

[System.Serializable]
public class Sound
{
    public string name;
    public AudioClip clip;

    [Range(0f, 1f)]
    public float volume = 1f;
    [Range(0.1f, 3f)]
    public float pitch = 1f;

    [HideInInspector]
    public AudioSource source;
}

public class AudioManager : MonoBehaviour
{
    public Sound[] sounds;

    void Awake()
    {
        foreach (Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;
            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
        }
    }

    private void Start()
    {
        GameController.Instance.modeSwitcher.OnSwitchMode += PlayThunder;
        GameController.Instance.gameloopController.OnWining += PlayWinSound;
        PlayerController.Instance.playerDeath.onPlayerDeath += PlayExpSound;
        PlayerController.Instance.movingPlatformDetector.OnBuble += PlayBubleSound;
    }
    public void PlaySound(string name)
    {
        Sound s = System.Array.Find(sounds, sound => sound.name == name);
        if (s == null)
        {
            Debug.LogWarning("Sound: " + name + " not found!");
            return;
        }
        s.source.Play();
    }

    void PlayThunder()
    {
        PlaySound("thunder");
    }
    void PlayWinSound()
    {
        PlaySound("win");
    }
    void PlayExpSound()
    {
        PlaySound("exp");
    }

    void PlayBubleSound()
    {
        PlaySound("buble");
    }
}