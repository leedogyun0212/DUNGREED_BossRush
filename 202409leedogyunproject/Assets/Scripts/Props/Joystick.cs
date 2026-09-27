using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Joystick : MonoBehaviour, IDragHandler, IPointerUpHandler,
    IPointerDownHandler
{
    private CharacterManager _CharacterManager = null;

    public Image m_JoystickImage;
    public Image m_JoystickBackground;
    public RectTransform rectTransform { get; private set; }

    // ÀÔ·Â °ª
    private Vector2 _InputVector = Vector2.zero;

    private void Awake()
    {
        _CharacterManager = GameManager.GetManagerClass<CharacterManager>();
        rectTransform = GetComponent<RectTransform>();
    }

    private void Update()
    {
        _CharacterManager.inputVector = new Vector2(_InputVector.x, _InputVector.y);
    }
    void IDragHandler.OnDrag(PointerEventData eventData)
    {
        float ratio = UnityEngine.Screen.width / 1600.0f;

        _InputVector = eventData.position / ratio;
        _InputVector -= rectTransform.offsetMin;

        float backHalfXSize = m_JoystickBackground.rectTransform.sizeDelta.x * 0.5f;

        _InputVector = (m_JoystickBackground.rectTransform.anchoredPosition - _InputVector) *
            -1.0f / backHalfXSize;

        _InputVector = (_InputVector.magnitude > 1.0f) ?
            _InputVector.normalized : _InputVector;

        m_JoystickImage.rectTransform.anchoredPosition = _InputVector * backHalfXSize;
    }
    void IPointerDownHandler.OnPointerDown(PointerEventData eventData)
    { }
    void IPointerUpHandler.OnPointerUp(PointerEventData eventData)
    { m_JoystickImage.rectTransform.anchoredPosition = _InputVector = Vector2.zero; }
}
