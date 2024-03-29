﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Camera))]
public class VehicleCamRig : MonoBehaviour {

    [SerializeField]
    private Vector3 boom;
    [SerializeField]
    private Vector3 zoomBoom;
    [SerializeField]
    private Transform target;

    private Camera cam;
    private Vector3 engagedBoom;

    [SerializeField]
    private float tilt = 35f;
    [SerializeField]
    private float fovDampening = 10;

    private Vector3 targetPosition;
    private float targetFov = 60;

    void Start ()
    {
        engagedBoom = boom;
        cam = GetComponent<Camera>();
    }
    
    public void SetFOV(float value)
    {
        targetFov = value;
    }

    public void ActivateZoomBoom()
    {
        engagedBoom = zoomBoom;
    }

    public void DeactivateZoomBoom()
    {
        engagedBoom = boom;
    }

    void FixedUpdate()
    {
        targetPosition = target.position + Quaternion.Euler(0, target.rotation.eulerAngles.y, 0) * engagedBoom;
    }

	protected void LateUpdate () {
        transform.position = Vector3.Lerp(
            transform.position,
            targetPosition,
            Time.deltaTime * 5f);
        transform.rotation = Quaternion.Euler(tilt, target.rotation.eulerAngles.y, 0);

        cam.fieldOfView = Mathf.Lerp(cam.fieldOfView, targetFov, Time.deltaTime * fovDampening);
	}

    internal void ResetTarget(Transform transform)
    {
        target = transform;
        engagedBoom = boom;
    }
}
