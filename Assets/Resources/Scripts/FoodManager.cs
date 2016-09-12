using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class FoodManager : ScriptableObject {

    public bool isFoodOnArea;
    public int foodPoint = 15;

    private PlayerController pc;
    // Use this for initialization
    void Start () {
        pc = (PlayerController)FindObjectOfType(typeof(PlayerController));
        createFood();

    }

    // Update is called once per frame
    void Update () {
        
    }

    public void eatFood()
    {
        isFoodOnArea = false;
       
        Debug.Log("Food is Eaten");
        
        Destroy(GameObject.FindGameObjectWithTag("food"));
 
        Destroy(GameObject.FindGameObjectWithTag("food").GetComponent<Rigidbody>());

        pc.setNamesAndTags();
    }

    public void createFood()
    {
        Vector3 foodScale = pc.getSnakeScale();
        int x;
        int y;
        int z;
        x = Random.Range(4, 195);
        y = Random.Range(4, 195);
        z = Random.Range(4, 195);
        GameObject food = Instantiate(Resources.Load("Prefabs/Food")) as GameObject;
        food.transform.position = new Vector3(x, y, z);

        food.transform.localScale = foodScale;
        food.name = "Food";
        food.tag = "food";

        //Rigidbody rigidbody = food.AddComponent(typeof(Rigidbody)) as Rigidbody;
        // rigidbody.useGravity = false;
        //rigidbody.freezeRotation = false;

        //BoxCollider foodBoxCollider = food.GetComponent<BoxCollider>();
        
        //foodMeshRenderer.material = Instantiate(Resources.Load("Assets/Prefabs/FoodTexture.mat",typeof(Material)) as Material) as Material;

        while (GameObject.FindGameObjectWithTag("snake").transform.position == food.transform.position)
        {
            x = Random.Range(4, 195);
            y = Random.Range(4, 195);
            z = Random.Range(4, 195);
            food.transform.position = new Vector3(x, y, z);
        }
        isFoodOnArea = true;
    }

    public Vector3 getFoodPosition()
    {
        return (GameObject.FindGameObjectWithTag("food").transform.position);
    }

    public int getFoodPoint()
    {
        return foodPoint;
    }
}
