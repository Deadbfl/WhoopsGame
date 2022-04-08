using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class joystickSpawnEverywhere : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IDragHandler
{
    [SerializeField] RectTransform joyBG;
    [SerializeField] Joystick joystick;
    Vector3 joyBGFirst;
    private void Awake()
    {
      joyBGFirst = joyBG.localPosition;
    }
    public void OnPointerDown(PointerEventData ped)
    {
        Vector2 diff = ped.position - (Vector2)GetComponent<RectTransform>().position;
        Vector2 modDiff = diff * (1f/GetComponentInParent<Canvas>().scaleFactor);
        joyBG.localPosition = modDiff;
    }
    public void OnDrag(PointerEventData ped)
    {
        joystick.ChangeJoy(ped.position);
    }
    public void OnPointerUp(PointerEventData ped)
    {
        joyBG.localPosition = joyBGFirst;
        joystick.ResetJoy();
    }
}
