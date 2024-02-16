using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class StartScreen : MonoBehaviour
{
    public bool enterPressed = false;
    public Canvas boss;
    public TextMeshProUGUI continueText;
    public Canvas soundPanel;

    // Start is called before the first frame update
    void Start()
    {
        soundPanel.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Return))
        {
            enterPressed = true;
        }

        if(enterPressed == true)
        {
            continueText.enabled = false;
            boss.enabled = false;
            soundPanel.enabled = true;
        }
    }

    void StartGame()
    {
        SceneManager.LoadScene("sample");
    }
}
