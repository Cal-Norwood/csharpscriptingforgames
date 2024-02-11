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
        if(roomActivated[0] == true && PM.roomCount != 2)
        {
            PM.StartCoroutine(PM.RoomTwo());
        }

        if(roomActivated[1] == true && PM.roomCount != 3)
        {
            PM.StartCoroutine(PM.RoomThree());
        }

        if (roomActivated[2] == true && PM.roomCount != 4)
        {
            PM.StartCoroutine(PM.RoomFour());
        }

        if (roomActivated[3] == true && PM.roomCount != 5)
        {
            PM.StartCoroutine(PM.RoomFive());
        }
    }
}
