using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TavernLoad : MonoBehaviour
{
    public Animator CM;
    public CoinHandler CH;
    public PlayerWeaponHandler PWH;
    public SaveVariables SV;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Delay());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private IEnumerator Delay()
    {
        yield return new WaitForSeconds(10);
        CM = GameObject.Find("Virtual Camera").GetComponent<Animator>();
        CH = GameObject.Find("Player").GetComponent<CoinHandler>();
        PWH = GameObject.Find("Player").GetComponent<PlayerWeaponHandler>();
    }

    private IEnumerator WaitForFade()
    {
        // load the tavern scene
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene("sample");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // if the player comes into context with the entrance fade the camera to black then assign the correct variables to the variable saver then start the function that opens the next scene
        if (collision.tag == "Player")
        {
            CM.Play("FadeToBlack");
            SV.currentWeapon = PWH.currentWeapon;
            SV.coinAmount = CH.currentCoins;
            StartCoroutine(WaitForFade());
        }
    }
}
