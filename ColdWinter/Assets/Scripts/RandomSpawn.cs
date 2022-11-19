using System.Collections;
using System.Collections.Generic;
using System.IO.IsolatedStorage;
using UnityEngine;

public class RandomSpawn : MonoBehaviour
{

    Transform transform;

    [SerializeField] GameObject object1;


    [SerializeField] float startRadius = 30f;
    private float endRadius;
    [SerializeField] int num = 3;



    void Start()
    {
        transform = GetComponent<Transform>();
        Spawn();
    }



    void Spawn()
    {
        Vector3 center = transform.position;

        for (int i = 0; i < num; i++)
        {
            Vector3 pos = RandomCircle(center, startRadius);
            Instantiate(object1, pos, Quaternion.identity);
        }
    }

    Vector3 RandomCircle(Vector3 center, float radius)
    {
        float angle = UnityEngine.Random.value * 360;
        Vector3 pos;
        pos.x = center.x + radius * Mathf.Sin(angle * Mathf.Deg2Rad);
        pos.z = center.z + radius * Mathf.Cos(angle * Mathf.Deg2Rad);
        pos.y = center.y;
        return pos;
    }
}
