using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using TMPro;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;
using UnityEngine.Timeline;
using Scene = UnityEngine.SceneManagement.Scene;

public class GameManager : MonoBehaviour
{

    GameObject camera;

    GameObject _menu;
    GameObject mainUI;
    GameObject deathUI;
    PlayableDirector transitionCutscene;
    PlayableDirector menuCutscene;


    TextMeshProUGUI scoretext;

    TextMeshProUGUI _highscore;
    TextMeshProUGUI _deathHighscore;


    private int highscore;

    public int GetHighscore() { return highscore; }



    private bool start = false;
    public bool GetStart() { return start; }


    public void OnEnable()
    {
        PlayerController.onDied += Death;
    }

    public void OnDisable()
    {
        PlayerController.onDied -= Death;
    }

    public void Awake()
    {
        start = false;
        Time.timeScale = 1f;
        if (PlayerPrefs.HasKey("highscore"))
        {
            highscore = PlayerPrefs.GetInt("highscore");
        }
        else highscore = 0;
    }

    public void Start()
    {
        camera = GameObject.Find("Camera");


        _menu = GameObject.Find("Menu");

        mainUI = GameObject.Find("MainUI");
        deathUI = GameObject.Find("DeathUI");

        scoretext = GameObject.Find("DeathScore").GetComponent<TextMeshProUGUI>();

        _highscore = GameObject.Find("Highscore").GetComponent<TextMeshProUGUI>();
        _deathHighscore = GameObject.Find("DeathHighscore").GetComponent<TextMeshProUGUI>();

        _highscore.text = $"Highscore: {highscore}";
        _deathHighscore.text = $"Highscore: {highscore}";

        transitionCutscene = GameObject.Find("TransitionCutscene").GetComponent<PlayableDirector>();
        menuCutscene = GameObject.Find("MenuCutscene").GetComponent<PlayableDirector>();

        mainUI.SetActive(false);
        deathUI.SetActive(false);
        Debug.Log(start);
    }

    public void StartGame()
    {
        StartCoroutine(StartGameCoroutine());
    }

    private IEnumerator StartGameCoroutine()
    {
        menuCutscene.Stop();
        _menu.SetActive(false);
        transitionCutscene.Play();
        yield return new WaitForSeconds(5.55f);

        Destroy(GameObject.Find("CM vcam1"));


        start = true;
        mainUI.SetActive(true);


        yield return null;
    }


    private void Death(int score)
    {
        mainUI.SetActive(false);
        deathUI.SetActive(true);

        start = false;

        camera.transform.LookAt(GameObject.Find("Moon").transform.position);

        Time.timeScale = 0f;

        if(score > highscore)
        {
            PlayerPrefs.SetInt("highscore", score);
            PlayerPrefs.Save();
            _deathHighscore.gameObject.SetActive(false);
            scoretext.text = $"Congratulations! \nNew Highscore: {score} !";
        }
        else
        {
            scoretext.text = $"Your score is {score}";
            _deathHighscore.gameObject.SetActive(true);
        }
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        Debug.Log("Restart");
    }

    public void ResetData()
    {
        PlayerPrefs.DeleteAll();
        highscore = 0;
        _highscore.text = $"Highscore: {highscore}";
    }
}
