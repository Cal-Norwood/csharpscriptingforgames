using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LevelDisplayHandler : MonoBehaviour
{
    public TextMeshProUGUI[] levelDisplays;
    public int currentLevel;
    public SaveVariables SV;
    public Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        // get current level from the variable saver
        currentLevel = SV.currentFloor;
        anim.Play("levelDisplay");
    }

    // Update is called once per frame
    void Update()
    {
        // depending on what the current level is disable and enable the correct corresponding level UI displays
        if(currentLevel == 0)
        {
            levelDisplays[0].enabled = true;
            levelDisplays[1].enabled = false;
            levelDisplays[2].enabled = false;
            levelDisplays[3].enabled = false;
            levelDisplays[4].enabled = false;
        }
        if (currentLevel == 1)
        {
            levelDisplays[1].enabled = true;
            levelDisplays[0].enabled = false;
            levelDisplays[2].enabled = false;
            levelDisplays[3].enabled = false;
            levelDisplays[4].enabled = false;
        }
        if (currentLevel == 2)
        {
            levelDisplays[2].enabled = true;
            levelDisplays[1].enabled = false;
            levelDisplays[0].enabled = false;
            levelDisplays[3].enabled = false;
            levelDisplays[4].enabled = false;
        }
        if (currentLevel == 3)
        {
            levelDisplays[3].enabled = true;
            levelDisplays[1].enabled = false;
            levelDisplays[2].enabled = false;
            levelDisplays[0].enabled = false;
            levelDisplays[4].enabled = false;
        }
        if (currentLevel == 4)
        {
            levelDisplays[4].enabled = true;
            levelDisplays[1].enabled = false;
            levelDisplays[2].enabled = false;
            levelDisplays[3].enabled = false;
            levelDisplays[0].enabled = false;
        }
    }
}
