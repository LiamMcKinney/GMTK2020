using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    public bool Shaking;
    private float ShakeDecay;
    private float ShakeIntensity;
    public Vector3 position;

    public Vector3 targetPosition;
    bool isZooming;

    Camera cam;

    public float zoomMoveMultiplier;
    public float minZoomChange;

    void Start()
    {
        Shaking = false;
        cam = GetComponent<Camera>();
    }
    // Update is called once per frame
    void Update()
    {
        if (isZooming)
        {
            position = Vector3.MoveTowards(position, targetPosition, Mathf.Max(minZoomChange, zoomMoveMultiplier * (targetPosition - position).magnitude));

            if(position == targetPosition)
            {
                isZooming = false;
            }
        }
        else
        {
            position = targetPosition;
        }

        transform.position = position;

        if (ShakeIntensity > 0)
        {
            transform.position = position + Random.insideUnitSphere * ShakeIntensity;

            ShakeIntensity -= ShakeDecay;
        }
        else if (Shaking)
        {
            Shaking = false;
        }
    }


    public void Shake()
    {
        Shake(.3f, .02f);
    }
    public void Shake(float intensity)
    {
        Shake(intensity, .02f);
    }
    public void Shake(float intensity, float decay)
    {
        ShakeIntensity = intensity;
        ShakeDecay = decay;
        Shaking = true;
    }

    public void ZoomToTarget(Vector3 target)
    {
        targetPosition = target;
        isZooming = true;
    }
}
