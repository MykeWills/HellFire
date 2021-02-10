using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class CheckPoint : MonoBehaviour {
    public int pointCheck;
    public Text checkPoint;
    public float fadeTime;
    public float Speed;
    public float x;
    

    // Use this for initialization
    void Start () {
       
        pointCheck = 0;
        checkPoint.text = "";
        fadeTime = 0.0f;
        Speed = 1.0f;
        
    }
	
	// Update is called once per frame
	void Update () {
        x = PlayerRespawn.health;

        if (pointCheck == 0 && x < 1)
        {
            transform.position = new Vector3(445f, -29f, 444f);
        }
        else if (pointCheck == 1 && x < 1)
        {
            transform.position = new Vector3(311f, -27f, 385f);
        }
        else if (pointCheck == 2 && x < 1)
        {
            transform.position = new Vector3(432f, -28f, 236f);
        }
        else if (pointCheck == 3 && x < 1)
        {
            transform.position = new Vector3(164f, -29f, 330f);
        }
        else if (pointCheck == 4 && x < 1)
        {
            transform.position = new Vector3(95f, -27f, 368f);
        }
        else if (pointCheck == 5 && x < 1)
        {
            transform.position = new Vector3(48f, -19f, 243f);
        }
        else if (pointCheck == 6 && x < 1)
        {
            transform.position = new Vector3(98f, -38f, 105f);
        }
        else if (pointCheck == 7 && x < 1)
        {
            transform.position = new Vector3(173f, -29f, 139f);
        }
        else if (pointCheck == 8 && x < 1)
        {
            transform.position = new Vector3(1195.2f, -29f, 479f);
        }

        if (fadeTime > 0)
        {
            checkPoint.text = "CheckPoint!";
            fadeTime -= Time.deltaTime * Speed;
        }

        else if (fadeTime < 0)
        {
            fadeTime = 0;
            checkPoint.text = "";
        }
    }
    void OnTriggerEnter(Collider other)
    {
        // Check Point 1
        if (other.gameObject.CompareTag("CheckPoint"))
        {
            pointCheck = pointCheck + 1;
            fadeTime += 5;
        }
        //Check point 2
        else if (other.gameObject.CompareTag("CheckPoint2"))
        {
            pointCheck = pointCheck + 1;
            fadeTime += 5;
        }
        //Check point 3
        else if (other.gameObject.CompareTag("CheckPoint3"))
        {
            pointCheck = pointCheck + 1;
            fadeTime += 5;
        }
        //Check point 4
        else if (other.gameObject.CompareTag("CheckPoint4"))
        {
            pointCheck = pointCheck + 1;
            fadeTime += 5;
        }
        //check Point 5
        else if (other.gameObject.CompareTag("CheckPoint5"))
        {
            pointCheck = pointCheck + 1;
            fadeTime += 5;
        }
        //Check point 6
        else if (other.gameObject.CompareTag("CheckPoint6"))
        {
            pointCheck = pointCheck + 1;
            fadeTime += 5;
        }
        // check point 7
        else if (other.gameObject.CompareTag("CheckPoint7"))
        {
            pointCheck = pointCheck + 1;
            fadeTime += 5;
        }
        //Boss Check point
        else if (other.gameObject.CompareTag("BossCheckPoint"))
        {
            pointCheck = pointCheck + 1;
            fadeTime += 5;
        }
    }
    void OnTriggerExit(Collider other)
    {
        // when player exits the Check point trigger
        if (other.gameObject.CompareTag("CheckPoint"))
        {
            other.gameObject.SetActive(false);      
        }
        else if (other.gameObject.CompareTag("CheckPoint2"))
        {
            other.gameObject.SetActive(false);     
        }
        else if (other.gameObject.CompareTag("CheckPoint3"))
        {
            other.gameObject.SetActive(false);
        }
        else if (other.gameObject.CompareTag("CheckPoint4"))
        {
            other.gameObject.SetActive(false);  
        }
        else if (other.gameObject.CompareTag("CheckPoint5"))
        {
            other.gameObject.SetActive(false);
        }
        else if (other.gameObject.CompareTag("CheckPoint6"))
        {
            other.gameObject.SetActive(false);
        }
        else if (other.gameObject.CompareTag("CheckPoint7"))
        {
            other.gameObject.SetActive(false);
        }
        else if (other.gameObject.CompareTag("BossCheckPoint"))
        {
            other.gameObject.SetActive(false);
        }
    }
}
