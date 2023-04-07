using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody playerRb;

    private float moveStep = 2.5f;
    public float jumpSpeed = 450;

    private float superJumpSpeed = 600;

    /// <summary>
    /// 1 - left, 2 - middle, 3 - right
    /// </summary>
    private int currentLane;

    private bool isOnGround = true;

    void Start()
    {
        playerRb = GetComponent<Rigidbody>();
        currentLane = 2;
    }

    void Update()
    {
        ChangeLanes();

        // jump
        if (Input.GetKeyDown(KeyCode.UpArrow) && isOnGround)
        {
            playerRb.AddForce(Vector3.up * jumpSpeed, ForceMode.Impulse);
            isOnGround = false;
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow) && !isOnGround)
        {
            playerRb.AddForce(Vector3.down * jumpSpeed, ForceMode.Impulse);
        }

        // float horizontalInput = Input.GetAxis("Horizontal");
        // transform.Translate(Vector3.right * horizontalInput * moveStep);
    }

    private void ChangeLanes()
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow) && currentLane > 1)
        {
            transform.position -= new Vector3(moveStep, 0, 0);
            currentLane--;
        }
        if (Input.GetKeyDown(KeyCode.RightArrow) && currentLane < 3)
        {
            transform.position += new Vector3(moveStep, 0, 0);
            currentLane++;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isOnGround = true;
        }
        else if (collision.gameObject.CompareTag("Obstacle"))
        {
            isOnGround = true;
        }
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Powerup"))
        {
            Destroy(other.gameObject);
        } else if (other.CompareTag("Obstacle"))
        {
            Destroy(gameObject);
        }
    }
}
