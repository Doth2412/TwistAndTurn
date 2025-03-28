using UnityEngine;
using UnityEngine.Audio;
using TMPro;
using UnityEngine.UI;

public class SFXManager : MonoBehaviour
{
    static public SFXManager instance;
    private AudioSource audioSource;
    [SerializeField] TextMeshProUGUI sFXText;
    [SerializeField] AudioMixer audioMixer;
    static private int volume = 5;

    // Start is called before the first frame update
    void Start()
    {   
        audioSource = GetComponent<AudioSource>();
        audioMixer.SetFloat("SFXVolume", 4f * volume - 40f);
        sFXText.text = volume.ToString();
    }

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

    void OnLevelWasLoaded()
    {
        try
        {
            sFXText = GameObject.Find("Canvas").transform.Find("Menu").transform.Find("SFX").transform.Find("SFXText").GetComponent<TextMeshProUGUI>();
            sFXText.text = volume.ToString();
        }
        catch
        {
            return;
        }
    }

    public void IncreaseVolume()
    {
        if (volume < 10)
            volume++;
        audioMixer.SetFloat("SFXVolume", 4f * volume - 40f);
        sFXText.text = volume.ToString();
    }

    public void DecreaseVolume()
    {
        if (volume > 0)
            volume--;
        if(volume == 0)
        {
            audioMixer.SetFloat("SFXVolume", -80f);
        }
        else
        {
            audioMixer.SetFloat("SFXVolume", 4f * volume - 40f);
        }
        sFXText.text = volume.ToString();
    }

    public void PlaySoundSFXClip(AudioClip audioClip)
    {
        if(audioSource.isPlaying)
        {
            audioSource.Stop();
        }
        audioSource.clip = audioClip;
        audioSource.pitch = Random.Range(0.95f, 1.05f);
        audioSource.Play();
    }
}
