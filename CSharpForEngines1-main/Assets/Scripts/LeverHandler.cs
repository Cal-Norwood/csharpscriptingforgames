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

    // Update is called once per frame
    void Update()
    {
        // if the player presses e while the lever is primed start the cutscene and change the look of the lever
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
