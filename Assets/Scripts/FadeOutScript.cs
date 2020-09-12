using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeOutScript : MonoBehaviour
{

    public float fadeDuration;
    private float i = 1;

    public bool fadeStart;

    private SpriteRenderer sr;

    private void Start()
    {
        sr = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        if (fadeStart)
        {
            if (i >= 0)
            {
                sr.color = new Color(sr.color.r, sr.color.g, sr.color.b, i);
                i -= Time.deltaTime / fadeDuration;
            }
            else Destroy(this.gameObject);
        }
    }

}
