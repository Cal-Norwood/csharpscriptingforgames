using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSwitcher : MonoBehaviour
{
    public string Scene;
    public Animator CM;
    public SaveVariables SV;
    public PlayerWeaponHandler PWH;
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
        if(collision.transform.gameObject.name == "Player")
        {
            CM.Play("FadeToBlack");
            StartCoroutine(WaitForFade());
        }
    }

    IEnumerator WaitForFade()
    {
        //save current weapon and load a new scene
        SV.currentWeapon = PWH.currentWeapon;
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene(Scene);
    }
}
