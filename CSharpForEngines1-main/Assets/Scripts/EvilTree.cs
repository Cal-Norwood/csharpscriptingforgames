using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UIElements;
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
    public bool abilityPlaying = false;
    public Collider2D wallCollider;
    public Animator anim;
    public GameObject Coin;
    public Material mat;
    public float dissolveAmount = 1;
    public bool isDissolving;
    public bool deathOnce = false;

    public NavMeshAgent navAgent;
    // Start is called before the first frame update
    void Start()
    {
        mat.SetFloat("_DissolveAmount", dissolveAmount);
        EH = GameObject.Find("DungeonSpawns").GetComponent<EnemyHandler>();
        if (EH.treeActive == false)
        {
            EH.isInstantiated = true;
        }
        player = GameObject.Find("Player");
        PM = GameObject.Find("ProceduralManager").GetComponent<ProceduralManager>();
        navAgent = gameObject.GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        // If the enemy is not using its ability allow it to move
        Debug.Log(deathOnce);
        if(abilityPlaying == false)
        {
            NavMove();
        }
        else
        {
            anim.enabled = false;
        }
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        // if the enemy touches the player deal damage
        if (abilityPlaying == false && collision.tag == "Player")
        {
            player.BroadcastMessage("DealDamage", 15);
        }
    }

    public void TakeDamage(int damage)
    {
        // make the enemy invunrable when its ability is playing
        if(abilityPlaying == false)
        {
            treeHealth -= damage;
        }

        // when the enemys health is reduced to below 0 destroy the enmy and instantiate coin pickups to drop as loot from the corpse as well as playing the dissolve effect
        if (treeHealth <= 0)
        {
            if(deathOnce == false)
            {
                EH.ResetTreeAbility();
                gameObject.GetComponent<SpriteRenderer>().material = mat;
                deathOnce = true;
                isDissolving = true;
                EH.StartCoroutine(EH.Dissolve(gameObject));
                EH.enemyCount -= 1;
                int randomNum = Random.Range(0, 4);
                for (int i = 0; i < randomNum; i++)
                {
                    GameObject spawnedCoin;
                    spawnedCoin = Instantiate(Coin, gameObject.transform.position + Coin.transform.position, quaternion.identity);
                    spawnedCoin.GetComponent<Rigidbody2D>().AddRelativeForce(Random.onUnitSphere * 1500);
                }
            }
        }
    }

    void NavMove()
    {
        // if the enemy is on a floor greater than once increase its speed
        if ((PM.currentFloor == 1))
        {
            navAgent.speed = 5;
        }
        anim.Play("TreeWalk");
        navAgent.SetDestination(player.transform.position);
    }
}

//Change ass movement to procedural

