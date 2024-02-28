using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class StoneImp : MonoBehaviour
{
    public float ImpHealth = 100;
    public GameObject player;
    public Animator anim;
    public NavMeshAgent navAgent;
    public Vector3 randomNavSphere;
    public bool setDestination = false;
    public ProceduralManager PM;
    public EnemyHandler EH;
    public bool killOnce = false;

    private void Awake()
    {
        navAgent.updateRotation = false;
        navAgent.updateUpAxis = false;
    }

    // Start is called before the first frame update
    void Start()
    {
        EH = GameObject.Find("DungeonSpawns").GetComponent<EnemyHandler>();
        PM = GameObject.Find("ProceduralManager").GetComponent<ProceduralManager>();
        player = GameObject.Find("Player");
        navAgent = gameObject.GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        NavMove();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // deal damage to the player if it collides with it
        if (collision.tag == "Player")
        {
            player.BroadcastMessage("DealDamage", 10);
        }
    }

    public void TakeDamage(int damage)
    {
        // if the imp takes damge remove some of its health
        ImpHealth -= damage;

        // if the imp has no health left kill it
        if (ImpHealth <= 0 && killOnce == false)
        {
            Debug.Log("enemydown");
            EH.enemyCount -= 1;
            killOnce = true;
            Destroy(gameObject);
        }
    }

    void NavMove()
    {
        // start the moving animation the start the movement coroutine
        anim.Play("ImpFly");
        if(setDestination == false)
        {
            setDestination = true;
            StartCoroutine(RandomMovement());
        }
    }

    // pick a random point in the current room then after a set duration set that to their destination and move the player
    private IEnumerator RandomMovement()
    {
        randomNavSphere = PM.currentRoom.transform.position + Random.insideUnitSphere * 10;
        navAgent.SetDestination(randomNavSphere);
        yield return new WaitForSeconds(2);
        navAgent.SetDestination(gameObject.transform.position);
        yield return new WaitForSeconds(0.5f);
        setDestination = false;
    }
}
