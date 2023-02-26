using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
public class OnScreenLog : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Application.logMessageReceived += Application_OnMessageReceived;
    }

    void Application_OnMessageReceived(string condition, string trace, LogType type)
    {
        this.gameObject.GetComponent<TMPro.TextMeshProUGUI>().text = condition;
    }
}
