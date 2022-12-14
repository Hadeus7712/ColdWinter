using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProgressBar : MonoBehaviour
{

    public int maximum;
    public int current;

    public Image mask;

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        GetCurrentFill();
    }

    private void GetCurrentFill()
    {
        float fillAmount = (float)current / (float)maximum;
        mask.fillAmount = fillAmount;
    }
}
