using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class LightSwitch : MonoBehaviour
{
    public Light2D L;
    private bool hasActivated = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (hasActivated == false)
        {
            StartCoroutine(SwitchTime());
            hasActivated = true;
        }
    }

    IEnumerator SwitchTime()
    {
        // set the light intensity from low to high over a period of time to give a set of lights turning on effect
        yield return new WaitForSeconds(0.5f);

        for(float i = 0; i < 1.7; i += 0.1f)
        {
            L.intensity = i;
            yield return new WaitForSeconds(0.25f);
        }
    }
}
