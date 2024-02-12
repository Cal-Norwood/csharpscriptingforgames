using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeaponHandler : MonoBehaviour
{
    public GameObject currentWeapon;
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
        currentWeapon.transform.position = holderPosFront.transform.position;
        currentWeapon.transform.parent = holderPosFront.transform;
    }

    // Update is called once per frame
    void Update()
    {
        if(currentWeapon)
        {
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
        if (lastMove[0] == true)
        {
            currentWeapon.transform.parent = holderPosFront.transform;
            currentWeapon.transform.position = holderPosFront.transform.position;
            currentWeapon.transform.localScale = new Vector3(-1.5f, 1.5f, 1);
            mousePos = Input.mousePosition;
            gunPos = Camera.main.WorldToScreenPoint(currentWeapon.transform.position);
            mousePos.x = mousePos.x - gunPos.x;
            mousePos.y = mousePos.y - gunPos.y;
            angle = Mathf.Atan2(mousePos.y, mousePos.x) * Mathf.Rad2Deg;
            if((angle < -90 || angle < 180) && angle > 60 || angle < -60)
            {
                currentWeapon.transform.localScale = new Vector3(-1.5f, -1.5f, 1);
            }
            else if(angle < 60 && angle > -90)
            {
                currentWeapon.transform.localScale = new Vector3(-1.5f, 1.5f, 1);
            }
            currentWeapon.transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
        }

        if (lastMove[1] == true)
        {
            currentWeapon.transform.parent = holderPosRight.transform;
            currentWeapon.transform.position = holderPosRight.transform.position;
            currentWeapon.transform.localScale = new Vector3(-1.5f, 1.5f, 1);
            mousePos = Input.mousePosition;
            gunPos = Camera.main.WorldToScreenPoint(currentWeapon.transform.position);
            mousePos.x = mousePos.x - gunPos.x;
            mousePos.y = mousePos.y - gunPos.y;
            angle = Mathf.Atan2(mousePos.y, mousePos.x) * Mathf.Rad2Deg;
            if ((angle < -90 || angle < 180) && angle > 60 || angle < -60)
            {
                currentWeapon.transform.localScale = new Vector3(-1.5f, -1.5f, 1);
            }
            else if (angle < 60 && angle > -90)
            {
                currentWeapon.transform.localScale = new Vector3(-1.5f, 1.5f, 1);
            }
            currentWeapon.transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
        }

        if (lastMove[2] == true)
        {
            currentWeapon.transform.parent = holderPosLeft.transform;
            currentWeapon.transform.position = holderPosLeft.transform.position;
            currentWeapon.transform.localScale = new Vector3(1.5f, 1.5f, 1);
            mousePos = Input.mousePosition;
            gunPos = Camera.main.WorldToScreenPoint(currentWeapon.transform.position);
            mousePos.x = mousePos.x - gunPos.x;
            mousePos.y = mousePos.y - gunPos.y;
            angle = Mathf.Atan2(mousePos.y, mousePos.x) * Mathf.Rad2Deg;
            if ((angle < -90 || angle < 180) && angle > 60 || angle < -60)
            {
                currentWeapon.transform.localScale = new Vector3(-1.5f, -1.5f, 1);
            }
            else if (angle < 60 && angle > -90)
            {
                currentWeapon.transform.localScale = new Vector3(-1.5f, 1.5f, 1);
            }
            currentWeapon.transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
        }

        if (lastMove[3] == true)
        {
            currentWeapon.transform.parent = holderPosBack.transform;
            currentWeapon.transform.position = holderPosBack.transform.position;
            currentWeapon.transform.localScale = new Vector3(1.5f, 1.5f, 1);
            mousePos = Input.mousePosition;
            gunPos = Camera.main.WorldToScreenPoint(currentWeapon.transform.position);
            mousePos.x = mousePos.x - gunPos.x;
            mousePos.y = mousePos.y - gunPos.y;
            angle = Mathf.Atan2(mousePos.y, mousePos.x) * Mathf.Rad2Deg;
            if ((angle < -90 || angle < 180) && angle > 60 || angle < -60)
            {
                currentWeapon.transform.localScale = new Vector3(-1.5f, -1.5f, 1);
            }
            else if (angle < 60 && angle > -90)
            {
                currentWeapon.transform.localScale = new Vector3(-1.5f, 1.5f, 1);
            }
            currentWeapon.transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
        }
    }
}
