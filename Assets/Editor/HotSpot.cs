using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using ObsessVR.HotSpot;

public class HotSpot : EditorWindow
{
    private float radius = 1.0f;
    private float ele = 0.0f;
    private float polar = 0.0f;

    [MenuItem("GameObject/ObsessVR/Hot Spot")]
    public static void ShowWindow()
    {
        GetWindow(typeof(HotSpot));
    }

    public void OnGUI()
    {
        GUILayout.Label("Insert Radius:");
        radius = EditorGUILayout.FloatField(radius);
        GUILayout.Label("Insert Ele (X):");
        ele = EditorGUILayout.FloatField(ele);
        GUILayout.Label("Insert Polar (Y):");
        polar = EditorGUILayout.FloatField(polar);

        if (GUILayout.Button("Create Hot Spot"))
        {
            HotSpotCreateHandler.CreateHotSpot(radius, ele, polar);
            Close();
        }
    }
}
