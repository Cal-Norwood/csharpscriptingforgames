using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProceduralManager : MonoBehaviour
{

    public int roomCount;
    // 1 = top, 2 = bot, 3 = left, 4 = right
    public GameObject[] corridors;
    public GameObject[] walls;
    public int horizontalChoice;
    public int verticalChoice;
    public int upCheck;
    public int downCheck;
    public int leftCheck;
    public int rightCheck;

    // 1 = room, c = corridor, -1 = empty
    public List<List<int>> roomMatrix = new List<List<int>>
    {
        new List<int> {-1, -1, -1},
        new List<int> {-1, -1, -1},
        new List<int> {-1, -1, -1}
    };

    public List<List<bool>> roomsVisited = new List<List<bool>>
    {
        new List<bool> {false, false, false},
        new List<bool> {false, false, false},
        new List<bool> {false, false, false}
    };

    public List<List<bool>> roomsValid = new List<List<bool>>
    {
        new List<bool> {false, false, false},
        new List<bool> {false, false, false},
        new List<bool> {false, false, false}
    };

    // Start is called before the first frame update
    void Start()
    {
        roomCount = Random.Range(3, 7);
        for(int i = 0; i < roomCount; i++)
        {
            horizontalChoice = Random.Range(0, 3);
            verticalChoice = Random.Range(0, 3);
            if (i > 0)
            {
                while (true)
                {
                    if (roomsValid[verticalChoice][horizontalChoice] == true && roomsVisited[verticalChoice][horizontalChoice] == false)
                    {
                        break;
                    }
                    else
                    {
                        horizontalChoice = Random.Range(0, 3);
                        verticalChoice = Random.Range(0, 3);
                    }
                }
            }

            upCheck = verticalChoice - 1;
            downCheck = verticalChoice + 1;
            leftCheck = horizontalChoice - 1;
            rightCheck = horizontalChoice + 1;

            for(int a = 0; a < 3; a++)
            {
                for (int b = 0; b < 3; b++)
                {
                    roomsValid[a][b] = false;
                }
            }

            if (upCheck > -1)
            {
                roomsValid[upCheck][horizontalChoice] = true;
            }
            if (downCheck < 3)
            {
                roomsValid[downCheck][horizontalChoice] = true;
            }
            if (leftCheck > -1)
            {
                roomsValid[verticalChoice][leftCheck] = true;
            }
            if (rightCheck < 3)
            {
                roomsValid[verticalChoice][rightCheck] = true;
            }

            roomsVisited[verticalChoice][horizontalChoice] = true;
            roomMatrix[verticalChoice][horizontalChoice] = i;
        }
        Debug.Log(roomMatrix[0][0]);
        Debug.Log(roomMatrix[0][1]);
        Debug.Log(roomMatrix[0][2]);
        Debug.Log(roomMatrix[1][0]);
        Debug.Log(roomMatrix[1][1]);
        Debug.Log(roomMatrix[1][2]);
        Debug.Log(roomMatrix[2][0]);
        Debug.Log(roomMatrix[2][1]);
        Debug.Log(roomMatrix[2][2]);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void SpawnSequance()
    {
        
    }
}
