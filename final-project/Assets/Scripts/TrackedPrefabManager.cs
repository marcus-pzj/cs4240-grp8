using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

[RequireComponent(typeof (ARTrackedImageManager))]
public class TrackedPrefabManager : MonoBehaviour
{
    private ARTrackedImageManager manager;

    private GameObject laserManager;

    private bool anchorOnLaser;

    public GameObject laserPointer;

    public GameObject glass1;

    public GameObject glass2;

    public GameObject mirror;

    public GameObject target;

    public GameObject wood;

    private Dictionary<string, GameObject>
        gameObjectsDict = new Dictionary<string, GameObject>();

    private void Start()
    {
        laserPointer =
            Instantiate(laserPointer, Vector3.zero, Quaternion.identity);
        glass1 = Instantiate(glass1, Vector3.zero, Quaternion.identity);
        glass2 = Instantiate(glass2, Vector3.zero, Quaternion.identity);
        mirror = Instantiate(mirror, Vector3.zero, Quaternion.identity);
        target = Instantiate(target, Vector3.zero, Quaternion.identity);
        wood = Instantiate(wood, Vector3.zero, Quaternion.identity);

        gameObjectsDict.Add("mirror", mirror);
        gameObjectsDict.Add("glass1", glass1);
        gameObjectsDict.Add("glass2", glass2);
        gameObjectsDict.Add("laser_pointer", laserPointer);
        gameObjectsDict.Add("target", target);
        gameObjectsDict.Add("wood", wood);

        laserPointer.SetActive(false);
        glass1.SetActive(false);
        glass2.SetActive(false);
        mirror.SetActive(false);
        wood.SetActive(false);
        target.SetActive(false);

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
            UpdatePrefab(trackedImage.referenceImage.name,
            trackedImage.transform,
            true);
        }

        foreach (var trackedImage in e.updated)
        {
            if (trackedImage.trackingState != TrackingState.Tracking)
            {
                UpdatePrefab(trackedImage.referenceImage.name,
                trackedImage.transform,
                false);
            }
            else
            {
                UpdatePrefab(trackedImage.referenceImage.name,
                trackedImage.transform,
                true);
            }
        }
        foreach (var trackedImage in e.removed)
        {
            UpdatePrefab(trackedImage.referenceImage.name,
            trackedImage.transform,
            false);
        }
    }

    private void UpdatePrefab(
        string label,
        Transform trackedImageTransform,
        bool active
    )
    {
        if (label == "laser_pointer")
        {
            GameObject laserPointer = gameObjectsDict[label];
            handleLaserManager (active);
            laserPointer.SetActive (active);
            laserPointer
                .transform
                .SetPositionAndRotation(trackedImageTransform.position,
                trackedImageTransform.rotation);
            anchorOnLaser = active;
        }
        else
        {
            GameObject item = gameObjectsDict[label];
            item.SetActive (active);
            float yVal = laserPointer.transform.position.y - 1;
            if (anchorOnLaser)
            {
                item
                    .transform
                    .SetPositionAndRotation(new Vector3(trackedImageTransform
                            .position
                            .x,
                        yVal,
                        trackedImageTransform.position.z),
                    trackedImageTransform.rotation);
            }
            else
            {
                item
                    .transform
                    .SetPositionAndRotation(trackedImageTransform.position,
                    trackedImageTransform.rotation);
            }
        }
    }

    private void handleLaserManager(bool active)
    {
        laserManager.SetActive (active);
        if (!active)
        {
            laserManager.GetComponent<LaserManager>().DestroyAllLasers();
        }
    }
}
