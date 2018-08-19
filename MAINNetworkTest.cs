using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MAINNetworkTest : MonoBehaviour
{
    static public MAINNetworkTest instance;
    public GameObject player;
    // Use this for initialization
    void Start()
    {
        instance = this;
    }

    // Update is called once per frame
    void Update()
    {

    }
    private void OnLevelWasLoaded(int level)
    {
        PhotonNetwork.Instantiate(player.name, new Vector3(Random.Range(6, 12), -0.5f, Random.Range(1,4)), new Quaternion(0, 0, 0, 0), 0);
        //GameObject.FindGameObjectWithTag("Player").transform.SetParent(GameObject.FindGameObjectWithTag("ZDown").transform);
    }
}
