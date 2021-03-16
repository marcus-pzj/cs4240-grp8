using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;


[RequireComponent(typeof(ARTrackedImageManager))]
public class TrackedPrefabManager : MonoBehaviour
{
    [SerializeField]
    private GameObject[] placeablePrefabs;

    private ARTrackedImageManager manager;
    private Dictionary<string, GameObject> spawnedPrefabs = new Dictionary<string, GameObject>();

    private void Awake()
    {
        manager = GetComponent<ARTrackedImageManager>();

        foreach (GameObject prefab in placeablePrefabs)
        {
            GameObject newPrefab = Instantiate(prefab, Vector3.zero, Quaternion.identity);
            newPrefab.name = prefab.name;
            spawnedPrefabs.Add(prefab.name, prefab);
        }
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
            spawnedPrefabs[trackedImage.referenceImage.name].SetActive(true);
        }

        foreach (var trackedImage in e.updated)
        {
            UpdateImage(trackedImage);
        }
        foreach (var trackedImage in e.removed)
        {
            spawnedPrefabs[trackedImage.name].SetActive(false);
        }
    }

    private void UpdateImage(ARTrackedImage trackedImage)
    {
        string name = trackedImage.referenceImage.name;
        GameObject prefab = spawnedPrefabs[name];

        if (trackedImage.trackingState == TrackingState.Tracking)
        {
            prefab.transform.position = trackedImage.transform.position;
            prefab.transform.rotation = trackedImage.transform.rotation;
        }
        else
        {
            prefab.SetActive(false);
        }

        //foreach (GameObject go in spawnedPrefabs.Values)
        //{

        //}
    }
}
