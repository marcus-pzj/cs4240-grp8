 using System.Collections;
 using System.Collections.Generic;
 using UnityEngine;
 using UnityEngine.UI;
 public class EnemyFollow : MonoBehaviour
 {
 
     public Transform Player;
     public int MoveSpeed = 4;
     public int MaxDist = 100;
     public int MinDist = 1;
 
     void Start()
     {
 
     }
 
     void Update()
     {
         transform.LookAt(Player);
 
         if (Vector3.Distance(transform.position, Player.position) >= MinDist)
         {
 
             transform.position += transform.forward * MoveSpeed * Time.deltaTime;
 
             if (Vector3.Distance(transform.position, Player.position) <= MaxDist)
             {
                 //Here Call any function U want Like Shoot at here or something
             }
 
         }
     }
 }