using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{

    public HealthManager HM;
    public Image healthBar;
    public bool delayBool = false;

    public Sprite[] healthStages;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (HM.Health > 75)
        {
            healthBar.sprite = healthStages[0];
        }
        else if (HM.Health > 50 && HM.Health <= 75)
        {
            healthBar.sprite = healthStages[1];
        }
        else if (HM.Health > 25 && HM.Health <= 50)
        {
            healthBar.sprite = healthStages[2];
        }
        else if (HM.Health > 0 && HM.Health <= 25 && delayBool == false)
        {
            StartCoroutine(LowHealthFlash());
        }
        else if(HM.Health <= 0)
        {
            healthBar.sprite = healthStages[4];
        }
    }

    private IEnumerator LowHealthFlash()
    {
        delayBool = true;
        healthBar.sprite = healthStages[3];
        while (HM.Health > 0)
        {
            yield return new WaitForSeconds(0.5f);
            healthBar.sprite = healthStages[4];
            yield return new WaitForSeconds(0.5f);
            healthBar.sprite = healthStages[3];
        }

        delayBool = false;
    }
}
