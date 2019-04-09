﻿using System.Collections;
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
    public Vector3 startPosition;
    public Vector3 destPosition;
    //private bool inSlide = false;

    public void SetSprite(int rand){
        type = rand;
        //GetComponent<SpriteRenderer>().sprite = tileSprites[type];
        if (rand >= 0)
            GetComponent<SpriteRenderer>().color = tileColors[type];
    }

    public bool IsMatch(GameObject gameObject1, GameObject gameObject2){
        TileScript ts1 = gameObject1.GetComponent<TileScript>();
        TileScript ts2 = gameObject1.GetComponent<TileScript>();
        return ts1 != null && ts2 != null && type == ts1.type && type == ts2.type;
    }

    public void SetupSlide(Vector2 newDestPos){
        //inSlide = true;
        startPosition = transform.localPosition;
        destPosition = newDestPos;
    }
}
