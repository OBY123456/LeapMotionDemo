using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class LeapButton : LeapBaseButton
{
    public override void HandEnter()
    {
        transform.DOScale(vector3, IntervalTime);
        
    }

    public override void HandExit()
    {
        transform.DOScale(new Vector3(1,1,1), IntervalTime);
    }
}
