using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using UnityEngine.SceneManagement;
using UnityEngine.Analytics;

public enum ScreenType{
    None,
    InfoPopup
}

public class UIController : MonoBehaviour
{

    private static UIController _instance;

    public static UIController Instance { get { return _instance; } }

    public GameObject[] popups;

    public List<UI> popupStack = new List<UI>();
    public UI currentPopup;

    private Canvas uiCanvas;

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }
    }

    public void Start()
    {
        uiCanvas = GetComponent<Canvas>();
    }


    public void Update()
    {

    }

    
    

    public void OpenPopup(ScreenType screenType, object[] arguments = null)
    {
        if(popupStack.Count > 0){
            if (popupStack[popupStack.Count - 1].screenType == screenType)
            {
                popupStack[popupStack.Count - 1].TurnOn(arguments);
                return;
            }
        }

        for (int i = 0; i < popups.Length; i++)
        {
            if (popups[i].GetComponent<UI>().screenType == screenType)
            {
                GameObject popup = Instantiate(popups[i], Vector3.zero, Quaternion.identity, transform);
                popup.transform.localRotation = Quaternion.identity;
                popup.transform.localPosition = Vector3.zero;
                UI instantiatedPopup = popup.GetComponent<UI>();
                popupStack.Add(instantiatedPopup);
                currentPopup = instantiatedPopup;
                instantiatedPopup.TurnOn(arguments);
                return;
            }
        }
        Debug.LogError("Popup with the name " + screenType + " not found.");
    }

    public void ClosePopup(ScreenType screenType = ScreenType.None)
    {
        if(popupStack.Count == 0){
            Debug.LogWarning("Trying to close a popup, but popup stack has no elements");
            return;
        }

        if (screenType == ScreenType.None && popupStack.Count > 0)
        {
            UI lastPopup = popupStack[popupStack.Count - 1];
            popupStack.RemoveAt(popupStack.Count - 1);
            lastPopup.TurnOff();
            return;
        }

        if(screenType != ScreenType.None && popupStack.Count > 0){
            if (!IsPopupOpen(screenType))
                return;

            for (int i = 0; i < popupStack.Count; i++){
                if(popupStack[i].screenType == screenType)
                {
                    UI popupToClose = popupStack[i];
                    popupStack.RemoveAt(i);
                    popupToClose.TurnOff();
                }
            }
        }
        if (currentPopup != null && currentPopup.screenType == screenType)
            currentPopup = null;
    }

    
    public bool IsPopupOpen(ScreenType popupType){
        foreach(UI popup in popupStack){
            if (popup.screenType == popupType)
                return true;
        }
        return false;
    }

   
    public void CloseAllPopupsExcept(ScreenType notToClose = ScreenType.None)
    {
        foreach (UI popup in popupStack.ToArray())
        {
            if(popup.screenType != notToClose)
            {
                popup.TurnOff();
            }
        }
    }

    public UI GetActiveUIByType(ScreenType uiType)
    {
        foreach(UI activeUI in popupStack)
        {
            if (activeUI.screenType == uiType)
                return activeUI;
        }
        
        return null;
    }
  
}
