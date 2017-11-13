using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ObsessVR.HotSpot
{
    public class HotSpotCreateHandler : MonoBehaviour
    {
        public static void CreateHotSpot(float distance, float ele, float polar)
        {
            GameObject tempSphere = (GameObject) Instantiate(Resources.Load("Prefabs/HotSpot"));
            tempSphere.name = "Hot Spot";
            MeshFilter mf = tempSphere.GetComponent<MeshFilter>();
            Mesh mesh = mf.sharedMesh;

            UpdatePosition(tempSphere, distance, ele, polar);
            GameObject parentObj = GameObject.Find("HotSpots");
            if (parentObj == null)
            {
                parentObj = new GameObject();
                parentObj.name = "HotSpots";
            }
            tempSphere.transform.parent = parentObj.transform;
        }

        public static void CreateHotSpotWithModel(HotSpotModel input)
        {
            GameObject tempSphere = (GameObject)Instantiate(Resources.Load("Prefabs/HotSpot"));
            tempSphere.name = input.productName;
            MeshFilter mf = tempSphere.GetComponent<MeshFilter>();
            Mesh mesh = mf.sharedMesh;

            UpdatePosition(tempSphere, input.radius, input.ele, input.polar);
            tempSphere.transform.parent = GameObject.Find("HotSpots").transform;
        }

        private static void UpdatePosition(GameObject obj, float distance, float ele, float polar)
        {
            float posX = distance * Mathf.Cos(Mathf.Deg2Rad * -ele) * Mathf.Sin(Mathf.Deg2Rad * polar);
            float posY = distance * Mathf.Sin(Mathf.Deg2Rad * -ele);
            float posZ = distance * Mathf.Cos(Mathf.Deg2Rad * -ele) * Mathf.Cos(Mathf.Deg2Rad * polar);
            obj.transform.position = new Vector3(posX, posY, posZ);
            //obj.GetComponent<MeshRenderer>().sharedMaterial = AssetDatabase.GetBuiltinExtraResource<Material>("Sprites-Default.mat");
        }
    }
}
