using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MakeGrid : MonoBehaviour
{
    public static MakeGrid Instance;
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
        Instance = this;
        tiles = new GameObject[WIDTH, HEIGHT];
        gridHolder = new GameObject();
        gridHolder.transform.position = new Vector3(-1f, -0.5f, 0);
        
        for (int i = 0; i < WIDTH; i++){
            for (int j = 0; j < HEIGHT; j++){
                GameObject newTile = Instantiate(tilePrefab);
                //newTile.name = i + "," + j;
                newTile.transform.parent = gridHolder.transform;
                //WHY does this matrix go from upper right to bottom left. Is this supposed to happen?
                //I know why, it's because then you can just use Input.GetAxis for movement.
                newTile.transform.localPosition = new Vector2(WIDTH - i - xOffset, HEIGHT - j - yOffset);
                //newTile.transform.localPosition = new Vector2( i - 1, HEIGHT - j - yOffset);

                tiles[i, j] = newTile;
                TileScript tileScript = newTile.GetComponent<TileScript>();
                tileScript.SetSprite(Random.Range(0, tileScript.tileColors.Length));
            }
        }

        //Initializing the player object
        int playerInitXPos = WIDTH / 2;
        int playerInitYPos = HEIGHT / 2;
        GameObject player = Instantiate(playerFab);
        TileScript playerTile = player.GetComponent<TileScript>();
        playerScript = player.GetComponent<PlayerScript>();
        player.transform.parent = gridHolder.transform;
        Vector2 playerStartPos = tiles[playerInitXPos, playerInitYPos].transform.localPosition;
        Destroy(tiles[playerInitXPos, playerInitYPos]);
        player.transform.localPosition = playerStartPos;
        tiles[playerInitXPos, playerInitYPos] = player;
        playerScript.xPos = playerInitXPos;
        playerScript.yPos = playerInitYPos;
        playerTile.SetSprite(-1);
    }
    
}
