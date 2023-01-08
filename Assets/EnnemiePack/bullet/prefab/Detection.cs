using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Detection : MonoBehaviour
{
    // Start is called before the first frame update
    private bool fire;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnTriggerStay2D(Collider2D gameO)
    {
        if(gameO.gameObject.name=="Player" && !fire)
        {
            fire=true;
            GameObject.Find("TowerEnnemy").GetComponent<Fire>().SetFire(fire);
            GameObject.Find("TowerEnnemy").GetComponent<Fire>().FireTower();
        }
    }
    void OnTriggerExit2D(Collider2D gameO)
    {
        fire=false;
        GameObject.Find("TowerEnnemy").GetComponent<Fire>().SetFire(fire);
    }
}
