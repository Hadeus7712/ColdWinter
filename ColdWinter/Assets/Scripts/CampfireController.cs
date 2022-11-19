using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class CampfireController : MonoBehaviour
{
    private ProgressBar heatBar;

    private ParticleSystem fire;

    private IEnumerator fading;

    private Light light;

    private SphereCollider warmCollider;


    private GameManager _gameManager;


    private bool start = true;

    public void Awake()
    {
        heatBar = GameObject.Find("HeatBar").GetComponent<ProgressBar>();
    }

    void Start()
    {
        _gameManager = FindObjectOfType<GameManager>();

        fire = GameObject.Find("Fire").GetComponent<ParticleSystem>();
        light = GameObject.Find("Fire").GetComponent<Light>();


        warmCollider = GetComponent<SphereCollider>();


        fading = Fading();

    }

    // Update is called once per frame
    void Update()
    {
        if (_gameManager.GetStart())
        {
            if (start)
            {
                StartCoroutine(fading);
                start = false;
            }

            float musicVolume = (float)(heatBar.current * 1.0 / heatBar.maximum);
            GetComponent<AudioSource>().volume = musicVolume;

            float fireScale = (float)(heatBar.current * 5.0 / heatBar.maximum);
            Vector3 size = new Vector3(fireScale, fireScale, fireScale);

            float lightRange = (float)(heatBar.current * 30.0 / heatBar.maximum);
            float lightIntensity = (float)(heatBar.current * 10.0 / heatBar.maximum);


            fire.transform.localScale = size;
            warmCollider.radius = fireScale;

            light.range = lightRange;
            light.intensity = lightIntensity;
        }


    }


    private IEnumerator Fading()
    {
        while (true)
        {
            if (heatBar.current > 0) heatBar.current--;
            yield return new WaitForSeconds(.1f);
        }
    }

}
