using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;
    public float offsetz;

    void LateUpdate()
    {
        transform.position = new Vector3(target.position.x, target.position.y, offsetz);
    }
}
