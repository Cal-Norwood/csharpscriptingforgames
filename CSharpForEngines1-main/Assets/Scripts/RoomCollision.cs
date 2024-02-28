using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomCollision : MonoBehaviour
{
    public BoxCollider2D bc;
    public RoomHandler RH;
    // Start is called before the first frame update
    void Start()
    {
        RH = GameObject.Find("RoomHandler").GetComponent<RoomHandler>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //if the player has collided with a new room set that room active ready for enemy spawns in the procedural manager
        if(collision.tag == "Player")
        {
            if(RH.PM.roomReady[0] == false)
            bc.enabled = false;
            RH.roomActivated.Add(true);
        }
    }
}
