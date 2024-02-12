using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthManager : MonoBehaviour
{

    public Collider2D hitBox;
    public float Health;
    public SaveVariables SV;
    // Start is called before the first frame update
    void Start()
    {
        Health = SV.Health;
    }

    // Update is called once per frame
    void Update()
    {
        if(Health <= 0)
        {
            Debug.Log("Dead");
        }
    }

    public void DealDamage(int damage)
    {
        Health -= damage;
    }
}
