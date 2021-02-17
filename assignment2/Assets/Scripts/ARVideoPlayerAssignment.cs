using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

[RequireComponent(typeof (ARTrackedImageManager))]
public class ARVideoPlayerAssignment : MonoBehaviour
{
    ARTrackedImageManager manager;

    public VideoClip videoSynergy;

    public VideoClip videoEML;

    public VideoClip videoGENUS;

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
        foreach (var trackedImage in args.updated)
        {
            Transform model = trackedImage.transform.GetChild(0);
            string imageName = trackedImage.referenceImage.name;
            model
                .gameObject
                .SetActive(trackedImage.trackingState != TrackingState.None &&
                trackedImage.trackingState != TrackingState.Limited);

            VideoPlayer videoPlayer =
                model.GetComponentInChildren<VideoPlayer>();

            if (videoPlayer)
            {
                switch (imageName)
                {
                    case "Synergy":
                        videoPlayer.clip = videoSynergy;
                        Debug.Log("should play synergy");
                        break;
                    case "EML":
                        videoPlayer.clip = videoEML;
                        Debug.Log("should play EML");
                        break;
                    case "GENUS":
                        videoPlayer.clip = videoGENUS;
                        Debug.Log("should play GENUS");
                        break;
                }
                videoPlayer.Play();
            }
        }
    }
}
