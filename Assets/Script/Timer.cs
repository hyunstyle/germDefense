using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer : MonoBehaviour {


    public GameObject timer;
    public float decreasingSpeed;
    public float xPosition;
    private float firstXposition;
    private float yPosition;
    private float zPosition;

    public ParticleSystem speedUpEffect;
	// Use this for initialization
	void Start ()
    {
        firstXposition = timer.gameObject.transform.localScale.x;
        xPosition = timer.gameObject.transform.localScale.x;
        yPosition = timer.gameObject.transform.localScale.y;
        zPosition = timer.gameObject.transform.localScale.z;
        StartCoroutine(decreasingTime());
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (xPosition > 0)
        {
            timer.gameObject.transform.localScale = new Vector3(xPosition, yPosition, zPosition);
        }
        else // 0되면
        {
            speedController.Instance.speed += 5f;
            StartCoroutine(doSpeedUpEffect());//speedUpEffect.gameObject.SetActive(true);
            
            xPosition = firstXposition;
        }
	}

    IEnumerator decreasingTime()
    {
        while (xPosition > 0)
        {
            xPosition -= Time.deltaTime * decreasingSpeed;
            yield return null;
        }

        yield return null;
    }

    IEnumerator doSpeedUpEffect()
    {
        speedUpEffect.gameObject.SetActive(true);

        yield return new WaitForSeconds(1f);

        speedUpEffect.gameObject.SetActive(false);
    }
}
