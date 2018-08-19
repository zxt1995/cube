using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ExpManager : MonoBehaviour {
    public GameObject []playerList;
    public GameObject  playerSelf;
    public List<GameObject> aa;
    public List<int> killList;
    public List<int> DeathList;
    bool isLoadPlayer = false;
    public Text timeClock;
    public GameObject result;
    public bool isEnd;
    // Use this for initialization
    void Start () {
        playerSelf = GameObject.FindGameObjectWithTag("Player");
        StartCoroutine(findplayer());
    }
	IEnumerator findplayer()
    {
        yield return new WaitForSeconds(2f);
        playerList = GameObject.FindGameObjectsWithTag("Player");
        LoadPlayerData();
        int timei = 1800;
        while(timei > 0)
        {
            timei--;
            timeClock.text = "Time: " + timei / 60 + ":" + timei % 60;
            yield return new WaitForSeconds(1);
        }
        isEnd = true;
    }
    void LoadPlayerData()
    {
        int length = playerList.Length;
        for(int i =0;i<length;i++)
        {
            GameObject kk = Resources.Load("head") as GameObject;
            aa.Add(Instantiate(kk,transform) as GameObject);
            
            killList.Add(0);
            DeathList.Add(0);
        }
        isLoadPlayer = true;
        for (int i = 0; i < length; i++)
        {
            if (playerList[i].name == playerSelf.name)
            {
                aa[i].transform.GetChild(1).gameObject.SetActive(true);
            }
        }
    }
    // Update is called once per frame
    void Update () {
        if (isLoadPlayer == true)
        {
            for (int i = 0; i < playerList.Length; i++)
            {
                killList[i] = playerList[i].GetComponent<PlayerINFO>().Kill;
                DeathList[i] = playerList[i].GetComponent<PlayerINFO>().Death;
                aa[i].transform.GetChild(0).GetComponent<Text>().text = "Kill" + (killList[i]);
            }
           // for (int i = 0; i < playerList.Length; i++) 
          //  {
          //      for (int j = 0; j < playerList.Length; j++)
         //       {
         //           if (killList[j] < killList[j + 1]) ;   
         //       }
         //   }
        }
        
        if (isEnd == true)
        {
            int max = 0;
            int maxi = 0;
            for (int i = 0; i < playerList.Length; i++)
            {
                if (max < killList[i])
                {
                    max = killList[i];
                    maxi = i;
                }
            }
            if(playerList[maxi].name == playerSelf.name)
            {
                result.transform.GetChild(0).GetComponent<Text>().text = "YOU WIN";
            }
            else
            {
                result.transform.GetChild(0).GetComponent<Text>().text = "YOU LOSE";
            }
            result.SetActive(true);
            isEnd = false;
        }

        for (int i = 0; i < playerList.Length; i++)
        {
            if (playerList[i].name != playerSelf.name)
            {
                if ((playerList[i].transform.position - playerSelf.transform.position).sqrMagnitude <= playerSelf.GetComponent<FieldOfView>().viewRadius * playerSelf.GetComponent<FieldOfView>().viewRadius)
                {
                    playerList[i].transform.GetChild(3).gameObject.SetActive(true);
                }
                else
                    playerList[i].transform.GetChild(3).gameObject.SetActive(false);
            }
        }
    }
}
