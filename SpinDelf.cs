using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class SpinDelf : MonoBehaviour {
    float k = 0;
    bool isGoing = true;
	// Use this for initialization
	void Start () {
        StartCoroutine(WorldSpin());
	}
	
	// Update is called once per frame
	void Update () {
       /* if (isGoing)
        {
            transform.Rotate(new Vector3(0, 0, 0.1f));
            k += 0.1f;
        }
        if(k >= 90f)
        {
            k = 0;
            isGoing = false;
            StartCoroutine(Wait());
        }*/
       
    }
    IEnumerator WorldSpin()
    {
        int qua = 90;
        yield return new WaitForSeconds(10f);
        while (true)
        {
            this.transform.DOLocalRotate(new Vector3(0, 0, qua), 8);
            qua += 90;
            yield return new WaitForSeconds(18f);
        }
    }
    IEnumerator Wait()
    {
        yield return new WaitForSeconds(8f);
        isGoing = true;
    }
}
