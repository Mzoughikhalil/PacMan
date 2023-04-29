using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//mzoughist
public class PlayerController : MonoBehaviour
{
    MovementController movementController;
    public SpriteRenderer sprite;
    public Animator animator;
    // Start is called before the first frame update
    void Awake()
    {
        animator = GetComponentInChildren<Animator>();
        sprite = GetComponentInChildren<SpriteRenderer>();
        movementController = GetComponent<MovementController>();
        movementController.lastMovingDirection = "left";
    }

    // Update is called once per frame
    void Update()
    {
        animator.SetBool("Moving", true);
        if (Input.GetKey(KeyCode.Q))
        {
            movementController.SetDirection("left");
        }
        if (Input.GetKey(KeyCode.Z))
        {
            movementController.SetDirection("up");
        }
        if (Input.GetKey(KeyCode.S))
        {
            movementController.SetDirection("down");
        }
        if (Input.GetKey(KeyCode.D))
        {
            movementController.SetDirection("right");
        }

        bool flipx = false;
        bool flipy = false;
        if(movementController.lastMovingDirection == "left")
        {
            animator.SetInteger("direction", 0);
        }
        else if (movementController.lastMovingDirection == "right")
        {
            animator.SetInteger("direction", 0);
            flipx = true;
        }
        else if (movementController.lastMovingDirection == "up")
        {
            animator.SetInteger("direction", 1);
        }
        else if (movementController.lastMovingDirection == "down")
        {
            animator.SetInteger("direction", 1);
            flipy = true;
        }

        sprite.flipX = flipx;
        sprite.flipY = flipy;

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "enemy")
        {
            Destroy(gameObject);
            
        }
    }
}
