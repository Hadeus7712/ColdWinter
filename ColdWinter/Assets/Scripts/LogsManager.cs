using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LogsManager : MonoBehaviour
{

    private PlayerController playerController;

    private ProgressBar heatBar;

    GameManager _gameManager;


    public static Action<int> onRemovedLogs;

    public void Awake()
    {
        heatBar = GameObject.Find("HeatBar").GetComponent<ProgressBar>();
    }

    void Start()
    {
        playerController = GameObject.Find("Player").GetComponent<PlayerController>();
        

        _gameManager = FindObjectOfType<GameManager>();
    }

    void Update()
    {

    }

    public void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            if ((playerController.logs > 0) && (heatBar.current < heatBar.maximum))
            {
                playerController.logs--;
                playerController.score += 30;
                onRemovedLogs?.Invoke(playerController.logs);
                heatBar.current += 30;
            }
        }

    }
}
