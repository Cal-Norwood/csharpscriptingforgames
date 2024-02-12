using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CoinHandler : MonoBehaviour
{

    public int currentCoins = 0;
    public int maxCoins = 999;
    public TextMeshProUGUI coinCounterDisplay;
    public SaveVariables SV;
    // Start is called before the first frame update
    void Start()
    {
        currentCoins = SV.coinAmount;
    }

    // Update is called once per frame
    void Update()
    {
        coinCounterDisplay.text = currentCoins.ToString();
    }
}
