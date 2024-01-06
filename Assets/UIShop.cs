using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;

public class UIShop : MonoBehaviour
{
    private Transform player;
    public Transform merchant;
    public GameObject UIShopComponent;
    

     void Update()
    {
        if (player == null)
        { 
            player = FindPlayer();
        }
        
        float distance = Vector3.Distance(player.position, merchant.position);
        Debug.Log("FAR AWAY: "+distance);
        
        if (distance <= 4f)
        {
            // Activate the UI component
            UIShopComponent.SetActive(true);
        }
        else
        {
            // Deactivate the UI component
            UIShopComponent.SetActive(false);
        }

        if (Input.GetMouseButtonDown(0)) // 0 means left mouse button, change as needed
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider != null && hit.collider.gameObject == gameObject)
                {
                    // Your code to handle the click goes here
                    Debug.Log("UI Clicked!");
                }
            }
        }
    }
     
     Transform FindPlayer()
     {
         return GameObject.FindWithTag("PlayerTag").transform;
     }
}
