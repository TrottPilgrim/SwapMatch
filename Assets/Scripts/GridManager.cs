using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    public static GridManager Instance;
    public GameObject[,] tiles;
    public GameObject tilePrefab;
    public GameObject playerFab;
    public const int WIDTH = 5;
    public const int HEIGHT = 7;

    float xOffset = WIDTH / 2f - 0.5f;
    float yOffset = HEIGHT / 2f - 0.5f;

    GameObject gridHolder;
    PlayerScript playerScript;
    public static float slideLerp = -1;
    public float lerpSpeed = 0.25f;
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

    
    void Update()
    {
        if (slideLerp < 0 && !Repopulate() && HasMatch()){
            RemoveMatches();
        }
    }
    public TileScript HasMatch(){
        for (int x = 0; x < WIDTH; x++){
            for (int y = 0; y < HEIGHT; y++){
                TileScript temp = tiles[x, y].GetComponent<TileScript>();
                if (temp is object){
                    if (x < WIDTH - 2 && temp.IsMatch(tiles[x + 1, y], tiles[x + 2, y]))
                        return temp;
                    if (y < HEIGHT - 2 && temp.IsMatch(tiles[x, y + 1], tiles[x, y + 2]))
                        return temp;
                }
            }
        }
        return null;
    }

    public void RemoveMatches(){
        for (int x = 0; x < WIDTH; x++){
            for (int y = 0; y < HEIGHT; y++){
                TileScript temp = tiles[x, y].GetComponent<TileScript>();
                if (temp is object){
                    if (x < WIDTH - 2 && temp.IsMatch(tiles[x + 1, y], tiles[x + 2, y]))
                    {
                        Destroy(tiles[x, y]);
                        Destroy(tiles[x + 1, y]);
                        Destroy(tiles[x + 2, y]);
                    }
                    if (y < HEIGHT - 2 && temp.IsMatch(tiles[x, y + 1], tiles[x, y + 2]))
                    {
                        Destroy(tiles[x, y]);
                        Destroy(tiles[x, y + 1]);
                        Destroy(tiles[x, y + 2]);
                    }
                }
            }
        }
    }

    public bool Repopulate(){
        bool repop = false;
        for (int x = 0; x < WIDTH; x++){
            for (int y = 0; y < HEIGHT; y++){
                if (tiles[x, y] == null){
                    repop = true;
                    if (y == 0){
                        tiles[x, y] = Instantiate(tilePrefab);
                        TileScript tileScript = tiles[x, y].GetComponent<TileScript>();
                        tileScript.SetSprite(Random.Range(0, tileScript.tileColors.Length));
                        tiles[x, y].transform.parent = gridHolder.transform;
                        tiles[x, y].transform.localPosition = new Vector2 (WIDTH - x - xOffset, HEIGHT - y - yOffset);

                    }
                    else {
                        slideLerp = 0;
                        tiles[x, y] = tiles[x, y - 1];
                        TileScript tileScript = tiles[x, y].GetComponent<TileScript>();
                        if (tileScript != null){
                            tileScript.SetupSlide(new Vector2(WIDTH - x - xOffset, HEIGHT - y - yOffset));
                            tiles[x, y - 1] = null;
                        }
                    }
                }
            }
        }
        return repop;
    }
}
