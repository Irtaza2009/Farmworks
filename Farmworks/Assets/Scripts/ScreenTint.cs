using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UI;

public class ScreenTint : MonoBehaviour
{
    [SerializeField] Color unTintedColor;
    [SerializeField] Color tintedColor;

    Image image;
    float f;

    public float tintSpeed = 0.5f;

    private void Awake()
    {
        image = GetComponent<Image>();
    }

    public void Tint()
    {
        StopAllCoroutines();
        f = 0f;
        StartCoroutine(TintScreen());
    }

    public void UnTint()
    {
        StopAllCoroutines();
        f = 0f;
        StartCoroutine(UnTintScreen());
    }

    private IEnumerator TintScreen()
    {
        while (f < 1f)
        {
            f += Time.deltaTime * tintSpeed;
            f = math.clamp(f, 0, 1f);
            Color c = image.color;
            c = Color.Lerp(unTintedColor, tintedColor, f);
            image.color = c;
            yield return new WaitForEndOfFrame();
        }


    }
    
     private IEnumerator UnTintScreen()
    {
        while (f < 1f)
        {
            f += Time.deltaTime * tintSpeed;
            f = math.clamp(f, 0, 1f);
            Color c = image.color;
            c = Color.Lerp(tintedColor, unTintedColor, f);
            image.color = c;
            yield return new WaitForEndOfFrame();
        }
        
    }
}
