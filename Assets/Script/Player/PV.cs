using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PV : MonoBehaviour
{
    // Start is called before the first frame update
    public float PVj=1200;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(PVj<=0)
        {
            Destroy(gameObject);
        }
    }
}
