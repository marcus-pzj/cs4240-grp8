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

    public void DestroyAllLasers() {
        foreach (GameObject laser in GameObject.FindGameObjectsWithTag("Laser")) {
            Destroy(laser);
        }
    }

    Vector3 Refract(float RI1, float RI2, Vector3 surfNorm, Vector3 incident) {
        surfNorm.Normalize(); //should already be normalized, but normalize just to be sure
        incident.Normalize();

        Vector3 refractedRay = (RI1/RI2 * Vector3.Cross(surfNorm, Vector3.Cross(-surfNorm, incident)) - surfNorm * Mathf.Sqrt(1 - Vector3.Dot(Vector3.Cross(surfNorm, incident)*(RI1/RI2*RI1/RI2), Vector3.Cross(surfNorm, incident)))).normalized;
        // float N_dot_L = Vector3.Dot(surfNorm, incident);
        // float RI1_over_RI2 = RI1/RI2;
        // float One_Minus_N_dot_L_squared = 1 - (N_dot_L*N_dot_L);
        // float subcomponent = Mathf.Sqrt(1 - ((RI1_over_RI2 * RI1_over_RI2) * One_Minus_N_dot_L_squared));
        // Vector3 refractedRay = (((RI1_over_RI2 * N_dot_L) - subcomponent) * surfNorm) - (RI1_over_RI2 * incident);

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

        /**
        *  TODO:
        *   Improve stop-gap measure to set to an arbitrary limit of lines allowed to prevent infinite recursive calls
        */

        if(lines.Count < 30) {
            if (!intersect) {
                // Advance line by another step
                hitPosition = startPosition + direction * maxStepDistance;
            }
            
            DrawLine(startPosition, hitPosition);

            if(intersect) {
                float incidentRI, refractedRI;

                Transform parentObstacleTransform = hit.transform.gameObject.transform.root;
                GameObject parentObstacleGO = parentObstacleTransform.gameObject;

                float obstacleRI = parentObstacleGO.GetComponent<Obstacle>().RefractiveIndex;
                bool isMirror = parentObstacleGO.GetComponent<Obstacle>().isMirror;

                /**
                *  NOTE:
                *   Working with assumption that one of the medium is ALWAYS vacuum
                *   Possible rework needed if obstacle-to-obstacle light path is allowed
                */

                if(CheckDirection(parentObstacleTransform.position, hitPosition, hit.normal)) {
                    incidentRI = 1.0f; // RI of vacuum
                    refractedRI = obstacleRI; 
                }

                else {
                    incidentRI = obstacleRI; 
                    refractedRI = 1.0f; // RI of vacuum
                }

                if (isMirror || Refract(incidentRI, refractedRI, hit.normal, direction) == Vector3.zero) {
                    CalcLaserLine(hitPosition, Vector3.Reflect(direction, hit.normal));  // Reflection
                }
                else {
                    // Advance the next line start position by a marginally small amount to prevent intra-planar collisions
                    // You can perceive this as the thickness of the obstacle wall
                    CalcLaserLine(hitPosition + 0.1f * direction, Refract(incidentRI, refractedRI, hit.normal, direction));   // Refraction
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
