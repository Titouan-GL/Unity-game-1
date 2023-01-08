using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyBullet : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(DestroyTime(1));
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    IEnumerator DestroyTime(float time)
    {
        yield return new WaitForSeconds(time);
        Destroy(gameObject);
    }
    void OnCollisionEnter2D(Collision2D go)
    {
        if(go.gameObject.tag=="Ennemie")
        {
            PV goPv=go.gameObject.GetComponent<PV>();
            goPv.PVj=goPv.PVj-12;
        }
        Destroy(gameObject);
    }
}
