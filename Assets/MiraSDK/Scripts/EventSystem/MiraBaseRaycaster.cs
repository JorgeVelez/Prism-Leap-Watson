﻿using UnityEngine;
using UnityEngine.EventSystems;

/// <summary>
/// MiraBaseRaycaster provides the ray for MiraGraphicRaycaster and MiraPhysicsRaycaster to use when they calculate intersections
/// in MiraInputModule
/// </summary>
public abstract class MiraBaseRaycaster : BaseRaycaster
{
    /// <summary>
    /// Either cast a ray from the controller itself or the camera.
    /// Casting from the camera will give most accurate results but
    /// will limit interactions to things within the users sight.
    /// Whereas in World mode the ray can reach out of sight objects
    /// but may cause perceptual issues for the user.
    /// </summary>
    public enum RaycastStyle
    {
        Camera,
        World
    }

    /// <summary>
    /// The default raycast style is World
    /// </summary>
    public RaycastStyle raycastStyle = RaycastStyle.World;

    protected MiraBaseRaycaster()
    {
    }

    private Ray lastray;

    public Ray GetLastRay()
    {
        return lastray;
    }

    /// <summary>
    /// Gets the ray that should be used to calculate intersections between the controller and what the user is pointing at
    /// </summary>
    /// <returns>The ray.</returns>
    protected Ray GetRay()
    {
        if (raycastStyle == RaycastStyle.World)
        {
            Debug.Log("WORLDRAYCAST,YO!");
            lastray = new Ray(MiraController.Position, MiraController.Transform.forward);
        }
        else if (raycastStyle == RaycastStyle.Camera)
        {
            Vector3 camPos = Camera.main.transform.position;
            Vector3 worldPoint = (MiraController.Transform.position + (MiraController.Transform.forward * MiraPointerManager.Pointer.maxDistance));
            Vector3 dir = (worldPoint - camPos).normalized;
            Vector3 start = camPos + (dir * Camera.main.nearClipPlane);

            lastray = new Ray(start, dir);
        }
        return lastray;
    }
}