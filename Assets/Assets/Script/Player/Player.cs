using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class Player : MonoBehaviour, IDamageabale
{
    public int diamonds;
    private Rigidbody2D rigid;

    [SerializeField]private float jumpForce = 5.0f;
    [SerializeField] private float playerSpeed = 3.0f;
    private PlayerAnimation playerAnim;
    private SpriteRenderer playerSprite;
    private SpriteRenderer swordArcSprite;
    private bool grounded = false;
    private bool isDead = false;
    private bool resetJump = false;
    private bool canAttack = true;

    public int Health { get; set; }
    // Start is called before the first frame update
    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
        playerAnim = GetComponent<PlayerAnimation>();
        playerSprite = GetComponentInChildren<SpriteRenderer>();
        swordArcSprite = transform.GetChild(1).GetComponent<SpriteRenderer>();
        Health = 4;
    }

    // Update is called once per frame
    void Update()
    {
        if (isDead == true)
        {
            return;
        }
        Movement();
        if (CrossPlatformInputManager.GetButtonDown("A_Button") && IsGrounded() && canAttack)
        {
            playerAnim.Attack();
            canAttack = false;
            StartCoroutine(CooldownForAttack());
        }
        
    }

    void Movement()
    {
        float move = CrossPlatformInputManager.GetAxis("Horizontal"); //Input.GetAxisRaw("Horizontal");
        grounded = IsGrounded();

        if (CrossPlatformInputManager.GetButtonDown("B_Button") && IsGrounded())
        {
            if (GameManager.Instance.HasFlyingBoots)
                jumpForce = 10;
            else
                jumpForce = 5;
            rigid.velocity = new Vector2(rigid.velocity.x, jumpForce);
            playerAnim.Jump(true);
            StartCoroutine(ResetJumpRoutine());
            
        }
        rigid.velocity = new Vector2(move * playerSpeed, rigid.velocity.y);
        playerAnim.Move(move);
        if (move > 0)
        {
            Flip(true);
        }
        else if (move < 0)
        {
            Flip(false);
        }
    }
    bool IsGrounded()
    {
        RaycastHit2D hitInfo = Physics2D.Raycast(transform.position, Vector2.down, 1f, 1 << 8);
        Debug.DrawRay(transform.position, Vector2.down, Color.green);
        if (hitInfo.collider != null)
        {
            if (resetJump == false)
            {
                playerAnim.Jump(false);
                return true;          
            }
                             
        }
        return false;
        
        
    }

    IEnumerator ResetJumpRoutine()
    {
        resetJump = true;
        yield return new WaitForSeconds(0.1f);
        resetJump = false;
    }

    void Flip(bool facingRight)
    {
        if (facingRight == true)
        {         
            playerSprite.flipX = false;
            swordArcSprite.flipX = false;
            swordArcSprite.flipY = false;

            Vector3 newPos = swordArcSprite.transform.localPosition;
            newPos.x = 1.01f;
            swordArcSprite.transform.localPosition = newPos;
        }
        else if (facingRight == false)
        {
            playerSprite.flipX = true;
            swordArcSprite.flipX = true;
            swordArcSprite.flipY = true;

            Vector3 newPos = swordArcSprite.transform.localPosition;
            newPos.x = -1.01f;
            swordArcSprite.transform.localPosition = newPos;
        }
    }

    public void Damage()
    {
        if (isDead == true)
        {
            return;
        }
        playerAnim.Hit();
        Health--;
        UIManager.Instance.UpdateLives(Health);
        if (Health < 1)
        {
            Debug.Log("Dead");
            playerAnim.Death();
            isDead = true;
        }
    }

    public void AddGems(int amount)
    {
        diamonds += amount;
        UIManager.Instance.UpdateGemCount(diamonds);
    }

    public void HasFireSword()
    {
        if (GameManager.Instance.HasFlameSword == true)   
            playerAnim.FireSword(true);
    }
    
    IEnumerator CooldownForAttack()
    {
        yield return new WaitForSeconds(2f);
        canAttack = true;
    }
}
