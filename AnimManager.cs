using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimManager : MonoBehaviour
{
    Animator anim;
    public GameObject prefFire;
    bool isRay = false;
    public LayerMask mask = 8;
    // Use this for initialization
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void WalkAnim()
    {
        anim.Play("Witch_Walk", 0);
    }
    public void WaitAnim()
    {
        anim.Play("Witch_Wait", 0);
    }
    public void DeathAnim()
    {
        anim.Play("Witch_Dead", 0);
    }
    public void DamageAnim()
    {
        anim.Play("Witch_Damage", 0);
    }
    [PunRPC]
    public void AttackAnim(string skillname)
    {
        anim.Play("Witch_Attack", 0);
        prefFire = GameObject.FindGameObjectWithTag(skillname);
        prefFire.transform.SetParent(transform);
        prefFire.transform.position = transform.position + new Vector3(0, 2, 0);
        prefFire.transform.rotation = new Quaternion(0, 0, 0, 0);
        if (skillname == "AuraFire")
        {
            StartCoroutine(SkillUSING(2));
        }
        else if (skillname == "FireSpray")
        {
            StartCoroutine(SkillUSING(3));
        }

    }
    IEnumerator SkillUSING(int mpcost)
    {
        GetComponent<PlayerINFO>().MPDamage(mpcost);
        if (mpcost == 2)
        {
            StartCoroutine(RayUse());
        }
        else
        {
            StartCoroutine(LineRayUse());
        }
        yield return new WaitForSeconds(3f);
        prefFire.transform.position = transform.position - new Vector3(0, 100, 0);
    }
    IEnumerator RayUse()
    {
        for (int j = 0; j < 3; j++)
        {
            int radius = 3;
            Collider[] cols = Physics.OverlapSphere(this.transform.position, radius, LayerMask.NameToLayer("layername"));
            if (cols.Length > 0)
            {
                for (int i = 0; i < cols.Length; i++)
                {
                    Debug.Log("检测到物体" + cols[i].name);
                    if (cols[i].tag == "Player" && cols[i].name != name)
                    {
                        cols[i].GetComponent<PlayerINFO>().HPDamage(4);
                        if (cols[i].GetComponent<PlayerINFO>().HP <= 0)
                        {
                            GetComponent<PlayerINFO>().Kill++;
                        }
                    }
                }
            }
            yield return new WaitForSeconds(1);
        }
    }
    IEnumerator LineRayUse()
    {
        Debug.Log("in lr");
        for (int i = 0; i < 30; i++)
        {
            //Debug.Log("in lr2");
            Ray ray = new Ray(transform.position + new Vector3(0, 0.5f, 0), transform.forward);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, 500, mask))
            {
                print(hit.collider.name);
                if (hit.transform.name != this.name)
                {
                    hit.transform.GetComponent<PlayerINFO>().HPDamage(1);
                    if (hit.transform.GetComponent<PlayerINFO>().HP == 0)
                    {
                        Debug.Log("hit.transform.GetComponent<PlayerINFO>().HP");
                        GetComponent<PlayerINFO>().Kill++;
                    }
                }
            }

            yield return new WaitForSeconds(0.1f);
        }

    }
}
