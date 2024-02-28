using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NextFloorLoad : MonoBehaviour
{
    public ProceduralManager PM;
    public HealthManager HM;
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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // when the player walks into the next floor set all relvent variables into the variable saver ready for load and fade to black
        if(collision.tag == "Player")
        {
            CM.Play("FadeToBlack");
            SV.currentWeapon = PWH.currentWeapon;
            SV.Health = HM.Health;
            SV.currentFloor += 1;
            SV.coinAmount = CH.currentCoins;
            StartCoroutine(WaitForFade());
        }
    }

    private IEnumerator Delay()
    {
        yield return new WaitForSeconds(10);
        CM = GameObject.Find("Virtual Camera").GetComponent<Animator>();
        PM = GameObject.Find("ProceduralManager").GetComponent<ProceduralManager>();
        HM = GameObject.Find("Player").GetComponent<HealthManager>();
        CH = GameObject.Find("Player").GetComponent<CoinHandler>();
        PWH = GameObject.Find("Player").GetComponent<PlayerWeaponHandler>();
    }

    private IEnumerator WaitForFade()
    {
        // load the dungeon scene
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene("dungeon");
    }
}
