using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
	Movement movement;
	PlayerAnimation playerAnim;
	Attack attack;
	Abilities abilities;
	
	public float speedFull;
	public float jumpSpeed;
	public float jumpHeight;
    public bool canMove;
    public bool canJump;
    public bool canDoubleJump;
    public bool canDash;
    public bool canWallSlide;

    //jumpLength ~= 9
	
    void Start()
    {
        movement=GetComponent<Movement>();
        playerAnim=GetComponent<PlayerAnimation>();
        //attack=GetComponent<Attack>();
        abilities=GetComponent<Abilities>();
        speedFull=10;
		jumpSpeed=12;
		jumpHeight = 3.5f;
        canMove = true;
        canJump = true;
        canDash = true;
        canDoubleJump = true;
        canWallSlide = true;
    }

    // Update is called once per frame
    void Update()
    {
		//attack.Attacking();
        abilities.StopTime();
        movement.rayscasts();
        if(canWallSlide){movement.wallJump(speedFull*Input.GetAxis("Horizontal"));}
        movement.dash(Input.GetAxis("Horizontal"));
        if(canJump){movement.jump(jumpSpeed,jumpHeight, speedFull);}
        if(canMove){movement.move(speedFull*Input.GetAxis("Horizontal"));}
        playerAnim.PlayAnimations();
        movement.generalUpdate();
    }
}
