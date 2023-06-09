using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//mzoughist
public class MovementController : MonoBehaviour
{
    public GameManager gameManager;

    public GameObject currentNode;
    public float speed = 4f;

    public string direction = "";
    public string lastMovingDirection = "";

    public bool canWarp = true;

    public bool isGhost;



    // Start is called before the first frame update
    void Awake()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        NodeController currentNodeController = currentNode.GetComponent<NodeController>();

        transform.position = Vector2.MoveTowards(transform.position, currentNode.transform.position, speed * Time.deltaTime);

        bool reversedirecion = false;

        if(direction == "left" && lastMovingDirection =="right" 
            || direction == "right" && lastMovingDirection == "left"
            || direction == "up" && lastMovingDirection == "down"
            || direction == "down" && lastMovingDirection == "up"
            )
        {
            reversedirecion = true;
        }
            
            

        if((transform.position.x == currentNode.transform.position.x 
            && transform.position.y ==currentNode.transform.position.y) || reversedirecion)
        {
            if (isGhost)
            {
                GetComponent<EnemyController>().ReachedCenterOfNode(currentNodeController);
            }
            if (currentNodeController.isWarpRightNode  && canWarp)
            {
                currentNode = gameManager.leftWarpNode;
                direction = "right";
                lastMovingDirection = "right";
                transform.position = currentNode.transform.position;
                canWarp = false;
            }
            else if (currentNodeController.isWarpLeftNode &&canWarp)
            {
                currentNode = gameManager.rightWarpNode;
                direction = "left";
                lastMovingDirection = "left";
                transform.position = currentNode.transform.position;
                canWarp = false;
            }
            else
            {
                GameObject newNode = currentNodeController.GetNodeFromDirection(direction);
                if (newNode != null)
                {
                    currentNode = newNode;
                    lastMovingDirection = direction;
                }
                else
                {
                    direction = lastMovingDirection;
                    newNode = currentNodeController.GetNodeFromDirection(direction);
                    if (newNode != null)
                    {
                        currentNode = newNode;
                    }
                }
            }
            

            
        }
        else
        {
            canWarp = true;
        }

    }

    public void SetDirection(string NewDirection)
    {
        direction = NewDirection;
    }
}
