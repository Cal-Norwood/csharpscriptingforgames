using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHandler : MonoBehaviour
{
    public bool treeActive = false;
    public bool isInstantiated = false;
    public int treeSpawnCount = 0;
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
    }

    private IEnumerator TreeHandler()
    {
        while(treeSpawnCount > 0)
        {
            yield return null;
        }
    }
}
