using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextFading : MonoBehaviour
{
    public Text pokeNameText;
    public Text catchText;
    public float showDuration = 1.0f;
    public float fadeDuration = 1.0f;

    private float baseDuration;
    private float baseTilFadeDuration;
    private bool fading;
    private bool faded;

    // Start is called before the first frame update
    void OnEnable()
    {
        fading = false;
        faded = false;

        baseDuration = fadeDuration;
        baseTilFadeDuration = showDuration;

        catchText.text = pokeNameText.text + " was caught!";
    }

    // Update is called once per frame
    void Update()
    {
        showDuration -= Time.deltaTime;
        if (showDuration <= 0.0f)
        {
            if (!fading)
            {
                CrossFade();
            }
            FadedUpdate();
        }   
    }

    private void FadedUpdate()
    {
        baseDuration -= Time.deltaTime;
        faded = (baseDuration <= 0.0f) ? true : false;
        if (faded)
        {
            this.gameObject.SetActive(false);
            showDuration = baseTilFadeDuration;
            fading = false;
        }
    }

    private void CrossFade()
    {
        catchText.CrossFadeAlpha(0, fadeDuration, false);
        fading = true;
    }
}
