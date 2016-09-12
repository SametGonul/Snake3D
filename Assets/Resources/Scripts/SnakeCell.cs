using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;


public class SnakeCell : MonoBehaviour
{
    private Direction currentDirection;
    private PlayerController pc;
    private FoodManager fm;
    private Score sm;
    public int point;
    private int snakeCollisions = 0;
    void Start()
    {
        pc = (PlayerController)FindObjectOfType(typeof(PlayerController));

        fm = (FoodManager)FindObjectOfType(typeof(FoodManager));

        sm = (Score)FindObjectOfType(typeof(Score));


    }

    void OnTriggerEnter(Collider coll)
    {
      
        Debug.Log("OnTriggerEnter");
        if (coll.tag == "wall")
        {
            int snakeHealth = pc.getSnakeHealth();
            //snakeHealth -= 25;
            pc.setSnakeHealth(snakeHealth);
            
            if (snakeHealth <= 0)
            {
                pc.restart = true;
                pc.destroySnake();
                sm.setScoreToZero();
            }
            Debug.Log("Entered");

        }

        if (coll.tag == "food")
        {
           
            point = fm.getFoodPoint();
            sm.increaseScore(point);
            fm.eatFood();
            GameObject newCell = pc.createSnakeCell(coll.transform.position);
            pc.addCellToSnake(0, newCell);
            
            fm.createFood();
        }
        if ((coll.tag == "snakeBody") && (coll.name != "snakebody1"))
        {
            Debug.Log("crashingbody");
            snakeCollisions++;
            int snakeHealth = pc.getSnakeHealth();
            if (snakeHealth > 0)
            {
                if (snakeCollisions == 1)
                {
                    snakeHealth = snakeHealth - 20;
                    pc.setSnakeHealth(snakeHealth);
                }
                else if (snakeCollisions == 2)
                {
                    snakeHealth = snakeHealth - 30;
                    pc.setSnakeHealth(snakeHealth);
                }
                else if (snakeCollisions == 3)
                {
                    snakeHealth = snakeHealth - 50;
                    pc.setSnakeHealth(snakeHealth);
                }
            }

            else if (snakeHealth <= 0)
            {
                pc.restart = true;
                pc.destroySnake();
                sm.setScoreToZero();
            }
           
        }
    }

    public Direction getDirection()
    {
        return currentDirection;
    }

    public void setDirection(Direction direction)
    {
        currentDirection = direction;
    }
}

