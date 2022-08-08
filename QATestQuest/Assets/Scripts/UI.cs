using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class UI : MonoBehaviour
{
    public int priority;
    [SerializeField]
    public ScreenType screenType;
    public abstract void TurnOn(object[] args);
    public abstract void TurnOff();

    public virtual void PopupClosed()
    {
    }                                 
}
