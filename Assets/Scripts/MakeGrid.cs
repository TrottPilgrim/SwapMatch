using System.Collections;
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
                newTile.transform.localPosition = new Vector2(WIDTH - i - xOffset, HEIGHT - j - yOffset);
                //newTile.transform.localPosition = new Vector2(i - xOffset, j - yOffset);

                tiles[i, j] = newTile;
                TileScript tileScript = newTile.GetComponent<TileScript>();
                tileScript.SetSprite(Random.Range(0, tileScript.tileColors.Length));
            }
        }
        GameObject player = Instantiate(playerFab);
        playerScript = player.GetComponent<PlayerScript>();
        player.transform.parent = gridHolder.transform;
        Vector2 playerStartPos = tiles[2, 3].transform.localPosition;
        Destroy(tiles[2, 3]);
        player.transform.localPosition = playerStartPos;

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetAxis("Horizontal") > 0){
            
            if (playerScript.xPos <= HEIGHT && playerScript.xPos >= 0){
                GameObject tileToSwap = tiles[playerScript.xPos + 1, playerScript.yPos];
            }
        }
        else if (Input.GetAxis("Horizontal") < 0) {

        }
        if (Input.GetAxis("Vertical") > 0){

        }
        else if (Input.GetAxis("Vertical") < 0) {

        }
    }
}
