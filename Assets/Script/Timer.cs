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
        else
        {

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
}
