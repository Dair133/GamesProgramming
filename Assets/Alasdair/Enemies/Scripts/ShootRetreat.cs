using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootRetreat : MonoBehaviour
{
   //maybe make it so retreat speed becomes faster the closer the player is to the enemy
    public float retreatSpeed;
    public float followSpeed;
    public Transform target;
    public float AggroRange;
    public float retreatRange;
    public float stopRange;

    // Start is called before the first frame update
    void Start()
    {
        followSpeed = 7;
        retreatSpeed = 8;
    }

    // Update is called once per frame
    void Update()
    {
        if (target == null)
        {
            GameObject player = GameObject.FindGameObjectWithTag("PlayerTag");
            if (player != null)
            {
                target = player.GetComponent<Transform>();
            }
        }
        else
        {
            float distanceToPlayer = Vector2.Distance(transform.position, target.position);
            //the enemy moves towards the player as long as the distance between them is less than minimumDistance. If you set a higher value for minimumDistance, the enemy will start moving towards the player even when they are farther apart.
            if (Vector2.Distance(transform.position, target.position) < AggroRange)
            {

                    transform.position = Vector2.MoveTowards(transform.position, target.position, -retreatSpeed * Time.deltaTime);
            }
            else if(Vector2.Distance(transform.position, target.position) > AggroRange)
            {
                transform.position = Vector2.MoveTowards(transform.position, target.position, followSpeed * Time.deltaTime);
            }
                
            else
            {
                //Attack Code
            }
        }
    }
}
