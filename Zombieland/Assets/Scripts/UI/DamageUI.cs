using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DamageUI : MonoBehaviour
{
    [SerializeField] Image[] bloodImages;
    float impactTime = 2.0f;

    int lastSplat = -1;

    void Start()
    {
        foreach (Image splat in bloodImages)
        {
            splat.enabled = false;
        }
    }

    public void ShowDamageUI()
    {
        int randomNum = GetRandomSplat();

        while (randomNum == lastSplat) {
            randomNum = GetRandomSplat();
        }

        lastSplat = randomNum;
        Image splat = bloodImages[randomNum];
        StartCoroutine(FadeImage(splat, .75f, 0.0f, impactTime));
    }

    private int GetRandomSplat()
    {
        return UnityEngine.Random.Range(0, bloodImages.Length);
    }

    public static IEnumerator FadeImage(Image image, float startAlpha, float endAlpha, float duration) //Fading the image using its alpha values
    {
        image.enabled = true;
        Color tempColor = image.color;
        tempColor.a = startAlpha;
        image.color = tempColor;

        for (float i = 1; i >= 0; i -= Time.deltaTime)
        {
            image.color = new Color(1, 1, 1, i);
            yield return null;
        }
        tempColor.a = endAlpha; 
        image.enabled = false;
    }
}
