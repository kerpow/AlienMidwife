using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    RaycastHit hit;
    public LayerMask floorMask;
    public LayerMask eggMask;
    public Cursor cursor;

    public Egg heldEgg;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame

    void Update()
    {

        if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, 100, floorMask))
        {

            cursor.transform.position = hit.point;
            if (Vector3.Dot(Vector3.up, hit.normal) > .9f)//make sure the surface is mostly horizontal
            {
                cursor.SetValid(true);


                if (heldEgg != null)
                {
                    if (Input.GetKey(KeyCode.Mouse0) && NoEggNear(hit.point))
                        heldEgg.Move(hit.point);
                    else
                        cursor.SetValid(false);
                }
            }
            else
                cursor.SetValid(false);


        }

        if(heldEgg == null && Input.GetKeyDown(KeyCode.Mouse0))
        {
            //raycast for an egg
            
            if(Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, 100, eggMask))
                heldEgg = hit.collider.GetComponentInParent<Egg>();
        }


        if (heldEgg != null && Input.GetKeyUp(KeyCode.Mouse0))
        {
            heldEgg = null;
        }

    }

    private bool NoEggNear(Vector3 point)
    {
        var colliders = Physics.OverlapSphere(point, .4f);

        foreach (var c in colliders)
        {
            var egg = c.GetComponentInParent<Egg>();
            if (egg != null && egg != heldEgg)
            {
                return false;
            }
        }
        return true;
    }
}
