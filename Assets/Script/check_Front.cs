using System.Collections;
using UnityEngine;
using System.Collections.Generic;


public class check_Front : delete_Sence
{
    public GameObject prefabToSpawn; // 要移動和旋轉的物體

    private bool isTouched = false; // 是否已經觸碰過檢查點

    public Transform targetPosition;

    
    
    private void Start()
    {
        if (deleteObjects.Count == 0)
        {
            
            deleteObjects.Add(GameObject.Find("SENCE"));

        }
        
    }
    private void OnTriggerEnter(Collider other)
    {
        // 檢查碰撞物體是否為玩家,並且尚未觸碰過檢查點
        if (other.CompareTag("Player") && !isTouched)
        {
            isTouched = true; // 標記為已觸碰

            
            if (prefabToSpawn != null)
            {
                GameObject spawnedObject = Instantiate(prefabToSpawn, targetPosition.position, targetPosition.rotation);
                deleteObjects.Add(spawnedObject); 

                

            }
            
            
            
        }

    }
    


   


}