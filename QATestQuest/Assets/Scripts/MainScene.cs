using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainScene : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        UIController.Instance.OpenPopup(ScreenType.InfoPopup);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
