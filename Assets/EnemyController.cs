using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//mzoughist
public class EnemyController : MonoBehaviour
{
    public enum GhostNodeStatesEnum
    {
        respawning,
        leftNode,
        rightNode,
        centerNode,
        startNode,
        MovingNodes
    }

    public GhostNodeStatesEnum ghostNodesState;

    public enum GhostType
    {
        red,
        blue,
        pink,
        orange
    }

    public GhostType ghostType;

    public GameObject ghostNodeLeft;
    public GameObject ghostNodeRight;
    public GameObject ghostNodeCenter;
    public GameObject ghostNodeStart;

    public MovementController MovementController;

    public GameObject startingNode;

    public bool readyToLeabeHome = false;

    public GameManager gameManager;

    public bool isFrightened;

    public bool IsVisible = true;

    public SpriteRenderer ghostsprite;
    public SpriteRenderer eyesSprite;




    // Start is called before the first frame update
    void Awake()
    {
        ghostsprite = GetComponent<SpriteRenderer>();
        eyesSprite = GetComponentInChildren<SpriteRenderer>();
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        MovementController = GetComponent<MovementController>();
        if (ghostType == GhostType.red)
        {
            ghostNodesState = GhostNodeStatesEnum.startNode;
            startingNode = ghostNodeStart;
        }
        else if (ghostType == GhostType.pink)
        {
            ghostNodesState = GhostNodeStatesEnum.centerNode;
            startingNode = ghostNodeCenter;
        }
        else if (ghostType == GhostType.blue)
        {
            ghostNodesState = GhostNodeStatesEnum.leftNode;
            startingNode = ghostNodeLeft;
        }
        else if (ghostType == GhostType.orange)
        {
            ghostNodesState = GhostNodeStatesEnum.rightNode;
            startingNode = ghostNodeRight;
        }
        MovementController.currentNode = startingNode;
        transform.position = startingNode.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (IsVisible)
        {
            ghostsprite.enabled = true;
            eyesSprite.enabled = true;
        }
        else
        {
            ghostsprite.enabled = false;
            eyesSprite.enabled = false;
        }
    }

    public void ReachedCenterOfNode(NodeController nodeController)
    {
        if ( ghostNodesState == GhostNodeStatesEnum.MovingNodes)
        {
            //pick next game node to go 
            if (ghostType == GhostType.red)
            {
                DetermineRedGhostDirection();
            }
        }
        else if (ghostNodesState == GhostNodeStatesEnum.respawning)
        {
            //pick quickest direction to home
        }
        else
        {
            //if we are ready to leave !
            if (readyToLeabeHome)
            {
               
                if (ghostNodesState == GhostNodeStatesEnum.leftNode )
                {
                    ghostNodesState = GhostNodeStatesEnum.centerNode;
                    MovementController.SetDirection("right");
                }
                else if (ghostNodesState == GhostNodeStatesEnum.rightNode)
                {
                    ghostNodesState = GhostNodeStatesEnum.centerNode;
                    MovementController.SetDirection("left");
                }
                else if (ghostNodesState == GhostNodeStatesEnum.centerNode)
                {
                    ghostNodesState = GhostNodeStatesEnum.startNode;
                    MovementController.SetDirection("up");
                }
                else if (ghostNodesState == GhostNodeStatesEnum.startNode)
                {
                    ghostNodesState = GhostNodeStatesEnum.MovingNodes;
                    MovementController.SetDirection("left");
                }

            }
            

        }
    }
    void DetermineRedGhostDirection()
    {
        string direction = GetClosestDirection(gameManager.pacman.transform.position);
        MovementController.SetDirection(direction);
    }
    void DeterminePinkGhostDirection()
    {

    }
    void DetermineBlueGhostDirection()
    {

    }
    void DetermineOrangeGhostDirection()
    {

    }
    string GetClosestDirection(Vector2 target)
    {
        float shortestDistance = 0;
        string lastMoveingDirection = MovementController.lastMovingDirection;
        string newDirection = "";
        NodeController nodeController = MovementController.currentNode.GetComponent<NodeController>();

        if (nodeController.canMoveUp && lastMoveingDirection!="down")
        {
            GameObject nodeUp = nodeController.NodeUp;
            float distance = Vector2.Distance(nodeUp.transform.position, target);

            if(distance<shortestDistance || shortestDistance ==0)
            {
                shortestDistance = distance;
                newDirection = "up";
            }
        }

        if (nodeController.canMoveDown && lastMoveingDirection != "up")
        {
            GameObject nodeDown = nodeController.NodeDown;
            float distance = Vector2.Distance(nodeDown.transform.position, target);

            if (distance < shortestDistance || shortestDistance == 0)
            {
                shortestDistance = distance;
                newDirection = "down";
            }
        }

        if (nodeController.canMoveLeft && lastMoveingDirection != "right")
        {
            GameObject nodeLeft = nodeController.NodeLeft;
            float distance = Vector2.Distance(nodeLeft.transform.position, target);

            if (distance < shortestDistance || shortestDistance == 0)
            {
                shortestDistance = distance;
                newDirection = "left";
            }
        }

        if (nodeController.canMoveRight && lastMoveingDirection != "left")
        {
            GameObject nodeRight = nodeController.NodeRight;
            float distance = Vector2.Distance(nodeRight.transform.position, target);

            if (distance < shortestDistance || shortestDistance == 0)
            {
                shortestDistance = distance;
                newDirection = "right";
            }
        }
        return newDirection;
    }
    public void SetVisible(bool newIsVisible)
    {
        IsVisible = newIsVisible;
    }
    /*private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "player")
        {
            if (isFrightened)
            {

            }
            else
            {
                StartCoroutine(gameManager.PlayerEaten());
            }
        }
    }*/

}
