using Newtonsoft.Json.Bson;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LogsNumber : MonoBehaviour
{

    private TextMeshProUGUI text;

    void Start()
    {
       text = GetComponent<TextMeshProUGUI>();
    }

    public void OnEnable()
    {
        PlayerController.onTookLog += AddLog;
        LogsManager.onRemovedLogs += RemoveLog;
    }

    public void OnDisable()
    {
        PlayerController.onTookLog -= AddLog;
        LogsManager.onRemovedLogs -= RemoveLog;
    }



    private void AddLog(int number)
    {
        text.text = $"x{number}";
    }

    private void RemoveLog(int number)
    {
        text.text = $"x{number}";
    }
}
