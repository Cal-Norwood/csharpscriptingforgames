using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class TimelineStart : MonoBehaviour
{
    public PlayableDirector director;
    public ParticleSystem Ps;
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
        yield return new WaitForSeconds(2.51f);
        director.Stop();
    }
}
