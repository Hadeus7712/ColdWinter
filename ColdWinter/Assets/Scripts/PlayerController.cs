using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using System;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Events;
using UnityEngine.Rendering;
using Random = UnityEngine.Random;

public class PlayerController : MonoBehaviour
{

    ProgressBar healthBar;
    ProgressBar heatBar;

    [SerializeField] float movementSpeed = 10f;
    [SerializeField] float jumpForce = 5f;


    private Rigidbody rigidbody;
    private Animator animator;

    public Transform groundCheckerTransform;
    public LayerMask groundLayer;


    GameObject axeIcon;

    private bool take;
    public int logs;

    public bool axe;

    public bool cut;

    private bool start = true;

    private bool warming;

    GameObject axeInArm;

    private IEnumerator warmingFreezing;
    private IEnumerator scoring;

    GameManager _gameManager;

    public int score = 0;


    public static Action<int> onDied;
    public static Action<int> onTookLog;


    TextMeshProUGUI scoreText;


    private GameObject navigationCanvas;

    private AudioSource walking;
    private float walkingRate = 0.4f;
    private float nextWalking = .0f;

    public void Awake()
    {
        healthBar = GameObject.Find("HealthBar").GetComponent<ProgressBar>();
        heatBar = GameObject.Find("HeatBar").GetComponent<ProgressBar>();
        _gameManager = FindObjectOfType<GameManager>();
        axeIcon = GameObject.Find("AxeIcon");
        scoreText = GameObject.Find("score").GetComponent<TextMeshProUGUI>();
    }
    void Start()
    {

        
        axeIcon.SetActive(false);

        animator = GetComponent<Animator>();
        rigidbody = GetComponent<Rigidbody>();

        

        

        warmingFreezing = WarmingFreezing();
        scoring = Scoring();

        navigationCanvas = GameObject.Find("NavigationCanvas");
        navigationCanvas.SetActive(false);
        navigationCanvas.transform.rotation = Quaternion.Euler(90, transform.rotation.y * -1.0f, 180);

        StartCoroutine(warmingFreezing);


        axeInArm = GameObject.Find("AxeInArm");
        axeInArm.SetActive(false);

        walking = GetComponent<AudioSource>();
    }


    void Update()
    {
        Debug.Log(_gameManager.GetStart());

        if (_gameManager.GetStart())
        {
            if (start)
            {
                StartCoroutine(scoring);
                start = false;
            }

            if (!navigationCanvas.activeSelf)
            {
                navigationCanvas.SetActive(true);
            }

            float horizontal = Input.GetAxis("Horizontal");
            float vertical = Input.GetAxis("Vertical");

            if (horizontal != 0 || vertical != 0) {
                animator.SetBool("move", true);
            } 
            else animator.SetBool("move", false);


            if((Time.time > nextWalking) && (horizontal != 0 || vertical !=0))
            {
                nextWalking = Time.time + walkingRate;
                walking.pitch = Random.Range(0.9f, 1.1f);
                walking.Play();
            }

            Vector3 movementDirection = new Vector3(horizontal, 0, vertical);

            if (movementDirection.magnitude > Mathf.Abs(0.01f))
            {
                transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(movementDirection), Time.deltaTime * 10);
                navigationCanvas.transform.rotation = Quaternion.Euler(90, transform.rotation.y * -1.0f, 180);
                
            }
                


            rigidbody.angularVelocity = Vector3.zero;


            Vector3 move = Vector3.ClampMagnitude(movementDirection, 1) * movementSpeed;
            rigidbody.velocity = new Vector3(move.x, rigidbody.velocity.y, move.z);

            if (Physics.Raycast(groundCheckerTransform.position, Vector3.down, 0.2f, groundLayer))
            {
                animator.SetTrigger("land");
            }

            if (Input.GetKeyDown(KeyCode.Space))
            {
                Jump();
            }

            if (Input.GetKeyDown(KeyCode.E))
            {
                StartCoroutine(Take());
            }

            if (Input.GetKeyDown(KeyCode.F))
            {
                StartCoroutine(Cut());
            }

            scoreText.text = $"Score: {score}";
        }

    }

    void Jump()
    {
        animator.SetTrigger("jump");
        if (Physics.Raycast(groundCheckerTransform.position, Vector3.down, 0.2f, groundLayer))
        {
            rigidbody.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }
    }


    IEnumerator Take()
    {
        take = true;
        animator.SetTrigger("take");
        yield return new WaitForSeconds(.1f);
        take = false;
        yield return null;
    }

    IEnumerator Cut()
    {
        cut = true;
        animator.SetTrigger("chop");
        yield return new WaitForSeconds(.1f);
        cut = false;
        yield return null;
    }

    public void OnTriggerStay(Collider other)
    {
        if (take)
        {

            if (other.gameObject.tag == "Log")
            {
                logs++;
                onTookLog?.Invoke(logs);
                Destroy(other.gameObject);
            }

            if (other.gameObject.tag == "Axe")
            {
                axeIcon.SetActive(true);
                axe = true;
                axeInArm.SetActive(true);
                other.gameObject.SetActive(false);
            }
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Campfire")
        {
            warming = true;
        }

        if (other.gameObject.tag == "Fire")
        {
            logs = 0;
        }
    }

    public void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Campfire")
        {
            warming = false;
        }
    }


    private IEnumerator WarmingFreezing()
    {
        while (true)
        {
            if (warming)
            {
                if (healthBar.current < healthBar.maximum) healthBar.current++;
                yield return new WaitForSeconds(.1f);
            }
            else
            {
                if (healthBar.current > 0) healthBar.current--;
                else {
                    onDied?.Invoke(score);
                    break;
                }
                
                yield return new WaitForSeconds(.1f);
            }
        }
    }

    private IEnumerator Scoring()
    {
        while (true)
        {
            score++;
            yield return new WaitForSeconds(2);
        }

    }
}
