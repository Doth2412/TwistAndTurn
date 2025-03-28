using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class SoundTrackManager : MonoBehaviour
{
    static public SoundTrackManager instance;
    public TextMeshProUGUI musicText;
    public AudioMixer audioMixer;
    static private int volume = 5;
    // Start is called before the first frame update
    
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
    }

    void Start()
    {
        musicText.text = volume.ToString();
        audioMixer.SetFloat("MusicVolume", 4f * volume - 40f);
    }

    void OnLevelWasLoaded()
    {
        try
        {
            musicText = GameObject.Find("Canvas").transform.Find("Menu").transform.Find("Music").transform.Find("MusicText").GetComponent<TextMeshProUGUI>();
            musicText.text = volume.ToString();
        }
        catch
        {
            return;
        }
    }

    public void IncreaseVolume()
    {
        if(volume < 10)
            volume++;
        audioMixer.SetFloat("MusicVolume", 4f * volume - 40f);
        musicText.text = volume.ToString();
    }

    public void DecreaseVolume()
    {
        if (volume > 0)
            volume--;
        if(volume == 0)
        {
            audioMixer.SetFloat("MusicVolume", -80f);
        }
        else
        {
            audioMixer.SetFloat("MusicVolume", 4f * volume - 40f);
        }
        musicText.text = volume.ToString();
    }
}
