using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class EnnemieAttribut : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] TextMeshProUGUI txt;
    private float pv=100;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(pv<=0)
        {
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D dmg)
    {
        if(dmg.gameObject.tag =="epee")
        {
            pv-=12;
            txt.text="PV: "+pv;
        }
    }
}
