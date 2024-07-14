using UnityEngine;
using System.Collections;

public class RandomBlendShapeController : MonoBehaviour
{
    public SkinnedMeshRenderer skinnedMeshRenderer; // 模型的SkinnedMeshRenderer組件
    public string[] blendShapeNames; // 要控制的Blendshape名稱列表
    public float minInterval = 1f; // 隨機時間範圍的最小值(秒)
    public float maxInterval = 3f; // 隨機時間範圍的最大值(秒)
    public float alltransitionTime = 0.5f; // 過渡時間


    private int[] blendShapeIndices; // Blendshape的索引列表
    private int currentIndex = -1; // 當前激活的Blendshape索引

    private void Start()
    {
        // 初始化Blendshape索引列表
        blendShapeIndices = new int[blendShapeNames.Length];
        for (int i = 0; i < blendShapeNames.Length; i++)
        {
            blendShapeIndices[i] = skinnedMeshRenderer.sharedMesh.GetBlendShapeIndex(blendShapeNames[i]);
        }

        // 開始隨機設置Blendshape
        StartCoroutine(RandomizeBlendShape());
    }

    private IEnumerator RandomizeBlendShape()
    {
        while (true)
        {
            // 隨機選擇一個新的Blendshape索引
            int newIndex = Random.Range(0, blendShapeIndices.Length);
            while (newIndex == currentIndex)
            {
                newIndex = Random.Range(0, blendShapeIndices.Length);
            }

            // 將當前激活的Blendshape權重逐漸過渡到0
            if (currentIndex >= 0)
            {
                StartCoroutine(TransitionBlendShape(currentIndex, 0f));
            }

            // 將新選擇的Blendshape權重逐漸過渡到100
            StartCoroutine(TransitionBlendShape(newIndex, 100f));

            // 更新當前激活的Blendshape索引
            currentIndex = newIndex;

            // 等待一個隨機的時間間隔
            float interval = Random.Range(minInterval, maxInterval);
            yield return new WaitForSeconds(interval);
        }
    }

    private IEnumerator TransitionBlendShape(int index, float targetWeight)
    {
        // 隨機選擇一個過渡時間
        float transitionTime = alltransitionTime ;
        float elapsedTime = 0f;
        float startWeight = skinnedMeshRenderer.GetBlendShapeWeight(blendShapeIndices[index]);

        while (elapsedTime < transitionTime)
        {
            // 計算當前的權重值
            float weight = Mathf.Lerp(startWeight, targetWeight, elapsedTime / transitionTime);
            skinnedMeshRenderer.SetBlendShapeWeight(blendShapeIndices[index], weight);

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // 確保最終權重值為目標值
        skinnedMeshRenderer.SetBlendShapeWeight(blendShapeIndices[index], targetWeight);
    }
}