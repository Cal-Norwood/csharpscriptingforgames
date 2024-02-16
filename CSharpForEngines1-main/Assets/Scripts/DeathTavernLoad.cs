using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class DeathTavernLoad : MonoBehaviour
{
    public SaveVariables SV;
    public TextMeshProUGUI coinAmount;
    public float numberOfCoins;
    public bool coinsDepleted = false;

    // Start is called before the first frame update
    void Start()
    {
        numberOfCoins = SV.coinAmount;
        coinAmount.text = numberOfCoins.ToString();
        StartCoroutine(Delay());
    }

    // Update is called once per frame
    void Update()
    {
        coinAmount.text = numberOfCoins.ToString();
        if (Input.GetKeyDown(KeyCode.Return) && coinsDepleted == true)
        {
            SV.coinAmount = (int)numberOfCoins;
            SceneManager.LoadScene("sample");
        }
    }

    private IEnumerator Delay()
    {
        yield return new WaitForSeconds(0.4f);
        while(numberOfCoins > 0)
        {
            numberOfCoins -= 1;
            yield return new WaitForSeconds(0.01f);
        }
        coinsDepleted = true;
    }
}
