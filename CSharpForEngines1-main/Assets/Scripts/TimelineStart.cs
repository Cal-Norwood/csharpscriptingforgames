using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using Cinemachine;

public class TimelineStart : MonoBehaviour
{
    public PlayableDirector director;
    public ParticleSystem Ps;
    public GameObject SceneEnd;
    public CinemachineVirtualCamera CM;
    public GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartTimeline()
    {
        director.Play();
        StartCoroutine(StopTimeline());
    }

    IEnumerator StopTimeline()
    {
        yield return new WaitForSeconds(6.5f);
        Ps.Play();
        yield return new WaitForSeconds(2.3f);
        director.Stop();
        SceneEnd.SetActive(true);
        CM.Follow = player.transform;
    }
}
