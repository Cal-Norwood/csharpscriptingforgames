using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NextFloorLoad : MonoBehaviour
{
    public ProceduralManager PM;
    public Animator CM;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Delay());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            CM.Play("FadeToBlack");
            PM.currentFloor += 1;
            StartCoroutine(WaitForFade());
        }
    }

    private IEnumerator Delay()
    {
        yield return new WaitForSeconds(10);
        CM = GameObject.Find("Virtual Camera").GetComponent<Animator>();
        PM = GameObject.Find("ProceduralManager").GetComponent<ProceduralManager>();
    }

    private IEnumerator WaitForFade()
    {
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene("dungeon");
    }
}
