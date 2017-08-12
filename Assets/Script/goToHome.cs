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
    public float currentSpeed;

    public ParticleSystem clearEffect;

    public GameObject monster_catched;
    public GameObject monster_in;

    private GameObject changedMonster;

    public GameObject forDistanceMeasurementInhaler;

    // 현재 Scene의 index
    private int currentNum;

    // 씬 타임. 5탄에서 속도 조정하기 위한 변수
    private float sceneTime;

    // Use this for initialization
    void Start ()
    {

        // 현재 Scene의 index 저장
        currentNum = SceneManager.GetActiveScene().buildIndex;

        currentScene = SceneManager.GetActiveScene();

        sceneTime = 0;

        string sceneName = currentScene.name;

        if (sceneName.Contains("1")) // stage 1
        {
            currentSpeed = 10f;
            iTween.MoveTo(this.transform.gameObject, iTween.Hash("x", 0, "y", 0, "easeType", iTween.EaseType.linear, "speed", currentSpeed));
            forDistanceMeasurementInhaler = GameObject.FindGameObjectWithTag("inhaler");
        }
        else if (sceneName.Contains("2")) // stage 2
        {
            currentSpeed = 15f;
            iTween.MoveTo(this.transform.gameObject, iTween.Hash("x", 0, "y", 0, "easeType", iTween.EaseType.linear, "speed", currentSpeed));
            forDistanceMeasurementInhaler = GameObject.FindGameObjectWithTag("inhaler");
        }
        else if (sceneName.Contains("3")) // stage 3
        {
            currentSpeed = 20f;
            iTween.MoveTo(this.transform.gameObject, iTween.Hash("x", 0, "y", 0, "easeType", iTween.EaseType.linear, "speed", currentSpeed));
            forDistanceMeasurementInhaler = GameObject.FindGameObjectWithTag("inhaler");
        }
        else if (sceneName.Contains("4")) // stage 4
        {
            currentSpeed = 25f;
            iTween.MoveTo(this.transform.gameObject, iTween.Hash("x", 0, "y", 0, "easeType", iTween.EaseType.linear, "speed", currentSpeed));
            forDistanceMeasurementInhaler = GameObject.FindGameObjectWithTag("inhaler");
        }
        else if (sceneName.Contains("5")) // stage 5
        {
            currentSpeed = 30f;
            iTween.MoveTo(this.transform.gameObject, iTween.Hash("x", 0, "y", 0, "easeType", iTween.EaseType.linear, "speed", speedController.Instance.speed));
            forDistanceMeasurementInhaler = GameObject.FindGameObjectWithTag("inhaler");
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

        if (forDistanceMeasurementInhaler != null && !isEnteredInBorder && !isInhaled && 
            Vector2.Distance(Vector2.zero, new Vector2(forDistanceMeasurementInhaler.transform.position.x, forDistanceMeasurementInhaler.transform.position.y))
            > Vector2.Distance(Vector2.zero, new Vector2(this.transform.position.x, this.transform.position.y)))
        {
            Debug.Log("good");
            changedMonster = Instantiate(monster_in);
            changedMonster.transform.position = this.transform.position;
            changedMonster.transform.parent = this.transform.parent;
            //changedMonster.transform.localScale = this.transform.localScale * 5;
            this.transform.localScale = this.transform.localScale / 5;

            iTween.MoveTo(changedMonster, iTween.Hash("x", 0, "y", 0, "easeType", iTween.EaseType.linear, "speed", currentSpeed));
            isEnteredInBorder = true;
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
                    Destroy(changedMonster.gameObject, 0.5f);
                }
                Destroy(this.gameObject, 0.5f);

                isDestroyCalled = true;
            }
            

            //isInhaled = false;
        }
	}

    

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.tag == "realCollider")
        {
            //StartCoroutine(triggerExitAgain());
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
                lifeController.Instance.plusEffect.Emit(1);

                if (changedMonster != null)
                {
                    //Debug.Log("실행해라");
                    Destroy(changedMonster);
                }
            }

            // 현재 stage의 목표치보다 적을 경우 score++
            else if (!isScoreDecreased && lifeController.Instance.remainedGermNumber < lifeController.Instance.stage[currentNum - 1])
            {
                if (!isEnteredInBorder)
                {
                    lifeController.Instance.remainedGermNumber++;
                    lifeController.Instance.plusEffect.Emit(1);
                }
                lifeController.Instance.remainedGerm.text = lifeController.Instance.remainedGermNumber.ToString();
                

                if (changedMonster != null)
                {
                    //Debug.Log("실행해라");
                    Destroy(changedMonster);
                }
            }
            
            // stage의 목표치를 달성했을 경우
            if (lifeController.Instance.life > 0 && lifeController.Instance.remainedGermNumber >= lifeController.Instance.stage[currentNum - 1])
            {
                Debug.Log("current goal: " + lifeController.Instance.stage[currentNum - 1]);
                
                // 슬라이드를 닫음
                lifeController.Instance.slideDown.SetActive(true);
                lifeController.Instance.isCleared = true;

                
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
            //Debug.Log("DD?");
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
            
            isInhaled = true;

        }
        else if (border.tag == "border" && !isInhaled && !isEnteredInBorder) 
        {
            
            //changedMonster = Instantiate(monster_in);
            //changedMonster.transform.position = this.transform.position;
            //changedMonster.transform.parent = this.transform.parent;
            ////changedMonster.transform.localScale = this.transform.localScale * 5;
            //this.transform.localScale = this.transform.localScale / 5;
            
            //iTween.MoveTo(changedMonster, iTween.Hash("x", 0, "y", 0, "easeType", iTween.EaseType.linear, "speed", currentSpeed));
            ////this.gameObject.SetActive(false);
            ////Debug.Log("결계 안에 진입");
            //isEnteredInBorder = true;
        }
    }

    
}
