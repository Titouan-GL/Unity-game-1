using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
	public bool isRunning;
    public bool isJumping;
    public bool isFalling;
    public bool isGrounded;
	public bool isAttacking;
	public bool isDashing;
    public bool facingRight;
	public bool isWallSliding;
    private Animator anim;
	private string facing;
      
    void Start()
    {
        anim=GetComponentInChildren<Animator>();
		isRunning = false;
		isJumping = false;
		isFalling = false;
		isGrounded = false;
		isAttacking = false;
		isDashing = false;
		isWallSliding = false;
		facingRight = true;
		facing = "-Right";
    }  
    
    public void PlayAnimations()
    {
		if(facingRight){facing = "-Right";}
		else{facing = "-Left";}

		if(isDashing){
				anim.Play("Dash"+facing);
		}
		else if(isWallSliding){
				anim.Play("Wall-Slide"+facing);
		}
        else if(isJumping && !isGrounded){
			string str = "Jump"+facing;
			string str2 = "Wall-Slide"+facing;
			if (anim.GetCurrentAnimatorStateInfo(0).IsName(str2) || anim.GetNextAnimatorStateInfo(0).IsName(str))
				anim.Play(str);
			else if (!anim.GetNextAnimatorStateInfo(0).IsName(str) && !anim.GetCurrentAnimatorStateInfo(0).IsName(str))
				anim.CrossFadeInFixedTime(str, 0.1f);
		}
        else if((!isJumping && !isGrounded) || isFalling){
			string str = "Fall"+facing;
			if (!anim.GetNextAnimatorStateInfo(0).IsName(str) && !anim.GetCurrentAnimatorStateInfo(0).IsName(str))
				anim.CrossFadeInFixedTime(str, 0.1f);
		}
		else if(!isRunning && isAttacking){
			if(facingRight)
			{
				anim.Play("gunfight");
			}
			else{
				anim.Play("gunfight flipped");
			}
		}
		else if(isRunning && isAttacking){
			if(facingRight)
			{
				anim.Play("runGunFight");
			}
			else{
				anim.Play("runGunFight flipped");
			}
		}
		else if(isRunning && isGrounded){
			string str = "Run"+facing;
			if (!anim.GetNextAnimatorStateInfo(0).IsName(str) && !anim.GetCurrentAnimatorStateInfo(0).IsName(str))
				anim.CrossFadeInFixedTime(str, 0.1f);
		}
        else{
			string str = "Idle"+facing;
			if (!anim.GetNextAnimatorStateInfo(0).IsName(str) && !anim.GetCurrentAnimatorStateInfo(0).IsName(str))
				anim.CrossFadeInFixedTime(str, 0.1f);
		}
    }
}
