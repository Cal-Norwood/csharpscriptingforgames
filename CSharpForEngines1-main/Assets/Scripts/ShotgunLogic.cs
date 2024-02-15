using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotgunLogic : MonoBehaviour
{
    public GameObject[] barrelPos;
    public GameObject bullet;
    public float fireRate;
    public List<GameObject> spawnedBullets;
    public bool readyToFire = true;
    public float bulletSpeed;
    public int shotgunDamage;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Mouse0) && readyToFire == true)
        {
            readyToFire = false;
            StartCoroutine(FireRateHandler());
            Fire(barrelPos);
        }
    }

    private IEnumerator FireRateHandler()
    {
        yield return new WaitForSeconds(fireRate * 0.1f);
        readyToFire = true;
    }

    void Fire(GameObject[] spawnPos)
    {
        if (gameObject.transform.localScale.y > 0)
        {
            spawnedBullets.Add(Instantiate(bullet, new Vector3(spawnPos[0].transform.position.x, spawnPos[0].transform.position.y, spawnPos[0].transform.position.z), Quaternion.Euler(0, 0, 180)));
            spawnedBullets[spawnedBullets.Count - 1].transform.right = -spawnPos[0].transform.right;
            spawnedBullets[spawnedBullets.Count - 1].GetComponent<Rigidbody2D>().velocity = transform.right * bulletSpeed;
            spawnedBullets[spawnedBullets.Count - 1].GetComponent<BulletHandler>().damage = shotgunDamage;

            spawnedBullets.Add(Instantiate(bullet, new Vector3(spawnPos[1].transform.position.x, spawnPos[1].transform.position.y, spawnPos[1].transform.position.z), Quaternion.Euler(0, 0, 180)));
            spawnedBullets[spawnedBullets.Count - 1].transform.right = spawnPos[1].transform.right;
            spawnedBullets[spawnedBullets.Count - 1].GetComponent<Rigidbody2D>().velocity = spawnedBullets[spawnedBullets.Count - 1].transform.right * bulletSpeed;
            spawnedBullets[spawnedBullets.Count - 1].GetComponent<BulletHandler>().damage = shotgunDamage;

            spawnedBullets.Add(Instantiate(bullet, new Vector3(spawnPos[2].transform.position.x, spawnPos[2].transform.position.y, spawnPos[2].transform.position.z), Quaternion.Euler(0, 0, 180)));
            spawnedBullets[spawnedBullets.Count - 1].transform.right = spawnPos[2].transform.right;
            spawnedBullets[spawnedBullets.Count - 1].GetComponent<Rigidbody2D>().velocity = spawnedBullets[spawnedBullets.Count - 1].transform.right * bulletSpeed;
            spawnedBullets[spawnedBullets.Count - 1].GetComponent<BulletHandler>().damage = shotgunDamage;
        }
        else
        {
            spawnedBullets.Add(Instantiate(bullet, new Vector3(spawnPos[0].transform.position.x, spawnPos[0].transform.position.y, spawnPos[0].transform.position.z), Quaternion.identity));
            spawnedBullets[spawnedBullets.Count - 1].transform.right = -spawnPos[0].transform.right;
            spawnedBullets[spawnedBullets.Count - 1].GetComponent<Rigidbody2D>().velocity = transform.right * bulletSpeed;
            spawnedBullets[spawnedBullets.Count - 1].GetComponent<BulletHandler>().damage = shotgunDamage;

            spawnedBullets.Add(Instantiate(bullet, new Vector3(spawnPos[1].transform.position.x, spawnPos[1].transform.position.y, spawnPos[1].transform.position.z), Quaternion.Euler(0, 0, 180)));
            spawnedBullets[spawnedBullets.Count - 1].transform.right = spawnPos[1].transform.right;
            spawnedBullets[spawnedBullets.Count - 1].GetComponent<Rigidbody2D>().velocity = spawnedBullets[spawnedBullets.Count - 1].transform.right * bulletSpeed;
            spawnedBullets[spawnedBullets.Count - 1].GetComponent<BulletHandler>().damage = shotgunDamage;

            spawnedBullets.Add(Instantiate(bullet, new Vector3(spawnPos[2].transform.position.x, spawnPos[2].transform.position.y, spawnPos[2].transform.position.z), Quaternion.Euler(0, 0, 180)));
            spawnedBullets[spawnedBullets.Count - 1].transform.right = spawnPos[2].transform.right;
            spawnedBullets[spawnedBullets.Count - 1].GetComponent<Rigidbody2D>().velocity = spawnedBullets[spawnedBullets.Count - 1].transform.right * bulletSpeed;
            spawnedBullets[spawnedBullets.Count - 1].GetComponent<BulletHandler>().damage = shotgunDamage;
        }
    }
}
