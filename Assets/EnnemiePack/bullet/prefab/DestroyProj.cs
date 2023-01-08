using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DestroyProj : MonoBehaviour
{
    // Start is called before the first frame update
    private GameObject txtGo;
    private TextMeshProUGUI txt;
    void Start()
    {
        StartCoroutine(DestroyTime(1));
        txtGo=GameObject.Find("Canvas1/TxtPV");
        txt=txtGo.GetComponent<TextMeshProUGUI>();
    }

    IEnumerator DestroyTime(float time)
    {
        yield return new WaitForSeconds(time);
        Destroy(gameObject);
    }

    void OnCollisionEnter2D(Collision2D go)
    {
        if(go.gameObject.name=="Player")
        {
            Transform goT=go.gameObject.GetComponent<Transform>();
            goT.position=new Vector3((goT.position.x)-0.1f, goT.position.y,goT.position.z);
            Debug.Log("Ouch !");
            PV goPv=go.gameObject.GetComponent<PV>();
            goPv.PVj=goPv.PVj-12;
            txt.text="Nombre de PV: "+goPv.PVj.ToString();
        }
        Destroy(gameObject);
    }
}
