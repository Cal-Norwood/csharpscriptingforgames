using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomHandler : MonoBehaviour
{
    public ProceduralManager PM;
    public List<bool> roomActivated;

    private void Start()
    {
        
    }

    private void Update()
    {
        if(roomActivated[0] == true)
        {
            PM.StartCoroutine(PM.RoomTwo());
        }

        if(roomActivated[1] == true)
        {
            PM.StartCoroutine(PM.RoomThree());
        }
    }
}
