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
            Debug.Log($"Tracked image detected: {trackedImage.referenceImage.name} with size: {trackedImage.size}");
        }

        UpdateTrackedImages(e.added);
        UpdateTrackedImages(e.updated);
        // FIXME: Try to split these up and only delete when image changed etc
    }

    private void UpdateTrackedImages(IEnumerable<ARTrackedImage> trackedImages)
    {
        // If the same image (ReferenceImageName)
        //var trackedImage =
        //    trackedImages.FirstOrDefault(x => x.referenceImage.name == ReferenceImageName);
        foreach(ARTrackedImage trackedImage in trackedImages)
        {
            GameObject gameObject = trackedImage.transform.GetChild(0).gameObject;

            if (trackedImage.trackingState != TrackingState.None)
            {
                var trackedImageTransform = trackedImage.transform;
                transform.SetPositionAndRotation(trackedImageTransform.position, trackedImageTransform.rotation);
                Debug.Log(trackedImage.referenceImage.name);

                SetObject(trackedImageTransform, true, trackedImage.referenceImage.name, gameObject);
            }

            if (trackedImage.trackingState == TrackingState.None || trackedImage.trackingState == TrackingState.Limited)
            {
                // TODO: Set the object to inactive
                SetObject(null, false, trackedImage.referenceImage.name, gameObject);
            }
        }
    }

    private void SetObject(
        Transform trackedImageTransform, bool active, string label,
        GameObject gameObject
    )
    {
        //switch (label)
        //{
        //    case "laser_pointer":
        //        gameObject = laserObject;
        //        break;
        //    default:
        //        gameObject = laserObject;
        //        break;
        //}
        if (active && trackedImageTransform)
        {
            gameObject.SetActive(true);
            gameObject.transform.SetPositionAndRotation(
                trackedImageTransform.position, trackedImageTransform.rotation
            );
        } else
        {
            gameObject.SetActive(false);
        }
    }
}
