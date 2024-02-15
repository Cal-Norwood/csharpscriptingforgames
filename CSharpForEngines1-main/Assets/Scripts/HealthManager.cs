using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthManager : MonoBehaviour
{

    public Collider2D hitBox;
    public float Health;
    public SaveVariables SV;
    public bool isInvunrable = false;
    public SpriteRenderer colourchanger;
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
        if(isInvunrable == false)
        {
            isInvunrable = true;
            Health -= damage;
            StartCoroutine(InvunrableTimer());
        }
    }

    private IEnumerator InvunrableTimer()
    {
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
}
