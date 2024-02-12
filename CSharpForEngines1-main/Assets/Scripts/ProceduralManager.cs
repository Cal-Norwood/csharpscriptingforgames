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
    public Transform[] dungeonSpawns;
    public EnemyHandler EH;
    public bool roomSpawn = true;
    public List<GameObject> spawnRoomWalls;
    public List<BoxCollider2D> roomDetection;
    public Vector3 wallOffset = new Vector3(0, 0, -1);
    public List<GameObject> dungeonWalls;
    public RoomHandler RH;
    public GameObject lastDungeonFloorRoom;
    public int currentFloor;
    public SaveVariables SV;

    public GameObject[] enemyDiff1;

    public List<GameObject> activeDoors;
    public List<GameObject> activeRooms;

    public GameObject currentRoom;

    public List<bool> roomReady;

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
        currentFloor = SV.currentFloor;
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
                        activeRooms.Add(Instantiate(mainRoom, new Vector3(roomPos[a][b].x, roomPos[a][b].y, 0), Quaternion.identity));
                        player.transform.Translate(roomPos[a][b].x, roomPos[a][b].y, 0);
                        previousRoom.x = a;
                        previousRoom.y = b;
                        spawnRoomWalls.Add(Instantiate(walls[0], new Vector3(roomPos[a][b].x, roomPos[a][b].y, 0) + walls[0].transform.position, walls[0].transform.rotation));
                        spawnRoomWalls.Add(Instantiate(walls[1], new Vector3(roomPos[a][b].x, roomPos[a][b].y, 0) + walls[1].transform.position, walls[1].transform.rotation));
                        spawnRoomWalls.Add(Instantiate(walls[2], new Vector3(roomPos[a][b].x, roomPos[a][b].y, 0) + walls[2].transform.position, walls[2].transform.rotation));
                        spawnRoomWalls.Add(Instantiate(walls[3], new Vector3(roomPos[a][b].x, roomPos[a][b].y, 0) + walls[3].transform.position, walls[3].transform.rotation));
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

        if(roomReady[0] == true)
        {
            StartCoroutine(RoomOne());
        }
    }

    private IEnumerator SpawnSequance()
    {
        roomsSpawned = true;
        int previousCorridorIndex = 0;
        for (int i = 1; i < roomCount + 1; i++)
        {
            for (int a = 0; a < 3; a++)
            {
                for (int b = 0; b < 3; b++)
                {
                    if (roomMatrix[a][b] == i)
                    {
                        yield return new WaitForSeconds(0.5f);
                        if (roomMatrix[a][b] != roomCount)
                        {
                            activeRooms.Add(Instantiate(mainRoom, new Vector3(roomPos[a][b].x, roomPos[a][b].y, 0), Quaternion.identity));
                        }

                        if (a > previousRoom.x)
                        {
                            int x = (int)previousRoom.x;
                            int y = (int)previousRoom.y;

                            if(roomMatrix[a][b] >= 2)
                            {
                                if(previousCorridorIndex == 0)
                                {
                                    Instantiate(walls[3], new Vector3(roomPos[x][y].x, roomPos[x][y].y, 0) + walls[3].transform.position + wallOffset, Quaternion.identity);
                                    Instantiate(walls[2], new Vector3(roomPos[x][y].x, roomPos[x][y].y, 0) + walls[3].transform.position + wallOffset, Quaternion.identity);
                                }

                                if (previousCorridorIndex == 2)
                                {
                                    Instantiate(walls[3], new Vector3(roomPos[x][y].x, roomPos[x][y].y, 0) + walls[3].transform.position + wallOffset, Quaternion.identity);
                                    Instantiate(walls[0], new Vector3(roomPos[x][y].x, roomPos[x][y].y, 0) + walls[3].transform.position + wallOffset, Quaternion.identity);
                                }

                                if (previousCorridorIndex == 3)
                                {
                                    Instantiate(walls[0], new Vector3(roomPos[x][y].x, roomPos[x][y].y, 0) + walls[3].transform.position + wallOffset, Quaternion.identity);
                                    Instantiate(walls[2], new Vector3(roomPos[x][y].x, roomPos[x][y].y, 0) + walls[3].transform.position + wallOffset, Quaternion.identity);
                                }
                            }

                            Instantiate(corridors[0], new Vector3(roomPos[a][b].x, roomPos[a][b].y, 0) + corridors[0].transform.position, corridors[0].transform.rotation);
                            previousCorridorIndex = 0;
                            activeDoors.Add(Instantiate(doors[1], new Vector3(roomPos[x][y].x, roomPos[x][y].y, 0), Quaternion.identity));
                            Instantiate(doorsOpen[1], new Vector3(roomPos[x][y].x, roomPos[x][y].y, 0), Quaternion.identity);
                            if (roomMatrix[a][b] == 1)
                            {
                                Destroy(spawnRoomWalls[1]);
                            }

                            if(roomMatrix[a][b] == roomCount - 1)
                            {
                                Instantiate(walls[3], new Vector3(roomPos[a][b].x, roomPos[a][b].y, 0) + walls[3].transform.position + wallOffset, Quaternion.identity);
                                Instantiate(walls[2], new Vector3(roomPos[a][b].x, roomPos[a][b].y, 0) + walls[3].transform.position + wallOffset, Quaternion.identity);
                                Instantiate(walls[1], new Vector3(roomPos[a][b].x, roomPos[a][b].y, 0) + walls[3].transform.position + wallOffset, Quaternion.identity);
                                Instantiate(lastDungeonFloorRoom, new Vector3(roomPos[a][b].x, roomPos[a][b].y, 0) + corridors[0].transform.position, corridors[0].transform.rotation);
                            }   
                        }
                        if (a < previousRoom.x)
                        {
                            int x = (int)previousRoom.x;
                            int y = (int)previousRoom.y;

                            if (roomMatrix[a][b] >= 2)
                            {
                                if (previousCorridorIndex == 1)
                                {
                                    Instantiate(walls[3], new Vector3(roomPos[x][y].x, roomPos[x][y].y, 0) + walls[3].transform.position + wallOffset, Quaternion.identity);
                                    Instantiate(walls[2], new Vector3(roomPos[x][y].x, roomPos[x][y].y, 0) + walls[3].transform.position + wallOffset, Quaternion.identity);
                                }

                                if (previousCorridorIndex == 2)
                                {
                                    Instantiate(walls[3], new Vector3(roomPos[x][y].x, roomPos[x][y].y, 0) + walls[3].transform.position + wallOffset, Quaternion.identity);
                                    Instantiate(walls[1], new Vector3(roomPos[x][y].x, roomPos[x][y].y, 0) + walls[3].transform.position + wallOffset, Quaternion.identity);
                                }

                                if (previousCorridorIndex == 3)
                                {
                                    Instantiate(walls[1], new Vector3(roomPos[x][y].x, roomPos[x][y].y, 0) + walls[3].transform.position + wallOffset, Quaternion.identity);
                                    Instantiate(walls[2], new Vector3(roomPos[x][y].x, roomPos[x][y].y, 0) + walls[3].transform.position + wallOffset, Quaternion.identity);
                                }
                            }

                            Instantiate(corridors[1], new Vector3(roomPos[a][b].x, roomPos[a][b].y, 0) + corridors[1].transform.position, corridors[1].transform.rotation);
                            previousCorridorIndex = 1;
                            activeDoors.Add(Instantiate(doors[0], new Vector3(roomPos[x][y].x, roomPos[x][y].y, 0), Quaternion.identity));
                            Instantiate(doorsOpen[0], new Vector3(roomPos[x][y].x, roomPos[x][y].y, 0), Quaternion.identity);

                            if (roomMatrix[a][b] == 1)
                            {
                                Destroy(spawnRoomWalls[0]);
                            }

                            if (roomMatrix[a][b] == roomCount - 1)
                            {
                                Instantiate(walls[3], new Vector3(roomPos[a][b].x, roomPos[a][b].y, 0) + walls[3].transform.position + wallOffset, Quaternion.identity);
                                Instantiate(walls[2], new Vector3(roomPos[a][b].x, roomPos[a][b].y, 0) + walls[3].transform.position + wallOffset, Quaternion.identity);
                                Instantiate(walls[0], new Vector3(roomPos[a][b].x, roomPos[a][b].y, 0) + walls[3].transform.position + wallOffset, Quaternion.identity);
                                Instantiate(lastDungeonFloorRoom, new Vector3(roomPos[a][b].x, roomPos[a][b].y, 0) + corridors[0].transform.position, corridors[0].transform.rotation);
                            }
                        }
                        if (b > previousRoom.y)
                        {
                            int x = (int)previousRoom.x;
                            int y = (int)previousRoom.y;

                            if (roomMatrix[a][b] >= 2)
                            {
                                if (previousCorridorIndex == 0)
                                {
                                    Instantiate(walls[1], new Vector3(roomPos[x][y].x, roomPos[x][y].y, 0) + walls[3].transform.position + wallOffset, Quaternion.identity);
                                    Instantiate(walls[2], new Vector3(roomPos[x][y].x, roomPos[x][y].y, 0) + walls[3].transform.position + wallOffset, Quaternion.identity);
                                }

                                if (previousCorridorIndex == 2)
                                {
                                    Instantiate(walls[1], new Vector3(roomPos[x][y].x, roomPos[x][y].y, 0) + walls[3].transform.position + wallOffset, Quaternion.identity);
                                    Instantiate(walls[0], new Vector3(roomPos[x][y].x, roomPos[x][y].y, 0) + walls[3].transform.position + wallOffset, Quaternion.identity);
                                }

                                if (previousCorridorIndex == 1)
                                {
                                    Instantiate(walls[0], new Vector3(roomPos[x][y].x, roomPos[x][y].y, 0) + walls[3].transform.position + wallOffset, Quaternion.identity);
                                    Instantiate(walls[2], new Vector3(roomPos[x][y].x, roomPos[x][y].y, 0) + walls[3].transform.position + wallOffset, Quaternion.identity);
                                }
                            }

                            Instantiate(corridors[2], new Vector3(roomPos[a][b].x, roomPos[a][b].y, 0) + corridors[2].transform.position, corridors[2].transform.rotation);
                            previousCorridorIndex = 2;
                            activeDoors.Add(Instantiate(doors[3], new Vector3(roomPos[x][y].x, roomPos[x][y].y, 0), Quaternion.identity));
                            Instantiate(doorsOpen[3], new Vector3(roomPos[x][y].x, roomPos[x][y].y, 0), Quaternion.identity);

                            if (roomMatrix[a][b] == 1)
                            {
                                Destroy(spawnRoomWalls[3]);
                            }

                            if (roomMatrix[a][b] == roomCount - 1)
                            {
                                Instantiate(walls[3], new Vector3(roomPos[a][b].x, roomPos[a][b].y, 0) + walls[3].transform.position + wallOffset, Quaternion.identity);
                                Instantiate(walls[0], new Vector3(roomPos[a][b].x, roomPos[a][b].y, 0) + walls[3].transform.position + wallOffset, Quaternion.identity);
                                Instantiate(walls[1], new Vector3(roomPos[a][b].x, roomPos[a][b].y, 0) + walls[3].transform.position + wallOffset, Quaternion.identity);
                                Instantiate(lastDungeonFloorRoom, new Vector3(roomPos[a][b].x, roomPos[a][b].y, 0) + corridors[0].transform.position, corridors[0].transform.rotation);
                            }
                        }
                        if (b < previousRoom.y)
                        {
                            int x = (int)previousRoom.x;
                            int y = (int)previousRoom.y;

                            if (roomMatrix[a][b] >= 2)
                            {
                                if (previousCorridorIndex == 0)
                                {
                                    Instantiate(walls[3], new Vector3(roomPos[x][y].x, roomPos[x][y].y, 0) + walls[3].transform.position + wallOffset, Quaternion.identity);
                                    Instantiate(walls[1], new Vector3(roomPos[x][y].x, roomPos[x][y].y, 0) + walls[3].transform.position + wallOffset, Quaternion.identity);
                                }

                                if (previousCorridorIndex == 1)
                                {
                                    Instantiate(walls[3], new Vector3(roomPos[x][y].x, roomPos[x][y].y, 0) + walls[3].transform.position + wallOffset, Quaternion.identity);
                                    Instantiate(walls[0], new Vector3(roomPos[x][y].x, roomPos[x][y].y, 0) + walls[3].transform.position + wallOffset, Quaternion.identity);
                                }

                                if (previousCorridorIndex == 3)
                                {
                                    Instantiate(walls[0], new Vector3(roomPos[x][y].x, roomPos[x][y].y, 0) + walls[3].transform.position + wallOffset, Quaternion.identity);
                                    Instantiate(walls[1], new Vector3(roomPos[x][y].x, roomPos[x][y].y, 0) + walls[3].transform.position + wallOffset, Quaternion.identity);
                                }
                            }

                            Instantiate(corridors[3], new Vector3(roomPos[a][b].x, roomPos[a][b].y, 0) + corridors[3].transform.position, corridors[3].transform.rotation);
                            previousCorridorIndex = 3;
                            activeDoors.Add(Instantiate(doors[2], new Vector3(roomPos[x][y].x, roomPos[x][y].y, 0), Quaternion.identity));
                            Instantiate(doorsOpen[2], new Vector3(roomPos[x][y].x, roomPos[x][y].y, 0), Quaternion.identity);

                            if (roomMatrix[a][b] == 1)
                            {
                                Destroy(spawnRoomWalls[2]);
                            }

                            if (roomMatrix[a][b] == roomCount - 1)
                            {
                                Instantiate(walls[0], new Vector3(roomPos[a][b].x, roomPos[a][b].y, 0) + walls[3].transform.position + wallOffset, Quaternion.identity);
                                Instantiate(walls[2], new Vector3(roomPos[a][b].x, roomPos[a][b].y, 0) + walls[3].transform.position + wallOffset, Quaternion.identity);
                                Instantiate(walls[1], new Vector3(roomPos[a][b].x, roomPos[a][b].y, 0) + walls[3].transform.position + wallOffset, Quaternion.identity);
                                Instantiate(lastDungeonFloorRoom, new Vector3(roomPos[a][b].x, roomPos[a][b].y, 0) + corridors[0].transform.position, corridors[0].transform.rotation);
                            }
                        }
                        previousRoom.x = a;
                        previousRoom.y = b;

                    }

                }
            }
        }

        currentRoom = activeRooms[0];

        yield return new WaitForSeconds(4f);
        roomReady.Add(true);

        foreach(GameObject g in activeRooms)
        {
            roomDetection.Add(g.GetComponent<BoxCollider2D>());
        }
    }

    private IEnumerator RoomOne()
    {
        if(roomSpawn == true)
        {
            EH.spawnedEnemies.Add(Instantiate(enemyDiff1[0], dungeonSpawns[Random.Range(0, 9)].transform.position + activeRooms[0].transform.position + enemyDiff1[0].transform.position, Quaternion.identity));
            EH.enemyCount += 1;
            roomSpawn = false;
        }

        if (EH.enemyCount == 0)
        {
            EH.ResetTreeAbility();
            currentRoom = activeRooms[1];
            roomReady[0] = false;
            yield return new WaitForSeconds(1);
            activeDoors[0].SetActive(false);
            roomReady.Add(false);
            roomSpawn = true;
        }
    }

    public IEnumerator RoomTwo()
    {
        if(roomSpawn == true)
        {
            EH.enemyCount += 1;
            roomSpawn = false;
            yield return new WaitForSeconds(0.5f);
            dungeonWalls.Add(Instantiate(walls[0], currentRoom.transform.position + walls[0].transform.position + wallOffset, Quaternion.identity));
            dungeonWalls.Add(Instantiate(walls[1], currentRoom.transform.position + walls[1].transform.position + wallOffset, Quaternion.identity));
            dungeonWalls.Add(Instantiate(walls[2], currentRoom.transform.position + walls[2].transform.position + wallOffset, Quaternion.identity));
            dungeonWalls.Add(Instantiate(walls[3], currentRoom.transform.position + walls[3].transform.position + wallOffset, Quaternion.identity));
            yield return new WaitForSeconds(1);
            EH.spawnedEnemies.Add(Instantiate(enemyDiff1[0], dungeonSpawns[Random.Range(0, 9)].transform.position + activeRooms[1].transform.position + enemyDiff1[0].transform.position, Quaternion.identity));
        }

        if (EH.enemyCount == 0)
        {
            yield return new WaitForSeconds(0.5f);
            Destroy(dungeonWalls[3]);
            Destroy(dungeonWalls[2]);
            Destroy(dungeonWalls[1]);
            Destroy(dungeonWalls[0]);
            dungeonWalls.Clear();
            activeDoors[1].SetActive(false);
            roomReady.Add(false);
            currentRoom = activeRooms[2];
            RH.roomActivated[0] = false;
            roomSpawn = true;
        }
    }

    public IEnumerator RoomThree()
    {
        if (roomSpawn == true)
        {
            EH.enemyCount += 1;
            roomSpawn = false;
            yield return new WaitForSeconds(0.5f);
            dungeonWalls.Add(Instantiate(walls[0], currentRoom.transform.position + walls[0].transform.position + wallOffset, Quaternion.identity));
            dungeonWalls.Add(Instantiate(walls[1], currentRoom.transform.position + walls[1].transform.position + wallOffset, Quaternion.identity));
            dungeonWalls.Add(Instantiate(walls[2], currentRoom.transform.position + walls[2].transform.position + wallOffset, Quaternion.identity));
            dungeonWalls.Add(Instantiate(walls[3], currentRoom.transform.position + walls[3].transform.position + wallOffset, Quaternion.identity));
            yield return new WaitForSeconds(1);
            EH.spawnedEnemies.Add(Instantiate(enemyDiff1[0], dungeonSpawns[Random.Range(0, 9)].transform.position + activeRooms[2].transform.position + enemyDiff1[0].transform.position, Quaternion.identity));
        }

        if (EH.enemyCount == 0)
        {
            yield return new WaitForSeconds(0.5f);
            Destroy(dungeonWalls[3]);
            Destroy(dungeonWalls[2]);
            Destroy(dungeonWalls[1]);
            Destroy(dungeonWalls[0]);
            dungeonWalls.Clear();
            currentRoom = activeRooms[3];
            activeDoors[2].SetActive(false);
            roomReady.Add(false);
            roomSpawn = true;
            RH.roomActivated[1] = false;
        }
    }

    public IEnumerator RoomFour()
    {
        if (roomSpawn == true)
        {
            EH.enemyCount += 1;
            roomSpawn = false;
            yield return new WaitForSeconds(0.5f);
            dungeonWalls.Add(Instantiate(walls[0], currentRoom.transform.position + walls[0].transform.position + wallOffset, Quaternion.identity));
            dungeonWalls.Add(Instantiate(walls[1], currentRoom.transform.position + walls[1].transform.position + wallOffset, Quaternion.identity));
            dungeonWalls.Add(Instantiate(walls[2], currentRoom.transform.position + walls[2].transform.position + wallOffset, Quaternion.identity));
            dungeonWalls.Add(Instantiate(walls[3], currentRoom.transform.position + walls[3].transform.position + wallOffset, Quaternion.identity));
            yield return new WaitForSeconds(1);
            EH.spawnedEnemies.Add(Instantiate(enemyDiff1[0], dungeonSpawns[Random.Range(0, 9)].transform.position + activeRooms[3].transform.position + enemyDiff1[0].transform.position, Quaternion.identity));
        }

        if (EH.enemyCount == 0)
        {
            yield return new WaitForSeconds(0.5f);
            Destroy(dungeonWalls[3]);
            Destroy(dungeonWalls[2]);
            Destroy(dungeonWalls[1]);
            Destroy(dungeonWalls[0]);
            dungeonWalls.Clear();
            currentRoom = activeRooms[4];
            activeDoors[3].SetActive(false);
            roomReady.Add(false);
            roomSpawn = true;
            RH.roomActivated[2] = false;
        }
    }

    public IEnumerator RoomFive()
    {
        if (roomSpawn == true)
        {
            EH.enemyCount += 1;
            roomSpawn = false;
            yield return new WaitForSeconds(0.5f);
            dungeonWalls.Add(Instantiate(walls[0], currentRoom.transform.position + walls[0].transform.position + wallOffset, Quaternion.identity));
            dungeonWalls.Add(Instantiate(walls[1], currentRoom.transform.position + walls[1].transform.position + wallOffset, Quaternion.identity));
            dungeonWalls.Add(Instantiate(walls[2], currentRoom.transform.position + walls[2].transform.position + wallOffset, Quaternion.identity));
            dungeonWalls.Add(Instantiate(walls[3], currentRoom.transform.position + walls[3].transform.position + wallOffset, Quaternion.identity));
            yield return new WaitForSeconds(1);
            EH.spawnedEnemies.Add(Instantiate(enemyDiff1[0], dungeonSpawns[Random.Range(0, 9)].transform.position + activeRooms[4].transform.position + enemyDiff1[0].transform.position, Quaternion.identity));
        }

        if (EH.enemyCount == 0)
        {
            yield return new WaitForSeconds(0.5f);
            Destroy(dungeonWalls[3]);
            Destroy(dungeonWalls[2]);
            Destroy(dungeonWalls[1]);
            Destroy(dungeonWalls[0]);
            dungeonWalls.Clear();
            activeDoors[4].SetActive(false);
            roomReady.Add(false);
            roomSpawn = true;
            RH.roomActivated[3] = false;
        }
    }
}