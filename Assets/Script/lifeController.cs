using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class lifeController : MonoBehaviour
{

    

    private static lifeController instance;
    public static lifeController Instance
    {
        get
        {
            if (instance == null)
            {
                return GameObject.FindObjectOfType<lifeController>();
            }
            return lifeController.instance;
        }
    }

    public int life;

    public GameObject[] healthBar;

    public Text remainedGerm;
    public int remainedGermNumber;

    public GameObject mainCanvas;
    public GameObject gameOverCanvas;
    public CanvasGroup mainCanvasGroup;

    public ParticleSystem bombEffect;
    public ParticleSystem clearEffect;
    private float gameOverTime;
    private bool isGameOver;
    private bool isEffectEnd;
    public bool isCleared;

    // 각 stage별 넘어야하는 score
    public int[] stage;
     
    // 현재 Scene의 index
    private int currentNum;

    // 슬라이드 효과 애니메이션
    public GameObject slideDown;
    public GameObject slideUp;

    private void Awake()
    {
        
    }

    // Use this for initialization
    void Start ()
    {
        // 각 stage별 목표치
        stage = new int[5];
        stage[0] = 0;
        stage[1] = 1;
        stage[2] = 10;
        stage[3] = 20;
        stage[4] = 30;

        // 현재 Scene의 index 저장
        currentNum = SceneManager.GetActiveScene().buildIndex;

        Debug.Log("current index: " + currentNum);
        //Debug.Log("current goal: " + stage[currentNum]);

        // stage별 점수 초기화
        remainedGermNumber = stage[currentNum - 1];
        remainedGerm.text = remainedGermNumber.ToString();

        isGameOver = false;
        isEffectEnd = false;
        isCleared = false;
    }

    private void NewMethod()
    {
        stage[0] = 0;
    }

    // Update is called once per frame
    void Update ()
    {
		if(life == 0)
        {
            // Game Over 됬을 때 explosion 실행
            if (!isGameOver)
            {
                gameOverTime = Time.time;
                bombEffect.gameObject.SetActive(true);

                isGameOver = true;
            }
            else
            {
                // explosion이 끝나면 LeaderBoard로 이동
                if(!isEffectEnd && gameOverTime + 1f < Time.time)
                {
                    bombEffect.gameObject.SetActive(false);
                    mainCanvas.SetActive(false);
                    gameOverCanvas.SetActive(true);

                    isEffectEnd = true;
                    SceneManager.LoadScene("LeaderBoard");
                }
            }
        }

        if(isCleared)
        {
            StartCoroutine(nextLevel());
            //StartCoroutine(DoFade());
            isCleared = false;
        }
	}

    IEnumerator nextLevel()
    {
        yield return new WaitForSeconds(1.0f);

        Scene currentScene = SceneManager.GetActiveScene();

        string sceneName = currentScene.name;

        if (sceneName.Contains("1"))
        {
            SceneManager.LoadScene(2);
        }
        else if (sceneName.Contains("2"))
        {
            SceneManager.LoadScene(3);
        }
        else if (sceneName.Contains("3"))
        {
            SceneManager.LoadScene(4);
        }
        else if (sceneName.Contains("4"))
        {
            SceneManager.LoadScene(5);
        }
        else if (sceneName.Contains("5"))
        {

        }
    }

    IEnumerator DoFade()
    {

        while (mainCanvasGroup.alpha > 0)
        {
            mainCanvasGroup.alpha -= Time.deltaTime * 1.5f;
            yield return null;
        }

        mainCanvasGroup.interactable = false;
        yield return null;
    }
}
