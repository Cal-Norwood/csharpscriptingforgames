using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveHandler : MonoBehaviour
{
    public HealthManager HM;
    public static float playerHealth;

    public PlayerWeaponHandler PWH;
    public static GameObject currentWeapon;

    public CoinHandler CH;
    public static int currentCoins;

    public ProceduralManager PM;
    public static int currentFloor;

    public LevelDisplayHandler LDH;
    public static int currentLevel;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Delay());
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(currentFloor);
        playerHealth = HM.Health;
        currentWeapon = PWH.currentWeapon;
        currentCoins = CH.currentCoins;
        currentFloor = PM.currentFloor;
        currentFloor = LDH.currentLevel;
    }

    private IEnumerator Delay()
    {
        yield return new WaitForSeconds(0.5f);
        Debug.Log("loadedcorrectly");
        HM.Health = playerHealth;
        PWH.currentWeapon = currentWeapon;
        CH.currentCoins = currentCoins;
        PM.currentFloor = currentFloor;
        LDH.currentLevel = currentLevel;
    }

    //[RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.SubsystemRegistration)]

    //turn domain reloading off
}
