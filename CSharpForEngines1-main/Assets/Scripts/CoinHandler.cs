using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CoinHandler : MonoBehaviour
{

    public int currentCoins = 0;
    public int maxCoins = 999;
    public TextMeshProUGUI coinCounterDisplay;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        coinCounterDisplay.text = currentCoins.ToString();
    }
}
