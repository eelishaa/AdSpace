using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public abstract class JoystickHandler : MonoBehaviour, IDragHandler,IPointerDownHandler,IPointerUpHandler 
{
    [SerializeField] private Image _joystickBackground;
    [SerializeField] private Image _joystick;
    [SerializeField] private Image _joystickArea;
    [SerializeField] private Camera gameCamera;
    [SerializeField] private Animator anim;

    private Vector2 _joystickBackgroundStartPosition;

    protected Vector2 _inputVector;
    private bool _joystickIsActive = false;


    void Start()
    {
        ClickEffect();
        _joystickBackgroundStartPosition = _joystickBackground.rectTransform.anchoredPosition;
    }

    public void OnDrag(PointerEventData eventData)
    {
        Vector2 joyPosition;
        if(RectTransformUtility.ScreenPointToLocalPointInRectangle(_joystickBackground.rectTransform, eventData.position, gameCamera,out joyPosition))
        {
            joyPosition.x = (joyPosition.x * 2 / _joystickBackground.rectTransform.sizeDelta.x);
            joyPosition.y = (joyPosition.y * 2 / _joystickBackground.rectTransform.sizeDelta.y);

            _inputVector = new Vector2(joyPosition.x, joyPosition.y);
            _inputVector = (_inputVector.magnitude > 1f) ? _inputVector.normalized : _inputVector;

            _joystick.rectTransform.anchoredPosition = new Vector2(_inputVector.x * (_joystickBackground.rectTransform.sizeDelta.x / 2), _inputVector.y * (_joystickBackground.rectTransform.sizeDelta.y / 2));

        }
    }
 
    public void OnPointerDown (PointerEventData eventData)
    {
        ClickEffect();
        Vector2 _joysticBackPosition;
        if (RectTransformUtility.ScreenPointToLocalPointInRectangle(_joystickArea.rectTransform, eventData.position, gameCamera, out _joysticBackPosition))
        {
            _joystickBackground.rectTransform.anchoredPosition = new Vector2(_joysticBackPosition.x, _joysticBackPosition.y);
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        _joystickBackground.rectTransform.anchoredPosition = _joystickBackgroundStartPosition;
        ClickEffect();
        _inputVector = Vector2.zero;
        _joystick.rectTransform.anchoredPosition = Vector2.zero;
    }

    private void ClickEffect()
    {
        if (!_joystickIsActive)
        {
            _joystickBackground.gameObject.SetActive(false);
            anim.SetInteger("AnimationPar", 0);
            _joystickIsActive = true;
        }
        else
        {
            _joystickBackground.gameObject.SetActive(true);
            anim.SetInteger("AnimationPar", 1);
            _joystickIsActive = false;
        }
    }
 }
  