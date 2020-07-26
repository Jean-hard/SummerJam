using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ReplayBtnHandler : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public Sprite onReplaySprite;
    public Sprite outReplaySprite;

    public void OnPointerEnter(PointerEventData pointerEventData)
    {
        this.GetComponent<Image>().sprite = onReplaySprite;
    }


    public void OnPointerExit(PointerEventData eventData)
    {
        this.GetComponent<Image>().sprite = outReplaySprite;
    }
}
