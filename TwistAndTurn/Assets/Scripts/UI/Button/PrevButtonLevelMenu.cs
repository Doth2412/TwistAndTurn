using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PrevButtonLevelMenu : AButton
{
    public override void Start()
    {
        base.Start();
        transform.localScale = new Vector3(-1f, 1f, 1f);
        button.interactable = false;
    }
    public override void OnButtonClick()
    {
        base.OnButtonClick();
        LevelMenuManager.instance.ChangePage(-1);
    }

    public override void OnPointerEnter(PointerEventData eventData)
    {
        if (button.interactable)
        {
            transform.localScale = new Vector3(-1.2f, 1.2f, 1.2f); ;
            PlayHoverSound();
        }
    }
    
    public override void OnPointerExit(PointerEventData eventData)
    {
        transform.localScale = new Vector3(-1f, 1f, 1f);
    }
}
