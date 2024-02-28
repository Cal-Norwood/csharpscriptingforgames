using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HealthManager : MonoBehaviour
{

    public Collider2D hitBox;
    public float Health;
    public SaveVariables SV;
    public bool isInvunrable = false;
    public SpriteRenderer colourchanger;
    public CoinHandler CH;
    public Animator CM;

    // Start is called before the first frame update
    void Start()
    {
        // conserve health over floors
        Health = SV.Health;
    }

    // Update is called once per frame
    void Update()
    {
        // if health is less than 0 save the players coin amount and fade the camera to black
        if(Health <= 0)
        {
            CM.Play("FadeToBlack");
            StartCoroutine(WaitForFade());
            SV.coinAmount = CH.currentCoins;
        }
    }

    public void DealDamage(int damage)
    {
        // if tyhe player is not in their invunrability timer deal damge to them when they take damage then start the invunrable timer
        if(isInvunrable == false)
        {
            isInvunrable = true;
            Health -= damage;
            StartCoroutine(InvunrableTimer());
        }
    }

    private IEnumerator InvunrableTimer()
    {
        // make the player flash grey when they are invunrable
        colourchanger.color = new Color(1f, 0f, 0f, 1f);
        yield return new WaitForSeconds(0.2f);
        colourchanger.color = new Color(0.5f, 0.5f, 0.5f, 0.3f);
        yield return new WaitForSeconds(0.2f);
        colourchanger.color = new Color(1f, 1f, 1f, 1f);
        yield return new WaitForSeconds(0.2f);
        colourchanger.color = new Color(0.5f, 0.5f, 0.5f, 0.3f);
        yield return new WaitForSeconds(0.2f);
        colourchanger.color = new Color(1f, 1f, 1f, 1f);
        isInvunrable = false;
    }

    private IEnumerator WaitForFade()
    {
        // load death screen on death
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene("StatScreen");
    }
}
