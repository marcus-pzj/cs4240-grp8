using System.Collections.Generic;
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
    public GameObject mirror;

    private Dictionary<string, GameObject> gameObjectsDict = new Dictionary<string, GameObject>();

    private void Start()
    {
        laserPointer = Instantiate(laserPointer, Vector3.zero, Quaternion.identity);
        glass = Instantiate(glass, Vector3.zero, Quaternion.identity);
        mirror = Instantiate(mirror, Vector3.zero, Quaternion.identity);

        gameObjectsDict.Add("mirror", mirror);
        gameObjectsDict.Add("glass", glass);
        gameObjectsDict.Add("laser_pointer", laserPointer);

        laserPointer.SetActive(false);
        glass.SetActive(false);
        mirror.SetActive(false);

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
        if (label == "laser_pointer")
        {
            GameObject laserPointer = gameObjectsDict[label];
            handleLaserManager(active);
            laserPointer.SetActive(active);
            laserPointer.transform.SetPositionAndRotation(
                trackedImageTransform.position,
                trackedImageTransform.rotation
            );
        }
        else {
            GameObject item = gameObjectsDict[label];
            item.SetActive(active);
            item.transform.SetPositionAndRotation(
                trackedImageTransform.position,
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
