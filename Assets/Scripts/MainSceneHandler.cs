using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class MainSceneHandler : MonoBehaviour
{	
	void Awake ()
    {
        if (GameObject.Find("HotSpots").transform.childCount > 0)
        {
            JsonReader.InitLoadedJson();
        }
    }
}
