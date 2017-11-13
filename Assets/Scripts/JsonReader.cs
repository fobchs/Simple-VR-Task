using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;

public static class JsonReader
{
    public static string jsonName = "StartingAppData";
    public static string path = Application.dataPath + "/Resources/" + jsonName + ".txt";

    //initial json load during Application launched
    public static string InitLoadedJson()
    {
        string output = "";
        output = ReadJson(path);

        Debug.Log("Read Input Json: " + output);

        PopulateParams(output);
        return output;
    }

    public static string ReadJson(string path)
    {
        string inputJson = "";

        try
        {
            if (Application.platform == RuntimePlatform.Android || Application.platform == RuntimePlatform.IPhonePlayer)
            {
                inputJson = Resources.Load<TextAsset>(jsonName).text;
            }
            else 
            {
                WWW www = new WWW(path);
                inputJson = www.text;
                //inputJson = Resources.Load<TextAsset>(jsonName).text;
            }
            
        }
        catch (Exception e)
        {
            Debug.Log(e);
        }
        
        return inputJson;
    }

    public static void WriteJson(string json)
    {
        StreamWriter sr = File.CreateText(path);
        sr.Write(json);
        sr.Close();
        Debug.Log("Read Output Json: " + json);
    }

    //populate params to its hotspot
    public static void PopulateParams(string input)
    {
        try
        {
            List<HotSpotModel> hotSpots = JsonConvert.DeserializeObject<List<HotSpotModel>>(input);
            foreach (HotSpotModel temp in hotSpots)
            {
                GameObject tempObj = GameObject.Find(temp.productName);
                if (tempObj != null)
                {
                    tempObj.GetComponent<HotSpotHandler>().hotSpot = temp;
                    Debug.Log(temp.productName + " added.");
                }
            }
        }
        catch (Exception e)
        {
            Debug.Log(e);
        }
    }
}
