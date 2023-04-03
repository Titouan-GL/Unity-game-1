using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class Movement : MonoBehaviour
{
    private Rigidbody2D rigidbody2d;
    private BoxCollider2D collider2d;
    //[SerializeField] private Transform groundPosL;
    //[SerializeField] private Transform groundPosR;
    private PlayerManager playerManager;
    private PlayerAnimation playerAnim;
    //private Transform myTransform;
    [SerializeField]private Transform modelTransform;
    
    public bool isGrounded;
    public bool isJumping;
    private bool facingRight;
    public bool isDashing;
    private bool isWallJumping;
    private bool canDoubleJump;
    
    private float currentJumpHeight = 0; //distance de saut actuelle qui sert a définir quand le joueur ne doit plus sauter
    
    private float DashCurrent;
    private float DashSpeed; 
    private float DashLength; 
    private float DashRecovery;
    private float currentDashRecovery;
    private int dashDirection;

    public float currentWallRecovery;
    private float maxWallRecovery;

    RaycastHit2D hitLeftDown;
    RaycastHit2D hitRightDown;
    RaycastHit2D hitLeftUp;
    RaycastHit2D hitRightUp;
    RaycastHit2D hitUpLeft;
    RaycastHit2D hitDownLeft;
    RaycastHit2D hitUpRight;
    RaycastHit2D hitDownRight;
    
    void Start()
    {
        rigidbody2d = GetComponent<Rigidbody2D>();
        playerManager = GetComponent<PlayerManager>();
        collider2d=GetComponent<BoxCollider2D>();
        playerAnim=GetComponent<PlayerAnimation>();
        //myTransform=GetComponent<Transform>();
        isGrounded=true;
        facingRight = true;
		DashCurrent=0;
		DashSpeed=30; 
		DashLength=5; 
		DashRecovery = 0.3f;
		currentDashRecovery = 0;
        maxWallRecovery = 0.2f;
        currentWallRecovery = maxWallRecovery;
    }

    public void rayscasts(){

        Vector2 bottomLeftDown = new Vector2(collider2d.bounds.center.x - collider2d.bounds.extents.x, collider2d.bounds.center.y - collider2d.bounds.extents.y-0.05f);
        Vector2 bottomRightDown = new Vector2(collider2d.bounds.center.x + collider2d.bounds.extents.x, collider2d.bounds.center.y - collider2d.bounds.extents.y-0.05f);
        Vector2 TopLeftUp = new Vector2(collider2d.bounds.center.x - collider2d.bounds.extents.x, collider2d.bounds.center.y + collider2d.bounds.extents.y+0.05f);
        Vector2 TopRightUp = new Vector2(collider2d.bounds.center.x + collider2d.bounds.extents.x, collider2d.bounds.center.y + collider2d.bounds.extents.y+0.05f);
        Vector2 LeftTop = new Vector2(collider2d.bounds.center.x - collider2d.bounds.extents.x-0.05f, collider2d.bounds.center.y + collider2d.bounds.extents.y);
        Vector2 LeftBottom = new Vector2(collider2d.bounds.center.x - collider2d.bounds.extents.x-0.05f, collider2d.bounds.center.y - collider2d.bounds.extents.y);
        Vector2 RightTop = new Vector2(collider2d.bounds.center.x + collider2d.bounds.extents.x+0.05f, collider2d.bounds.center.y + collider2d.bounds.extents.y);
        Vector2 RightBottom = new Vector2(collider2d.bounds.center.x + collider2d.bounds.extents.x+0.05f, collider2d.bounds.center.y - collider2d.bounds.extents.y);

        hitLeftDown = Physics2D.Raycast(bottomLeftDown, Vector2.down, 0.1f);
        hitRightDown = Physics2D.Raycast(bottomRightDown, Vector2.down, 0.1f);
        hitLeftUp = Physics2D.Raycast(TopLeftUp, Vector2.up, 0.1f);
        hitRightUp = Physics2D.Raycast(TopRightUp, Vector2.up, 0.1f);
        hitUpLeft = Physics2D.Raycast(LeftTop, Vector2.left, 0.1f);
        hitDownLeft = Physics2D.Raycast(LeftBottom, Vector2.left, 0.1f);
        hitUpRight = Physics2D.Raycast(RightTop, Vector2.right, 0.1f);
        hitDownRight = Physics2D.Raycast(RightBottom, Vector2.right, 0.1f);


		Debug.DrawLine(new Vector3(bottomLeftDown.x, bottomLeftDown.y, 0f), new Vector3(bottomLeftDown.x, bottomLeftDown.y-0.1f, 0f), Color.red);
		Debug.DrawLine(new Vector3(bottomRightDown.x, bottomRightDown.y, 0f), new Vector3(bottomRightDown.x, bottomRightDown.y-0.1f, 0f), Color.red);
		Debug.DrawLine(new Vector3(TopLeftUp.x, TopLeftUp.y, 0f), new Vector3(TopLeftUp.x, TopLeftUp.y+0.1f, 0f), Color.red);
		Debug.DrawLine(new Vector3(TopRightUp.x, TopRightUp.y, 0f), new Vector3(TopRightUp.x, TopRightUp.y+0.1f, 0f), Color.red);
		Debug.DrawLine(new Vector3(LeftTop.x, LeftTop.y, 0f), new Vector3(LeftTop.x-0.1f, LeftTop.y, 0f), Color.red);
		Debug.DrawLine(new Vector3(LeftBottom.x, LeftBottom.y, 0f), new Vector3(LeftBottom.x-0.1f, LeftBottom.y, 0f), Color.red);
		Debug.DrawLine(new Vector3(RightTop.x, RightTop.y, 0f), new Vector3(RightTop.x+0.1f, RightTop.y, 0f), Color.red);
		Debug.DrawLine(new Vector3(RightBottom.x, RightBottom.y, 0f), new Vector3(RightBottom.x+0.1f, RightBottom.y, 0f), Color.red);

    }

    public void generalUpdate(){
        playerAnim.facingRight = facingRight;
        if(rigidbody2d.velocity.x > 0){
            modelTransform.localEulerAngles = new Vector3(0f, 120f, 0f);
        }
        if(rigidbody2d.velocity.x < 0){
            modelTransform.localEulerAngles = new Vector3(0f, 240, 0f);
        }
    }

    public void move(float z)
    {
        if(currentWallRecovery == maxWallRecovery){
            rigidbody2d.velocity = new Vector2(z,rigidbody2d.velocity.y);

            if(z==0)
            {
                playerAnim.isRunning = false;
            }
            if(z>0)
            {
                playerAnim.isRunning = true;
                facingRight = true;
            }
            if(z<0)
            {
                playerAnim.isRunning = true;
                facingRight = false;
            }
            if(isWallJumping){
                facingRight = !facingRight;
            }
        }
       
    }
    public void jump(float jumpSpeed,float jumpHeight, float speedFull){
        isGrounded = hitLeftDown || hitRightDown;
		playerAnim.isGrounded = isGrounded;
		playerAnim.isJumping = isJumping;
		
        if(hitLeftUp || hitRightUp){
			isJumping = false;
		}
        if(Input.GetButtonDown("Jump")){
			if(isGrounded){
				isJumping=true;
			}
			else if(canDoubleJump){
				isJumping=true;
				currentJumpHeight = 0;
				canDoubleJump = false;
			}
        }
        if((currentJumpHeight >= jumpHeight || Input.GetButtonUp("Jump")) && rigidbody2d.velocity.y > 0 && currentWallRecovery == maxWallRecovery){
        	rigidbody2d.velocity = new Vector2(rigidbody2d.velocity.x, 0);
            isJumping=false;
        }
        
        
        if(isJumping==true && currentJumpHeight >= 0)
        {
			currentJumpHeight += jumpSpeed*(Time.deltaTime/Time.timeScale);
            if(isWallJumping || (currentWallRecovery < maxWallRecovery)){
                if(facingRight){
                    rigidbody2d.velocity = new Vector2(speedFull, jumpSpeed);
                }
                else if(!facingRight){
                    rigidbody2d.velocity = new Vector2(-speedFull, jumpSpeed);
                }
                currentWallRecovery -= Time.deltaTime;
            }
            else{
                rigidbody2d.velocity = new Vector2(rigidbody2d.velocity.x, jumpSpeed);
            }
        }
        if(isGrounded || isWallJumping)
        {
			currentJumpHeight = 0;
			playerAnim.isFalling = false;
			playerManager.canDash = true;
			if(playerManager.canDoubleJump){canDoubleJump = true;}
		}
		else
		{
			playerAnim.isFalling = true;
		}
	}    

    public void wallJump(float z){
        if(((hitUpLeft && hitDownLeft) || (hitUpRight && hitDownRight)) && !isJumping && !isGrounded && currentWallRecovery == maxWallRecovery){
            if( z < 0 || z > 0){
                rigidbody2d.velocity = new Vector2(rigidbody2d.velocity.x,-4.0f);
                rigidbody2d.gravityScale = 0;
                isJumping = false;
                isWallJumping = true;
                isDashing = false;
                playerAnim.isWallSliding = true;
                facingRight = z < 0 ?   true : false;
            }
            else{
                playerAnim.isWallSliding = false;
                isWallJumping = false;
            }
        }
        else{
            playerManager.canJump = true;
            playerAnim.isWallSliding = false;
            isWallJumping = false;
            if(!isDashing){
                rigidbody2d.gravityScale = 2;
            }
        }
        if(currentWallRecovery <= 0){
            currentWallRecovery = maxWallRecovery;
        }
    }

    public void dash(float z){
		if(Input.GetButtonDown("Dash") && DashCurrent ==  0 && isDashing == false && playerManager.canDash){
				playerAnim.isDashing = true;
				playerManager.canMove = false;
				playerManager.canJump = false;
				DashCurrent += DashSpeed*(Time.deltaTime/Time.timeScale);
                rigidbody2d.gravityScale = 0;
				isJumping=false;
				isDashing=true;
				playerManager.canDash = false;
                if(facingRight){
                    dashDirection = 1;
                }
                else{
                    dashDirection = -1;
                }
                currentWallRecovery = maxWallRecovery;
		}
		if(isDashing)
		{
			if(DashCurrent > 0 && DashCurrent < DashLength){
					DashCurrent += DashSpeed*(Time.deltaTime/Time.timeScale);
                    if(dashDirection == 1){
                        rigidbody2d.velocity = new Vector2(DashSpeed,0);
                    }
                    else if(dashDirection == -1){
                        rigidbody2d.velocity = new Vector2(-DashSpeed,0);
                    }
                    
			}
			else if(DashCurrent >= DashLength && currentDashRecovery == 0){
				currentDashRecovery += Time.deltaTime/Time.timeScale;
				playerAnim.isDashing = false;
				rigidbody2d.velocity = new Vector2(0,0);
				DashCurrent = 0;
			}
			else if(currentDashRecovery >= 0){
				currentDashRecovery += Time.deltaTime/Time.timeScale;
				playerManager.canMove = true;
				playerManager.canJump = true;
                rigidbody2d.gravityScale = 2;
			}
			if(currentDashRecovery >= DashRecovery){
				currentDashRecovery = 0;
				isDashing = false;
			}
		}
        else{
			playerAnim.isDashing = false;
            playerManager.canMove = true;
            playerManager.canJump = true;
			DashCurrent = 0;
			currentDashRecovery = 0;
        }
	}
}

