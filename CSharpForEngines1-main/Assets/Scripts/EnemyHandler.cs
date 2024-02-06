using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyHandler : MonoBehaviour
{
    public bool treeActive = false;
    public bool isInstantiated = false;
    public int treeSpawnCount = 0;
    public EvilTree ET;
    public ProceduralManager PM;
    public List<GameObject> spawnedEnemies;

    public GameObject gameObjectPrefab;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (treeActive == false && isInstantiated == true)
        {
            StartCoroutine(TreeHandler());
            treeActive = true;
        }

        if (spawnedEnemies[0])
        {
            ET = spawnedEnemies[0].GetComponent<EvilTree>();
        }

        if (ET.splitCooldown == false && ET.treeHealth <= 150)
        {
            ET.anim.enabled = false;
            StartCoroutine(Cooldown());
            ET.splitCooldown = true;
        }
    }

    private IEnumerator TreeHandler()
    {
        while(treeSpawnCount > 0)
        {
            yield return null;
        }
    }

    private IEnumerator Cooldown()
    {
        ET.abilityPlaying = true;
        yield return new WaitForSeconds(0.5f);
        ET.currentHole = Instantiate(ET.hole, gameObject.transform.position + ET.hole.transform.position, Quaternion.identity);
        yield return new WaitForSeconds(0.5f);
        float t = 0f;
        Transform treePos = spawnedEnemies[0].transform;
        Transform holePos = ET.currentHole.transform;

        while (true)
        {
            t += Time.deltaTime;
            spawnedEnemies[0].transform.position = new Vector3(Mathf.Lerp(treePos.position.x, holePos.position.x, t / 0.5f), Mathf.Lerp(treePos.position.y, holePos.position.y + 2, t / 0.5f), spawnedEnemies[0].transform.position.z);
            if (t >= 0.4)
            {
                break;
            }
            yield return null;
        }
        t = 0f;
        treePos = gameObject.transform;
        holePos = ET.currentHole.transform;

        while (true)
        {
            t += Time.deltaTime;
            spawnedEnemies[0].transform.position = new Vector3(treePos.position.x, Mathf.Lerp(treePos.position.y, holePos.position.y - 5.45f, t / 0.4f), spawnedEnemies[0].transform.position.z);
            spawnedEnemies[0].transform.localScale = new Vector3(Mathf.Lerp(treePos.localScale.x, 0.6f, t / 0.4f), Mathf.Lerp(treePos.localScale.y, 0.2f, t / 0.4f));
            yield return null;
            if (t >= 0.4)
            {
                if (treeSpawnCount >= 15)
                {
                    treeSpawnCount -= 1;
                    Destroy(spawnedEnemies[0]);
                    break;
                }
                else
                {
                    spawnedEnemies[0].transform.localScale = new Vector3(0, 0, 0);
                    Destroy(ET.currentHole);
                    yield return new WaitForSeconds(0.75f);
                    List<int> randomSpawns = new List<int> { };
                    int randomTry = Random.Range(0, 9);
                    ET.instantiatedHoles.Add(Instantiate(ET.hole, PM.dungeonSpawns[randomTry].transform.position + PM.currentRoom.transform.position + ET.hole.transform.position, Quaternion.identity));
                    ET.instantiatedTrees.Add(Instantiate(gameObjectPrefab, ET.instantiatedHoles[0].transform.position + ET.instantionOffset, Quaternion.identity));
                    treeSpawnCount += 1;
                    ET.instantiatedTrees[0].transform.localScale = new Vector3(0, 0, 0);
                    StartCoroutine(DigUpHandler(0, ET.instantiatedTrees[0], ET.instantiatedHoles[0]));
                    randomSpawns.Add(randomTry);

                    yield return new WaitForSeconds(0.2f);
                    while (true)
                    {
                        randomTry = Random.Range(0, 9);

                        if (randomTry == randomSpawns[0])
                        {
                            randomTry = Random.Range(0, 9);
                        }
                        else
                        {
                            randomSpawns.Add(randomTry);
                            ET.instantiatedHoles.Add(Instantiate(ET.hole, PM.dungeonSpawns[randomTry].transform.position + PM.currentRoom.transform.position + ET.hole.transform.position, Quaternion.identity));
                            ET.instantiatedTrees.Add(Instantiate(gameObjectPrefab, ET.instantiatedHoles[1].transform.position + ET.instantionOffset, Quaternion.identity));
                            treeSpawnCount += 1;
                            StartCoroutine(DigUpHandler(0, ET.instantiatedTrees[1], ET.instantiatedHoles[1]));
                            break;
                        }
                    }

                    yield return new WaitForSeconds(0.2f);

                    while (true)
                    {
                        randomTry = Random.Range(0, 9);

                        if (randomTry == randomSpawns[0] || randomTry == randomSpawns[1])
                        {
                            randomTry = Random.Range(0, 9);
                        }
                        else
                        {
                            randomSpawns.Add(randomTry);
                            ET.instantiatedHoles.Add(Instantiate(ET.hole, PM.dungeonSpawns[randomTry].transform.position + PM.currentRoom.transform.position + ET.hole.transform.position, Quaternion.identity));
                            ET.instantiatedTrees.Add(Instantiate(gameObjectPrefab, ET.instantiatedHoles[2].transform.position + ET.instantionOffset, Quaternion.identity));
                            treeSpawnCount += 1;
                            StartCoroutine(DigUpHandler(0, ET.instantiatedTrees[2], ET.instantiatedHoles[2]));
                            break;
                        }
                    }

                    yield return new WaitForSeconds(0.2f);

                    while (true)
                    {
                        randomTry = Random.Range(0, 9);

                        if (randomTry == randomSpawns[0] || randomTry == randomSpawns[1] || randomTry == randomSpawns[2])
                        {
                            randomTry = Random.Range(0, 9);
                        }
                        else
                        {
                            randomSpawns.Add(randomTry);
                            ET.instantiatedHoles.Add(Instantiate(ET.hole, PM.dungeonSpawns[randomTry].transform.position + PM.currentRoom.transform.position + ET.hole.transform.position, Quaternion.identity));
                            ET.instantiatedTrees.Add(Instantiate(gameObjectPrefab, ET.instantiatedHoles[3].transform.position + ET.instantionOffset, Quaternion.identity));
                            treeSpawnCount += 1;
                            StartCoroutine(DigUpHandler(0, ET.instantiatedTrees[3], ET.instantiatedHoles[3]));
                            break;
                        }
                    }
                    break;
                }

            }
        }
    }

    private IEnumerator DigUpHandler(int dir, GameObject itree, GameObject ihole)
    {
        if (dir == 0)
        {
            yield return new WaitForSeconds(2f);
            float t = 0f;
            Transform treePos = itree.transform;
            Transform holePos = ihole.transform;
            while (true)
            {
                itree.GetComponent<EvilTree>().treeHealth = ET.treeHealth / 4;
                t += Time.deltaTime;
                itree.transform.position = new Vector3(itree.transform.position.x, Mathf.Lerp(treePos.position.y, holePos.position.y + 2, t / 10f), itree.transform.position.z);
                itree.transform.position = new Vector3(Mathf.Lerp(treePos.position.x, holePos.position.x - 2, t / 10f), itree.transform.position.y, itree.transform.position.z);
                itree.transform.localScale = new Vector3(Mathf.Lerp(treePos.localScale.x, 0.75f, t / 0.4f), Mathf.Lerp(treePos.localScale.y, 0.75f, t / 0.4f));
                yield return null;
                if (t >= 3)
                {
                    break;
                }
            }
        }
        else
        {

        }

        ET.anim.enabled = true;
        treeSpawnCount -= 1;

        ET.splitCooldown = true;

        ET.instantiatedTrees.Clear();
        ET.instantiatedHoles.Clear();
        ET.abilityPlaying = false;


        Destroy(ET.instantiatedHoles[0]);
        Destroy(ET.instantiatedHoles[1]);
        Destroy(ET.instantiatedHoles[2]);
        Destroy(ET.instantiatedHoles[3]);
        Destroy(spawnedEnemies[0]);
    }

}
