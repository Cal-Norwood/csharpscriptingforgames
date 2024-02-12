using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SaveVariables", menuName = "Persistence")]
public class SaveVariables : ScriptableObject
{
    public float Health;
    public int coinAmount;
    public GameObject currentWeapon;
    public int currentFloor;
    private void OnEnable()
    {
        Health = 100;
        coinAmount = 0;
        currentFloor = 0;
    }
}