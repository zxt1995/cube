using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StateFollow : MonoBehaviour {
    float xOffset = 0;
    float yOffset = 125;
    float yOffset2 = 100;
    float yOffset3 = 150;
    public Slider recT;
    public Slider recTMP;
    public Text nameText;
    // Use this for initialization
    void Start () {
		
	}


    void Update()
    {
        if (recT != null && recTMP != null && nameText != null)
        {
            recT.GetComponent<RectTransform>().position = (Vector2)Camera.main.WorldToScreenPoint(transform.position) + new Vector2(xOffset, yOffset);
            recTMP.GetComponent<RectTransform>().position = (Vector2)Camera.main.WorldToScreenPoint(transform.position) + new Vector2(xOffset, yOffset2);
            nameText.GetComponent<RectTransform>().position = (Vector2)Camera.main.WorldToScreenPoint(transform.position) + new Vector2(xOffset, yOffset3);
        }
    }
}
