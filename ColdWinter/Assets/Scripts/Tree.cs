using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using System;
using UnityEngine;
using Random = System.Random;

public class Tree : MonoBehaviour
{
    private Rigidbody rigidBody;
    private Rigidbody force;

    private Transform player;

    private PlayerController playerController;

    private Random random = new Random();

    [SerializeField] private GameObject log;

    public void Start()
    {


        playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();


        rigidBody = GetComponent<Rigidbody>();



        force = transform.GetComponentInParent<Rigidbody>();
        player = GameObject.FindWithTag("Player").GetComponent<Transform>();


    }
    private void OnEnable()
    {
        //playerController.onChopped += Fall;
    }

    private void OnDisable()
    {
        //playerController.onChopped -= Fall;
    }

    private void Fall()
    {

        Vector3 direction = Vector3.ClampMagnitude(transform.position - player.position, 1);


        rigidBody.constraints = RigidbodyConstraints.None;
        force.velocity = new Vector3(direction.x * 2, 0, direction.z * 2);
        
    }

    public void OnTriggerStay(Collider other)
    {
        if(other.gameObject.tag == "Player" && playerController.axe && playerController.cut)
        {
            Fall();
            StartCoroutine(CreateLogs());
        }
    }

    private IEnumerator CreateLogs()
    {
        yield return new WaitForSeconds(3);

        for (int i = 0; i < (int)random.Next(1, 3); i++) 
            Instantiate(log, new Vector3(transform.position.x, transform.position.y + 1, transform.position.z), new Quaternion());

        Destroy(gameObject);

        

        yield return null;
    }
}
