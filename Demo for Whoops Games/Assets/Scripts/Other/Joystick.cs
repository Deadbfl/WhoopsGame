using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Joystick : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IDragHandler
{
    public RectTransform joystick;
    public Vector2 result;

    public void OnPointerDown(PointerEventData ped)
    {
        ChangeJoy(ped.position);
    }
    public void OnDrag(PointerEventData ped)
    {
        ChangeJoy(ped.position);
    }
    public void OnPointerUp(PointerEventData ped)
    {
        ResetJoy();
    }
    public void ChangeJoy(Vector2 pedPos)
    {
        Vector2 diff = pedPos - (Vector2)GetComponent<RectTransform>().position;
        Vector2 modDiff = diff * (1f / GetComponentInParent<Canvas>().scaleFactor);
        modDiff /= GetComponent<RectTransform>().sizeDelta * 0.5f;
        result = Vector2.ClampMagnitude(modDiff, 1f);
        modDiff = result * GetComponent<RectTransform>().sizeDelta * 0.3f;
        joystick.localPosition = modDiff;
    }
    public void ResetJoy()
    {
        result = Vector2.zero;
        joystick.localPosition = Vector2.zero;


    }
}
