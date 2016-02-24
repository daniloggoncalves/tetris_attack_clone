using UnityEngine;
using System.Collections;

public class Screen_Behavior : MonoBehaviour {

    public float initial_grid_velocity;
    public int width;
    public int height;
    public GameObject Block;

    public float Lim_Time;

    float Delta_Time;
    int[,] Grid;
    GameObject[,] Blocks;
    Vector3[,] Block_Position;

    void Start()
    {
        Delta_Time = 0;
        Grid = new int[width, height];
        Blocks = new GameObject[width, height];
        Block_Position = new Vector3[width, height];
        int i, j;

        for (i = 0; i < width; i++)
        {
            for (j = 0; j < height; j++)
            {
                Grid[i, j] = 0;
                Blocks[i, j] = (GameObject)Instantiate(Block, new Vector3(1.1f * i, 1.1f * j, 10), Quaternion.identity);
                Block_Position[i, j] = Blocks[i, j].GetComponent<Transform>().transform.position;
            }
        }

        for (i = 0; i < width; i++)
        {
            for (j = 0; j < (height/2); j++)
            {
                Grid[i, j] = Random.Range(1,8);
            }
        }
        Grid[0, 8] = 2;
        //Grid[0, 9] = 1;
    }

    void Update()
    {
        int i, j;

        Colour_Block();
        Delta_Time += Time.deltaTime;

        if (Blocks[0, 11].transform.position.y > 13)
        {
            Roll_Grid_Down();
            Colour_Block();
        }

        if (Delta_Time > Lim_Time)
        {
            Delta_Time = 0;
            Gravity();
            for (i = 0; i < width; i++)
            {
                for (j = 0; j < height; j++)
                {
                    Block_Position[i, j].y += initial_grid_velocity;
                    Blocks[i, j].GetComponent<Transform>().transform.position = Block_Position[i, j];
                }
            }
        }        

        Control_Block();
    }



    void Control_Block()
    {
        int i, j;
        Touch touch_one = Input.GetTouch(0);
        Vector3 touch_position = new Vector3((touch_one.position.x / (float)Screen.width) * 10, (touch_one.position.y / (float)Screen.height) * 16, 10);
        if (Input.touchCount > 0 && Input.GetTouch(0).phase != TouchPhase.Ended)
        {
            for (i = 0; i < width; i++)
            {
                for (j = 0; j < height; j++)
                {
                    Block_Position[i, j] = Blocks[i, j].GetComponent<Transform>().transform.position;
                    if (Vector3.Distance(touch_position, Block_Position[i, j]) < 1)
                    {
                        Blocks[i, j].GetComponent<Transform>().transform.localScale = new Vector3(1.5f, 1.5f, 1f);
                    }
                }
            }
        }

        else
        {
            for (i = 0; i < width; i++)
            {
                for (j = 0; j < height; j++)
                {                   
                        Blocks[i, j].GetComponent<Transform>().transform.localScale = new Vector3(1f, 1f, 1f);                    
                }
            }
        }
    }




    void Gravity()
    {
        int i, j;
        for (i = 0; i < width; i++)
        {
            for (j = 1; j < height; j++)
            {
                if (Grid[i, j] != 0 && Grid[i, j-1] == 0)
                {
                    Grid[i, j-1] = Grid[i, j];
                    Grid[i, j] = 0;
                }
            }
        }
    }

    void Roll_Grid_Down()
    {
        int i,j;

        for (i = 0; i < width; i++)
        {
            for (j = height - 1; j > 0; j--)
            {
                Grid[i, j] = Grid[i, j - 1];
            }

            Grid[i, 0] = Random.Range(1, 8);
        }

        for (i = 0; i < width; i++)
        {
            for (j = 0; j < height; j++)
            {
                Block_Position[i, j] = Blocks[i, j].GetComponent<Transform>().transform.position;
                Block_Position[i, j].y = Block_Position[i, j].y - 1.1f;
                Blocks[i, j].GetComponent<Transform>().transform.position = Block_Position[i, j];
            }
        }

    }


    void Colour_Block()
    {
        int i, j;

        for (i = 0; i < width; i++)
        {
            for (j = 0; j < height; j++)
            {
                if (Grid[i,j] == 0)
                {
                    Blocks[i, j].GetComponent<Renderer>().material.SetColor("_Color", Color.white);
                }
                if (Grid[i, j] == 1)
                {
                    Blocks[i,j].GetComponent<Renderer>().material.SetColor("_Color", Color.yellow);
                }

                if (Grid[i, j] == 2)
                {
                    Blocks[i, j].GetComponent<Renderer>().material.SetColor("_Color", Color.red);
                }

                if (Grid[i, j] == 3)
                {
                    Blocks[i, j].GetComponent<Renderer>().material.SetColor("_Color", Color.blue);
                }

                if (Grid[i, j] == 4)
                {
                    Blocks[i, j].GetComponent<Renderer>().material.SetColor("_Color", Color.green);
                }

                if (Grid[i, j] == 5)
                {
                    Blocks[i, j].GetComponent<Renderer>().material.SetColor("_Color", Color.cyan);
                }

                if (Grid[i, j] == 6)
                {
                    Blocks[i, j].GetComponent<Renderer>().material.SetColor("_Color", Color.magenta);
                }

                if (Grid[i, j] == 7)
                {
                    Blocks[i, j].GetComponent<Renderer>().material.SetColor("_Color", new Vector4(0.3f, 0.3f, 0.3f, 1));
                }
            }
        }
    }
}
