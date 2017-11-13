using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

//Modified the opensource from NoskeWiki

public class PhotoSphere : EditorWindow
{
    private float scale = 1.0f;
    private Object source;

    [MenuItem("GameObject/ObsessVR/Photo Sphere")]
    public static void ShowWindow()
    {
        GetWindow(typeof(PhotoSphere));
    }

    public void OnGUI()
    {
        GUILayout.Label("Insert Radius:");
        scale = EditorGUILayout.FloatField(scale);
        GUILayout.Label("Insert Material:");
        source = EditorGUILayout.ObjectField(source, typeof(Material), true);

        if (GUILayout.Button("Create Photo Sphere"))
        {
            CreatePhotoSphere(scale, source);
            Close();
        }
    }

    private void CreatePhotoSphere(float size, Object source)
    {
        GameObject tempSphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        MeshFilter mf = tempSphere.GetComponent<MeshFilter>();
        Mesh mesh = mf.sharedMesh;

        GameObject reveredSphere = new GameObject();
        reveredSphere.name = "Photo Sphere";
        MeshFilter mfNew = reveredSphere.AddComponent<MeshFilter>();
        mfNew.sharedMesh = new Mesh();

        //Scale the vertices;
        Vector3[] vertices = mesh.vertices;
        for (int i = 0; i < vertices.Length; i++)
            vertices[i] = vertices[i];
        mfNew.sharedMesh.vertices = vertices;

        // Reverse the triangles
        int[] triangles = mesh.triangles;
        for (int i = 0; i < triangles.Length; i += 3)
        {
            int t = triangles[i];
            triangles[i] = triangles[i + 2];
            triangles[i + 2] = t;
        }
        mfNew.sharedMesh.triangles = triangles;

        // Reverse the normals;
        Vector3[] normals = mesh.normals;
        for (int i = 0; i < normals.Length; i++)
            normals[i] = -normals[i];
        mfNew.sharedMesh.normals = normals;


        mfNew.sharedMesh.uv = mesh.uv;
        mfNew.sharedMesh.uv2 = mesh.uv2;
        mfNew.sharedMesh.RecalculateBounds();

        // Add the same material that the original sphere used
        MeshRenderer mr = reveredSphere.AddComponent<MeshRenderer>();
        mr.sharedMaterial = tempSphere.GetComponent<Renderer>().sharedMaterial;

        DestroyImmediate(tempSphere);

        reveredSphere.transform.localScale = Vector3.one * size * 2;
        reveredSphere.GetComponent<MeshRenderer>().sharedMaterial = (Material)source;
        reveredSphere.GetComponent<MeshRenderer>().receiveShadows = false;
        reveredSphere.GetComponent<MeshRenderer>().shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.Off;
    }
}