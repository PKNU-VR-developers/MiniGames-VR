using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RadialProgress : MonoBehaviour
{
    public Text progressIndicator;
    public Image loadingBar;
    float currentValue;
    float pressTime = 1f;

    // Start is called before the first frame update

    // Update is called once per frame
    void Update()
    {
        if (currentValue <= pressTime)
        {
            currentValue += Time.deltaTime;
            progressIndicator.text = "Manage Axe";
        }
        else
        {
            progressIndicator.text = "Manage UI";
        }

        loadingBar.fillAmount = currentValue;
    }

    private void OnDisable()
    {
        loadingBar.fillAmount = 0f;
        currentValue = 0f;
    }
}
