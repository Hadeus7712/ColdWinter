using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Log : MonoBehaviour
{
    public void OnTriggerEnter(Collider other)
    {
        Destroy(gameObject);
    }
}
