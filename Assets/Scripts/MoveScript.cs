using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveScript : MonoBehaviour
{
    PlayerScript playerScript;
    public GameObject[,] tiles;
    // Start is called before the first frame update
    void Start()
    {
        playerScript = gameObject.GetComponentInParent(typeof(PlayerScript)) as PlayerScript;
        tiles = GridManager.Instance.tiles;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.anyKeyDown){
            MovePlayer();
        }
    }

    /* 
     */
    void MovePlayer(){
        int hozMove = (int) Input.GetAxisRaw("Horizontal");
        int verMove = (int) Input.GetAxisRaw("Vertical");
        if (hozMove != 0)
            verMove = 0;
        if (verMove != 0)
            hozMove = 0;
        int newXPos = playerScript.xPos - hozMove;
        int newYPos = playerScript.yPos - verMove;

        if (newXPos < GridManager.WIDTH &&
            newXPos >= 0 &&
            newYPos < GridManager.HEIGHT &&
            newYPos >= 0 &&
            (hozMove != 0 ||
            verMove != 0))
        {
            //Get the tile that needs to swap with the player and save its position
            GameObject tileToSwap = tiles[newXPos, newYPos];
            Debug.Log(tileToSwap);
            Vector2 newPosition = tileToSwap.transform.localPosition;
        
            //Swap the locations of the two objects in the game space
            tileToSwap.transform.localPosition = playerScript.gameObject.transform.localPosition;
            playerScript.gameObject.transform.localPosition = newPosition;

            //Swap the two objects in the 2D array
            tiles[newXPos, newYPos] = playerScript.gameObject;
            tiles[playerScript.xPos, playerScript.yPos] = tileToSwap;

            //Update the x position and y position of the player.
            playerScript.xPos -= hozMove;
            playerScript.yPos -= verMove;
            playerScript.gameObject.SendMessage("decrementTurns");
        }
    }
}
