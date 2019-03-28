using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileScript : MonoBehaviour
{
    public int type;
    //public Sprite[] tileSprites;
    public Color[] tileColors = 
    {
        Color.red,
        new Color(0.96f, 0.63f, 0f, 1.0f), // Orange
        Color.yellow,
        Color.green,
        Color.blue,
        new Color(0.65f, 0f, 0.96f, 1.0f) // Violet
    };

    public void SetSprite(int rand){
        type = rand;
        //GetComponent<SpriteRenderer>().sprite = tileSprites[type];
        GetComponent<SpriteRenderer>().color = tileColors[type];
    }

}
