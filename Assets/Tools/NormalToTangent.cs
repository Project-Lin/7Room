using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class NormalToTangent
{

    [MenuItem("美术/法線寫入切線")]
    public static void NormalToTangentMenu()
    {
        MeshFilter[] meshFilters = Selection.activeGameObject.GetComponentsInChildren<MeshFilter>();
        foreach (var meshFilter in meshFilters)
        {
            Mesh mesh = meshFilter.sharedMesh;
            WriteNormalToTangent(mesh);
        }

        SkinnedMeshRenderer[] skinnedMeshRenderers =
            Selection.activeGameObject.GetComponentsInChildren<SkinnedMeshRenderer>();
        foreach (var skinnedMeshRenderer in skinnedMeshRenderers)
        {
            Mesh mesh = skinnedMeshRenderer.sharedMesh;
            WriteNormalToTangent(mesh);
        }
    }

    // public static void WriteNormalToTangent(Mesh mesh)
    // {
    //     Dictionary<Vector3, Vector3> normalDict = new Dictionary<Vector3, Vector3>();
    //
    //     for (int i = 0; i < mesh.vertexCount; i++)
    //
    //     {
    //         if (normalDict.ContainsKey(mesh.vertices[i]))
    //         {
    //             normalDict.Add(mesh.vertices[i], mesh.normals[i]);
    //         }
    //         else
    //         {
    //             normalDict[mesh.vertices[i]] += mesh.normals[i];
    //         }
    //     }
    //
    //     Vector4[] tangents = null;
    //     bool hasTangents = mesh.tangents.Length == mesh.vertexCount;
    //     if (hasTangents)
    //     {
    //         tangents = mesh.tangents;
    //     }
    //     else
    //     {
    //         tangents = new Vector4[mesh.vertexCount];
    //     }
    //
    //
    //
    //     for (int i = 0; i < mesh.vertexCount; i++)
    //     {
    //         Vector3 avgNormal = normalDict[mesh.vertices[i]].normalized;
    //         tangents[i] = new Vector4(avgNormal.x, avgNormal.y, avgNormal.z, 0f);
    //     }
    //
    //     mesh.tangents = tangents;
    //     
    //     SaveMesh(mesh, mesh.name+"_NormalToTangent",true,true);
    //
    // }

    public static void WriteNormalToTangent(Mesh mesh)
    {
        Dictionary<Vector3, Vector3> normalDict = new Dictionary<Vector3, Vector3>();

        normalDict.Clear();
        for (int i = 0; i < mesh.vertexCount; i++)
        {
            if (!normalDict.ContainsKey(mesh.vertices[i]))
            {
                normalDict.Add(mesh.vertices[i], Vector3.zero);
            }
        }

        for (int i = 0; i < mesh.vertexCount; i++)
        {
            if (normalDict.ContainsKey(mesh.vertices[i]))
            {
                normalDict[mesh.vertices[i]] += mesh.normals[i];
            }
            else
            {
                normalDict.Add(mesh.vertices[i], mesh.normals[i]);
            }
        }

        Vector4[] tangents = null;
        bool hasTangents = mesh.tangents.Length == mesh.vertexCount;
        if (hasTangents)
        {
            tangents = mesh.tangents;
        }
        else
        {
            tangents = new Vector4[mesh.vertexCount];
        }

        for (int i = 0; i < mesh.vertexCount; i++)
        {
            Vector3 avgNormal = normalDict[mesh.vertices[i]].normalized;
            tangents[i] = new Vector4(avgNormal.x, avgNormal.y, avgNormal.z, 0f);
        }

        mesh.tangents = tangents;

        SaveMesh(mesh, mesh.name + "_NormalToTangent", true, true);
    }
    
    public static void SaveMesh(Mesh mesh, string name, bool mackNewInstance, bool optimizeMesh)
    {
        string path = EditorUtility.SaveFilePanel("Save Mesh", "Assets/", name, "asset");
        
        if(string.IsNullOrEmpty(path)) return;
        
        path = FileUtil.GetProjectRelativePath(path);
        
        Mesh meshToSave = (mackNewInstance) ? Object.Instantiate(mesh) : mesh;
        
        if(optimizeMesh) MeshUtility.Optimize(meshToSave);
        
        AssetDatabase.CreateAsset(meshToSave, path);
        
        AssetDatabase.SaveAssets();
        
    }
}