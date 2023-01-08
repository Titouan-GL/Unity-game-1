using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Abilities : MonoBehaviour
{
    // Start is called before the first frame update
    private Rigidbody2D rb;
    private bool clic;
    private PlayerAnimation playerAnim;
    private PlayerManager playerManager;
    private Animator anim;
    private float animSpeed;
    
    
    void Start()
    {
        rb=GetComponent<Rigidbody2D>();
        playerAnim=GetComponent<PlayerAnimation>();
        playerManager=GetComponent<PlayerManager>();
        clic=false;
        anim=GetComponentInChildren<Animator>();
        animSpeed=anim.speed;
    }

    // Update is called once per frame
    public void StopTime()
    {
        if(Input.GetKeyDown(KeyCode.Mouse1) && !clic)
        {
            Time.timeScale=0.5f;
            playerManager.speedFull=10/0.5f;
            playerManager.jumpSpeed=12/0.5f;
            anim.speed=animSpeed/0.5f;
            rb.gravityScale=2/0.5f;
            clic=true;
        }else{
            if(clic && Input.GetKeyDown(KeyCode.Mouse1))
            {
                Time.timeScale=1;
                playerManager.speedFull=10;
                playerManager.jumpSpeed=12;
                anim.speed=animSpeed;
                clic=false;
                rb.gravityScale=2;
            }
            
        }
    }
}
