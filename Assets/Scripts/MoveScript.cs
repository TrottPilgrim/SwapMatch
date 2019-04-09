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
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
