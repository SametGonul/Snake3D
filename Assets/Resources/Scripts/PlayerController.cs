using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Collections;

public class PlayerController : ScriptableObject
{
    public float snakeSize;
 
    public float snakeSpeed;

    private List<GameObject> snakeList;
    private Direction currentDirection = Direction.FORWARD;
    private string input;
    private int time;

    public bool restart;

    public int snakeHealth = 100;

    private float rotateSpeed = 0.3f;

    void Start()
    {
        snakeList = new List<GameObject>();
        GameObject snakeHead = createSnakeCell(new Vector3(200, 200, 200));      
        addCellToSnake(0, snakeHead);
        //Camera.main.transform.position = new Vector3(snakeHead.transform.position.x, snakeHead.transform.position.y + 30, snakeHead.transform.position.z - 60);
        restart = false;
    }

    void getInput()
    {
        if (restart == false)
        {
            if (Input.GetKeyDown(KeyCode.W))
            {
                if (currentDirection == Direction.FORWARD || currentDirection == Direction.LEFT || currentDirection == Direction.BACKWARD || currentDirection == Direction.RIGHT)
                {

                    currentDirection = Direction.UP;
                }
            }
            else if (Input.GetKeyDown(KeyCode.S))
            {
                if (currentDirection == Direction.FORWARD || currentDirection == Direction.LEFT || currentDirection == Direction.BACKWARD || currentDirection == Direction.RIGHT)
                {

                    currentDirection = Direction.DOWN;
                }
            }
            else if (Input.GetKeyDown(KeyCode.A))
            {

                if (currentDirection == Direction.FORWARD)
                {
                    currentDirection = Direction.LEFT;
                }
                else if (currentDirection == Direction.LEFT)
                {
                    currentDirection = Direction.BACKWARD;

                }

                else if (currentDirection == Direction.BACKWARD)
                {
                    currentDirection = Direction.RIGHT;
                }

                else if (currentDirection == Direction.RIGHT)
                {
                    currentDirection = Direction.FORWARD;
                }
                else if (currentDirection == Direction.UP)
                {
                    currentDirection = Direction.LEFT;
                }
                else if (currentDirection == Direction.DOWN)
                {
                    currentDirection = Direction.LEFT;
                }
            }
            else if (Input.GetKeyDown(KeyCode.D))
            {

                if (currentDirection == Direction.FORWARD)
                {
                    currentDirection = Direction.RIGHT;
                }
                else if (currentDirection == Direction.LEFT)
                {
                    currentDirection = Direction.FORWARD;

                }

                else if (currentDirection == Direction.BACKWARD)
                {
                    currentDirection = Direction.LEFT;
                }

                else if (currentDirection == Direction.RIGHT)
                {
                    currentDirection = Direction.BACKWARD;
                }
                else if (currentDirection == Direction.UP)
                {
                    currentDirection = Direction.RIGHT;
                }
                else if (currentDirection == Direction.DOWN)
                {
                    currentDirection = Direction.RIGHT;
                }
            }

            SnakeCell sc = snakeList[0].GetComponent(typeof(SnakeCell)) as SnakeCell;
            sc.setDirection(currentDirection);
        }
    }
    void moveSnake()
    {
        if (restart == false)
        {
            for (int i = snakeList.Count - 1; i > 0; i--)
            {
                snakeList[i].transform.position = snakeList[i - 1].transform.position;
            }

            Vector3 newPosition = snakeList[0].transform.position;
            if (currentDirection == Direction.UP)
            {
                newPosition.y = newPosition.y + snakeSpeed;
            }
            else if (currentDirection == Direction.DOWN)
            {
                newPosition.y = newPosition.y - snakeSpeed;
            }
            else if (currentDirection == Direction.LEFT)
            {
                newPosition.x = newPosition.x - snakeSpeed;
            }
            else if (currentDirection == Direction.RIGHT)
            {
                newPosition.x = newPosition.x + snakeSpeed;
            }
            else if (currentDirection == Direction.FORWARD)
            {
                newPosition.z = newPosition.z + snakeSpeed;
            }
            else if (currentDirection == Direction.BACKWARD)
            {
                newPosition.z = newPosition.z - snakeSpeed;
            }

            snakeList[0].transform.position = newPosition;


            updateCellsDirections();
        }
    }

    private void updateCellsDirections()
    {
        for (int i = snakeList.Count - 1; i > 0; i--)
        {
            SnakeCell sc = snakeList[i - 1].GetComponent(typeof(SnakeCell)) as SnakeCell;
            SnakeCell nextSc = snakeList[i].GetComponent(typeof(SnakeCell)) as SnakeCell;
            nextSc.setDirection(sc.getDirection());
        }
    }

