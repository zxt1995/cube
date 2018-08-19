using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour {
    public GameObject Player;
    private Vector3 offset = new Vector3(0, 3, -2);
    bool isFindPlayer = true;
	// Use this for initialization
	void Start () {
        
    }
	
	// Update is called once per frame
	void Update () {
        if (isFindPlayer)
        {
            Player = GameObject.FindGameObjectWithTag("Player");
            this.transform.position = Player.transform.position + offset;
            this.transform.SetParent(Player.transform);
            isFindPlayer = false;
        }
    }
    private void LateUpdate()
    {
        
    }
}
