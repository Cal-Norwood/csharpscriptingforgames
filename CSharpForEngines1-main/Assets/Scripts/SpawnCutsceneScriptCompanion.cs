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
        yield return new WaitForSeconds(3.9f);
        VC.Follow = player.transform;
    }
}
