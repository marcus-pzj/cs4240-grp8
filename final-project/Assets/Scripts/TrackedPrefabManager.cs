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

    void OnTrackedImagesChanged(ARTrackedImagesChangedEventArgs args)
    {
        bool isTracking = false;
        // TODO: We only want to disable the model when it does not exist in frame

        foreach (var trackedImage in args.updated)
        {
            Transform model = trackedImage.transform.GetChild(0);
            string imageName = trackedImage.referenceImage.name;

            model
                .gameObject
                .SetActive(trackedImage.trackingState != TrackingState.None &&
                trackedImage.trackingState != TrackingState.Limited);

            if (model.gameObject.activeSelf)
            {
                //currentImageText.text = "<b>" + trackedImage.referenceImage.name + "</b>";
                isTracking = true;
            }
        }

        if (!isTracking)
        {
            // Spawn some text here
        }

        foreach (var trackedImage in args.added)
        {

            string imageName = trackedImage.referenceImage.name;

            //if (videoPlayer)
            //{
            //    currentImageText.text = "<b>" + imageName + "</b>";
            //    switch (imageName)
            //    {
            //        case "Synergy":
            //            videoPlayer.clip = videoSynergy;
            //            break;
            //        case "EML":
            //            videoPlayer.clip = videoEML;
            //            break;
            //        case "GENUS":
            //            videoPlayer.clip = videoGENUS;
            //            break;
            //    }
            //}
        }

        foreach (var trackedImage in args.removed)
        {
            // Empty for now
        }
    }
}
