using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class LightSwitch : MonoBehaviour
{
    public Light2D L;
    private bool hasActivated = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

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
        yield return new WaitForSeconds(0.5f);

        for(float i = 0; i < 1.7; i += 0.1f)
        {
            L.intensity = i;
            yield return new WaitForSeconds(0.25f);
        }
    }
}
