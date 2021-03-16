using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;


[RequireComponent(typeof(ARTrackedImageManager))]
public class TrackedPrefabManager : MonoBehaviour
{
    private ARTrackedImageManager manager;

    private void Awake()
    {
        manager = GetComponent<ARTrackedImageManager>();
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
            UpdatePrefab(trackedImage.referenceImage.name, trackedImage.transform, false);
        }

        foreach (var trackedImage in e.updated)
        {
            if (trackedImage.trackingState != TrackingState.Tracking)
            {
                UpdatePrefab(trackedImage.referenceImage.name, trackedImage.transform, false);
            } else
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
        if (label == "glass")
        {
            trackedImageTransform.GetChild(1).gameObject.SetActive(active);
        } else
        {
            trackedImageTransform.GetChild(0).gameObject.SetActive(active);
        }
    }
}
