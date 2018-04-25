using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class TheGhost : MonoBehaviour
{
    public GameObject target;
    public float moveSpeed;
    private NavMeshAgent agent;
    int modeHelper = 0;
    int fhelper = 0;
    int resHelper;
    private float previousMoveSpeed;
    private int regularSpeed = 14;
    public float frightenedMoveSpeed = 4.9f;
    private float respawnTimer = 0;
    private int respawnStart = 4;
    private GameObject[] listofnodes;
    public int frightenedModeDuration = 10;
    public int startBlinkingAt = 7;
    private float frightenedModeTimer = 0;
    private float blinkerTimer = 0;
    private GameObject thenoderandom;
    private bool frightenedModeIsWhite = false;

    public int PinkyReleaseTimer = 5;
    public float ghostReleaseTimer = 0;

    public Material ghostYellow;
    public Material ghostWhite;
    public Material ghostRed;
    public Material ghostPink;
    public Material ghostBlue;
    public Material ghostOrange;

    public Node startingPosition;
    public int scatterModeTimer1 = 7;
    public int chaseModeTimer1 = 20;
    public int scatterModeTimer2 = 7;
    public int chaseModeTimer2 = 20;
    public int scatterModeTimer3 = 5;
    public int chaseModeTimer3 = 20;
    public int scatterModeTimer4 = 5;
    private int modeChangeIteration = 1;
    private float modeChangeTimer = 0;
    public enum Mode
    {
        Chase,
        Scatter,
        Fightened
    }

    public GameObject[] corners = {

    };


    public Mode currentMode = Mode.Scatter;
    Mode previousMode;

    private Node currentNode, targetNode, previousNode;
    private Vector3 direction, nextDirection;


    // Use this for initialization
    void Start()
    {
        listofnodes = GameObject.FindGameObjectsWithTag("Pick Up");
        agent = GetComponent<NavMeshAgent>();
        if (target == null)
        {
            target = GameObject.FindGameObjectWithTag("Pacman");
            targetNode = GetNodeAtPosition(target.transform.transform.position);
        }
        SetDestination();
    }

    // Update is called once per frame
    void Update()
    {
        ModeUpdate();
    }


    void OnTriggerEnter(Collider other)
    {

        if (currentMode != Mode.Fightened)
        {
            if (other.gameObject.CompareTag("Inter"))
            {
                ModeUpdate();
                Move();
                SetDestination();
            }
            else if (other.gameObject.CompareTag("Corners") && currentMode == Mode.Scatter)
            {
                if (gameObject.name == "GRojo")
                {
                    agent.destination = corners[3].transform.position;
                }
                else if (gameObject.name == "GRosa")
                {
                    agent.destination = corners[0].transform.position;
                }
                else if (gameObject.name == "GAzul")
                {
                    agent.destination = corners[2].transform.position;
                }
                else if (gameObject.name == "GNaranja")
                {
                    agent.destination = corners[1].transform.position;
                }
            }

        }
        else
        {
            if (other.gameObject.CompareTag("Random"))
            {
                fhelper = 0;
                thenoderandom.gameObject.tag = "Pick Up";
            }
            if (other.gameObject.CompareTag("Pacman"))
            {
//                gameObject.SetActive(false);
//                Respawn();
				agent.destination = new Vector3(0f,1f,3.5f);
				transform.GetComponent<Renderer>().material = ghostBlue;				
				moveSpeed = 40;
				transform.GetComponent<NavMeshAgent>().speed = moveSpeed;

            }
			if (other.gameObject.CompareTag ("GhostHouse")) {
				ChangeMode(previousMode);
			}
        }

    }


    void Move()
    {

        if (targetNode != currentNode && targetNode != null)
        {

            if (OverShotTarget())
            {

                currentNode = targetNode;

                transform.localPosition = currentNode.transform.position;

                //				GameObject otherPortal = GetPortal (currentNode.transform.position);

                //				if (otherPortal != null) {
                //
                //					transform.localPosition = otherPortal.transform.position;
                //					currentNode = otherPortal.GetComponent<Node> ();
                //				}

                targetNode = ChooseNextNode();

                previousNode = currentNode;

                currentNode = null;
            }
            else
            {

                transform.localPosition += (Vector3)direction * moveSpeed * Time.deltaTime;
            }
        }
    }


    void ModeUpdate()
    {

        if (currentMode != Mode.Fightened)
        {

            transform.GetComponent<NavMeshAgent>().speed = regularSpeed;
            modeChangeTimer += Time.deltaTime;

            if (gameObject.name == "GRosa")
            {
                transform.GetComponent<Renderer>().material = ghostPink;
            }
            if (gameObject.name == "GRojo")
            {
                transform.GetComponent<Renderer>().material = ghostRed;
            }
            if (gameObject.name == "GAzul")
            {
                transform.GetComponent<Renderer>().material = ghostBlue;
            }
            if (gameObject.name == "GNaranja")
            {
                transform.GetComponent<Renderer>().material = ghostOrange;
            }

            if (modeChangeIteration == 1)
            {

                if (currentMode == Mode.Scatter && modeChangeTimer > scatterModeTimer1)
                {
                    ChangeMode(Mode.Chase);
                    modeChangeTimer = 0;
                }

                if (currentMode == Mode.Chase && modeChangeTimer > chaseModeTimer1)
                {
                    modeChangeIteration = 2;
                    ChangeMode(Mode.Scatter);
                    int scattertarget = Random.Range(0, 3);
                    agent.destination = corners[scattertarget].transform.position;
                    modeChangeTimer = 0;
                }
            }
            else if (modeChangeIteration == 2)
            {

                if (currentMode == Mode.Scatter && modeChangeTimer > scatterModeTimer2)
                {
                    ChangeMode(Mode.Chase);
                    modeChangeTimer = 0;
                }

                if (currentMode == Mode.Chase && modeChangeTimer > chaseModeTimer2)
                {
                    modeChangeIteration = 3;
                    ChangeMode(Mode.Scatter);
                    int scattertarget = Random.Range(0, 3);
                    agent.destination = corners[scattertarget].transform.position;
                    modeChangeTimer = 0;
                }

            }
            else if (modeChangeIteration == 3)
            {

                if (currentMode == Mode.Scatter && modeChangeTimer > scatterModeTimer3)
                {
                    ChangeMode(Mode.Chase);
                    int scattertarget = Random.Range(0, 3);
                    agent.destination = corners[scattertarget].transform.position;
                    modeChangeTimer = 0;
                }

                if (currentMode == Mode.Chase && modeChangeTimer > chaseModeTimer3)
                {
                    modeChangeIteration = 4;
                    ChangeMode(Mode.Scatter);
                    int scattertarget = Random.Range(0, 3);
                    agent.destination = corners[scattertarget].transform.position;
                    modeChangeTimer = 0;
                }

            }
            else if (modeChangeIteration == 4)
            {

                if (currentMode == Mode.Scatter && modeChangeTimer > scatterModeTimer4)
                {
                    ChangeMode(Mode.Chase);
                    modeChangeTimer = 0;
                }
            }
        }
        else if (currentMode == Mode.Fightened)
        {
            frightenedModeTimer += Time.deltaTime;

            if (frightenedModeTimer >= frightenedModeDuration)
            {
                frightenedModeTimer = 0;
                ChangeMode(previousMode);
                fhelper = 0;
                GameObject text = GameObject.FindGameObjectWithTag("textToTheMoon");
                //text.active = false;
            }

            if (frightenedModeTimer >= startBlinkingAt)
            {
                blinkerTimer += Time.deltaTime;

                if (blinkerTimer >= 0.1f)
                {
                    blinkerTimer = 0;

                    if (frightenedModeIsWhite)
                    {
                        transform.GetComponent<Renderer>().material = ghostYellow;
                        frightenedModeIsWhite = false;
                    }
                    else
                    {
                        transform.GetComponent<Renderer>().material = ghostWhite;
                        frightenedModeIsWhite = true;
                    }
                }

            }
            else
            {

                thenoderandom = listofnodes[Random.Range(0, (listofnodes.Length - 1))];
                transform.GetComponent<Renderer>().material = ghostYellow;
//				agent.destination = new Vector3(0f,1f,3.5f);
//				moveSpeed = moveSpeed * 2;
                if (fhelper == 0)
                {
                    agent.destination = thenoderandom.transform.position;
                    thenoderandom.gameObject.tag = "Random";
                    fhelper = 1;
                }
            }
            
        }
        SetDestination();
    }

    void ChangeMode(Mode m)
    {

        if (currentMode == Mode.Fightened)
        {
            moveSpeed = previousMoveSpeed;
        }
        if (m == Mode.Fightened)
        {
            previousMoveSpeed = moveSpeed;
            moveSpeed = frightenedMoveSpeed;
            transform.GetComponent<NavMeshAgent>().speed = moveSpeed;
        }

        /*if(currentMode != Mode.Fightened)
        {
            previousMode = currentMode;
            currentMode = m;
        }*/

        previousMode = currentMode;
        currentMode = m;
        ModeUpdate();
    }

    public void StartFrightenedMode()
    {
        frightenedModeTimer = 0;
        ChangeMode(Mode.Fightened);
    }

//    public void Respawn()
//    {
//        respawnTimer += Time.deltaTime;
//        Debug.Log(respawnTimer);
//        if (gameObject.name == "GRojo")
//        {
//            if (gameObject.activeSelf == false)
//            {
//                if (respawnTimer >= respawnStart)
//                {
//                    transform.position = new Vector3(0, 1, 8);
//                    gameObject.SetActive(true);
//                    respawnTimer = 0;
//                }
//
//            }
//
//        }
//        if (gameObject.name == "GRosa")
//        {
//            if (gameObject.activeSelf == false)
//            {
//                if (respawnTimer >= respawnStart)
//                {
//                    transform.position = new Vector3(0, 1, 3.5f);
//                    gameObject.SetActive(true);
//                    respawnTimer = 0;
//                }
//
//            }
//        }
//        if (gameObject.name == "GAzul")
//        {
//            if (gameObject.activeSelf == false)
//            {
//                if (respawnTimer >= respawnStart)
//                {
//                    transform.position = new Vector3(-4, 1, 3.5f);
//                    gameObject.SetActive(true);
//                    respawnTimer = 0;
//                }
//
//            }
//        }
//        if (gameObject.name == "GNaranja")
//        {
//            if (gameObject.activeSelf == false)
//            {
//                if (respawnTimer >= respawnStart)
//                {
//                    transform.position = new Vector3(4, 1, 3.5f);
//                    gameObject.SetActive(true);
//                    respawnTimer = 0;
//                }
//
//            }
//        }
//
//    }
//

    Node ChooseNextNode()
    {

        Vector3 targetTile = Vector3.zero;

        Vector3 pacManPosition = agent.transform.position;
        targetTile = new Vector3(Mathf.RoundToInt(pacManPosition.x), Mathf.RoundToInt(pacManPosition.z));

        Node moveToNode = null;

        Node[] foundNodes = new Node[4];
        Vector3[] foundNodesDirection = new Vector3[4];

        int nodeCounter = 0;

        for (int i = 0; i < currentNode.neighbors.Length; i++)
        {

            if (currentNode.validDirections[i] != direction * -1)
            {

                foundNodes[nodeCounter] = currentNode.neighbors[i];
                foundNodesDirection[nodeCounter] = currentNode.validDirections[i];
                nodeCounter++;
            }
        }

        if (foundNodes.Length == 1)
        {

            moveToNode = foundNodes[0];
            direction = foundNodesDirection[0];
        }

        if (foundNodes.Length > 1)
        {

            float leastDistance = 100000f;

            for (int i = 0; i < foundNodes.Length; i++)
            {

                if (foundNodesDirection[i] != Vector3.zero)
                {

                    float distance = GetDistance(foundNodes[i].transform.position, targetTile);

                    if (distance < leastDistance)
                    {

                        leastDistance = distance;
                        moveToNode = foundNodes[i];
                        direction = foundNodesDirection[i];
                    }
                }
            }
        }

        return moveToNode;
    }

    Node GetNodeAtPosition(Vector3 pos)
    {
        GameObject tile = GameObject.Find("Game").GetComponent<GameBoard>().board[(int)pos.x, (int)pos.z];

        if (tile != null)
        {
            if (tile.GetComponent<Node>() != null)
                return tile.GetComponent<Node>();

        }

        return null;
    }

    float LengthFromNode(Vector3 targetPosition)
    {

        Vector3 vec = targetPosition - (Vector3)previousNode.transform.position;
        return vec.sqrMagnitude;
    }

    bool OverShotTarget()
    {

        float nodeToTarget = LengthFromNode(targetNode.transform.position);
        float nodeToSelf = LengthFromNode(transform.localPosition);

        return nodeToSelf > nodeToTarget;
    }

    float GetDistance(Vector3 posA, Vector3 posB)
    {

        float dx = posA.x - posB.x;
        float dz = posA.z - posB.z;

        float distance = Mathf.Sqrt(dx * dx + dz * dz);

        return distance;
    }

    void SetDestination()
    {

        if (currentMode == Mode.Chase)
        {
            if (gameObject.name == "GRojo")
            {
                agent.destination = target.transform.transform.position;
            }
            else if (gameObject.name == "GRosa")
            {
                Vector3 move;
                float direction = target.transform.localEulerAngles.y;
                switch (direction.ToString())
                {
                    case "0":
                        move = new Vector3((target.transform.localPosition.x + 5), target.transform.localPosition.y, (target.transform.localPosition.z + 5));
                        agent.destination = move;
                        break;
                    case "90":
                        move = new Vector3((target.transform.localPosition.x + 5), target.transform.localPosition.y, target.transform.localPosition.z);
                        agent.destination = move;
                        break;
                    case "180":
                        move = new Vector3(target.transform.localPosition.x, target.transform.localPosition.y, (target.transform.localPosition.z - 5));
                        agent.destination = move;
                        break;
                    case "270":
                        move = new Vector3((target.transform.localPosition.x - 5), target.transform.localPosition.y, target.transform.localPosition.z);
                        agent.destination = move;
                        break;
                }
            }
            else if (gameObject.name == "GAzul")
            {
                Vector3 move;
                float direction = target.transform.localEulerAngles.y;
                switch (direction.ToString())
                {
                    case "0":
                        move = new Vector3((target.transform.localPosition.x + 5), target.transform.localPosition.y, (target.transform.localPosition.z + 5));
                        agent.destination = move;
                        break;
                    case "90":
                        move = new Vector3((target.transform.localPosition.x + 5), target.transform.localPosition.y, target.transform.localPosition.z);
                        agent.destination = move;
                        break;
                    case "180":
                        move = new Vector3(target.transform.localPosition.x, target.transform.localPosition.y, (target.transform.localPosition.z - 5));
                        agent.destination = move;
                        break;
                    case "270":
                        move = new Vector3((target.transform.localPosition.x - 5), target.transform.localPosition.y, target.transform.localPosition.z);
                        agent.destination = move;
                        break;
                }
            }
            else if (gameObject.name == "GNaranja")
            {
                agent.destination = target.transform.transform.position;
            }

            modeHelper = 0;
        }
        else if (currentMode == Mode.Scatter && modeHelper == 0)
        {
            if (gameObject.name == "GRojo")
            {
                agent.destination = corners[2].transform.position;
            }
            else if (gameObject.name == "GRosa")
            {
                agent.destination = corners[3].transform.position;
            }
            else if (gameObject.name == "GAzul")
            {
                agent.destination = corners[1].transform.position;
            }
            else if (gameObject.name == "GNaranja")
            {
                agent.destination = corners[0].transform.position;
            }
            modeHelper = 1;
        }

    }
}
