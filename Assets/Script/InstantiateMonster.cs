using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InstantiateMonster : MonoBehaviour {

    private GameObject clone;

    
    public GameObject normalMon;
    public GameObject stage2Mon;
    public GameObject stage3Mon;
    public GameObject stage4Mon;
    public GameObject stage5Mon;

    public GameObject parentPanel;
    private float time;
    public float spawnTime;

    private const int LEFT = 0;
    private const int RIGHT = 1;
    private const int UP = 2;
    private const int DOWN = 3;

    private int currentStage;
    // Use this for initialization
    void Start ()
    {
        time = 0f;
        Scene currentScene = SceneManager.GetActiveScene();

        string sceneName = currentScene.name;

        if (sceneName.Contains("1"))
        {
            currentStage = 1;
        }
        else if (sceneName.Contains("2"))
        {
            currentStage = 2;
        }
        else if (sceneName.Contains("3"))
        {
            currentStage = 3;
        }
        else if (sceneName.Contains("4"))
        {
            currentStage = 4;
        }
        else if (sceneName.Contains("5"))
        {
            currentStage = 5;
        }
    }
	
	// Update is called once per frame
	void Update ()
    {
        time += Time.deltaTime;
        if(time > spawnTime)
        {

            switch(currentStage)
            {
                case 1:
                    clone = Instantiate(normalMon, parentPanel.transform);
                    break;
                case 2:
                    clone = Instantiate(stage2Mon, parentPanel.transform);
                    break;
                case 3:
                    clone = Instantiate(stage3Mon, parentPanel.transform);
                    break;
                case 4:
                    clone = Instantiate(stage4Mon, parentPanel.transform);
                    break;
                case 5:
                    clone = Instantiate(stage5Mon, parentPanel.transform);
                    break;
                default:
                    break;

            }


            int randomDir = Random.Range(0, 4);
            float randomX = 999;
            float randomY = 999;
            switch (randomDir)
            {
                case LEFT:
                    randomX = Random.Range(-Screen.width * 2, -Screen.width);
                    randomY = Random.Range(-Screen.height, Screen.height);
                    break;
                case RIGHT:
                    randomX = Random.Range(Screen.width, Screen.width * 2);
                    randomY = Random.Range(-Screen.height, Screen.height);
                    break;
                case UP:
                    randomX = Random.Range(-Screen.width, Screen.width);
                    randomY = Random.Range(Screen.height, Screen.height * 2);
                    break;
                case DOWN:
                    randomX = Random.Range(-Screen.width, Screen.width);
                    randomY = Random.Range(-Screen.height, -Screen.height * 2);
                    break;
                default:
                    break;
            }

            clone.transform.localPosition = new Vector3(randomX, randomY);

            time = 0f;
            //clone.transform.position = new Vector3();
        }
	}





    void makeMonster(GameObject mon, string monName, ref int num)
    {
        while (num > 0)
        {
            clone = Instantiate(mon, this.transform);
            float randomX = Random.Range(-Screen.width / 2, Screen.width / 2);
            float randomY = Random.Range(-100, Screen.height / 2 - 190);
            clone.transform.localPosition = new Vector3(randomX, randomY, 0);
            clone.transform.parent = this.transform;
            clone.name = monName;
            clone.SetActive(true);
            num--;
        }
    }


}
