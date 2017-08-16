using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class buttonClick : MonoBehaviour
{

    public CanvasGroup startCanvas;
    public CanvasGroup gameCanvas;
    public CanvasGroup settingCanvas;
    
    private bool isStarted;

    private const int STAGE1 = 1;
    private const int MAINMENU = 0;
    private const int LEADERBOARD = 7;

    private bool isMenuToggled;

    private void Start()
    {
        isStarted = false;
        isMenuToggled = false;
    }

    private void Update()
    {
        
    }

    public void startGame()
    {
        if(Time.timeScale == 0)
        {
            Time.timeScale = 1;
        }
        StartCoroutine(DoFade(STAGE1, startCanvas));
    }

    // Main과 help로 가는 함수
    public void gotoMain()
    {
        StartCoroutine(DoFade(MAINMENU, startCanvas)); // SceneManager.LoadScene("mainMenu");
    }

    public void gotoHelp()
    {
        SceneManager.LoadScene("help");
    }

	public void restart()
    {
        if (Time.timeScale == 0)
        {
            Time.timeScale = 1;
        }
        SceneManager.LoadScene(0);
        //Debug.Log(SceneManager.sceneCount);
    }

    public void goToMainMenu()
    {

        StartCoroutine(GoToMain(MAINMENU, gameCanvas, settingCanvas));
    }

    public void toggleMenu()
    {
        settingCanvas.gameObject.SetActive(true);
        Time.timeScale = 0;
        //gameCanvas.interactable = false;
    }

    public void backToTheGame()
    {
        settingCanvas.gameObject.SetActive(false);
        Time.timeScale = 1;
        //gameCanvas.interactable = true;
    }

    public void goToLeaderBoard()
    {
        lifeController.score = 0;
        StartCoroutine(DoFade(LEADERBOARD, startCanvas));
    }


    IEnumerator DoFade(int where, CanvasGroup canvas)
    {

        while (canvas.alpha > 0)
        {
            canvas.alpha -= Time.deltaTime * 1.5f;
            yield return null;
        }

        SceneManager.LoadScene(where);
        isStarted = false;

        yield return null;
    }

    IEnumerator GoToMain(int where, CanvasGroup canvas, CanvasGroup anotherCanvas)
    {
        
        anotherCanvas.gameObject.SetActive(false);
        
        while (canvas.alpha > 0)
        {
            canvas.alpha -= 0.02f; 
            yield return null;
        }

        if(Time.timeScale == 0)
        {
            Time.timeScale = 1;
        }

        SceneManager.LoadScene(where);
        isStarted = false;

        yield return null;
    }
}
