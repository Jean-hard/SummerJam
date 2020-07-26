using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PlayBtnHandler : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public Sprite onPlaySprite;
    public Sprite outPlaySprite;

    public void OnPointerEnter(PointerEventData pointerEventData)
    {
        this.GetComponent<Image>().sprite = onPlaySprite;
    }


    public void OnPointerExit(PointerEventData eventData)
    {
        this.GetComponent<Image>().sprite = outPlaySprite;
    }
}
