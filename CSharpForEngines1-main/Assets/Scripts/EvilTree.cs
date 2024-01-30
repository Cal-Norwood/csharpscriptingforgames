using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EvilTree : MonoBehaviour
{
    public bool splitCooldown = false;
    public GameObject hole;
    public GameObject currentHole;
    public EnemyHandler EH;
    // Start is called before the first frame update
    void Start()
    {
        EH = GameObject.Find("DungeonSpawns").GetComponent<EnemyHandler>();
        if (EH.treeActive == false)
        {
            EH.isInstantiated = true;
            EH.treeSpawnCount =+ 1;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (splitCooldown == false)
        {
            StartCoroutine(Cooldown());
            splitCooldown = true;
        }
    }

    private IEnumerator Cooldown()
    {
        yield return new WaitForSeconds(6f);
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
                EH.treeSpawnCount -= 1;
                Destroy(gameObject);
                break;
            }
        }
    }
}
