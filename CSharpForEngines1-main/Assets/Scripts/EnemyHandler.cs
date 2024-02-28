using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyHandler : MonoBehaviour
{
    public bool treeActive = false;
    public bool isInstantiated = false;
    public int enemyCount = 0;
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
        if (spawnedEnemies[0])
        {
            ET = spawnedEnemies[0].GetComponent<EvilTree>();
        }

        // If the evil tree enemy's health is below half play its signiture ability
        if (ET.splitCooldown == false && ET.treeHealth <= 150)
        {
            ET.anim.enabled = false;
            ET.splitCooldown = true;
            StartCoroutine(Cooldown());
        }
    }

    private IEnumerator Cooldown()
    {
        // Instantiating the hole for the enemy to jump into for its ability then assigning the corresponding variables ready to be manipulated
        ET.abilityPlaying = true;
        yield return new WaitForSeconds(0.5f);
        ET.currentHole = Instantiate(ET.hole, spawnedEnemies[0].transform.position + ET.hole.transform.position, Quaternion.identity);
        yield return new WaitForSeconds(0.5f);
        float t = 0f;
        Transform treePos = spawnedEnemies[0].transform;
        Transform holePos = ET.currentHole.transform;

        // makes the enemy jump up in 0.5 seconds
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
        treePos = spawnedEnemies[0].transform;
        holePos = ET.currentHole.transform;

        // makes the player go into the hole then reduce its scale so it appears hidden
        while (true)
        {
            t += Time.deltaTime;
            spawnedEnemies[0].transform.position = new Vector3(treePos.position.x, Mathf.Lerp(treePos.position.y, holePos.position.y - 5.45f, t / 0.4f), spawnedEnemies[0].transform.position.z);
            spawnedEnemies[0].transform.localScale = new Vector3(Mathf.Lerp(treePos.localScale.x, 0.6f, t / 0.4f), Mathf.Lerp(treePos.localScale.y, 0.2f, t / 0.4f));
            yield return null;
            if (t >= 0.4)
            {
                if (enemyCount >= 15)
                {
                    enemyCount -= 1;
                    Destroy(spawnedEnemies[0]);
                    break;
                }
                else
                {
                    spawnedEnemies[0].transform.localScale = new Vector3(0, 0, 0);
                    Destroy(ET.currentHole);
                    yield return new WaitForSeconds(0.75f);
                    List<int> randomSpawns = new List<int> { };
                    int randomTry;

                    yield return new WaitForSeconds(0.2f);
                    // instantiate more holes and then trees with a low scale so they can be manipulated later to appear as if they are jumping out of the tree
                    while (true)
                    {
                        randomTry = Random.Range(0, 9);

                        randomSpawns.Add(randomTry);
                        ET.instantiatedHoles.Add(Instantiate(ET.hole, PM.dungeonSpawns[randomTry].transform.position + PM.currentRoom.transform.position + ET.hole.transform.position, Quaternion.identity));
                        ET.instantiatedTrees.Add(Instantiate(gameObjectPrefab, ET.instantiatedHoles[0].transform.position + ET.instantionOffset, Quaternion.identity));
                        enemyCount += 1;
                        StartCoroutine(DigUpHandler(0, ET.instantiatedTrees[0], ET.instantiatedHoles[0]));
                        break;
                        
                    }

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
                            enemyCount += 1;
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
                            enemyCount += 1;
                            StartCoroutine(DigUpHandler(0, ET.instantiatedTrees[2], ET.instantiatedHoles[2]));
                            break;
                        }
                    }
                    enemyCount -= 1;
                    break;
                }

            }
        }
    }

    private IEnumerator DigUpHandler(int dir, GameObject itree, GameObject ihole)
    {
        // make the spawned in trees have a 3rd of the original trees health to balence the difficulty
        if (dir == 0)
        {
            itree.GetComponent<EvilTree>().treeHealth = ET.treeHealth / 3;
        }

        yield return new WaitForSeconds(2);

        ET.anim.enabled = true;

        ET.instantiatedTrees.Clear();
        ET.abilityPlaying = false;

        Destroy(ET.instantiatedHoles[2]);
        Destroy(ET.instantiatedHoles[1]);
        Destroy(ET.instantiatedHoles[0]);

        ET.instantiatedHoles.Clear();
        Destroy(spawnedEnemies[0]);
    }

    public void ResetTreeAbility()
    {
        ET.treeHealth = 250;
        ET.splitCooldown = false;
        spawnedEnemies.Clear();
    }

    public IEnumerator Dissolve(GameObject tree)
    {
        // when an evil tree is dead create a dissolve effect by modifying the slider in the shader I created to appear like it is dissolving instead of just disappearing 
        while (true)
        {
            ET.dissolveAmount -= (Time.deltaTime * 0.6f);
            ET.mat.SetFloat("_DissolveAmount", ET.dissolveAmount);
            if (ET.dissolveAmount <= 0)
            {
                ET.isDissolving = false;
                break;
            }
            yield return null;
        }
        ET.deathOnce = false;
        ET.mat.SetFloat("_DissolveAmount", 1);
        ET.dissolveAmount = 1;
        Destroy(tree);
    }
}
