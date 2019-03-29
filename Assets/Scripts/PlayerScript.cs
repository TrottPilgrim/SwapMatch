using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    public int turnsRemaining;
    TextMesh turnsText;
    public int xPos, yPos;
    // Start is called before the first frame update
    void Start()
    {
        turnsText = this.gameObject.transform.GetChild(0).gameObject.GetComponent<TextMesh>();
        resetTurns(6);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void resetTurns(int i)
    {
        turnsRemaining = i;
        turnsText.text = "" + turnsRemaining;
    }
}
