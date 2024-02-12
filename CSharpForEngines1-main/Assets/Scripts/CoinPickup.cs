using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinPickup : MonoBehaviour
{
    public CoinHandler CH;
    // Start is called before the first frame update
    void Start()
    {
        CH = GameObject.Find("Player").GetComponent<CoinHandler>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            CH.currentCoins += 5;
            Destroy(gameObject);
        }
    }
}
