
using UnityEngine;
using System.Collections.Generic;
using Unity.VisualScripting;

public class check_Back : delete_Sence
{
    public GameObject prefabToSpawn_Correct; // 正確房間
    public GameObject prefabToSpawn_Wrong; // 錯誤房間

    public GameObject prefabToSpawn = null; // 抽取房間模板
    
    private bool isTouched = false; // 是否已經觸碰過檢查點 熱檢查點指觸發一次 玩家出房間

    public GameObject objectToDestory; //刪除原本的轉角
    public Transform targetPosition_Back; //生成正確房間的位置
    
    public Transform targetPosition_Front; //生成錯誤房間的位置

    private void Start()
    {
   
        if (deleteObjects.Count == 0)
        {
            
            deleteObjects.Add(GameObject.Find("base_Scenes_v01"));

        }
        
    }
    private void OnTriggerEnter(Collider other)
    {
        // 檢查碰撞物體是否為玩家,並且尚未觸碰過檢查點
        if (other.CompareTag("Player") && !isTouched )
        {
            isTouched = true; // 標記為已觸碰
            

            
            if (prefabToSpawn_Correct != null && prefabToSpawn_Wrong != null)
            {
                //決定下一個房間是正確房間還是錯誤房間
                int ramdomNumb = Random.Range(0, 3);

                if (ramdomNumb <2)
                {
                    prefabToSpawn = prefabToSpawn_Correct;
                }
                else
                {
                    prefabToSpawn = prefabToSpawn_Wrong;
                }
                
                //創建房間 並更改門牌號碼
                if (ramdomNumb < 2) //正確房間
                {

                    
                    GameObject spawnedObject = Instantiate(prefabToSpawn, targetPosition_Back.position, targetPosition_Back.rotation);
                    deleteObjects.Add(spawnedObject);
                    

                    GameObject spawnedObject1 = Instantiate(prefabToSpawn, targetPosition_Front.position, targetPosition_Front.rotation);
                    deleteObjects.Add(spawnedObject1);




                    
                }
                else //錯誤房間
                {
                    
                    GameObject spawnedObject = Instantiate(prefabToSpawn, targetPosition_Back.position, targetPosition_Back.rotation);
                    deleteObjects.Add(spawnedObject);
                    
                    
                    GameObject spawnedObject1 = Instantiate(prefabToSpawn, targetPosition_Front.position, targetPosition_Front.rotation);
                    deleteObjects.Add(spawnedObject1);



                }

                
                
            }

            if (objectToDestory != null)
            {
                Destroy(objectToDestory); 
            }

            

        }

    }

    
    void Update()
    {

    }

    
    

    

}