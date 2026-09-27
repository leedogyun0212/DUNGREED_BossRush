using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UIDragHandler : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
{
    private RectTransform rectTransform;
    private Vector2 originalPosition;

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        originalPosition = rectTransform.anchoredPosition;
    }

    public void OnDrag(PointerEventData eventData)
    {
        rectTransform.anchoredPosition += eventData.delta / GetCanvasScaleFactor();
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        PlayerPrefs.SetFloat(gameObject.name + "_PosX", rectTransform.anchoredPosition.x);
        PlayerPrefs.SetFloat(gameObject.name + "_PosY", rectTransform.anchoredPosition.y);
        PlayerPrefs.Save();
    }

    private float GetCanvasScaleFactor()
    {
        return GetComponentInParent<Canvas>().scaleFactor;
    }
}
