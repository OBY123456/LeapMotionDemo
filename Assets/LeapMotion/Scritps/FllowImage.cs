using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using Leap.Unity;
using Leap;
using Image = UnityEngine.UI.Image;

public class FllowImage : MonoBehaviour
{
    public float Times = 2.0f;
    private bool IsComplete = true;
    [HideInInspector]
    public LeapProvider provider;
    public HandModelBase leftHandModel;
    public HandModelBase rightHandModel;

    Tweener tweener;

    private Image image;
    // Start is called before the first frame update
    void Start()
    {
        provider = FindObjectOfType<LeapProvider>() as LeapProvider;
        image = transform.GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        if (rightHandModel.IsTracked)
        {
            //Rotation2();
            //Scale();
            FllowHandImage();
            //OnClick();
        }
    }

    private void FllowHandImage()
    {
        Frame frame = provider.CurrentFrame;
        foreach (Hand hand in frame.Hands)
        {
            if (hand.IsRight)
            {
                transform.position = Camera.main.WorldToScreenPoint(new Vector3(hand.PalmPosition.x, hand.PalmPosition.y, hand.PalmPosition.z));
            }
        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Enter");
        if (collision.tag.Contains("Button"))
        {
            collision.GetComponent<baseOnClick>().OnEnter();
            image.fillAmount = 0;
            IsComplete = false;
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
       // Debug.Log("Stay");
        if (collision.tag.Contains("Button") && !IsComplete)
        {
            IsComplete = true;

            tweener = image.DOFillAmount(1, Times);
            tweener.OnComplete(() =>
            {
                collision.GetComponent<baseOnClick>().OnStay();
                //image.fillAmount = 1;
            }).SetEase(Ease.Linear);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        Debug.Log("Exit");
        collision.GetComponent<baseOnClick>().OnExit();
        tweener.Kill();
        image.fillAmount = 1;
    }
}
