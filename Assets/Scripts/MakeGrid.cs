﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MakeGrid : MonoBehaviour
{
    public const int WIDTH = 5;
    public const int HEIGHT = 7;

    float xOffset = WIDTH / 2f - 0.5f;
    float yOffset = HEIGHT / 2f - 0.5f;

    public GameObject[,] tiles;
    public GameObject tilePrefab;

    public GameObject playerFab;

    GameObject gridHolder;
    PlayerScript playerScript;

    // Start is called before the first frame update
    void Start()
    {
        tiles = new GameObject[WIDTH, HEIGHT];
        gridHolder = new GameObject();
        gridHolder.transform.position = new Vector3(-1f, -0.5f, 0);
        
        for (int i = 0; i < WIDTH; i++){
            for (int j = 0; j < HEIGHT; j++){
                GameObject newTile = Instantiate(tilePrefab);
                newTile.name = i + "," + j;
                newTile.transform.parent = gridHolder.transform;
                //WHY does this matrix go from upper right to bottom left. Is this supposed to happen?
                //newTile.transform.localPosition = new Vector2(WIDTH - i - xOffset, HEIGHT - j - yOffset);
                newTile.transform.localPosition = new Vector2( i - 1, HEIGHT - j - yOffset);

                tiles[i, j] = newTile;
                TileScript tileScript = newTile.GetComponent<TileScript>();
                tileScript.SetSprite(Random.Range(0, tileScript.tileColors.Length));
            }
        }
        int playerInitXPos = WIDTH / 2;
        int playerInitYPos = HEIGHT / 2;
        GameObject player = Instantiate(playerFab);
        playerScript = player.GetComponent<PlayerScript>();
        player.transform.parent = gridHolder.transform;
        Vector2 playerStartPos = tiles[playerInitXPos, playerInitYPos].transform.localPosition;
        Destroy(tiles[playerInitXPos, playerInitYPos]);
        player.transform.localPosition = playerStartPos;
        playerScript.xPos = playerInitXPos;
        playerScript.yPos = playerInitYPos;

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("d") || Input.GetKeyDown(KeyCode.RightArrow)){
            if (playerScript.xPos < WIDTH - 1){
                GameObject tileToSwap = tiles[playerScript.xPos + 1, playerScript.yPos];
                Vector2 newPosition = tileToSwap.transform.localPosition;

                // Changes the positions of the two objects in the game space
                tileToSwap.transform.localPosition = playerScript.gameObject.transform.localPosition;
                playerScript.gameObject.transform.localPosition = newPosition;

                // Changes the positions of the two objects in the 2D array
                tiles[playerScript.xPos + 1, playerScript.yPos] = playerScript.gameObject;
                tiles[playerScript.xPos, playerScript.yPos] = tileToSwap;
                playerScript.xPos++;
                playerScript.gameObject.SendMessage("decrementTurns");
            }
        }
        else if (Input.GetKeyDown("a") || Input.GetKeyDown(KeyCode.LeftArrow)){
            if (playerScript.xPos > 0){
                GameObject tileToSwap = tiles[playerScript.xPos - 1, playerScript.yPos];
                Vector2 newPosition = tileToSwap.transform.localPosition;
                
                // Changes the positions of the two objects in the game space
                tileToSwap.transform.localPosition = playerScript.gameObject.transform.localPosition;
                playerScript.gameObject.transform.localPosition = newPosition;

                // Changes the positions of the two objects in the 2D array
                tiles[playerScript.xPos - 1, playerScript.yPos] = playerScript.gameObject;
                tiles[playerScript.xPos, playerScript.yPos] = tileToSwap;
                playerScript.xPos--;
                playerScript.gameObject.SendMessage("decrementTurns");
            }
        }
        
        else if (Input.GetKeyDown("w") || Input.GetKeyDown(KeyCode.UpArrow)){
            if (playerScript.yPos > 0){
                GameObject tileToSwap = tiles[playerScript.xPos, playerScript.yPos - 1];
                Vector2 newPosition = tileToSwap.transform.localPosition;
                
                // Changes the positions of the two objects in the game space
                tileToSwap.transform.localPosition = playerScript.gameObject.transform.localPosition;
                playerScript.gameObject.transform.localPosition = newPosition;

                // Changes the positions of the two objects in the 2D array
                tiles[playerScript.xPos, playerScript.yPos - 1] = playerScript.gameObject;
                tiles[playerScript.xPos, playerScript.yPos] = tileToSwap;
                playerScript.yPos--;
                playerScript.gameObject.SendMessage("decrementTurns");
            }
        }
        else if (Input.GetKeyDown("s") || Input.GetKeyDown(KeyCode.DownArrow)){
            if (playerScript.yPos < HEIGHT - 1){
                GameObject tileToSwap = tiles[playerScript.xPos, playerScript.yPos + 1];
                Vector2 newPosition = tileToSwap.transform.localPosition;
                
                // Changes the positions of the two objects in the game space
                tileToSwap.transform.localPosition = playerScript.gameObject.transform.localPosition;
                playerScript.gameObject.transform.localPosition = newPosition;

                // Changes the positions of the two objects in the 2D array
                tiles[playerScript.xPos, playerScript.yPos + 1] = playerScript.gameObject;
                tiles[playerScript.xPos, playerScript.yPos] = tileToSwap;
                playerScript.yPos++;
                playerScript.gameObject.SendMessage("decrementTurns");
            }
        }
    }

    /*
        This function checks if a line has some matching sprites
        Inputs: initial X position, initial Y position 
     */
    void checkLine() {
    
    }
}
