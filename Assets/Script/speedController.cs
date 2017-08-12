using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class speedController : MonoBehaviour
{
    private static speedController instance;
    public static speedController Instance
    {
        get
        {
            if (instance == null)
            {
                return GameObject.FindObjectOfType<speedController>();
            }
            return speedController.instance;
        }
    }


    private float sceneTime;
    public float speed = 30f; // 5탄 초기값

	// Use this for initialization
	void Start ()
    {
        sceneTime = 0;
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (lifeController.Instance.currentNum == 5)
        {
            sceneTime += Time.deltaTime;

            if (sceneTime > 5f) // 이거랑
            {
                speed += 10f; // 이거 고치면 속도 얼마마다 얼만큼 조절할지 가능 ㅅㄱ
                
                Debug.Log("스피드업@@@ " + speed);
                sceneTime = 0f;
            }
        }
    }
}
