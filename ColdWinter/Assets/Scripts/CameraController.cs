using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{

    public Transform playerTransform;
    public Vector3 offset;
    public float camPositionSpeed;



    void Update()
    {
        Vector3 newCamPostition = new Vector3(playerTransform.position.x + offset.x, playerTransform.position.y + offset.y, playerTransform.position.z + offset.z);
        transform.position = Vector3.Lerp(transform.position, newCamPostition, camPositionSpeed * Time.deltaTime);
    }
}
