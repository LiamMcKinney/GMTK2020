﻿using System.Collections;
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

    AudioSource audio;

    public float zoomMoveMultiplier;
    public float minZoomChange;

    public int totalRedFrames;
    int redFramesLeft;
    public SpriteRenderer redFlash;

    void Start()
    {
        Shaking = false;
        cam = GetComponent<Camera>();
        audio = GetComponent<AudioSource>();
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

        redFramesLeft--;
        if (redFramesLeft < 0)
        {
            redFlash.enabled = false;
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

    public void PlayClip(AudioClip clip)
    {
        audio.clip = clip;
        audio.Play();
        //audio.PlayOneShot(clip);
    }


    public void FlashRed()
    {
        redFlash.enabled = true;
        redFramesLeft = totalRedFrames;
    }
}
