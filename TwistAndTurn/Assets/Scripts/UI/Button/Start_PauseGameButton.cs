using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Start_PauseGameButton : AButton
{
    private Image image;
    public Sprite startSprite;
    public Sprite pauseSprite;
    
    public override void Start()
    {
        base.Start();
        image = GetComponent<Image>();
        image.sprite = startSprite;
    }
    
    public override void OnPointerEnter(UnityEngine.EventSystems.PointerEventData eventData)
    {
        if (button.interactable)
        {
            transform.localScale = new Vector3(1.1f, 1.1f, 1.1f);
        }
    }
    
    public override void OnButtonClick()
    {
        base.OnButtonClick();
        GridSystem.instance.isGameStart = !GridSystem.instance.isGameStart;
        GridSystem.instance.canPlaceTile = !GridSystem.instance.canPlaceTile;
        image.sprite = GridSystem.instance.isGameStart ? pauseSprite : startSprite;
    }
    
    public void ChangeButtonSprite()
    {
        image.sprite = startSprite;
    }
}
