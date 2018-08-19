using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerINFO : Photon.MonoBehaviour//MonoBehaviour, IPunObservable
{
    public int HP = 100;
    public int MP = 100;
    public int Kill = 0;
    public int Death = 0;
    public int realName = 65;
    int maxHP = 100, maxMP = 100;
    public Slider HPslider;
    public Slider MPslider;
    public Text realNameText;

    public int myname;
    void Start()
    {
        myname = Random.Range(10000, 99999);
        this.gameObject.name = myname.ToString();
        StartCoroutine(RefreshMP());
    }
    IEnumerator RefreshMP()
    {
        while (true)
        {
            if (MP < maxMP)
            {
                MP++;
            }
            yield return new WaitForSeconds(3f);
        }
    }
    void Update()
    {
        HPslider.GetComponent<Slider>().value = (float)HP / maxHP;
        MPslider.GetComponent<Slider>().value = (float)MP / maxMP;
        realNameText.text = ((char)realName).ToString();
        if (!photonView.isMine)
        {
            transform.position = Vector3.Lerp(transform.position, correctPlayerPos, Time.deltaTime * 5);
            transform.rotation = Quaternion.Lerp(transform.rotation, correctPlayerRot, Time.deltaTime * 5);
        }
    }
    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.isWriting)
        {
            stream.SendNext(realName);
            stream.SendNext(this.HP);
            stream.SendNext(this.MP);
            stream.SendNext(this.myname);
            stream.SendNext(this.Kill);
            stream.SendNext(this.Death);
            stream.SendNext(transform.position);
            stream.SendNext(transform.rotation);
        }
        else
        {
            realName = (int)stream.ReceiveNext();
            this.HP = (int)stream.ReceiveNext();
            this.MP = (int)stream.ReceiveNext();
            this.myname = (int)stream.ReceiveNext();
            this.Kill = (int)stream.ReceiveNext();
            this.Death = (int)stream.ReceiveNext();
            this.gameObject.name = myname.ToString();
            correctPlayerPos = (Vector3)stream.ReceiveNext();
            correctPlayerRot = (Quaternion)stream.ReceiveNext();
        }
    }
    private Vector3 correctPlayerPos = Vector3.zero; //We lerp towards this
    private Quaternion correctPlayerRot = Quaternion.identity;
    public void HPDamage(int i)
    {
        HP -= i;
        if (HP <= 0)
        {
            GetComponent<AnimManager>().DeathAnim();
            StartCoroutine(Refresh());
        }
        else if (i > 0)
        {
            GetComponent<AnimManager>().DamageAnim();
        }
    }
    public void MPDamage(int i)
    {
        MP -= i;
    }
    void HPMPRefresh()
    {
        if (HP <= 0)
        {
            HP = maxHP;
            MP = maxMP;
        }
    }
    void Killadd(int i)
    {
        Kill += i;
    }
    void Deathadd()
    {
        Death++;
    }
    IEnumerator Refresh()
    {
        yield return new WaitForSeconds(3f);
        this.transform.position = new Vector3(Random.Range(6, 12), -0.5f, Random.Range(1, 4));
        HPMPRefresh();
        GetComponent<AnimManager>().WaitAnim();
    }
}
