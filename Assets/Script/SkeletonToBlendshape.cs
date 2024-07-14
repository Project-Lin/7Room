using UnityEngine;

public class BlendShapeController : MonoBehaviour
{
    public SkinnedMeshRenderer skinnedMeshRenderer; // 模型的SkinnedMeshRenderer組件
    public string blendShapeName; // 要控制的Blendshape名稱
    public Transform boneTransform; // 指定的骨骼Transform
    public float minRotationZ; // 骨骼Z軸旋轉的最小值(角度)
    public float maxRotationZ; // 骨骼Z軸旋轉的最大值(角度)
    public float minBlendShapeWeight; // Blendshape的最小權重值
    public float maxBlendShapeWeight; // Blendshape的最大權重值

    private int blendShapeIndex; // Blendshape的索引

    private void Start()
    {
        // 獲取指定Blendshape的索引
        blendShapeIndex = skinnedMeshRenderer.sharedMesh.GetBlendShapeIndex(blendShapeName);
    }

    private void Update()
    {
        // 獲取骨骼的當前Z軸旋轉值(角度)
        float currentRotationZ = boneTransform.localRotation.eulerAngles.z;

        // 計算骨骼Z軸旋轉值在指定範圍內的插值比例
        float lerpValue = Mathf.InverseLerp(minRotationZ, maxRotationZ, currentRotationZ);

        // 計算Blendshape的權重值
        float blendShapeWeight = Mathf.Lerp(minBlendShapeWeight, maxBlendShapeWeight, lerpValue);

        // 設置Blendshape的權重值
        skinnedMeshRenderer.SetBlendShapeWeight(blendShapeIndex, blendShapeWeight);
    }
}