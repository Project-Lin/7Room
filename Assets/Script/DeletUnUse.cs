using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeletUnUse : MonoBehaviour
{
    private GameObject currentGameObject;

    private void Awake()
    {
        // 在 Awake() 方法中獲取當前遊戲物體
        currentGameObject = gameObject;
    }
    
    public void DestroyUnUse()
    {
        Destroy(gameObject);
    }
    
}
