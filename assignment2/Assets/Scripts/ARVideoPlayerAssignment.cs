using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.UI;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

[RequireComponent(typeof (ARTrackedImageManager))]
public class ARVideoPlayerAssignment : MonoBehaviour
{
    ARTrackedImageManager manager;

    public VideoClip videoSynergy;

    public VideoClip videoEML;

    public VideoClip videoGENUS;

    [SerializeField]
    private Text currentImageText;

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
			currentImageText.text = "<b>" + imageName + "</b>";

			model
                .gameObject
                .SetActive(trackedImage.trackingState != TrackingState.None &&
                trackedImage.trackingState != TrackingState.Limited);

            if (trackedImage.trackingState == TrackingState.None ||
                trackedImage.trackingState == TrackingState.Limited)
            {
                currentImageText.text = "<b>Scan image</b>";
            }
            // TODO: Keep this in case we wanna switch back, remove before submission
            // VideoPlayer videoPlayer =
            //     model.GetComponentInChildren<VideoPlayer>();

            // if (videoPlayer)
            // {
            //     switch (imageName)
            //     {
            //         case "Synergy":
            //             videoPlayer.clip = videoSynergy;
            //             Debug.Log("should play synergy");
            //             break;
            //         case "EML":
            //             videoPlayer.clip = videoEML;
            //             Debug.Log("should play EML");
            //             break;
            //         case "GENUS":
            //             videoPlayer.clip = videoGENUS;
            //             Debug.Log("should play GENUS");
            //             break;
            //     }
            //     // videoPlayer.Play();
            // }
        }
        foreach (var trackedImage in args.added)
        {
            VideoPlayer videoPlayer = trackedImage.transform.GetChild(0).GetComponentInChildren<VideoPlayer>();
            string imageName = trackedImage.referenceImage.name;
            currentImageText.text = "<b>" + imageName + "</b>";

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
            }
        }

        foreach (var trackedImage in args.removed)
        {
            // Empty for now
        }
    }
}
