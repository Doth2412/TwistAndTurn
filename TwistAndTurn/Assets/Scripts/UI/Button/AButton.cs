using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

[RequireComponent(typeof(AudioSource))]  

public abstract class AButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public Button button;
    //private AudioSource audioSource;
    
    public AudioClip hoverSound;
    public AudioClip clickSound;

    public virtual void Start()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(OnButtonClick);
        transform.localScale = new Vector3(1, 1, 1);
        hoverSound = Resources.Load<AudioClip>("Sounds/hover");
        clickSound = Resources.Load<AudioClip>("Sounds/click");
    }

    public virtual void OnButtonClick()
    {
        SFXManager.instance.PlaySoundSFXClip(clickSound);
    }

    public virtual void OnPointerEnter(PointerEventData eventData)
    {
        if(button.interactable)
        {
            transform.localScale = new Vector3(1.2f, 1.2f, 1.2f); ;
            PlayHoverSound();
        }
    }
    
    public virtual void OnPointerExit(PointerEventData eventData)
    {
        transform.localScale = new Vector3(1, 1, 1);
    }

    public void PlayHoverSound()
    {
        SFXManager.instance.PlaySoundSFXClip(hoverSound);
    }
} 
