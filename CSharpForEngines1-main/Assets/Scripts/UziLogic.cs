using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UziLogic : MonoBehaviour
{
    public GameObject barrelPos;
    public GameObject bullet;
    public float fireRate;
    public List<GameObject> spawnedBullets;
    public bool readyToFire = true;
    public float bulletSpeed;
    public int uziDamage;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKey(KeyCode.Mouse0) && readyToFire == true)
        {
            readyToFire = false;
            StartCoroutine(FireRateHandler());
            Fire(barrelPos.transform);
        }
    }

    void Fire(Transform spawnPos)
    {
        if(gameObject.transform.localScale.y > 0)
        {
            spawnedBullets.Add(Instantiate(bullet, new Vector3(spawnPos.position.x, spawnPos.position.y, spawnPos.position.z), Quaternion.Euler(0, 0, 180)));
            spawnedBullets[spawnedBullets.Count - 1].transform.right = -spawnPos.right;
            spawnedBullets[spawnedBullets.Count - 1].GetComponent<Rigidbody2D>().velocity = transform.right * bulletSpeed;
            spawnedBullets[spawnedBullets.Count - 1].GetComponent<BulletHandler>().damage = uziDamage;
        }
        else
        {
            spawnedBullets.Add(Instantiate(bullet, new Vector3(spawnPos.position.x, spawnPos.position.y, spawnPos.position.z), Quaternion.identity));
            spawnedBullets[spawnedBullets.Count - 1].transform.right = -spawnPos.right;
            spawnedBullets[spawnedBullets.Count - 1].GetComponent<Rigidbody2D>().velocity = transform.right * bulletSpeed;
            spawnedBullets[spawnedBullets.Count - 1].GetComponent<BulletHandler>().damage = uziDamage;
        }
    }

    private IEnumerator FireRateHandler()
    {
        yield return new WaitForSeconds(fireRate * 0.1f);
        readyToFire = true;
    }
}
