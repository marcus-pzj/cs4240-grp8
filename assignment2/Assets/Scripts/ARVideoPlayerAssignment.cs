using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
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

            if (
                trackedImage.trackingState == TrackingState.None ||
                trackedImage.trackingState == TrackingState.Limited
            )
            {
                currentImageText.text = "<b>Scan image</b>";
            }
        }
        foreach (var trackedImage in args.added)
        {
            Transform videoPlayerTransform = trackedImage.transform.GetChild(0);

            VideoPlayer videoPlayer =
                videoPlayerTransform.GetComponentInChildren<VideoPlayer>();

            Vector3 currScale = videoPlayerTransform.localScale;

            float heightWidthRatio =
                trackedImage.referenceImage.size[1] /
                trackedImage.referenceImage.size[0];
            float currWidth = trackedImage.size[0];

            videoPlayerTransform.localScale =
                new Vector3(currWidth, 0.0001f, currWidth * heightWidthRatio);
            string imageName = trackedImage.referenceImage.name;
            currentImageText.text = "<b>" + imageName + "</b>";

            if (videoPlayer)
            {
                switch (imageName)
                {
                    case "Synergy":
                        videoPlayer.clip = videoSynergy;
                        break;
                    case "EML":
                        videoPlayer.clip = videoEML;
                        break;
                    case "GENUS":
                        videoPlayer.clip = videoGENUS;
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
