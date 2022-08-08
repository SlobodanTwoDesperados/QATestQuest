using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InfoPopup : UI
{
    public Button closeButton;
    public TextMeshProUGUI textTMP;

    public override void TurnOff()
    {
        Destroy(this.gameObject);
    }

    public override void TurnOn(object[] args = null)
    {
        if (args != null && args.Length > 0)
        {
            //TODO: good action, bad action, etc.
            string text = (string)args[0];
            textTMP.text = text;
        }

        closeButton.onClick.AddListener(() => Close());

        gameObject.SetActive(true);
    }


    public void Close()
    {
        UIController.Instance.ClosePopup(ScreenType.InfoPopup);
    }    
}

