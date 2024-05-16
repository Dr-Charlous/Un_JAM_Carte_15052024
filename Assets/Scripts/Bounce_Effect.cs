using DG.Tweening;
using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoTween_Bounce : MonoBehaviour
{

    bool bouncing = true;
    public float BounceConstant = 0.2f;
    public float BounceDuration = 0.6f;
    public int BounceVibrato = 7;
    public float BounceElasticity = 0.7f;

    void OnMouseOver()
    {
        
    }

    private void OnMouseEnter()
    {
        Vector3 punch;
        punch = new Vector3(BounceConstant, BounceConstant, BounceConstant);

        if (bouncing == true)
        {
            //If your mouse hovers over the GameObject with the script attached, output this message
            bouncing = false;
            transform.DOComplete();
            transform.DOPunchScale(punch, BounceDuration, BounceVibrato, BounceElasticity);
            StartCoroutine(WaitTilBounceAgain());
        }
    }

    IEnumerator WaitTilBounceAgain()
    {
        yield return new WaitForSeconds(BounceDuration);
        bouncing = true;

    }

    void OnMouseExit()
    {
        //The mouse is no longer hovering over the GameObject so output this message each frame
        Debug.Log("Mouse is no longer on GameObject.");
        transform.DOKill();
        transform.DOScale(1.0f, .3f).SetEase(Ease.Linear);
        bouncing = true;
    }
}
