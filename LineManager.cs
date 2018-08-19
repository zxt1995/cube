using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
public class LineManager : MonoBehaviour
{
    static public LineManager instance;
    Animator anim;
    bool isMouseDown = false;
    public RawImage centerOfSix;
    public RawImage[] oneOfSix;
    public float recLength = 50;
    public float usingTime = 0.25f;
    public Material material;
    private List<Vector3> lineInfo;
    private int skillList = 0;
    private bool isEnterCheck0 = false;
    private bool isEnterCheck1 = false;
    private bool isEnterCheck3 = false;
    // Use this for initialization
    void Start()
    {
        lineInfo = new List<Vector3>();
        instance = this;

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && Input.mousePosition.x > Screen.width * 1 / 2)
        {
            Debug.Log("123");
            centerOfSix.rectTransform.position = (Vector2)Input.mousePosition;
            centerOfSix.rectTransform.DOScale(new Vector2(2, 2), usingTime);
            for (int i = 0; i < 6; i++)
            {
                oneOfSix[i].rectTransform.position = (Vector2)Input.mousePosition;
                oneOfSix[i].rectTransform.DOScale(new Vector2(2, 2), usingTime);
            }

            oneOfSix[0].rectTransform.DOMove((Vector2)Input.mousePosition + new Vector2(0, recLength), usingTime);
            oneOfSix[1].rectTransform.DOMove((Vector2)Input.mousePosition + new Vector2(1.732f / 2 * recLength, recLength / 2), usingTime);
            oneOfSix[2].rectTransform.DOMove((Vector2)Input.mousePosition + new Vector2(1.732f / 2 * recLength, -recLength / 2), usingTime);
            oneOfSix[3].rectTransform.DOMove((Vector2)Input.mousePosition + new Vector2(0, -recLength), usingTime);
            oneOfSix[4].rectTransform.DOMove((Vector2)Input.mousePosition + new Vector2(-1.732f / 2 * recLength, -recLength / 2), usingTime);
            oneOfSix[5].rectTransform.DOMove((Vector2)Input.mousePosition + new Vector2(-1.732f / 2 * recLength, recLength / 2), usingTime);
            isMouseDown = true;
        }
        if (Input.GetMouseButton(0) && isMouseDown == true)
        {
            lineInfo.Add(Input.mousePosition);
            if (((Vector2)Input.mousePosition - ((Vector2)centerOfSix.rectTransform.position + new Vector2(0, recLength))).sqrMagnitude <= 50 && isEnterCheck0 == false)
            {
                skillList += 100000;
                isEnterCheck0 = true;
            }
            if (((Vector2)Input.mousePosition - ((Vector2)centerOfSix.rectTransform.position + new Vector2(1.732f / 2 * recLength, recLength / 2))).sqrMagnitude <= 50 && isEnterCheck1 == false)
            {
                skillList += 1;
                isEnterCheck1 = true;
            }
            if (((Vector2)Input.mousePosition - ((Vector2)centerOfSix.rectTransform.position + new Vector2(0, -recLength))).sqrMagnitude <= 50 && isEnterCheck3 == false)
            {
                skillList += 100;
                isEnterCheck3 = true;
            }
        }
        if (Input.GetMouseButtonUp(0) && isMouseDown == true)
        {
            isEnterCheck0 = false;
            isEnterCheck1 = false;
            isEnterCheck3 = false;
            CheckSkill();
            skillList = 0;
            isMouseDown = false;
            lineInfo.Clear();
            centerOfSix.rectTransform.position = new Vector2(-Screen.width, -Screen.height);
            centerOfSix.rectTransform.DOScale(new Vector2(0.01f, 0.01f), usingTime);
            for (int i = 0; i < 6; i++)
            {
                oneOfSix[i].rectTransform.DOScale(new Vector2(0.01f, 0.01f), usingTime);
            }

        }
    }
    private void CheckSkill()
    {
        if (GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerINFO>().MP >= 0)
        {
            PhotonView pv = GameObject.FindGameObjectWithTag("Player").GetComponent<PhotonView>();
            if (skillList == 100001)
            {
                pv.RPC("AttackAnim", PhotonTargets.All, "AuraFire");
            }
            if (skillList == 101)
            {
                pv.RPC("AttackAnim", PhotonTargets.All, "FireSpray");
            }
        }
    }

    void OnPostRender()
    {
        material.SetPass(0);//设置该材质通道，0为默认值
        GL.LoadOrtho();//设置绘制2d图像
        GL.Begin(GL.LINES);//绘制类型为线段

        for (int i = 0; i < lineInfo.Count - 1; i++)
        {
            Vector3 start = lineInfo[i];
            Vector3 end = lineInfo[i + 1];
            Draw(start.x, start.y, end.x, end.y);
        }

        GL.End();
    }

    //将屏幕中某个点的像素坐标进行转换
    void Draw(float x1, float y1, float x2, float y2)
    {
        GL.Vertex(new Vector3(x1 / Screen.width, y1 / Screen.height, 0));
        GL.Vertex(new Vector3(x2 / Screen.width, y2 / Screen.height, 0));
    }
}