    public GameObject createSnakeCell(Vector3 position)
    {

        GameObject newCell = GameObject.CreatePrimitive(PrimitiveType.Cube);
        newCell.transform.localScale = new Vector3(snakeSize, snakeSize, snakeSize);
        newCell.transform.position = position;

        newCell.tag = "snake";
        newCell.name = "Snake";

        BoxCollider headBoxCollider = newCell.GetComponent(typeof(BoxCollider)) as BoxCollider;
        headBoxCollider.isTrigger = true;

        newCell.AddComponent(typeof(SnakeCell));

        Rigidbody rigidbody = newCell.AddComponent(typeof(Rigidbody)) as Rigidbody;
        rigidbody.useGravity = false;
        rigidbody.freezeRotation = true;

        
        return newCell;
    }

    public void addCellToSnake(int index, GameObject cell)
    {
        snakeList.Insert(index, cell);
    }

    public void destroySnake()
    {
        for (int i = 0; i < snakeList.Count; i++)
        {
            Destroy(snakeList[i]);
        }
    }

    void restartStart()
    {
        currentDirection = Direction.FORWARD;
        snakeList = new List<GameObject>();
        GameObject snakeHead = createSnakeCell(new Vector3(100, 100, 100));
        addCellToSnake(0, snakeHead);
        snakeHealth = 100;
        restart = false;
    }

    void Update()
    {

        
        getInput();
        if (time++ % 3 != 0) return;
        moveSnake();
        if (restart == true)
        {
            restartStart();
        }

        //setCamera();
    }

    public Vector3 getSnakeHeadPosition()
    {
        return snakeList[0].transform.position;
    }

    public bool getRestart()
    {
        return restart;
    }

    public Vector3 getSnakeScale()
    {
        Vector3 snakeScale = new Vector3(snakeSize, snakeSize, snakeSize);
        return snakeScale;
    }
    /*public string getCurrentDirection()
    {
        return currentDirection;
    }*/

   /* void setCamera()
    {
        Vector3 newPosition = snakeList[0].transform.position;
        if (currentDirection == Direction.FORWARD)
        {
            LeanTween.rotate(Camera.main.gameObject, new Vector3(0, 0, 0), rotateSpeed);
            Camera.main.transform.position = new Vector3(snakeList[0].transform.position.x, snakeList[0].transform.position.y + 30, snakeList[0].transform.position.z - 60);

        }
        else if (currentDirection == Direction.BACKWARD)
        {
            LeanTween.rotate(Camera.main.gameObject, new Vector3(0, -180, 0), rotateSpeed);
            Camera.main.transform.position = new Vector3(snakeList[0].transform.position.x, snakeList[0].transform.position.y + 30, snakeList[0].transform.position.z + 60);
        }
        else if (currentDirection == Direction.LEFT)
        {
            LeanTween.rotate(Camera.main.gameObject, new Vector3(0, -90, 0), rotateSpeed);
            Camera.main.transform.position = new Vector3(snakeList[0].transform.position.x + 60, snakeList[0].transform.position.y + 30, snakeList[0].transform.position.z);
        }
        else if (currentDirection == Direction.RIGHT)
        {
            LeanTween.rotate(Camera.main.gameObject, new Vector3(0, 90, 0), rotateSpeed);
            Camera.main.transform.position = new Vector3(snakeList[0].transform.position.x - 60, snakeList[0].transform.position.y + 30, snakeList[0].transform.position.z);
        }
        else if (currentDirection == Direction.UP)
        {
            LeanTween.rotate(Camera.main.gameObject, new Vector3(-90, 0, 0), rotateSpeed);
            Camera.main.transform.position = new Vector3(snakeList[0].transform.position.x, snakeList[0].transform.position.y - 60, snakeList[0].transform.position.z + 30);
        }
        else if (currentDirection == Direction.DOWN)
        {
            LeanTween.rotate(Camera.main.gameObject, new Vector3(90, 0, 0), rotateSpeed);
            Camera.main.transform.position = new Vector3(snakeList[0].transform.position.x, snakeList[0].transform.position.y + 60, snakeList[0].transform.position.z + 30);
        }
    }*/

    public int getSnakeHealth()
    {
        return snakeHealth;
    }

    public void setSnakeHealth(int newSnakeHealth)
    {
        snakeHealth = newSnakeHealth;
    }
    public void setNamesAndTags()
    {
        snakeList[0].name = "snakehead";
        snakeList[0].tag = "snakeHead";
        for(int i = 1; i < snakeList.Count; i++)
        {
            snakeList[i].name = "snakebody"+i;
            snakeList[i].tag = "snakeBody";
        }
    }
}
