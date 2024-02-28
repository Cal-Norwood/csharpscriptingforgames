using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotgunUpgrade : MonoBehaviour
{
    public GameObject shotgunPrefab;
    public CoinHandler CH;
    public bool hasPickedUp = false;
    public PlayerWeaponHandler PWH;

    public GameObject panelDisplay;
    public GameObject coinDisplay;
    public GameObject purchasedDisplay;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(PWH.currentWeapon == shotgunPrefab)
        {
            hasPickedUp = true;
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        // if the player is in range and they press e and have the correct coins allow the player to pickup the shotgun and store that in the save variables script
        if(collision.tag == "Player")
        {
            if (Input.GetKey(KeyCode.E))
            {
                if (CH.currentCoins >= 50 && hasPickedUp == false)
                {
                    hasPickedUp = true;
                    coinDisplay.SetActive(false);
                    purchasedDisplay.SetActive(true);
                    CH.currentCoins -= 50;
                    Destroy(PWH.weaponInPlay);
                    PWH.currentWeapon = shotgunPrefab;
                    PWH.WeaponSetup();
                }
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // if the player is in range display the correct UI
        if (collision.tag == "Player")
        {
            panelDisplay.SetActive(true);
            if (hasPickedUp == false)
            {
                coinDisplay.SetActive(true);
            }
            else
            {
                purchasedDisplay.SetActive(true);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            panelDisplay.SetActive(false);
            coinDisplay.SetActive(false);
            purchasedDisplay.SetActive(false);
        }
    }
}
