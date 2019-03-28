using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileScript : MonoBehaviour
{
    public int type;
    public Sprite[] tileSprites;

    public void SetSprite(int rand){
        type = rand;
        GetComponent<SpriteRenderer>().sprite = tileSprites[type];
    }
}
