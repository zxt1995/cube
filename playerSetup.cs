using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerSetup : MonoBehaviour {
    public Behaviour[] stuffNeedDisable;
    //public Camera myCamera;
    //public GameObject myMapCamera;
    PhotonView pv;
	// Use this for initialization
	void Start () {
        //myMapCamera = GameObject.FindGameObjectWithTag("MapCamera");
        pv = GetComponent<PhotonView>();
        if(!pv.isMine)
        {
           // myCamera.enabled = false;
            //myMapCamera.SetActive(false);

            for (int i = 0;i< stuffNeedDisable.Length; i++)
            {
                stuffNeedDisable[i].enabled = false;
            }
        }
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
