﻿using System.Collections;
using UnityEngine;
using UnityEditor;

[RequireComponent(typeof(MeshFilter))]
[RequireComponent(typeof(MeshRenderer))]

public class MeshCombiner : MonoBehaviour {

    GameObject highlight;

    void Start()
    {
        MeshFilter[] meshFilters = GetComponentsInChildren<MeshFilter>();
        CombineInstance[] combine = new CombineInstance[meshFilters.Length];
        int i = 0;
        while (i < meshFilters.Length)
        {
            combine[i].mesh = meshFilters[i].sharedMesh;
            combine[i].transform = meshFilters[i].transform.localToWorldMatrix;
            meshFilters[i].gameObject.SetActive(false);
            i++;
        }

        transform.GetComponent<MeshFilter>().mesh = new Mesh();
        transform.GetComponent<MeshFilter>().mesh.CombineMeshes(combine);
        transform.gameObject.SetActive(true);

        //SaveMesh();
    }

    void SaveMesh()
    {
        Debug.Log("Saving Mesh");
        Mesh m1 = transform.GetComponent<MeshFilter>().mesh;
        AssetDatabase.CreateAsset(m1, transform.name + ".asset");
        AssetDatabase.SaveAssets();
    }
}