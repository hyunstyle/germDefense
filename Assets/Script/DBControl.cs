using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DBControl : MonoBehaviour {

    int length;
    int[] LankingScore;

    public Text lank1;
    public Text lank2;
    public Text lank3;
    public Text lank4;
    public Text lank5;
    public Text lank6;
    public Text lank7;
    public Text lank8;
    public Text lank9;
    public Text lank10;

    public Text nowScore;

    public CanvasGroup gameOverToLeaderBoard;
    public CanvasGroup mainToLeaderBoard;

    // Use this for initialization
    void Start () {
        length = LankDB.GetInstance().getMaxLank();
        LankingScore = new int[length];

        StartCoroutine(showRanking());
	}

    void showLank()
    {
        LankingScore = LankDB.GetInstance().loadLanking();
        Array.Sort(LankingScore);
        Array.Reverse(LankingScore);

        int score = Int32.Parse(nowScore.text.ToString());
        if ( score > LankingScore[9])
        {
            int[] array = new int[11];

            for (int i = 0; i <10; i++)
                array[i] = LankingScore[i];

            array[10] = score;

            Array.Sort(array);
            Array.Reverse(array);

            for (int i = 0; i < 10; i++)
                LankingScore[i] = array[i];

            LankDB.GetInstance().saveAllLank(LankingScore);
        }
        else
        {

        }

        if(gameOverToLeaderBoard.gameObject.activeSelf)
        {
            lank1.text = LankingScore[0].ToString();
            lank2.text = LankingScore[1].ToString();
            lank3.text = LankingScore[2].ToString();
            lank4.text = LankingScore[3].ToString();
            lank5.text = LankingScore[4].ToString();
            lank6.text = LankingScore[5].ToString();
            lank7.text = LankingScore[6].ToString();
            lank8.text = LankingScore[7].ToString();
            lank9.text = LankingScore[8].ToString();
            lank10.text = LankingScore[9].ToString();
        }
        else
        {
            for(int i = 0; i<10; i++)
            {
                mainToLeaderBoard.gameObject.transform.GetChild(i).GetComponent<Text>().text = LankingScore[i].ToString();
            }
        }
    }

    IEnumerator showRanking()
    {
        if(nowScore.text.ToString().Equals("0")) // 메인에서 리더보드 갔을 경우
        {
            mainToLeaderBoard.gameObject.SetActive(true);
            gameOverToLeaderBoard.gameObject.SetActive(false);
        }
        else // 게임오버 되서 리더보드 갔을 경우
        {
            Debug.Log("이거실행 " + nowScore.text.ToString());
            mainToLeaderBoard.gameObject.SetActive(false);
            gameOverToLeaderBoard.gameObject.SetActive(true);
        }
        //yield return new WaitForSeconds(0.5f);

        showLank();

        yield return null;
    }

}
