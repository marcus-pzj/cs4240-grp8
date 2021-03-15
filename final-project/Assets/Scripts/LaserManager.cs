using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserManager : MonoBehaviour {
    static public LaserManager instance;

    public GameObject LinePrefab;

    List<LaserGun> lasers = new List<LaserGun>();
    List<GameObject> lines = new List<GameObject>();

    float maxStepDistance = 20;

    public void AddLaser(LaserGun laser) {
        lasers.Add(laser);
    }

    public void RemoveLaser(LaserGun laser) {
        lasers.Remove(laser);
    }

    /**
    * returns:
    *  normalized Vector3 refracted by passing from one medium to another in a realistic manner according to Snell's Law
    *
    * parameters:
    *  RI1 - the refractive index of the first medium
    *  RI2 - the refractive index of the second medium
    *  surfNorm - the normal of the interface between the two mediums (for example the normal returned by a raycast)
    *  incident - the incoming Vector3 to be refracted
    *
    * usage example (laser pointed from a medium with RI roughly equal to air through a medium with RI roughly equal to water):
    *  Vector3 laserRefracted = Refract(1.0f, 1.33f, waterPointNorm, laserForward);
    */

    Vector3 Refract(float RI1, float RI2, Vector3 surfNorm, Vector3 incident) {
        surfNorm.Normalize(); //should already be normalized, but normalize just to be sure
        incident.Normalize();

        Vector3 refractedRay = (RI1/RI2 * Vector3.Cross(surfNorm, Vector3.Cross(-surfNorm, incident)) - surfNorm * Mathf.Sqrt(1 - Vector3.Dot(Vector3.Cross(surfNorm, incident)*(RI1/RI2*RI1/RI2), Vector3.Cross(surfNorm, incident)))).normalized;
        
        return refractedRay;
    }

    void RemoveOldLines(){
        if(lines.Count > 0){
            Destroy(lines[lines.Count - 1]);
            lines.RemoveAt(lines.Count - 1);
            RemoveOldLines();
        }
    }

    // Start is called before the first frame update
    void Awake() {
        instance = this;
    }

    // Update is called once per frame
    void Update() {   
        RemoveOldLines();
        foreach (LaserGun laser in lasers) {
            CalcLaserLine(laser.transform.position + laser.transform.forward*0.6f, laser.transform.forward);
        }
    }

    void CalcLaserLine(Vector3 startPosition, Vector3 direction) {
        RaycastHit hit;
        Ray ray = new Ray(startPosition, direction);
        bool intersect = Physics.Raycast(ray, out hit, maxStepDistance);

        Vector3 hitPosition = hit.point;
        if(lines.Count < 30) {
            if (!intersect) {
                hitPosition = startPosition + direction * maxStepDistance;
            }
            
            DrawLine(startPosition, hitPosition);

            if(intersect) {
                /**
                *  TODO:
                *   Set and retrieve refractive indexes from obstacle's property
                *   If obstacle is tagged as "Mirror" then reflect only by default
                *   Currently hardcoded values
                */
                float refractiveIndex1 = 1.5f; 
                float refractiveIndex2 = 1.0f; 

                if(CheckDirection(hit.transform.gameObject.transform.root.position, hitPosition, hit.normal)) {
                    refractiveIndex1 = 1.0f; 
                    refractiveIndex2 = 1.5f; 
                }

                if (Refract(refractiveIndex1, refractiveIndex2, hit.normal, direction) == Vector3.zero) {
                    CalcLaserLine(hitPosition, Vector3.Reflect(direction, hit.normal));  // Reflection
                    Debug.Log("Reflected");
                    Debug.Log(refractiveIndex1);
                }
                else {
                    CalcLaserLine(hitPosition, Refract(refractiveIndex1, refractiveIndex2, hit.normal, direction));   // Refraction
                    Debug.Log("Refracted");
                    Debug.Log(refractiveIndex1);                    
                }
            }
        }
    }

    void DrawLine(Vector3 startPosition, Vector3 finishPosition) {
        GameObject go = Instantiate(LinePrefab, Vector3.zero, Quaternion.identity);
        VolumetricLines.VolumetricLineBehavior line = go.GetComponent<VolumetricLines.VolumetricLineBehavior>();
        lines.Add(go);
        line.StartPos = startPosition;
        line.EndPos = finishPosition;
    }

    bool CheckDirection (Vector3 obstaclePosition, Vector3 hitPosition, Vector3 hitNormal) {
        float dist = Vector3.Distance(obstaclePosition, hitPosition);
        Vector3 newPosition = hitPosition + hitNormal;
        float distTemp = Vector3.Distance(obstaclePosition, newPosition);

        if (distTemp <= dist) {
            return true;
        } 
        
        else{
            return false;
        }
    }
}
