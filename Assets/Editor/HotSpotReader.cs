using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using Newtonsoft.Json;
using ObsessVR.HotSpot;
using System;

public class HotSpotReader : EditorWindow
{
    private static string inputJson;

    [MenuItem("GameObject/ObsessVR/Populate Hot Spots")]
    public static void ReadJson()
    {
        string path = EditorUtility.OpenFilePanel("Choose your JSON", "", "txt");
        inputJson = JsonReader.ReadJson(path);

        if (inputJson.Length > 0)
        {
            PopulateHotSpot(inputJson);
        }
    }

    public static void PopulateHotSpot (string input)
    {
        GameObject parentObj = GameObject.Find("HotSpots");
        if (parentObj == null)
        {
            parentObj = new GameObject();
            parentObj.name = "HotSpots";
        }

        List<GameObject> existHotSpots = GetChilds(parentObj.transform);

        try
        {
            List<HotSpotModel> hotSpots = JsonConvert.DeserializeObject<List<HotSpotModel>>(input);
            foreach (HotSpotModel temp in hotSpots)
            {
                HotSpotCreateHandler.CreateHotSpotWithModel(temp);
            }
            //StoreHotSpotJson(input);
            JsonReader.WriteJson(inputJson);

            if (existHotSpots.Count > 0)
            {
                DeleteChilds(existHotSpots);
            }
            Debug.Log("Populated");
            Debug.Log(Application.dataPath);
        }
        catch (Exception e)
        {
            Debug.Log(e);
        }
    }

    private static List<GameObject> GetChilds(Transform obj)
    {
        int children = obj.childCount;
        List<GameObject> returnVal = new List<GameObject>();
        for (int i = 0; i < children; ++i)
        {
            returnVal.Add(obj.GetChild(i).gameObject);
        }
        return returnVal;
    }
        
    private static void DeleteChilds(List<GameObject> objs)
    {
        for (int i = 0; i < objs.Count; i++)
        {
            DestroyImmediate(objs[i]);
        }
    }

    private static void StoreHotSpotJson(string input)
    {
        PlayerPrefs.SetString("inputJson", input);
    }
}
