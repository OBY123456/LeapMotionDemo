using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Leap;
using Leap.Unity;

public class FellowHand : MonoBehaviour
{
    public Transform Hand;
    private bool IsDrag = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        if(ControlAnimation.Instance.rightHandModel.IsTracked)
        {
            if (IsDrag)
            {
                transform.localPosition = Hand.position;
            }
            else
            {
                transform.localPosition = new Vector3(0, 0, 0.75f);
            }

            if(!HandIsDrag())
            {
                IsDrag = false;
            }
            //Frame frame = ControlAnimation.Instance.provider.CurrentFrame;
            //foreach (Hand hand in frame.Hands)
            //{
            //    if (hand.IsRight)
            //    {
            //        Debug.Log(ControlAnimation.Instance.isOpenFullHand(hand));
            //    }
            //}
            
        }
        else
        {
            transform.localPosition = new Vector3(0, 0, 0.75f);
        }

       
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag.Contains("Player") && HandIsDrag())
        {
            IsDrag = true;
        }
    }

    private bool HandIsDrag()
    {
        Frame frame = ControlAnimation.Instance.provider.CurrentFrame;
        foreach (Hand hand in frame.Hands)
        {
            if(hand.IsRight)
            {
                return ControlAnimation.Instance.isOpenFullHand(hand);
            }
        }
        return false;
    }

    
}
