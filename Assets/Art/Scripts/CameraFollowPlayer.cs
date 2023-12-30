using System;
using UnityEngine;

public class CameraFollowPlayer : MonoBehaviour
{
    public Transform target;
    public float offsetz;
    void LateUpdate()
    {
        if (target == null)
        { 
            target = FindPlayer();
        }
        transform.position = new Vector3(target.position.x, target.position.y, offsetz);
    }

    Transform FindPlayer()
    {
        return GameObject.FindWithTag("PlayerTag").transform;
    }
}
