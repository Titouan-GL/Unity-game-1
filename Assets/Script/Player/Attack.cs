using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    [SerializeField] GameObject bullet;
    private SpriteRenderer rend;
    private PlayerAnimation playerAnim;
    private float speedBullet=5;
    private bool tir;
    
    void Start()
    {
        playerAnim=GetComponent<PlayerAnimation>();
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.A))
        {
            playerAnim.isAttacking=true;
            tir=true;
            StartCoroutine(spawn1(0.1f,bullet));
        }
        if(Input.GetKeyUp(KeyCode.A))
        {
            playerAnim.isAttacking=false;
            tir=false;
        }
    }

    IEnumerator spawn1(float time,GameObject prefabs)
    {
        yield return new WaitForSeconds(time);
        if(tir)
        {
            if(GetComponent<PlayerAnimation>().facingRight)
            {
                Transform bulletTransf=prefabs.GetComponent<Transform>();
                bulletTransf.position=new Vector3((GetComponent<Transform>().position.x)+1.64f,(GetComponent<Transform>().position.y)+1.68f,bulletTransf.position.z);
                GameObject bullet1=Instantiate(prefabs,bulletTransf.position,bulletTransf.rotation);
                Rigidbody2D bullet1rb=bullet1.GetComponent<Rigidbody2D>();
                bullet1rb.velocity=new Vector2(10*speedBullet,bullet1rb.velocity.y);
                StartCoroutine(spawn1(time,prefabs));
            }else{
                Transform bulletTransf=prefabs.GetComponent<Transform>();
                bulletTransf.position=new Vector3((GetComponent<Transform>().position.x)-1.64f,(GetComponent<Transform>().position.y)+1.68f,bulletTransf.position.z);
                GameObject bullet1=Instantiate(prefabs,bulletTransf.position,bulletTransf.rotation);
                Rigidbody2D bullet1rb=bullet1.GetComponent<Rigidbody2D>();
                bullet1rb.velocity=new Vector2(-10*speedBullet,bullet1rb.velocity.y);
                StartCoroutine(spawn1(time,prefabs));
            }
        }
    }
}
