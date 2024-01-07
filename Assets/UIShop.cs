using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using UnityEngine;
using Object = UnityEngine.Object;

public class UIShop : MonoBehaviour
{
    private GameObject player;
    public Transform merchant;
    public GameObject UIShopComponent;

    private GameObject success;
    private GameObject failure;

    void Start()
    {
        success = GameObject.FindGameObjectWithTag("Success");
        failure = GameObject.FindGameObjectWithTag("Failure");
        UIShopComponent.SetActive(false);
        success.SetActive(false);
        failure.SetActive(false);
    }

    void Update()
    {
        if (player == null)
        {
            player = FindPlayer();
        }

        if (checkRange())
        {
            UIShopComponent.SetActive(true);
            if (Input.GetKeyDown(KeyCode.T) && UIShopComponent.activeSelf)
            {
                int coins = player.GetComponent<PlayerScripts>().getCoins();
                if (coins >= 10)
                {
                    failure.SetActive(false);
                    UIShopComponent.GetComponentInParent<UIManager>().addPotion();
                    player.GetComponent<PlayerScripts>().goldPickup(-10);
                    Debug.Log("Success");
                    success.SetActive(true);
                }
                else
                {
                    success.SetActive(false);
                    Debug.Log("Failure");
                    failure.SetActive(true);
                }
            }
        }
        else
        {
            failure.SetActive(false);
            success.SetActive(false);
            UIShopComponent.SetActive(false);
        }
    }

    GameObject FindPlayer()
    {
        return GameObject.FindWithTag("PlayerTag");
    }

    bool checkRange()
    {
        float distance = Vector3.Distance(player.transform.position, merchant.position);
        return distance <= 9f;
    }
}
