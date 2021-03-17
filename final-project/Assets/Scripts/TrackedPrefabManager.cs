using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;


[RequireComponent(typeof(ARTrackedImageManager))]
public class TrackedPrefabManager : MonoBehaviour
{
    private ARTrackedImageManager manager;

    public GameObject laserPointer;
    public GameObject glass;

    private void Start()
    {
        laserPointer = Instantiate(laserPointer, Vector3.zero, Quaternion.identity);
        glass = Instantiate(glass, Vector3.zero, Quaternion.identity);
    }

    private void Awake()
    {
        manager = FindObjectOfType<ARTrackedImageManager>();
    }

    private void OnEnable()
    {
        manager.trackedImagesChanged += OnTrackedImagesChanged;
    }

    private void OnDisable()
    {
        manager.trackedImagesChanged -= OnTrackedImagesChanged;
    }

    private void OnTrackedImagesChanged(ARTrackedImagesChangedEventArgs e)
    {

        foreach (var trackedImage in e.added)
        {
            // Prefab here must have already been preloaded before hand
            UpdatePrefab(trackedImage.referenceImage.name, trackedImage.transform, true);
        }

        foreach (var trackedImage in e.updated)
        {
            if (trackedImage.trackingState != TrackingState.Tracking)
            {
                UpdatePrefab(trackedImage.referenceImage.name, trackedImage.transform, false);
            }
            else
            {
                UpdatePrefab(trackedImage.referenceImage.name, trackedImage.transform, true);
            }

        }
        foreach (var trackedImage in e.removed)
        {
            UpdatePrefab(trackedImage.referenceImage.name, trackedImage.transform, false);
        }
    }

    private void UpdatePrefab(string label, Transform trackedImageTransform, bool active)
    {
        if (active)
        {
            if (label == "glass")
            {
                glass.SetActive(true);
                glass.transform.SetPositionAndRotation(
                    trackedImageTransform.position, trackedImageTransform.rotation
                );
            }
            else
            {
                laserPointer.SetActive(true);
                laserPointer.transform.SetPositionAndRotation(
                    trackedImageTransform.position, trackedImageTransform.rotation
                );
            }
        }
        else
        {
            // nothing for now
            if (label == "glass")
            {
                glass.SetActive(false);
            }
            else
            {
                laserPointer.SetActive(false);
            }
        }
    }
}
