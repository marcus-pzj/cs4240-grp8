using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;


[RequireComponent(typeof(ARTrackedImageManager))]
public class TrackedPrefabManager : MonoBehaviour
{
    private ARTrackedImageManager manager;
    private GameObject laserManager;

    public GameObject laserPointer;
    public GameObject glass;

    private void Start()
    {
        laserPointer = Instantiate(laserPointer, Vector3.zero, Quaternion.identity);
        glass = Instantiate(glass, Vector3.zero, Quaternion.identity);
        laserPointer.SetActive(false);
        glass.SetActive(false);
        laserManager = GameObject.FindGameObjectWithTag("LaserManager");
        laserManager.SetActive(false);
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
        // Use laser as an "anchor" -> but how do we find the transform and rotation for anchor?
        if (label == "glass")
        {
            glass.SetActive(active);
            glass.transform.SetPositionAndRotation(
                trackedImageTransform.position,
                trackedImageTransform.rotation
            );
        }
        else
        {
            handleLaserManager(active);
            laserPointer.SetActive(active);
            laserPointer.transform.SetPositionAndRotation(
                new Vector3(
                    trackedImageTransform.position.x, glass.transform.position.y + 0.5f, trackedImageTransform.position.z
                ),
                //trackedImageTransform.position,
                trackedImageTransform.rotation
            );
        }
    }

    private void handleLaserManager(bool active)
    {
        laserManager.SetActive(active);
        if (!active)
        {
            laserManager.GetComponent<LaserManager>().DestroyAllLasers();
        }
    }
}
