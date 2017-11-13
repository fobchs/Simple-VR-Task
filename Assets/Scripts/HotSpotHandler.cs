using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ObsessVR.DetailPanel;

public class HotSpotHandler : MonoBehaviour {

    public HotSpotModel hotSpot;

    private Color startColor = Color.white;
    private Color targetColor = Color.white;
    private Renderer rend;
    private float duration = 5.0f;

    void Start()
    {
        rend = transform.GetComponent<MeshRenderer>();
        startColor = rend.material.color;
        targetColor = rend.material.color;   
    }
    void Update()
    {
        rend.material.color = Color.Lerp(rend.material.color, targetColor, Time.deltaTime * duration);
    }

    public void PointerEnter()
    {
        Debug.Log("Object Entered");

        targetColor = Color.blue;
    }

    public void PointerExit()
    {
        Debug.Log("Object Exited");

        targetColor = startColor;
    }

    public void PointerClick()
    {
        Debug.Log("Object Click");
        DetailPanelCreateHandler.CreateDetailPanel(hotSpot);
    }
}
