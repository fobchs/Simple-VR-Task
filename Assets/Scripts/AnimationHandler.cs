using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ObsessVR.Animation
{
    public class AnimationHandler : MonoBehaviour
    {
        //Pop up animation Trigger
        public static void PopUpAnimationTrigger(Animator anim)
        {
            anim.SetTrigger("PopupTrigger");
        }

        //Disappear animation Trigger
        public static void DisappearAnimationTrigger(Animator anim)
        {
            anim.SetTrigger("DisappearTrigger");
        }

        //Clicked animation Trigger
        public static void ClickedAnimationTrigger(Animator anim)
        {
            anim.SetTrigger("ClickTrigger");
        }
    }
}
