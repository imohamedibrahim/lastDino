using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatUi : MonoBehaviour
{
    [SerializeField]
    private float toScaleUntil = 1.5f;
    [SerializeField]
    private float interval = 10f;
    private Vector3 toScaleUntilVector;
    private Vector3 initialScale;
    private bool zoomIn;
    private Vector3 tmp;
    void Start()
    {
        toScaleUntilVector = new Vector3(toScaleUntil, toScaleUntil, toScaleUntil);
        initialScale = transform.localScale;
        zoomIn = false;
    }
    // Update is called once per frame
    void Update()
    {

        if (!zoomIn && transform.localScale != toScaleUntilVector)
            tmp = toScaleUntilVector;
        else if (zoomIn && transform.localScale != initialScale)
            tmp = initialScale;
        else
            zoomIn = !zoomIn;

        transform.localScale = Vector3.Lerp(transform.localScale, tmp, interval);

    }
}
