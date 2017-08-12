using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class rotateInhaler : MonoBehaviour
{

    //public GameObject inhaler;
    /*[SerializeField]
    float horizontalLimit = 50f, verticalLimit = 50f, dragSpeed = 0.1f;
   
    Transform inhaler;

    Vector3 startingPos;*/
    private static rotateInhaler instance;
    public static rotateInhaler Instance
    {
        get
        {
            if (instance == null)
            {
                return GameObject.FindObjectOfType<rotateInhaler>();
            }
            return rotateInhaler.instance;
        }
    }


    private GameObject inhaler; // 움직일 청소기

    public Color normalColor;
    private Color clickedColor;
    public static bool isRotating;
    public float speed;
    public GameObject house;
    private float directionX;
    private float directionY;

    private float inhalerPosX;
    private float inhalerPosY;

    private float touchPosX;
    private float touchPosY;

    private const int QUADRANT1 = 1;
    private const int QUADRANT2 = 2;
    private const int QUADRANT3 = 3;
    private const int QUADRANT4 = 4;

    private const int LEFT = 91;
    private const int RIGHT = 92;
    private int movingDirection;

    private int inhalerQuadrant;
    private int touchQuadrant;

    private Vector3 oldPos;

    Vector2 dir;
    float dist;
    float check;
    float touchAngle;
    float inhalerAngle;

    public GameObject prObj;
    public GameObject ihl;

    public static GameObject currentInhaler;

    // Use this for initialization
    void Start ()
    {
        isRotating = false;
        inhaler = this.transform.gameObject;
        inhalerQuadrant = 0;
        touchQuadrant = 0;
        oldPos = this.transform.position;
        movingDirection = 0;
        normalColor = this.transform.GetComponent<SpriteRenderer>().color;
        clickedColor = new Color(173, 173, 255);
        check = 360;

       

        Scene currentScene = SceneManager.GetActiveScene();

        string sceneName = currentScene.name;

        if(sceneName.Contains("1"))
        {

        }
        else if(sceneName.Contains("2"))
        {

        }
        else if (sceneName.Contains("3"))
        {

        }
        else if (sceneName.Contains("4"))
        {

        }
        else if (sceneName.Contains("5"))
        {

        }

        //Debug.Log(currentScene.name);
        /*inhaler = this.transform;

        startingPos = inhaler.position;*/
	}

    // Update is called once per frame
    void Update()
    {

        if(isRotating && this.transform.GetComponent<SpriteRenderer>().color == Color.green)
        {
            //touchAngle = Vector3.Angle(Vector3.up, new Vector3(Input.GetTouch(0).position.x - (Screen.width / 2), Input.GetTouch(0).position.y - (Screen.height / 2), 0));

            touchAngle = Mathf.Atan2(Input.GetTouch(0).position.y - (Screen.height / 2), Input.GetTouch(0).position.x - (Screen.width / 2)) * Mathf.Rad2Deg;

            if (prObj.name.Contains("1"))
            {
                prObj.transform.rotation = Quaternion.Euler(0, 0, touchAngle - 90f);
            }
            else if(prObj.name.Contains("2"))
            {
                prObj.transform.rotation = Quaternion.Euler(0, 0, touchAngle - 180f);
            }
            else if(prObj.name.Contains("3"))
            {
                prObj.transform.rotation = Quaternion.Euler(0, 0, touchAngle);
            }
            else
            {
                prObj.transform.rotation = Quaternion.Euler(0, 0, touchAngle + 90f);
            }
            
        }


        /*if (isRotating)
        {

            directionX = (this.transform.localPosition.x + (Screen.width / 2)) - Input.GetTouch(0).position.x;
            directionY = (this.transform.localPosition.y + (Screen.height / 2)) - Input.GetTouch(0).position.y;

            touchPosX = Input.GetTouch(0).position.x - (Screen.width / 2);
            touchPosY = Input.GetTouch(0).position.y - (Screen.height / 2);
            Debug.Log("x : " + directionX + "y: " + directionY);

            if (touchPosX > 0)
            {
                if (touchPosY > 0) // 1사분면 터치 ( x>0, y>0 )
                {
                    touchQuadrant = QUADRANT1;
                    //orbitAround(directionX, directionY, QUADRANT1);
                
                }
                else // 4사분면 터치 ( x>0, y<0 )
                {
                    touchQuadrant = QUADRANT4;
                    //orbitAround(directionX, directionY, QUADRANT4); // 원래 y좌표가 더 작음. -> 즉 왼쪽으로 드래그한것
                  
                }
            }
            else
            {
                if (touchPosY > 0) // 2사분면 터치 ( x<0, y>0 )
                {
                    touchQuadrant = QUADRANT2;
                    //orbitAround(directionX, directionY, QUADRANT2);
    
                }
                else // 3사분면 터치 ( x<0, y<0 )
                {
                    touchQuadrant = QUADRANT3;
                    //(directionX, directionY, QUADRANT3);

                }
            }

            inhalerPosX = this.transform.localPosition.x;
            inhalerPosY = this.transform.localPosition.y;
            //Debug.Log("위치 x : " + this.transform.localPosition.x + "위치 y : " + (this.transform.localPosition.y + 65f));

            Debug.Log("거리:" + Vector2.Distance(new Vector2(inhalerPosX, inhalerPosY), new Vector2(touchPosX, touchPosY)));

            //기준 : (0,0)월드좌표
            if(inhalerPosX > 0) 
            {
                if (inhalerPosY > 0) // 1사분면 터치 ( x>0, y>0 )
                {
                    inhalerQuadrant = QUADRANT1;
                    if (Vector2.Distance(new Vector2(inhalerPosX, inhalerPosY), new Vector2(touchPosX, touchPosY)) < 70)
                    {

                        orbitAround(directionX, directionY, QUADRANT1);
                    }
                    else
                    {
                        if (this.transform.position != oldPos && movingDirection != 0)
                        {
                            direction(movingDirection);
                        }
                    }
                    
                }
                else // 4사분면 터치 ( x>0, y<0 )
                {
                    inhalerQuadrant = QUADRANT4;
                    if (Vector2.Distance(new Vector2(inhalerPosX, inhalerPosY), new Vector2(touchPosX, touchPosY)) < 70)
                    {
                        orbitAround(directionX, directionY, QUADRANT4); // 원래 y좌표가 더 작음. -> 즉 왼쪽으로 드래그한것
                    }
                    else
                    {
                        if (this.transform.position != oldPos && movingDirection != 0)
                        {
                            direction(movingDirection);
                        }
                    }
                }
            }
            else
            {
                if (inhalerPosY > 0) // 2사분면 터치 ( x<0, y>0 )
                {
                    inhalerQuadrant = QUADRANT2;
                    if (Vector2.Distance(new Vector2(inhalerPosX, inhalerPosY), new Vector2(touchPosX, touchPosY)) < 70)
                    {
                        orbitAround(directionX, directionY, QUADRANT2);
                    }
                    else
                    {
                        if (this.transform.position != oldPos && movingDirection != 0)
                        {
                            direction(movingDirection);
                        }
                    }
                    
                }
                else // 3사분면 터치 ( x<0, y<0 )
                {
                    inhalerQuadrant = QUADRANT3;
                    if (Vector2.Distance(new Vector2(inhalerPosX, inhalerPosY), new Vector2(touchPosX, touchPosY)) < 70)
                    {
                        orbitAround(directionX, directionY, QUADRANT3);
                    }
                    else
                    {
                        if (this.transform.position != oldPos && movingDirection != 0)
                        {
                            direction(movingDirection);
                        }
                    }

                }
            }
            
            //Vector from center to mouse pos
            //dir = Input.GetTouch(0).position - new Vector2(this.transform.position.x, this.transform.position.y);
            //dist = Vector2.Distance(Input.GetTouch(0).position, new Vector2(this.transform.localPosition.x + (Screen.width / 2), this.transform.localPosition.y + (Screen.height / 2)));

            //Debug.Log("터치 x :" + Input.GetTouch(0).position.x);
            //Debug.Log("풍혈 x :" + (this.transform.localPosition.x + (Screen.width / 2)));

            //iTween.MoveTo(this.transform.gameObject, iTween.Hash("x", , "y", , "easeType", iTween.EaseType.easeOutCirc, "speed", 10f));

        }


                //(new Vector2(Input.GetTouch(0).position.x - (Screen.width / 2), Input.GetTouch(0).position.y - (Screen.height / 2)) - new Vector2(this.transform.position.x, this.transform.position.y));
            //Debug.Log("벡터:" + dir);
            //Debug.Log("풍혈 위치:" + this.transform.position);

            //Debug.Log(dir);
            //Distance between mouse and the center
            //dist = Vector2.Distance(Input.GetTouch(0).position, new Vector2(this.transform.localPosition.x + (Screen.width / 2), this.transform.localPosition.y + (Screen.height / 2)));
            //dist = Mathf.Sqrt(dir.x * dir.x + dir.y * dir.y);
            //Debug.Log("거리: " + dist);
            
            //if mouse is not outside nor too inside the wheel
            //if (dist < 150)
            //{
            //    angle = Mathf.Atan2(dir.x, dir.y) * Mathf.Rad2Deg; //alien technology
            //    angle = (angle > 0) ? angle : angle + 360; //0 to 360 instead of -180 to 180

            //    Debug.Log(angle);
                //this if blocks going back or jumping too far
                //if ((angle < check && check - angle < 90) || angle > 350)
                //{
                 //   check = angle;
                 //   this.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
                  //  Debug.Log("돌아가라~");
                    //Vector3.back for counter clockwise, Vector3.forward for clockwise..I think
                //}
         //   }

            
        //}
        //to confirm if it has passed full circle
        /*if (angle > 160 && angle < 200)
        {
            checkPoint = true;
        }

        if (angle > 350 && checkPoint)
        {
            checkPoint = false;
            Debug.log("SCORE++");
        }*/
        //orbitAround();

        /*if(Input.touchCount > 0)
        {
            Vector2 deltaPosition = Input.GetTouch(0).deltaPosition;

            switch(Input.GetTouch(0).phase)
            {
                case TouchPhase.Began:
                    break;
                case TouchPhase.Moved:
                    dragInhaler(deltaPosition);
                    break;
                case TouchPhase.Ended:
                    Plane objPlane = new Plane(Camera.main.transform.forward * -1, this.transform.position);

                    Vector3 endPos = new Vector3(0, 0, 0);

                    Ray mRay = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);
                    float rayDistance;
                    if (objPlane.Raycast(mRay, out rayDistance))
                    {
                        endPos = mRay.GetPoint(rayDistance);
                    }

                    if(endPos.x > this.transform.position.x)
                    {

                    }


                    break;
                default:
                    break;
            }
        }


        if ((Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Ended)) //|| Input.GetMouseButton(0))
        {

            Debug.Log("터치~");
            Plane objPlane = new Plane(Camera.main.transform.forward * -1, this.transform.position);

            Ray mRay = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);
            float rayDistance;
            if (objPlane.Raycast(mRay, out rayDistance))
            {
                this.transform.position = mRay.GetPoint(rayDistance);
            }
        }*/


    }


    private void OnMouseDown()
    {
        //inhaler = Instantiate(inhaler_off);
        //inhaler.transform.position = this.transform.position;
        //inhaler.transform.localScale = this.transform.localScale;

        
        currentInhaler = this.gameObject;
        if (currentInhaler.transform.GetComponent<SpriteRenderer>().color == normalColor)
        {
            this.transform.GetComponent<SpriteRenderer>().color = Color.green;
            //inhaler_off.SetActive(false);
            Debug.Log("켜짐");
            isRotating = true;
        }


    }

    private void OnMouseUp()
    {
        isRotating = false;
        movingDirection = 0;
        

        if (this.transform.GetComponent<SpriteRenderer>().color != Color.red)
        {
            this.transform.GetComponent<SpriteRenderer>().color = normalColor;
        }
        Debug.Log("마우스업");
    }

    /*void dragInhaler(Vector2 dp)
    {
        inhaler.position = new Vector3(Mathf.Clamp((dp.x * dragSpeed) + inhaler.position.x, startingPos.x - horizontalLimit, startingPos.x + horizontalLimit),
                                       Mathf.Clamp((dp.y * dragSpeed) + inhaler.position.y, startingPos.y - verticalLimit, startingPos.y + verticalLimit), inhaler.position.z);
    }*/

    void orbitAround(float xDir, float yDir, int where)
    {
        switch(where)
        {
            case QUADRANT1:
                if(xDir > 0) // 오른쪽으로 드래그
                {
                    direction(RIGHT);
                    movingDirection = RIGHT;
                    //Debug.Log("여기~");
                    
                    
                }
                else // 왼쪽으로 드래그
                {
                    direction(LEFT);
                    movingDirection = LEFT;
                    //Debug.Log("여기222~");
                    
                }
                
                break;

            case QUADRANT2:
                if (xDir > 0) // 오른쪽으로 드래그
                {
                    direction(RIGHT);
                    movingDirection = RIGHT;
                    //Debug.Log("여기~");
                   

                }
                else // 왼쪽으로 드래그
                {
                    direction(LEFT);
                    movingDirection = LEFT;
                    //Debug.Log("여기222~");
                    
                }
                break;
            case QUADRANT3:
                if (xDir > 0) // 왼쪽으로 드래그
                {
                    direction(LEFT);
                    movingDirection = LEFT;
                    //Debug.Log("여기~");
                    

                }
                else // 오른쪽으로 드래그
                {
                    direction(RIGHT);
                    movingDirection = RIGHT;
                    //Debug.Log("여기222~");
                   
                }
                break;
            case QUADRANT4:
                if (xDir < 0) // 오른쪽으로 드래그
                {
                    direction(RIGHT);
                    movingDirection = RIGHT;
                    //Debug.Log("여기~");
                    

                }
                else // 왼쪽으로 드래그
                {
                    Debug.Log("이게실행된다고?");
                    direction(LEFT);
                    movingDirection = LEFT;
                    //Debug.Log("여기222~");
                    /*if (yDir > 0)
                    {
                        transform.RotateAround(house.transform.position, Vector3.forward, speed * Time.deltaTime);
                        
                    }
                    else
                    {
                        transform.RotateAround(house.transform.position, Vector3.forward, speed * Time.deltaTime);
                        
                    }*/
                }
                break;
            default:
                break;
        }
        
        
    }

    void direction(int where)
    {
        if(where == LEFT)
        {
            transform.RotateAround(house.transform.position, new Vector3(0, 0, -1), speed * Time.deltaTime);
        }
        else if (where == RIGHT)
        {
            transform.RotateAround(house.transform.position, Vector3.forward, speed * Time.deltaTime);
        }
    }
}
