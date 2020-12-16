using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour, IDamagable
{
    public int Health { get; set; }
    public bool isHealthPlus;

    public AudioSource onHitPlayPlayer;

    public int diamond;

    private Rigidbody2D _rigid;
    public float _jumpForce = 7.0f;
    private bool swing = false;
    private bool resetJumpNeeded = false;
    private PlayerAnimation _playerAnim;
    private SpriteRenderer _playerSprite;
    private SpriteRenderer _swordArcSprite;

    [SerializeField]
    private bool _grounded = false;

    [SerializeField]
    private float _playerSpeed = 3.0f;

    [SerializeField]
    private LayerMask _groundLayer;

    

    // Start is called before the first frame update
    void Start()
    {
        _rigid = GetComponent<Rigidbody2D>();
        _playerAnim = GetComponent<PlayerAnimation>();
        _playerSprite = GetComponentInChildren<SpriteRenderer>();
        _swordArcSprite = transform.GetChild(1).GetComponent<SpriteRenderer>();
        onHitPlayPlayer = GetComponent<AudioSource>();
        Health = 6;
    }

    // Update is called once per frame
    void Update()
    {
        Movement();
        isGrounded();

        if (CrossPlatformInputManager.GetButtonDown("A_button") && _grounded == true) //   Input.GetMouseButtonDown(0)
        {
            StartCoroutine(afterswing());
            _playerAnim.Attack();
            onHitPlayPlayer.Play();


        }
    
    }

    void Movement()
    {
        float move = CrossPlatformInputManager.GetAxis("Horizontal");       //; Input.GetAxisRaw("Horizontal")

        flip(move);

        // If Space key and isGrounded == true (JUMP) //  Input.GetKeyDown(KeyCode.Space)
        if (CrossPlatformInputManager.GetButtonDown("B_button") && _grounded == true)
        {
            // current Velocity = new velocity (current x, jumpforce)
            //Grounded = false
            _rigid.velocity = new Vector2(_rigid.velocity.x, _jumpForce);
            _grounded = false;
            resetJumpNeeded = true;
            StartCoroutine(resetJumpNeededRoutine());

            // Tell Player To play jump Anim
            _playerAnim.Jump(true);
        }

        _rigid.velocity = new Vector2(move * _playerSpeed, _rigid.velocity.y);
        _playerAnim.Move(move);
    }

    void isGrounded()
    {
        // if Raycast to the ground 
        RaycastHit2D hitInfo = Physics2D.Raycast(transform.position, Vector2.down, 0.6f, _groundLayer);

        // if Hitinfo != null
        if (hitInfo.collider != null)
        {
            //Debug.Log("Hit: " + hitInfo.collider.name);
            if (resetJumpNeeded == false)
            {
                // set animator jump bool to false
                _playerAnim.Jump(false);
                _grounded = true;
            }

        }
    }

    IEnumerator afterswing()
    {
        yield return new WaitForSeconds(1.0f);
    }

    IEnumerator resetJumpNeededRoutine()
    {
        yield return new WaitForSeconds(0.7f);
        resetJumpNeeded = false;

    }

    void flip(float move)
    {
        // If move is greater then 0 //Face Right
        if (move > 0)
        {
            _playerSprite.flipX = false;

            _swordArcSprite.flipX = false;
            _swordArcSprite.flipY = false;

            Vector3 newPos = _swordArcSprite.transform.localPosition;
            newPos.x = 2.0f;
            _swordArcSprite.transform.localPosition = newPos;
        }

        // else if move < 0 // Face Left
        else if (move < 0)
        {
            _playerSprite.flipX = true;

            _swordArcSprite.flipX = true;
            _swordArcSprite.flipY = true;

            Vector3 newPos = _swordArcSprite.transform.localPosition;
            newPos.x = -0.6f;
            _swordArcSprite.transform.localPosition = newPos;
        }
    }

    public void Damage()
    {

        if (Health < 1)
        {
            return;
        }

        Debug.Log("Player Damaged() ");

        // Remove 1 from health
        Health--;
        isHealthPlus = false;


        // Update UI Display
        UIManager.Instance.UpdateLives(Health, isHealthPlus );

        // Check if Dead
        if (Health < 1)
        {
            // Play Dead Animations
            _playerAnim.Death();
            SceneManager.LoadScene("Restart");
        }

        
    }

    public void AddGems(int amount)
    {
        diamond += amount;
        UIManager.Instance.GemCount(diamond);
    }

}
