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
        yield return null;
    }
}
