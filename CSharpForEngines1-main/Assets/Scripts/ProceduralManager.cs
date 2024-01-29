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
    public Vector2 activeRoomSize = new Vector2(12, 12);
    public bool roomsSpawned = false;
    public Vector2 previousRoom = new Vector2(0, 0);
    public GameObject[] doors;
    public GameObject[] doorsOpen;

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
        for (int i = 0; i < roomCount; i++)
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

            for (int a = 0; a < 3; a++)
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

        for (int a = 0; a < 3; a++)
        {
            for (int b = 0; b < 3; b++)
            {
                if (roomMatrix[a][b] >= 0)
                {
                    if (roomMatrix[a][b] == 0)
                    {
                        Instantiate(mainRoom, new Vector3(roomPos[a][b].x, roomPos[a][b].y, 0), Quaternion.identity);
                        player.transform.Translate(roomPos[a][b].x, roomPos[a][b].y, 0);
                        previousRoom.x = a;
                        previousRoom.y = b;
                        Instantiate(walls[1], new Vector3(roomPos[a][b].x, roomPos[a][b].y, 0) + walls[1].transform.position, walls[1].transform.rotation);
                        Instantiate(walls[2], new Vector3(roomPos[a][b].x, roomPos[a][b].y, 0) + walls[2].transform.position, walls[2].transform.rotation);
                        Instantiate(walls[3], new Vector3(roomPos[a][b].x, roomPos[a][b].y, 0) + walls[3].transform.position, walls[3].transform.rotation);
                        Instantiate(walls[0], new Vector3(roomPos[a][b].x, roomPos[a][b].y, 0) + walls[0].transform.position, walls[0].transform.rotation);
                    }

                    mapNodeGrid[a][b].color = Color.white;
                }
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (roomsSpawned == false)
        {
            StartCoroutine(SpawnSequance());
        }
    }

    private IEnumerator SpawnSequance()
    {
        roomsSpawned = true;
        for (int i = 1; i < roomCount + 1; i++)
        {
            for (int a = 0; a < 3; a++)
            {
                for (int b = 0; b < 3; b++)
                {
                    if (roomMatrix[a][b] == i)
                    {
                        yield return new WaitForSeconds(0.5f);
                        Instantiate(mainRoom, new Vector3(roomPos[a][b].x, roomPos[a][b].y, 0), Quaternion.identity);
                        if (a > previousRoom.x)
                        {
                            int x = (int)previousRoom.x;
                            int y = (int)previousRoom.y;
                            Instantiate(corridors[0], new Vector3(roomPos[a][b].x, roomPos[a][b].y, 0) + corridors[0].transform.position, corridors[0].transform.rotation);
                            Instantiate(doors[1], new Vector3(roomPos[x][y].x, roomPos[x][y].y, 0), Quaternion.identity);
                            Instantiate(doorsOpen[1], new Vector3(roomPos[x][y].x, roomPos[x][y].y, 0), Quaternion.identity);
                            Instantiate(walls[1], new Vector3(roomPos[a][b].x, roomPos[a][b].y, 0) + walls[1].transform.position, walls[1].transform.rotation);
                            Instantiate(walls[2], new Vector3(roomPos[a][b].x, roomPos[a][b].y, 0) + walls[2].transform.position, walls[2].transform.rotation);
                            Instantiate(walls[3], new Vector3(roomPos[a][b].x, roomPos[a][b].y, 0) + walls[3].transform.position, walls[3].transform.rotation);
                            if (roomMatrix[a][b] == 1)
                            {
                                Instantiate(walls[0], new Vector3(roomPos[x][y].x, roomPos[x][y].y, 0), walls[0].transform.rotation);
                                Instantiate(walls[2], new Vector3(roomPos[a][b].x, roomPos[x][y].y, 0), walls[2].transform.rotation);
                                Instantiate(walls[3], new Vector3(roomPos[a][b].x, roomPos[x][y].y, 0), walls[3].transform.rotation);
                            }
                        }
                        if (a < previousRoom.x)
                        {
                            int x = (int)previousRoom.x;
                            int y = (int)previousRoom.y;
                            Instantiate(corridors[1], new Vector3(roomPos[a][b].x, roomPos[a][b].y, 0) + corridors[1].transform.position, corridors[1].transform.rotation);
                            Instantiate(doors[0], new Vector3(roomPos[x][y].x, roomPos[x][y].y, 0), Quaternion.identity);
                            Instantiate(doorsOpen[0], new Vector3(roomPos[x][y].x, roomPos[x][y].y, 0), Quaternion.identity);
                            Instantiate(walls[0], new Vector3(roomPos[a][b].x, roomPos[a][b].y, 0) + walls[0].transform.position, walls[0].transform.rotation);
                            Instantiate(walls[2], new Vector3(roomPos[a][b].x, roomPos[a][b].y, 0) + walls[2].transform.position, walls[2].transform.rotation);
                            Instantiate(walls[3], new Vector3(roomPos[a][b].x, roomPos[a][b].y, 0) + walls[3].transform.position, walls[3].transform.rotation);
                            if (roomMatrix[a][b] == 1)
                            {
                                Instantiate(walls[1], new Vector3(roomPos[x][y].x, roomPos[x][y].y, 0), walls[1].transform.rotation);
                                Instantiate(walls[2], new Vector3(roomPos[a][b].x, roomPos[x][y].y, 0), walls[2].transform.rotation);
                                Instantiate(walls[3], new Vector3(roomPos[a][b].x, roomPos[x][y].y, 0), walls[3].transform.rotation);
                            }
                        }
                        if (b > previousRoom.y)
                        {
                            int x = (int)previousRoom.x;
                            int y = (int)previousRoom.y;
                            Instantiate(corridors[2], new Vector3(roomPos[a][b].x, roomPos[a][b].y, 0) + corridors[2].transform.position, corridors[2].transform.rotation);
                            Instantiate(doors[3], new Vector3(roomPos[x][y].x, roomPos[x][y].y, 0), Quaternion.identity);
                            Instantiate(doorsOpen[3], new Vector3(roomPos[x][y].x, roomPos[x][y].y, 0), Quaternion.identity);
                            Instantiate(walls[1], new Vector3(roomPos[a][b].x, roomPos[a][b].y, 0) + walls[1].transform.position, walls[1].transform.rotation);
                            Instantiate(walls[0], new Vector3(roomPos[a][b].x, roomPos[a][b].y, 0) + walls[0].transform.position, walls[0].transform.rotation);
                            Instantiate(walls[3], new Vector3(roomPos[a][b].x, roomPos[a][b].y, 0) + walls[2].transform.position, walls[3].transform.rotation);
                            if (roomMatrix[a][b] == 1)
                            {
                                Instantiate(walls[0], new Vector3(roomPos[x][y].x, roomPos[x][y].y, 0), walls[0].transform.rotation);
                                Instantiate(walls[2], new Vector3(roomPos[a][b].x, roomPos[x][y].y, 0), walls[2].transform.rotation);
                                Instantiate(walls[1], new Vector3(roomPos[a][b].x, roomPos[x][y].y, 0), walls[1].transform.rotation);

                            }
                        }
                        if (b < previousRoom.y)
                        {
                            int x = (int)previousRoom.x;
                            int y = (int)previousRoom.y;
                            Instantiate(corridors[3], new Vector3(roomPos[a][b].x, roomPos[a][b].y, 0) + corridors[3].transform.position, corridors[3].transform.rotation);
                            Instantiate(doors[2], new Vector3(roomPos[x][y].x, roomPos[x][y].y, 0), Quaternion.identity);
                            Instantiate(doorsOpen[2], new Vector3(roomPos[x][y].x, roomPos[x][y].y, 0), Quaternion.identity);
                            Instantiate(walls[1], new Vector3(roomPos[a][b].x, roomPos[a][b].y, 0) + walls[1].transform.position, walls[1].transform.rotation);
                            Instantiate(walls[2], new Vector3(roomPos[a][b].x, roomPos[a][b].y, 0) + walls[2].transform.position, walls[3].transform.rotation);
                            Instantiate(walls[0], new Vector3(roomPos[a][b].x, roomPos[a][b].y, 0) + walls[0].transform.position, walls[0].transform.rotation);
                            if (roomMatrix[a][b] == 1)
                            {
                                Instantiate(walls[0], new Vector3(roomPos[x][y].x, roomPos[x][y].y, 0), walls[0].transform.rotation);
                                Instantiate(walls[2], new Vector3(roomPos[a][b].x, roomPos[x][y].y, 0), walls[2].transform.rotation);
                                Instantiate(walls[3], new Vector3(roomPos[a][b].x, roomPos[x][y].y, 0), walls[3].transform.rotation);
                            }
                        }
                        previousRoom.x = a;
                        previousRoom.y = b;

                    }

                }
            }
        }
    }
}


//TODO get walls working for first room
