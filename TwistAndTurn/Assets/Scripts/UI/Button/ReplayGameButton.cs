using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class ReplayGameButton : AButton
{
    private Image image;
    public Sprite sprite;
    public Start_PauseGameButton startPauseGameButton;
    
    public override void Start()
    {
        base.Start();
        image = GetComponent<Image>();
        image.sprite = sprite;
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
        GridSystem.instance.LoadMap(GridSystem.currentMapPath);
        startPauseGameButton.ChangeButtonSprite();
    }
}
