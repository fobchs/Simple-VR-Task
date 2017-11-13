using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ObsessVR.Animation;

public class ButtonHandler : MonoBehaviour
{
    //Button Clicked
	public void ButtonClicked()
    {
        StartCoroutine(ButtonClickedWithSec());
    }

    //Button Clicked with Disappearing
    public void ButtonClickedWithDisappear()
    {
        StartCoroutine(ButtonClickedWithDisappearWithSec());
    }

    IEnumerator ButtonClickedWithSec()
    {
        AnimationHandler.ClickedAnimationTrigger(transform.GetComponent<Animator>());
        yield return new WaitForSeconds(0.4f);
    }

    IEnumerator ButtonClickedWithDisappearWithSec()
    {
        AnimationHandler.DisappearAnimationTrigger(transform.GetComponent<Animator>());
        yield return new WaitForSeconds(0.4f);
    }
}
