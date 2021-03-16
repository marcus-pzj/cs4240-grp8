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
            trackedImage.transform.GetChild(0).gameObject.SetActive(true);
        }

        foreach (var trackedImage in e.updated)
        {
            if (trackedImage.trackingState != TrackingState.Tracking)
            {
                trackedImage.transform.GetChild(0).gameObject.SetActive(false);
            } else
            {
                trackedImage.transform.GetChild(0).gameObject.SetActive(true);
            }

        }
        foreach (var trackedImage in e.removed)
        {
            trackedImage.transform.GetChild(0).gameObject.SetActive(false);
        }
    }
}
