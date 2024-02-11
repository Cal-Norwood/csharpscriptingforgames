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

    public NavMeshAgent navAgent;
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
        navAgent = gameObject.GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
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
            EH.enemyCount -= 1;
            Destroy(gameObject);
        }
    }

    void NavMove()
    {
        anim.Play("TreeWalk");
        navAgent.SetDestination(player.transform.position);
    }
}

//Change ass movement to procedural

