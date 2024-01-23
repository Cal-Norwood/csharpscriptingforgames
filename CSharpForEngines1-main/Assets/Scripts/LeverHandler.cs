using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeverHandler : MonoBehaviour
{
    public GameObject leverUnswitched;
    public GameObject leverSwitched;
    public GameObject timelineManager;

    public bool hasActivated = false;
    public bool leverPrimed = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && hasActivated == false && leverPrimed == true)
        {
            timelineManager.BroadcastMessage("StartTimeline");
            hasActivated = true;
            leverSwitched.transform.position = new Vector3(0, 0, -5);
            leverUnswitched.transform.position = new Vector3(0, 0, 1);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        leverPrimed = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        leverPrimed = false;
    }
}
