using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fire : MonoBehaviour
{
    [SerializeField] GameObject prefab1;
    [SerializeField] GameObject prefab2;
    [SerializeField] GameObject prefab3;
    [SerializeField] GameObject prefab4;
    private float speedBullet=10;
    private bool fire;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void FireTower()
    {
        StartCoroutine(spawn1(0.5f,prefab1));
    }
    public void SetFire(bool tir)
    {
        fire=tir;
    }
    
    IEnumerator spawn1(float time,GameObject prefabs)
    {
        yield return new WaitForSeconds(time);
        if(fire)
        {
            Transform bulletTransf=prefabs.GetComponent<Transform>();
            GameObject bullet1=Instantiate(prefabs,bulletTransf.position,bulletTransf.rotation);
            Rigidbody2D bullet1rb=bullet1.GetComponent<Rigidbody2D>();
            bullet1rb.velocity=new Vector2(-10*speedBullet,bullet1rb.velocity.y);
            StartCoroutine(spawn2(0.1f,prefab2));
        }
        
    }
    IEnumerator spawn2(float time,GameObject prefabs)
    {
        yield return new WaitForSeconds(time);
        if(fire)
        {
            Transform bulletTransf=prefabs.GetComponent<Transform>();
            GameObject bullet1=Instantiate(prefabs,bulletTransf.position,bulletTransf.rotation);
            Rigidbody2D bullet1rb=bullet1.GetComponent<Rigidbody2D>();
            bullet1rb.velocity=new Vector2(-10*speedBullet,bullet1rb.velocity.y);
            StartCoroutine(spawn3(0.1f,prefab3));
        }
        
    }
    IEnumerator spawn3(float time,GameObject prefabs)
    {
        yield return new WaitForSeconds(time);
        if(fire)
        {
            Transform bulletTransf=prefabs.GetComponent<Transform>();
            GameObject bullet1=Instantiate(prefabs,bulletTransf.position,bulletTransf.rotation);
            Rigidbody2D bullet1rb=bullet1.GetComponent<Rigidbody2D>();
            bullet1rb.velocity=new Vector2(-10*speedBullet,bullet1rb.velocity.y);
            StartCoroutine(spawn4(0.1f,prefab4));
        }
        
    }
    IEnumerator spawn4(float time,GameObject prefabs)
    {
        yield return new WaitForSeconds(time);
        if(fire)
        {
            Transform bulletTransf=prefabs.GetComponent<Transform>();
            GameObject bullet1=Instantiate(prefabs,bulletTransf.position,bulletTransf.rotation);
            Rigidbody2D bullet1rb=bullet1.GetComponent<Rigidbody2D>();
            bullet1rb.velocity=new Vector2(-10*speedBullet,bullet1rb.velocity.y);
            StartCoroutine(spawn1(0.1f,prefab1));
        }
        
    }

}
