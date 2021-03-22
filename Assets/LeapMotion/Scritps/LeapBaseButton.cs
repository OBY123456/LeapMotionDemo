using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using MTFrame.MTEvent;

public class LeapBaseButton:MonoBehaviour
{
    protected Vector3 vector3 = new Vector3(1.1f, 1.1f, 1.1f);
    protected float IntervalTime = 0.1f;

    public virtual void HandEnter()
    {

    }

    public virtual void HandExit()
    {

    }

    public virtual void HandSaty()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag.Contains("Player"))
        {
            HandEnter();
            SendButtonName();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag.Contains("Player"))
            HandExit();
    }

    private void OnCollisionStay(Collision collision)
    {
        HandSaty();
    }

    protected void SendButtonName()
    {
        EventParamete eventParamete = new EventParamete();
        eventParamete.AddParameter(this.name);
        EventManager.TriggerEvent(GenericEventEnumType.Message,"ButtonName", eventParamete);
    }
}
