using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class SpawnCutsceneScriptCompanion : MonoBehaviour
{
    public CinemachineVirtualCamera VC;
    public GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Delay());
    }

    // Update is called once per frame
    void Update()
    {

    }

    private IEnumerator Delay()
    {
        // when the player is first spawned into the dungeon start a cutscene to show the player what the dungeon map looks like in a cool way
        yield return new WaitForSeconds(4f);
        Vector3 deltaPos = player.transform.position - VC.transform.position;
        float timePassed = 0;
        while (timePassed < 1f)
        {
            timePassed += Time.deltaTime;
            VC.transform.position += deltaPos * Time.deltaTime;
            VC.transform.position = new Vector3(VC.transform.position.x, VC.transform.position.y, -35);
            yield return null;
        }
        VC.transform.position = player.transform.position;
        VC.Follow = player.transform;
    }
}
