using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Animator))]
public class PlayerController2D : MonoBehaviour
{
    //Plateformer control classic, like hollow kngiht.
    //Handle movement, and jump for now on.

    //Movement
    [SerializeField] float _speed = 5f;
    [SerializeField] float _jumpForce = 5f;
    [SerializeField] float _gravity = 1f;

    //Ground Check
    [SerializeField] Transform _groundCheck;
    [SerializeField] float _groundCheckRadius = 0.1f;
    [SerializeField] LayerMask _groundLayer;
    
    //Components
    Rigidbody2D _rb;
    Animator _anim;

    //State
    bool _isGrounded = false;
    bool _isJumping = false;
    
    // Start is called before the first frame update
    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {

        //Check if grounded
        _isGrounded = Physics2D.OverlapCircle(_groundCheck.position, _groundCheckRadius, _groundLayer);

        //Get input
        float horizontalInput = Input.GetAxisRaw("Horizontal");

        //Move
        _rb.velocity = new Vector2(horizontalInput * _speed, _rb.velocity.y);

        //Flip
        if (horizontalInput != 0)
        {
            transform.localScale = new Vector3(horizontalInput, 1, 1);
        }

        //Jump
        if (Input.GetButtonDown("Jump") && _isGrounded)
        {
            _isJumping = true;
        }

        //Jump animation
        //_anim.SetBool("isJumping", !_isGrounded);

        //Jump
        if (_isJumping)
        {
            _rb.velocity = new Vector2(_rb.velocity.x, _jumpForce);
            _isJumping = false;
        }

        //Gravity
        _rb.velocity = new Vector2(_rb.velocity.x, _rb.velocity.y - _gravity);

        //Animation
        //_anim.SetFloat("speed", Mathf.Abs(horizontalInput));


        //Add Grapplinf force toward mouse direction with right click
        if (Input.GetMouseButtonDown(1))
        {
            //Get mouse position
            Vector3 mousePos =  Camera.main.ScreenToViewportPoint(Input.mousePosition); 

            //Get direction
            Vector3 direction = (mousePos - transform.position).normalized;
            

            //Add force
            Debug.Log("Direction : " + direction);
            _rb.AddForce(direction * 100f, ForceMode2D.Impulse);
        }
    }

    private void OnDrawGizmos() {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(_groundCheck.position, _groundCheckRadius);
    }
}
