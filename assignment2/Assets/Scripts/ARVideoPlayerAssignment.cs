using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;


[RequireComponent(typeof(ARTrackedImageManager))]
public class ARVideoPlayerAssignment : MonoBehaviour
{
    ARTrackedImageManager manager;
    VideoPlayer videoPlayer;
    public VideoClip video1;
    public VideoClip video2;
    public VideoClip video3;

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

    void OnTrackedImagesChanged (ARTrackedImagesChangedEventArgs eventArgs)
    {
        foreach (var trackedImage in eventArgs.added)
        {
            Debug.Log("Addeed new image: " + trackedImage.referenceImage.name);
        }

        string lastTracked = null;

        foreach (var trackedImage in eventArgs.updated)
        {
            if (trackedImage.trackingState == TrackingState.Tracking)
            {
                trackedImage.gameObject.SetActive(true);
                Debug.Log("Tracking new image: " + trackedImage.referenceImage.name);

                trackedImage.destroyOnRemoval = true;
                string videoName = trackedImage.referenceImage.name;

                if (manager.trackedImagePrefab.activeSelf && (lastTracked != videoName || lastTracked == null))
                {
                    videoPlayer = manager.trackedImagePrefab.GetComponentInChildren<VideoPlayer>();
                    lastTracked = videoName;

                    switch (videoName)
                    {
                        case "Synergy":
                            videoPlayer.clip = video1;
                            Debug.Log("should play synergy");
                            videoPlayer.Play();
                            break;
                        case "EML":
                            videoPlayer.clip = video2;
                            Debug.Log("should play EML");
                            videoPlayer.Play();
                            break;
                        case "GENUS":
                            videoPlayer.clip = video3;
                            Debug.Log("should play GENUS");
                            videoPlayer.Play();
                            break;
                    }
                }
            }
            else if (trackedImage.trackingState == TrackingState.Limited || trackedImage.trackingState == TrackingState.None) {
                trackedImage.gameObject.SetActive(false);
            }
            
        }
    }
}
