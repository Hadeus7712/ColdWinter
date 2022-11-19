using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    private GameManager _gameManager;
    private GameObject _menu;


    void Start()
    {
        _gameManager = FindObjectOfType<GameManager>();
    }


    public void StartGame()
    {
        _gameManager.StartGame();
    }

    

    public void Exit()
    {
        Application.Quit();
        Debug.Log("Exited the game!");
    }
}
