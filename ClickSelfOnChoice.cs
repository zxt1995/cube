using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ClickSelfOnChoice : MonoBehaviour {
    public InputField K;
    public Image kk;
       // Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    public void OnclickSelf()
    {
        GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerINFO>().realName = (int)K.text[0];
        kk.gameObject.SetActive(false);
    }
}
