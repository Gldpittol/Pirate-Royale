using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBarScript : MonoBehaviour
{
    private float scaleY;

    private GameObject attachedTo;

    private void Start()
    {
        scaleY = transform.localScale.y;
    }

    private void LateUpdate()
    {
        transform.position = new Vector2(attachedTo.transform.position.x, attachedTo.transform.position.y + 1);
    }

    public void AttachHealthBar(GameObject toAttach)
    {
        attachedTo = toAttach;
    }

    public void UpdateHealthBarSize(float multiplier)
    {
        transform.localScale = new Vector2(transform.localScale.x, scaleY * multiplier);
    }
}
