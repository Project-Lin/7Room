using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public  class  delete_Sence : MonoBehaviour
{
    public static List<GameObject> deleteObjects = new List<GameObject>();
    private GameObject deleteobj = null;
    
    
    //替換門牌號
    public Material[] newMaterials; // 新材質
    private Renderer targetRenderer; // 目標物體的Renderer組件

    
    
    
    public void AddSpawnedObject(GameObject obj)
    {
        deleteObjects.Add(obj);
    }
    
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (deleteObjects.Count > 4)
        {

            deleteobj = deleteObjects[0];
            deleteObjects.RemoveAt(0);
            Destroy(deleteobj);
                    
        }
    }
    public void ChangeMaterial(int index,GameObject targetObject)
    {
        targetRenderer = targetObject.GetComponent<Renderer>();
        if (targetRenderer != null && newMaterials != null && newMaterials.Length > 0)
        {
            if (index <= newMaterials.Length)
            {
                targetRenderer.material = newMaterials[index];
            }
            
        }
    }
}
