using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour {
    int speed = 4;
    int Rotatespeed = 100;
    //public GameObject Zup, Zdown;
    //bool isUp = true, isDown = false;
	// Use this for initialization
	void Start () {
        //Zup = GameObject.FindGameObjectWithTag("Zup");
       // Zdown = GameObject.FindGameObjectWithTag("ZDown");
	}
	
	// Update is called once per frame
	void Update () {

        if (Input.GetKey(KeyCode.A))
        {
            transform.Rotate(new Vector3(0, -Rotatespeed * Time.deltaTime, 0), Space.Self);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            transform.Rotate(new Vector3(0, +Rotatespeed * Time.deltaTime, 0), Space.Self);
        }
        else if (Input.GetKey(KeyCode.W))
        {
            transform.Translate(new Vector3(0, 0, speed * Time.deltaTime), Space.Self);
            //Debug.Log(this.transform.forward);
        }
        else if (Input.GetKey(KeyCode.S))
        {
            transform.Translate(new Vector3(0, 0, -speed * Time.deltaTime), Space.Self);
            //transform.localPosition += -this.transform.forward * speed * Time.deltaTime;
        }


        if(this.transform.position.z > 7.2f) //&& isUp == false)
        {
            GameObject.FindGameObjectWithTag("Player").transform.SetParent(GameObject.FindGameObjectWithTag("Zup").transform);
        }
        if(this.transform.position.z<= 6.7f) 
        {
            GameObject.FindGameObjectWithTag("Player").transform.SetParent(GameObject.FindGameObjectWithTag("ZDown").transform);
        }
    }
    void transRotate1()
    {
        //Debug.Log("im up");
        //this.transform.localRotation = new Quaternion(this.transform.localRotation.x, 0, this.transform.localRotation.z, 0);
        //this.transform.localPosition = new Vector3(this.transform.localPosition.x, 0.5f, this.transform.localPosition.z);
        //StartCoroutine(Wait(isDown));
    }
    void transRotate2()
    {
       // Debug.Log("im down");
        //this.transform.localRotation = new Quaternion(this.transform.localRotation.x, 0, this.transform.localRotation.z, 0);
       //this.transform.localPosition = new Vector3(this.transform.localPosition.x, 0.5f, this.transform.localPosition.z);
       // StartCoroutine(Wait(isUp));
    }
    //IEnumerator Wait(bool isUpORDown)
    //{
       // yield return null;
       // isUpORDown = false;
   // }
}
