using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class goToHome : MonoBehaviour
{

    private bool isInhaled;
    private bool isEnteredInBorder;
    
    public GameObject inhaler;

    private bool isDestroyCalled;
    private bool isScoreDecreased;

    private Scene currentScene;
    private float currentSpeed;

    public ParticleSystem clearEffect;

    public GameObject monster_catched;
    public GameObject monster_in;

    private GameObject changedMonster;

    // 현재 Scene의 index
    private int currentNum;

    // Use this for initialization
    void Start ()
    {
        // 현재 Scene의 index 저장
        currentNum = SceneManager.GetActiveScene().buildIndex;

        currentScene = SceneManager.GetActiveScene();

        string sceneName = currentScene.name;

        if (sceneName.Contains("1")) // stage 1
        {
            currentSpeed = 10f;
            iTween.MoveTo(this.transform.gameObject, iTween.Hash("x", 0, "y", 0, "easeType", iTween.EaseType.linear, "speed", currentSpeed));
        }
        else if (sceneName.Contains("2")) // stage 2
        {
            currentSpeed = 15f;
            iTween.MoveTo(this.transform.gameObject, iTween.Hash("x", 0, "y", 0, "easeType", iTween.EaseType.linear, "speed", currentSpeed));
        }
        else if (sceneName.Contains("3")) // stage 3
        {
            currentSpeed = 20f;
            iTween.MoveTo(this.transform.gameObject, iTween.Hash("x", 0, "y", 0, "easeType", iTween.EaseType.linear, "speed", currentSpeed));
        }
        else if (sceneName.Contains("4")) // stage 4
        {
            currentSpeed = 25f;
            iTween.MoveTo(this.transform.gameObject, iTween.Hash("x", 0, "y", 0, "easeType", iTween.EaseType.linear, "speed", currentSpeed));
        }
        else if (sceneName.Contains("5")) // stage 5
        {
            currentSpeed = 30f;
            iTween.MoveTo(this.transform.gameObject, iTween.Hash("x", 0, "y", 0, "easeType", iTween.EaseType.linear, "speed", currentSpeed));
        }
        //iTween.MoveTo(this.transform.gameObject, iTween.Hash("x", 0, "y", 0, "easeType", iTween.EaseType.linear, "speed", 10f));
        isInhaled = false;
        isEnteredInBorder = false;
        isDestroyCalled = false;
        isScoreDecreased = false;

        
        
    }
	
	// Update is called once per frame
	void Update ()
    {
        if(isEnteredInBorder)
        {

        }

        if(isInhaled)
        {

            if (inhaler.gameObject == null)
            {
                isInhaled = false;
            }
            else
            {
                iTween.MoveTo(this.transform.gameObject, iTween.Hash("x", inhaler.transform.position.x, "y", inhaler.transform.position.y, "easeType", iTween.EaseType.linear, "speed", 20f));
            }

            if (!isDestroyCalled)
            {
                if (changedMonster != null)
                {
                    Destroy(changedMonster.gameObject, 1f);
                }
                Destroy(this.gameObject, 1f);

                isDestroyCalled = true;
            }
            

            //isInhaled = false;
        }
	}

    /*ivate void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.tag == "inhaler")
        {
            iTween.MoveTo(this.transform.gameObject, iTween.Hash("x", inhaler.transform.position.x, "y", inhaler.transform.position.y, "easeType", iTween.EaseType.linear, "speed", 20f));
        }
    }*/

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.tag == "realCollider")
        {
            StartCoroutine(triggerExitAgain());
        }
    }


    IEnumerator triggerExitAgain()
    {
        yield return new WaitForSeconds(0.5f);

        if (lifeController.Instance.remainedGermNumber > 0)
        {
            lifeController.Instance.remainedGermNumber--;
            lifeController.Instance.remainedGerm.text = lifeController.Instance.remainedGermNumber.ToString();
            isScoreDecreased = true;
        }

        


        if (changedMonster != null)
        {
            Destroy(changedMonster.gameObject);
        }

        Destroy(this.gameObject);
    }

    private void OnDestroy()
    {
        try
        {
            // stage가 마지막 stage일 경우 점수는 무한대까지 증가
            if (!isScoreDecreased && currentNum >= 5)
            {
                lifeController.Instance.remainedGermNumber++;
                lifeController.Instance.remainedGerm.text = lifeController.Instance.remainedGermNumber.ToString();

                if (changedMonster != null)
                {
                    //Debug.Log("실행해라");
                    Destroy(changedMonster);
                }
            }

            // 현재 stage의 목표치보다 적을 경우 score++
            else if (!isScoreDecreased && lifeController.Instance.remainedGermNumber < lifeController.Instance.stage[currentNum])
            {
                lifeController.Instance.remainedGermNumber++;
                lifeController.Instance.remainedGerm.text = lifeController.Instance.remainedGermNumber.ToString();

                if(changedMonster != null)
                {
                    //Debug.Log("실행해라");
                    Destroy(changedMonster);
                }
            }
            
            // stage의 목표치를 달성했을 경우
            if (lifeController.Instance.life > 0 && lifeController.Instance.remainedGermNumber >= lifeController.Instance.stage[currentNum])
            {
                Debug.Log("current goal: " + lifeController.Instance.stage[currentNum]);
                
                // 슬라이드를 닫음
                lifeController.Instance.slideDown.SetActive(true);
                lifeController.Instance.isCleared = true;

                //StartCoroutine(nextLevel());
                /*Scene currentScene = SceneManager.GetActiveScene();

                string sceneName = currentScene.name;

                if (sceneName.Contains("1"))
                {
                    SceneManager.LoadScene(1);
                }
                else if (sceneName.Contains("2"))
                {
                    SceneManager.LoadScene(2);
                }
                else if (sceneName.Contains("3"))
                {
                    SceneManager.LoadScene(3);
                }
                else if (sceneName.Contains("4"))
                {
                    SceneManager.LoadScene(4);
                }
                else if (sceneName.Contains("5"))
                {

                }*/
            }

            if (rotateInhaler.isRotating && inhaler.gameObject == rotateInhaler.currentInhaler.gameObject)
            {
                //Debug.Log("초록색으로");
                inhaler.GetComponent<SpriteRenderer>().color = Color.green;
            }
            else
            {
                //Debug.Log("일반으로");
                inhaler.GetComponent<SpriteRenderer>().color = new Color(255, 255, 255, 255);
            }
        }
        catch(System.Exception e)
        {
            
        }
    }

    private void OnTriggerEnter2D(Collider2D border)
    {
        if (border.tag == "house")
        {
            if (lifeController.Instance.life > 0)
            {
                //isEnteredInBorder = true;
                lifeController.Instance.life--;

                lifeController.Instance.healthBar[lifeController.Instance.life].SetActive(false);

                if(changedMonster != null)
                {
                    //Debug.Log("없애라");
                    Destroy(changedMonster.gameObject);
                }
                Destroy(this.gameObject);
            }


            if (lifeController.Instance.life <= 0)
            {

                Debug.Log("GameOver");
                // GameOver Scene 만들기
                //SceneManager.LoadScene(1);
            }
        }
        else if (border.tag == "realcollider" && !isEnteredInBorder && !isInhaled)
        {

            inhaler = border.transform.parent.GetChild(0).gameObject;//gameObject;

            inhaler.GetComponent<SpriteRenderer>().color = Color.red;

           
            changedMonster = Instantiate(monster_catched);
            changedMonster.transform.position = this.transform.position;
            changedMonster.transform.parent = this.transform.parent;
            //changedMonster.transform.localScale = this.transform.localScale * 5;
            this.transform.localScale = this.transform.localScale / 5;
            
            iTween.MoveTo(changedMonster, iTween.Hash("x", inhaler.transform.position.x, "y", inhaler.transform.position.y, "easeType", iTween.EaseType.linear, "speed", 20f));
            //rotateInhaler.Instance.GetComponent<SpriteRenderer>().color = Color.red;
            //Debug.Log("풍혈 포지션 : " + inhalerPosition);



            //this.transform.RotateAround(inhaler.transform.localPosition, Vector3.forward, 90f * Time.deltaTime);
            //iTween.MoveTo(this.transform.gameObject, iTween.Hash("x", border.transform.position.x, "y", border.transform.position.y, "easeType", iTween.EaseType.linear, "speed", 15f));

            isInhaled = true;

            


        }
        else if (border.tag == "border" && !isInhaled && !isEnteredInBorder) 
        {
            
            changedMonster = Instantiate(monster_in);
            changedMonster.transform.position = this.transform.position;
            changedMonster.transform.parent = this.transform.parent;
            //changedMonster.transform.localScale = this.transform.localScale * 5;
            this.transform.localScale = this.transform.localScale / 5;
            
            iTween.MoveTo(changedMonster, iTween.Hash("x", 0, "y", 0, "easeType", iTween.EaseType.linear, "speed", currentSpeed));
            //this.gameObject.SetActive(false);
            //Debug.Log("결계 안에 진입");
            isEnteredInBorder = true;
        }
    }

    
}
