using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LevelDisplayHandler : MonoBehaviour
{
    public TextMeshProUGUI[] levelDisplays;
    public int currentLevel = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(currentLevel == 0)
        {
            levelDisplays[0].enabled = true;
        }
        if (currentLevel == 1)
        {
            levelDisplays[1].enabled = true;
        }
        if (currentLevel == 2)
        {
            levelDisplays[2].enabled = true;
        }
        if (currentLevel == 3)
        {
            levelDisplays[3].enabled = true;
        }
        if (currentLevel == 4)
        {
            levelDisplays[4].enabled = true;
        }
    }
}
