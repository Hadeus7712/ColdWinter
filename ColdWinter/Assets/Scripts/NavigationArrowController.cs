using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;


public class NavigationArrowController : MonoBehaviour
{

    private Vector3 campfire;
    private RectTransform pointerRectTransform; 

    public void Awake()
    {
        campfire =  new Vector3(GameObject.Find("Campfire").transform.position.x, 0, GameObject.Find("Campfire").transform.position.z);
        pointerRectTransform = GetComponent<RectTransform>();
    }

    void Update()
    {
        Vector3 toPosition = campfire;
        Vector3 fromPosition = pointerRectTransform.position;


        Vector3 dir = (toPosition - fromPosition).normalized;

        float n = Mathf.Atan2(dir.z, dir.x) * Mathf.Rad2Deg;
        if (n < 0) n += 360;

        float angle = n;

        pointerRectTransform.localEulerAngles = new Vector3(0, 0, angle);


    }
}
