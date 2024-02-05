using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;

public class EvilTree : MonoBehaviour
{
    public bool splitCooldown = false;
    public GameObject hole;
    public GameObject currentHole;
    public EnemyHandler EH;
    public ProceduralManager PM;
    public GameObject gameObjectPrefab;
    public List<GameObject> instantiatedTrees;
    public List<GameObject> instantiatedHoles;
    public Vector3 instantionOffset = new Vector3(0, 2.5f, 0);
    public GameObject player;
    public float treeHealth = 250f;
    public Vector3 playerPos;
    private bool abilityPlaying = false;
    public bool moveBool = false;
    public bool hitWall = false;
    public Collider2D wallCollider;
    public Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        EH = GameObject.Find("DungeonSpawns").GetComponent<EnemyHandler>();
        if (EH.treeActive == false)
        {
            EH.isInstantiated = true;
        }
        player = GameObject.Find("Player");
        PM = GameObject.Find("ProceduralManager").GetComponent<ProceduralManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (splitCooldown == false && treeHealth <= 150)
        {
            StartCoroutine(Cooldown());
            splitCooldown = true;
        }

        if (moveBool == false && abilityPlaying == false)
        {
            moveBool = true;
            StartCoroutine(TreeDash());
        }
    }

    private IEnumerator Cooldown()
    {
        moveBool = true;
        abilityPlaying = true;
        yield return new WaitForSeconds(0.5f);
        currentHole = Instantiate(hole, gameObject.transform.position + hole.transform.position, Quaternion.identity);
        yield return new WaitForSeconds(0.5f);
        float t = 0f;
        Transform treePos = gameObject.transform;
        Transform holePos = currentHole.transform;
        while (true)
        {
            t += Time.deltaTime;
            gameObject.transform.position = new Vector3(Mathf.Lerp(treePos.position.x, holePos.position.x, t / 0.5f), Mathf.Lerp(treePos.position.y, holePos.position.y + 2, t / 0.5f), gameObject.transform.position.z);
            if(t >= 0.4)
            {
                break;
            }
            yield return null;
        }
        t = 0f;
        treePos = gameObject.transform;
        holePos = currentHole.transform;

        while (true)
        {
            t += Time.deltaTime;
            gameObject.transform.position = new Vector3(treePos.position.x, Mathf.Lerp(treePos.position.y, holePos.position.y - 5.45f, t / 0.4f), gameObject.transform.position.z);
            gameObject.transform.localScale = new Vector3(Mathf.Lerp(treePos.localScale.x, 0.6f, t/ 0.4f), Mathf.Lerp(treePos.localScale.y, 0.2f, t / 0.4f));
            yield return null;
            if (t >= 0.4)
            {
                if(EH.treeSpawnCount >= 15)
                {
                    EH.treeSpawnCount -= 1;
                    Destroy(gameObject);
                    break;
                }
                else
                {
                    gameObject.transform.localScale = new Vector3(0, 0, 0);
                    Destroy(currentHole);
                    yield return new WaitForSeconds(0.75f);
                    List<int> randomSpawns = new List<int> { };
                    int randomTry = Random.Range(0, 9);
                    instantiatedHoles.Add(Instantiate(hole, PM.dungeonSpawns[randomTry].transform.position + PM.currentRoom.transform.position + hole.transform.position, Quaternion.identity));
                    instantiatedTrees.Add(Instantiate(gameObjectPrefab, instantiatedHoles[0].transform.position + instantionOffset, quaternion.identity));
                    EH.treeSpawnCount += 1;
                    instantiatedTrees[0].transform.localScale = new Vector3(0, 0, 0);
                    StartCoroutine(DigUpHandler(0, instantiatedTrees[0], instantiatedHoles[0]));
                    randomSpawns.Add(randomTry);

                    yield return new WaitForSeconds(0.2f);
                    while(true)
                    {
                        randomTry = Random.Range(0, 9);

                        if(randomTry == randomSpawns[0])
                        {
                            randomTry = Random.Range(0, 9);
                        }
                        else
                        {
                            randomSpawns.Add(randomTry);
                            instantiatedHoles.Add(Instantiate(hole, PM.dungeonSpawns[randomTry].transform.position + PM.currentRoom.transform.position + hole.transform.position, Quaternion.identity));
                            instantiatedTrees.Add(Instantiate(gameObjectPrefab, instantiatedHoles[1].transform.position + instantionOffset, quaternion.identity));
                            EH.treeSpawnCount += 1;
                            StartCoroutine(DigUpHandler(0, instantiatedTrees[1], instantiatedHoles[1]));
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
                            instantiatedHoles.Add(Instantiate(hole, PM.dungeonSpawns[randomTry].transform.position + PM.currentRoom.transform.position + hole.transform.position, Quaternion.identity));
                            instantiatedTrees.Add(Instantiate(gameObjectPrefab, instantiatedHoles[2].transform.position + instantionOffset, quaternion.identity));
                            EH.treeSpawnCount += 1;
                            StartCoroutine(DigUpHandler(0, instantiatedTrees[2], instantiatedHoles[2]));
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
                            instantiatedHoles.Add(Instantiate(hole, PM.dungeonSpawns[randomTry].transform.position + PM.currentRoom.transform.position + hole.transform.position, Quaternion.identity));
                            instantiatedTrees.Add(Instantiate(gameObjectPrefab, instantiatedHoles[3].transform.position + instantionOffset, quaternion.identity));
                            EH.treeSpawnCount += 1;
                            StartCoroutine(DigUpHandler(0, instantiatedTrees[3], instantiatedHoles[3]));
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
        moveBool = true;
        if (dir == 0)
        {
            yield return new WaitForSeconds(2f);
            float t = 0f;
            Transform treePos = itree.transform;
            Transform holePos = ihole.transform;
            while (true)
            {
                moveBool = true;
                itree.GetComponent<EvilTree>().treeHealth = treeHealth / 4;
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

        foreach(GameObject g in instantiatedTrees)
        {
            g.GetComponent<EvilTree>().moveBool = false;
        }
        anim.enabled = true;
        EH.treeSpawnCount -= 1;
        Destroy(gameObject);
        Destroy(instantiatedHoles[0]);
        Destroy(instantiatedHoles[1]);
        Destroy(instantiatedHoles[2]);
        Destroy(instantiatedHoles[3]);

        splitCooldown = true;

        instantiatedTrees.Clear();
        instantiatedHoles.Clear();
        abilityPlaying = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (abilityPlaying == false && collision.tag == "Player")
        {
            player.BroadcastMessage("DealDamage", 15);
        }
    }

    public void TakeDamage(int damage)
    {
        if(abilityPlaying == false)
        {
            treeHealth -= damage;
        }

        if (treeHealth <= 0)
        {
            EH.treeSpawnCount -= 1;
            Destroy(gameObject);
        }
    }

    private IEnumerator TreeDash()
    {
        anim.Play("TreeWalk");
        yield return new WaitForSeconds(1.5f);
        playerPos = player.transform.position - transform.position;
        gameObject.GetComponent<Rigidbody2D>().velocity = playerPos * 2;

        while (true)
        {
            if(abilityPlaying == true)
            {
                anim.enabled = false;
                gameObject.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
                break;
            }

            if (hitWall == true)
            {
                hitWall = false;
                break;
            }
            yield return null;
        }

        if (abilityPlaying == true)
        {
            gameObject.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            StopCoroutine(TreeDash());
        }

        anim.enabled = false;
        anim.enabled = true;
        yield return new WaitForSeconds(2);
        moveBool = false;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        hitWall = true;
        gameObject.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
    }
}

