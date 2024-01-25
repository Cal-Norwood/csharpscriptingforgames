using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProceduralManager : MonoBehaviour
{

    public int roomCount;
    // 1 = top, 2 = bot, 3 = left, 4 = right
    public GameObject mainRoom;
    public GameObject player;
    public GameObject[] corridors;
    public GameObject[] walls;
    public Image[] mapNodes;
    public int horizontalChoice;
    public int verticalChoice;
    public int upCheck;
    public int downCheck;
    public int leftCheck;
    public int rightCheck;
    public Vector2 activeRoomSize = new Vector2(12,12);

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

    public List<List<Image>> mapNodeGrid = new List<List<Image>>
    {
        new List<Image> { },
        new List<Image> { },
        new List<Image> { }
    };

    public List<List<Vector2>> roomPos = new List<List<Vector2>>
    {
        new List<Vector2> {new Vector2(-36, 32), new Vector2(0, 32), new Vector2(36, 32)},
        new List<Vector2> {new Vector2(-36, 0), new Vector2(0, 0), new Vector2(36, 0)},
        new List<Vector2> {new Vector2(-36, -32), new Vector2(0, -32), new Vector2(36, -32)}
    };

    // Start is called before the first frame update
    void Start()
    {
        mapNodeGrid[0].Add(mapNodes[0]);
        mapNodeGrid[0].Add(mapNodes[1]);
        mapNodeGrid[0].Add(mapNodes[2]);
        mapNodeGrid[1].Add(mapNodes[3]);
        mapNodeGrid[1].Add(mapNodes[4]);
        mapNodeGrid[1].Add(mapNodes[5]);
        mapNodeGrid[2].Add(mapNodes[6]);
        mapNodeGrid[2].Add(mapNodes[7]);
        mapNodeGrid[2].Add(mapNodes[8]);

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

        for(int a = 0; a < 3; a++)
        {
            for(int b = 0; b < 3; b++)
            {
                if (roomMatrix[a][b] >= 0)
                {
                    if(roomMatrix[a][b] == 0)
                    {
                        Instantiate(mainRoom, new Vector3(roomPos[a][b].x, roomPos[a][b].y, 0), Quaternion.identity);
                        player.transform.Translate(roomPos[a][b].x, roomPos[a][b].y, 0);
                    }

                    mapNodeGrid[a][b].color = Color.white;
                }
            }
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
