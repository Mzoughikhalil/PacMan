using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//mzoughist
public class NodeController : MonoBehaviour
{
    public bool canMoveLeft = false;
    public bool canMoveRight = false;
    public bool canMoveUp = false;
    public bool canMoveDown = false;

    public GameObject NodeLeft;
    public GameObject NodeRight;
    public GameObject NodeUp;
    public GameObject NodeDown;

    public bool isWarpRightNode = false;
    public bool isWarpLeftNode = false;

    public bool isPelletNode = false;
    public bool hasPellet = false;

    public SpriteRenderer pelletSpriter;

    public GameManager gameManager;

    // Start is called before the first frame update
    void Awake()
    {
        gameManager  = GameObject.Find("GameManager").GetComponent<GameManager>();
        if (transform.childCount>0)
        {
            hasPellet = true;
            isPelletNode = true;
            pelletSpriter = GetComponentInChildren<SpriteRenderer>();
        }

        RaycastHit2D[] hitsUp;
        //raycast down
        hitsUp = Physics2D.RaycastAll(transform.position, Vector2.up);

        for (int i = 0; i < hitsUp.Length; i++)
        {
            float distance = Mathf.Abs(hitsUp[i].point.y - transform.position.y);
            if (distance < 0.4f && hitsUp[i].collider.tag == "node")
            {
                canMoveUp = true;
                NodeUp = hitsUp[i].collider.gameObject;
            }

        }

        RaycastHit2D[] hitsRight;
        //raycast down
        hitsRight = Physics2D.RaycastAll(transform.position, Vector2.right);

        for (int i = 0; i < hitsRight.Length; i++)
        {
            float distance = Mathf.Abs(hitsRight[i].point.x - transform.position.x);
            if (distance < 0.4f && hitsRight[i].collider.tag == "node")
            {
                canMoveRight = true;
                NodeRight = hitsRight[i].collider.gameObject;
            }

        }


        RaycastHit2D[] hitsLeft;
        //raycast down
        hitsLeft = Physics2D.RaycastAll(transform.position, -Vector2.right);

        for (int i = 0; i < hitsLeft.Length; i++)
        {
            float distance = Mathf.Abs(hitsLeft[i].point.x - transform.position.x);
            if (distance < 0.4f && hitsLeft[i].collider.tag == "node")
            {
                canMoveLeft = true;
                NodeLeft = hitsLeft[i].collider.gameObject;
            }

        }



        RaycastHit2D[] hitsDown;
        //raycast down
        hitsDown = Physics2D.RaycastAll(transform.position, -Vector2.up);

        for (int i = 0;i <hitsDown.Length; i++)
        {
            float distance = Mathf.Abs(hitsDown[i].point.y - transform.position.y);
            if(distance < 0.4f && hitsDown[i].collider.tag == "node")
            {
                canMoveDown = true;
                NodeDown = hitsDown[i].collider.gameObject;
            }

        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public GameObject GetNodeFromDirection(string direction)
    {
        if (direction =="left"&& canMoveLeft)
        {
            return NodeLeft;
        }
        else if (direction == "right" && canMoveRight)
        {
            return NodeRight;
        }
        else if (direction == "up" && canMoveUp)
        {
            return NodeUp;
        }
        else if (direction == "down" && canMoveDown)
        {
            return NodeDown;
        }
        else
        {
            return null;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag =="player" && hasPellet)
        {
            hasPellet = false;
            pelletSpriter.enabled = false;
            gameManager.CollectedPellet(this);
        }
    }

}
