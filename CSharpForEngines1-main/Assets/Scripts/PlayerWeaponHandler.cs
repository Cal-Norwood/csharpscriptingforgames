using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeaponHandler : MonoBehaviour
{
    public GameObject currentWeapon;
    public GameObject weaponInPlay;
    public GameObject holderPosFront;
    public GameObject holderPosRight;
    public GameObject holderPosLeft;
    public GameObject holderPosBack;
    public PlayerMovement PM;
    public SaveVariables SV;

    public bool[] lastMove = { true, false, false, false };
    // Start is called before the first frame update
    void Start()
    {
        currentWeapon = SV.currentWeapon;
        WeaponSetup();
    }

    // Update is called once per frame
    void Update()
    {
        if (weaponInPlay)
        {
            // get players velocity and depending what it is put the gun in a different posion so that it looks natural
            if(PM.velocity.x > 0.8)
            {
                lastMove[1] = true;
                lastMove[0] = false;
                lastMove[2] = false;
                lastMove[3] = false;
            }
            if (PM.velocity.x < -0.8)
            {
                lastMove[2] = true;
                lastMove[1] = false;
                lastMove[0] = false;
                lastMove[3] = false;
            }
            if (PM.velocity.y > 0.8)
            {
                lastMove[3] = true;
                lastMove[0] = false;
                lastMove[2] = false;
                lastMove[1] = false;
            }
            if (PM.velocity.y < -0.8)
            {
                lastMove[0] = true;
                lastMove[1] = false;
                lastMove[2] = false;
                lastMove[3] = false;
            }

            GunToMouse();
        }
    }

    void GunToMouse()
    {
        Vector2 mousePos;
        Vector2 gunPos;
        float angle;
        // make the gun rotate to the mouse position of the cursor  depending on what the last move of the player was I.E what direction it is facing
        if (lastMove[0] == true)
        {
            weaponInPlay.transform.parent = holderPosFront.transform;
            weaponInPlay.transform.position = holderPosFront.transform.position;
            weaponInPlay.transform.localScale = new Vector3(-1.5f, 1.5f, 1);
            mousePos = Input.mousePosition;
            gunPos = Camera.main.WorldToScreenPoint(weaponInPlay.transform.position);
            mousePos.x = mousePos.x - gunPos.x;
            mousePos.y = mousePos.y - gunPos.y;
            angle = Mathf.Atan2(mousePos.y, mousePos.x) * Mathf.Rad2Deg;
            if((angle < -90 || angle < 180) && angle > 60 || angle < -60)
            {
                weaponInPlay.transform.localScale = new Vector3(-1.5f, -1.5f, 1);
            }
            else if(angle < 60 && angle > -90)
            {
                weaponInPlay.transform.localScale = new Vector3(-1.5f, 1.5f, 1);
            }
            weaponInPlay.transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
        }

        if (lastMove[1] == true)
        {
            weaponInPlay.transform.parent = holderPosRight.transform;
            weaponInPlay.transform.position = holderPosRight.transform.position;
            weaponInPlay.transform.localScale = new Vector3(-1.5f, 1.5f, 1);
            mousePos = Input.mousePosition;
            gunPos = Camera.main.WorldToScreenPoint(weaponInPlay.transform.position);
            mousePos.x = mousePos.x - gunPos.x;
            mousePos.y = mousePos.y - gunPos.y;
            angle = Mathf.Atan2(mousePos.y, mousePos.x) * Mathf.Rad2Deg;
            if ((angle < -90 || angle < 180) && angle > 60 || angle < -60)
            {
                weaponInPlay.transform.localScale = new Vector3(-1.5f, -1.5f, 1);
            }
            else if (angle < 60 && angle > -90)
            {
                weaponInPlay.transform.localScale = new Vector3(-1.5f, 1.5f, 1);
            }
            weaponInPlay.transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
        }

        if (lastMove[2] == true)
        {
            weaponInPlay.transform.parent = holderPosLeft.transform;
            weaponInPlay.transform.position = holderPosLeft.transform.position;
            weaponInPlay.transform.localScale = new Vector3(1.5f, 1.5f, 1);
            mousePos = Input.mousePosition;
            gunPos = Camera.main.WorldToScreenPoint(weaponInPlay.transform.position);
            mousePos.x = mousePos.x - gunPos.x;
            mousePos.y = mousePos.y - gunPos.y;
            angle = Mathf.Atan2(mousePos.y, mousePos.x) * Mathf.Rad2Deg;
            if ((angle < -90 || angle < 180) && angle > 60 || angle < -60)
            {
                weaponInPlay.transform.localScale = new Vector3(-1.5f, -1.5f, 1);
            }
            else if (angle < 60 && angle > -90)
            {
                weaponInPlay.transform.localScale = new Vector3(-1.5f, 1.5f, 1);
            }
            weaponInPlay.transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
        }

        if (lastMove[3] == true)
        {
            weaponInPlay.transform.parent = holderPosBack.transform;
            weaponInPlay.transform.position = holderPosBack.transform.position;
            weaponInPlay.transform.localScale = new Vector3(1.5f, 1.5f, 1);
            mousePos = Input.mousePosition;
            gunPos = Camera.main.WorldToScreenPoint(weaponInPlay.transform.position);
            mousePos.x = mousePos.x - gunPos.x;
            mousePos.y = mousePos.y - gunPos.y;
            angle = Mathf.Atan2(mousePos.y, mousePos.x) * Mathf.Rad2Deg;
            if ((angle < -90 || angle < 180) && angle > 60 || angle < -60)
            {
                weaponInPlay.transform.localScale = new Vector3(-1.5f, -1.5f, 1);
            }
            else if (angle < 60 && angle > -90)
            {
                weaponInPlay.transform.localScale = new Vector3(-1.5f, 1.5f, 1);
            }
            weaponInPlay.transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
        }
    }

    public void WeaponSetup()
    {
        weaponInPlay = Instantiate(currentWeapon);
        weaponInPlay.transform.position = holderPosFront.transform.position;
        weaponInPlay.transform.parent = holderPosFront.transform;
    }
}
