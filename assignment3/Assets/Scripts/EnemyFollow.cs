 using System.Collections;
 using System.Collections.Generic;
 using UnityEngine;
 using UnityEngine.UI;
 public class EnemyFollow : MonoBehaviour
 {
 
     public int MoveSpeed = 4;
     public int MaxDist = 100;
     public int MinDist = 1;

     void Start()
     {
 
     }
 
     void Update()
     {
        GameObject player = GameObject.FindWithTag("Player");
         transform.LookAt(player.transform);
 
         if (Vector3.Distance(transform.position, player.transform.position) >= MinDist)
         {
 
             transform.position += transform.forward * MoveSpeed * Time.deltaTime;
 
             if (Vector3.Distance(transform.position, player.transform.position) <= MaxDist)
             {
                 //Here Call any function U want Like Shoot at here or something
             }
 
         }
     }
 }
