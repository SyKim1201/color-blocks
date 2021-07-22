using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Playfield : MonoBehaviour
{

    public static Playfield instance;
    public int width;
    public int height;
    public GameObject[,] grid;
    
    public List<Sprite> blockSprites = new List<Sprite>();


    public GameObject block;


    void Start()
    {
        instance = GetComponent<Playfield>();
        Vector2 blockDim = block.GetComponent<SpriteRenderer>().bounds.size;
        GenerateGrid(blockDim.x, blockDim.y);
    }

    private void GenerateGrid(float w, float h) 
    {
        grid = new GameObject[width, height];

        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                float xPos = transform.position.x + (w * x);
                float yPos = transform.position.y + (h * y);

                GameObject newBlock = Instantiate(block,
                    new Vector3(xPos, yPos, 0),
                    block.transform.rotation);

                grid[x,y] = newBlock;

                newBlock.transform.parent = transform;
                newBlock.GetComponent<SpriteRenderer>().sprite = blockSprites[Random.Range(0, blockSprites.Count)];

            }
        }
    }

    public void Drop() 
    {

        for (int x = 0; x < width; x++)
        {
            for (int y = 1; y < height; y++)
            {
                if (grid[x,y].GetComponent<SpriteRenderer>().sprite != null && 
                    grid[x,y-1].GetComponent<SpriteRenderer>().sprite == null)
                {
                    int i = x;
                    int j = y;
                    while (j > 0 && grid[i,j-1].GetComponent<SpriteRenderer>().sprite == null)
                    {
                        grid[i,j-1].GetComponent<SpriteRenderer>().sprite = grid[i,j].GetComponent<SpriteRenderer>().sprite;
                        grid[i,j].GetComponent<SpriteRenderer>().sprite = null;
                        j--;
                    }
                }
            }
        }
    }

    public void LeftAlign()
    {
        for (int i = 0; i < width - 1; i++)
        {
            for (int x = 0; x < width - 1; x++)
            {
                bool empty = true;
                for (int y = 0; y < height; y++)
                {
                    if (grid[x,y].GetComponent<SpriteRenderer>().sprite != null)
                    {
                        empty = false;
                    }
                }
                if (empty)
                {
                    for (int y = 0; y < height; y++)
                    {
                        grid[x,y].GetComponent<SpriteRenderer>().sprite = grid[x+1,y].GetComponent<SpriteRenderer>().sprite;
                        grid[x+1,y].GetComponent<SpriteRenderer>().sprite = null;
                    }
                }
            }
        }
    }

    public bool CheckGameOver()
    {
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                if (grid[x,y].GetComponent<SpriteRenderer>().sprite != null)
                {
                    if (x > 0)
                    {
                        if (grid[x,y].GetComponent<SpriteRenderer>().sprite == grid[x-1,y].GetComponent<SpriteRenderer>().sprite)
                        {
                            return false;
                        }
                    }
                    if (x < width-1) {
                        if (grid[x,y].GetComponent<SpriteRenderer>().sprite == grid[x+1,y].GetComponent<SpriteRenderer>().sprite)
                        {
                            return false;
                        }
                    }
                    if (y > 0)
                    {
                        if (grid[x,y].GetComponent<SpriteRenderer>().sprite == grid[x,y-1].GetComponent<SpriteRenderer>().sprite)
                        {
                            return false;
                        }
                    }
                    if (y < height-1) {
                        if (grid[x,y].GetComponent<SpriteRenderer>().sprite == grid[x,y+1].GetComponent<SpriteRenderer>().sprite)
                        {
                            return false;
                        }
                    }
                }
            }
        }
        return true;
    }

    public void GameOver()
    {
        bool go = CheckGameOver();

        if (go)
        {
            MainMenuController.instance.over();
            GameManager.instance.FinalScore();
        }
    }


}
