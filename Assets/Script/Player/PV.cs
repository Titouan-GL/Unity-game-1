using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class PV : MonoBehaviour
{
    // Start is called before the first frame update
    public float PVj=1200;
    public float totalPV=1200;
    public Image bar;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        UpdateHealth(PVj);
        if(PVj<=0)
        {
            Destroy(gameObject);
        }
    }

    void UpdateHealth(float value)
    {
        bar.fillAmount=(float)value/totalPV;
    }
}
