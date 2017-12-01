using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {
    public float jumpCheckDist;
    public LayerMask groundCheckLayer;
    public LayerMask floorCheckLayer;
    public float moveSpeed;
    public float jumpForce;
    bool isGrounded;
    bool isOnTheFloor = false;
    Rigidbody rb;
    GameManager gm;
    private void Start()
    {
        gm = FindObjectOfType<GameManager>();
        rb = GetComponent<Rigidbody>();
    }
    void FixedUpdate ()
    {      
        GroundChecks();
#if !UNITY_ANDROID
        Move(Input.GetAxis("Horizontal"));
#endif
        ScreenClamp();
    }

    RaycastHit hit;
    void GroundChecks()
    {
        isGrounded = Physics.SphereCast(transform.position, 0.1f, -transform.up, out hit, jumpCheckDist, groundCheckLayer) ||
           Physics.Raycast(transform.position, -transform.right, jumpCheckDist, groundCheckLayer) ||
           Physics.Raycast(transform.position, transform.right, jumpCheckDist, groundCheckLayer);

        isOnTheFloor = Physics.Raycast(transform.position, -transform.up, jumpCheckDist, floorCheckLayer);

        if (!isOnTheFloor)
        {
            gm.AddScore();
        }
    }
    public void Jump()
    {
        if (isGrounded)
        {
            rb.velocity = new Vector3(rb.velocity.x, 0, 0);
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }
    }
    public void Move(float hInput)
    {
        Vector3 vel = rb.velocity;
        vel.x = hInput * moveSpeed;
        rb.velocity = vel;
    }
    void ScreenClamp()
    {

        float screenRatio = (float)Screen.width / (float)Screen.height;
        float widthOrtho = Camera.main.orthographicSize * screenRatio;
        Vector3 pos = transform.position;
        if(pos.x > widthOrtho)
        {
            pos.x = widthOrtho;
        }
        if (pos.x < -widthOrtho)
        {
            pos.x = -widthOrtho;
        }
        if(pos.y > Camera.main.orthographicSize)
        {
            pos.y = Camera.main.orthographicSize;
        }
        transform.position = pos;

    }
    public GameObject deathParticles;
    public void Death()
    {
        gm.isGameOver = true;
        Instantiate(deathParticles, transform.position, transform.rotation);
        Destroy(gameObject);
    }
}
