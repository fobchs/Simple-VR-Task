using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ObsessVR.DetailPanel
{
    public class DetailPanelCreateHandler : MonoBehaviour
    {
        public static float defaultRadius = 6.0f;

        //if Detail Panel is existing, destroy it
        public static void FindDetailPanelAndDestroyIt()
        {
            GameObject currentPanel = GameObject.Find("Detail Panel");
            if (currentPanel != null)
            {
                Destroy(currentPanel);
            }
        }

        //Create Panel with default radius
        public static void CreateDetailPanel(HotSpotModel hotSpot)
        {
            CreateDetailPanel(hotSpot, defaultRadius);
        }

        //Create Panel with custom radius
        public static void CreateDetailPanel(HotSpotModel hotSpot, float radius)
        {
            FindDetailPanelAndDestroyIt();

            GameObject tempPanel = (GameObject)Instantiate(Resources.Load("Prefabs/DetailPanel"));
            tempPanel.name = "Detail Panel";
            float posX = Camera.main.transform.position.x + radius * Mathf.Sin(hotSpot.polar * Mathf.Deg2Rad);
            float posY = Camera.main.transform.position.y + radius * Mathf.Tan(-hotSpot.ele * Mathf.Deg2Rad);
            float posZ = Camera.main.transform.position.z + radius * Mathf.Cos(hotSpot.polar * Mathf.Deg2Rad);
            tempPanel.transform.position = new Vector3(posX, posY, posZ);
            tempPanel.transform.eulerAngles = new Vector3(hotSpot.ele, hotSpot.polar, 0);
            tempPanel.GetComponent<DetailPanelHandler>().hotSpot = hotSpot;
        }
    }
}
