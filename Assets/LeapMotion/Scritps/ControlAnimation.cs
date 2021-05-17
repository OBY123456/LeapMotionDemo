using Leap;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Leap.Unity;
using DG.Tweening;
using UnityEngine.UI;
using MTFrame.MTEvent;
using System;
using Image = UnityEngine.UI.Image;

public class ControlAnimation : MonoBehaviour
{
    public static ControlAnimation Instance;

    [HideInInspector]
    public LeapProvider provider;
    public HandModelBase leftHandModel;
    public HandModelBase rightHandModel;

    private const float rotate_sensitive = 1500f;  //旋转灵敏度
    private const float displacement_sensitive = 0.00004f; //位移灵敏度
    private const float rotate_initial_value = 0f;  //旋转初始位置值

    public Text LogText,LogText2;
    public Transform Sphere;

    public Image[] images;
    private int CurrentImage = 0;
    private bool IsComplete = true;
    private float TweenTime = 0.5f;
    /// <summary>
    /// 判断条件  
    /// </summary>
    const float smallestVelocity = 0.1f;
    public float deltaVelocity = 0.7f;
    const float deltaCloseFinger = 0.06f;

   // public RectTransform HandImage;

    private void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        provider = FindObjectOfType<LeapProvider>() as LeapProvider;

        images[CurrentImage].gameObject.GetComponent<RectTransform>().localScale = Vector3.one * 2;
    }

    //private void AUpdate(float timeProcess = 0.5f)
    //{
    //    //双手同时在
    //    if (leftHandModel.IsTracked && rightHandModel.IsTracked)
    //    {
    //        //Zoom();
    //        //LogText.text = "双手都在";
    //        //Position();
    //        //Scale();
    //        Scale();
    //        DisplaySphere();
    //        return;

    //    }

    //    //双手同时不在
    //    if (!leftHandModel.IsTracked && !rightHandModel.IsTracked)
    //    {
    //        //Debug.Log("双手同时不在");
    //        LogText.text = "双手都不在";
    //        //transform.localEulerAngles = Vector3.zero;
    //        //transform.localScale = new Vector3(1, 1, 1);
    //    }

    //    if (leftHandModel.IsTracked || rightHandModel.IsTracked)
    //    {
    //        //Rotation2();
    //        //Scale();
    //        Position();

    //    }
    //}



    //private void FllowHandImage()
    //{
    //    Frame frame = provider.CurrentFrame;
    //    foreach (Hand hand in frame.Hands)
    //    {
    //        if(hand.IsRight)
    //        {
    //            HandImage.position = Camera.main.WorldToScreenPoint(new Vector3(hand.PalmPosition.x, hand.PalmPosition.y, hand.PalmPosition.z));
    //        }
    //    }
            
    //}

    void Update()
    {
        
        
        //Rotation();
        //Position();
        //双手同时在
        if (leftHandModel.IsTracked && rightHandModel.IsTracked)
        {
            //Zoom();
            //LogText.text = "双手都在";
            //Position();
            //Scale();

            //DisplaySphere();
            return;

        }

        //双手同时不在
        if (!leftHandModel.IsTracked && !rightHandModel.IsTracked)
        {
            //Debug.Log("双手同时不在");
            LogText.text = "双手都不在";
            //transform.localEulerAngles = Vector3.zero;
            //transform.localScale = new Vector3(1, 1, 1);
        }

        if ( rightHandModel.IsTracked)
        {
            //Rotation2();
            //Scale();
            //FllowHandImage();
            Position();
            Scale();
            //DisplaySphere();
            //OnClick();
        }
    }
    /// <summary>
    /// 缩小
    /// </summary>
    public void Scale()
    {
        Frame frame = provider.CurrentFrame;
        foreach (Hand hand in frame.Hands)
        {
            //if (isOpenFullHand(hand))
            //{
            //    //Debug.Log("大");
            //    LogText.text = "五指伸直——变大";
            //    //Vector3 value = transform.localScale;
            //    //value += new Vector3(value.x * 0.01f, value.y * 0.01f, value.z * 0.01f);
            //    ////    Debug.Log(value);
            //    //transform.localScale = value;
            //}

            //Hand LeftHand = leftHandModel.GetLeapHand();
            //Hand RightHand = rightHandModel.GetLeapHand();
            if (hand.IsRight && isCloseHand(hand))
            {
                //Debug.Log("小");
                LogText.text = "五指弯曲(握拳)——确定";
                LogMsg();
                //if (!Sphere.gameObject.activeSelf)
                //{
                //    Sphere.gameObject.SetActive(true);
                //}
                //Vector3 value = transform.localScale;
                //value -= new Vector3(value.x * 0.01f, value.y * 0.01f, value.z * 0.01f);
                ////   Debug.Log(value);
                //transform.localScale = value;
            }
        }
    }
    /// <summary>
    /// 旋转
    /// </summary>
    public void Rotation()
    {
        Frame frame = provider.CurrentFrame;
        foreach (Hand hand in frame.Hands)
        {
            if ((hand.IsLeft || hand.IsRight) && isCloseHand(hand))
            {
                Vector3 value = transform.localEulerAngles;
                value = new Vector3(hand.PalmPosition.y * rotate_sensitive + rotate_initial_value, hand.PalmPosition.x * rotate_sensitive + rotate_initial_value, 0);
                transform.localEulerAngles = value;
            }
            else
            {
                hand.PalmPosition.y = transform.localEulerAngles.x;
                hand.PalmPosition.x = transform.localEulerAngles.y;
            }
        }
    }

    private float time = 0.1f;
    public void Position()
    {
        Frame frame = provider.CurrentFrame;
        foreach (Hand hand in frame.Hands)
        {
            if (hand.IsLeft)
            {
                    //if (isMoveUp(hand))
                    //{
                    //    Vector3 vector3 = new Vector3(0, hand.PalmPosition.y * displacement_sensitive, 0) + transform.localPosition;
                    //    //transform.DOLocalMove(vector3, time);
                    //}

                    //if (isMoveDown(hand))
                    //{
                    //    Vector3 vector3 = new Vector3(0, -hand.PalmPosition.y * displacement_sensitive, 0) + transform.localPosition;
                    //    //transform.DOLocalMove(vector3, time);
                        
                    //}

                    //if (isMoveRight(hand))
                    //{
                    //    //Vector3 vector3 = new Vector3(hand.PalmPosition.x * displacement_sensitive, 0, 0) + transform.localPosition;
                    //    //transform.DOLocalMove(vector3, time);
                    //    LogText.text = "向左挥手"/*+ HandTimeLeft + "次"*/;
                    //    MoveImageRight();
                    //}

                //Debug.Log(hand.PalmPosition);

            }

            if(hand.IsRight)
            {
                if (isMoveLeft(hand))
                {
                    //Vector3 vector3 = new Vector3(-hand.PalmPosition.x * displacement_sensitive, 0, 0) + transform.localPosition;
                    //transform.DOLocalMove(vector3, time);
                    LogText.text = "向右挥手" /*+ HandTimeRight + "次"*/;
                    MoveImageLeft();
                    
                }

                if (isMoveRight(hand))
                {
                    //Vector3 vector3 = new Vector3(hand.PalmPosition.x * displacement_sensitive, 0, 0) + transform.localPosition;
                    //transform.DOLocalMove(vector3, time);
                    LogText.text = "向左挥手"/*+ HandTimeLeft + "次"*/;
                    MoveImageRight();
                }
            }
        }
    }

    /// <summary>
    /// 向左移动
    /// </summary>
    private void MoveImageLeft()
    {
        if(IsComplete)
        {
            //Debug.Log("MoveImageLeft");
            IsComplete = false;
            CurrentImage--;
            if(CurrentImage < 0)
            {
                CurrentImage = images.Length - 1;
            }
            foreach (var item in images)
            {
                item.gameObject.GetComponent<RectTransform>().localScale = Vector3.one;
            }

            images[CurrentImage].gameObject.GetComponent<RectTransform>().DOScale(Vector3.one * 2, TweenTime).OnComplete(() => {
                IsComplete = true;
            });
        }
    }

    /// <summary>
    /// 向右移动
    /// </summary>
    private void MoveImageRight()
    {
        if (IsComplete)
        {
            IsComplete = false;
            //Debug.Log("MoveImageRight");
            CurrentImage++;
            if (CurrentImage > images.Length - 1)
            {
                CurrentImage = 0;
            }
            foreach (var item in images)
            {
                item.gameObject.GetComponent<RectTransform>().localScale = Vector3.one;
            }

            images[CurrentImage].gameObject.GetComponent<RectTransform>().DOScale(Vector3.one * 2, TweenTime).OnComplete(() => {
                IsComplete = true;
            });
        }
    }

    private void Rotation2()
    {
        Frame frame = provider.CurrentFrame;
        RightHandPinch(frame);
        
    }

    //private void OnClick()
    //{
    //    Frame frame = provider.CurrentFrame;
    //    foreach (Hand hand in frame.Hands)
    //    {
    //        if(hand.IsRight && isMoveDown(hand))
    //        {
    //            LogMsg();
    //        }
    //    }
    //}

    private void LogMsg()
    {
        switch (CurrentImage)
        {
            case 0:
                LogText2.text = "A";
                break;
            case 1:
                LogText2.text = "B";
                break;
            case 2:
                LogText2.text = "C";
                break;
            case 3:
                LogText2.text = "D";
                break;
            default:
                break;
        }
    }

    protected bool isMoveRight(Hand hand)// 手划向右边
    {
        //Debug.Log(hand.PalmVelocity);
        return hand.PalmVelocity.x > deltaVelocity && !isStationary(hand);
    }
    protected bool isMoveLeft(Hand hand)   // 手划向左边
    {
        //Debug.Log(hand.PalmVelocity);
        //x轴移动的速度   deltaVelocity = 0.7f    isStationary (hand)  判断hand是否禁止
        return hand.PalmVelocity.x < -deltaVelocity && !isStationary(hand);
    }

    protected bool isMoveUp(Hand hand)   //手向上 
    {
        return hand.PalmVelocity.y > deltaVelocity && !isStationary(hand);
    }

    protected bool isMoveDown(Hand hand) //手向下  
    {
        return hand.PalmVelocity.y < -deltaVelocity && !isStationary(hand);
    }

    protected bool isStationary(Hand hand)// 固定不动的
    {
        return hand.PalmVelocity.Magnitude < smallestVelocity;
    }
    public bool isCloseHand(Hand hand)     //是否握拳
    {
        List<Finger> listOfFingers = hand.Fingers;
        int count = 0;
        for (int f = 0; f < listOfFingers.Count; f++)
        { //循环遍历所有的手~~
            Finger finger = listOfFingers[f];
            if ((finger.TipPosition - hand.PalmPosition).Magnitude < deltaCloseFinger)    // Magnitude  向量的长度 。是(x*x+y*y+z*z)的平方根。    //float  deltaCloseFinger = 0.05f;
            {
                count++;
                //  if (finger.Type == Finger.FingerType.TYPE_THUMB)
                //  Debug.Log ((finger.TipPosition - hand.PalmPosition).Magnitude);
            }
        }
        return (count == 5);
    }
    public bool isOpenFullHand(Hand hand)         //手掌全展开~
    {
        //Debug.Log (hand.GrabStrength + " " + hand.PalmVelocity + " " +  hand.PalmVelocity.Magnitude);
        //return hand.GrabStrength == 0;这个经过测试不行
        if (hand.Fingers[0].IsExtended && hand.Fingers[1].IsExtended
            && hand.Fingers[2].IsExtended && hand.Fingers[3].IsExtended
            && hand.Fingers[4].IsExtended)
        {
            return true;
        }
        return false;
    }
    /// <summary>
    /// 判断手指是否伸展或弯曲,下面是食指伸直,其它手指弯曲
    /// </summary>
    /// <param name="hand"></param>
    /// <param name="arr"></param>
    /// <returns></returns>
    private bool JudgeIndexDetector(Hand hand)
    {
        if (!hand.Fingers[0].IsExtended && hand.Fingers[1].IsExtended && !hand.Fingers[2].IsExtended && !hand.Fingers[3].IsExtended
            && !hand.Fingers[4].IsExtended)
        {
            return true;
        }
        return false;
    }

    /// <summary>
    /// 判断右手双指是否捏合
    /// </summary>
    /// <param name="frame"></param>
    public void RightHandPinch(Frame frame)
    {
        foreach (Hand hand in frame.Hands)
        {
            if (hand.IsRight)
            {
                if(!isCloseHand(hand))
                {
                    bool pinchBool = (hand.Fingers[0].TipPosition - hand.Fingers[1].TipPosition).Magnitude < 0.07f;
                    if (pinchBool)
                    {
                        LogText.text = "右手双指捏合——自转";
                        transform.Rotate(Vector3.down);
                    }
                }

            }
        }
    }

    float Palm_Mix_Distance = 0.1f;
    float Palm_Max_Distance = 0.3f;
    /// <summary>
    /// 判断合掌
    /// </summary>
    /// <param name="frame"></param>
    private void HeZhang(Frame frame)
    {
        if (frame.Hands.Count < 2)
            return;

        foreach (Hand hand in frame.Hands)
        {
            //左手张开
            if(hand.IsLeft)
            {
                if(!isOpenFullHand(hand))
                {
                    return;
                }
            }

            //右手张开
            if (hand.IsRight)
            {
                if (!isOpenFullHand(hand))
                {
                    return;
                }
            }

            Hand LeftHand = leftHandModel.GetLeapHand();
            Hand RightHand = rightHandModel.GetLeapHand();

            if (RightHand.PalmPosition.x - LeftHand.PalmPosition.x < Palm_Mix_Distance)
            {
                LogText.text = "合掌——变小";
                Vector3 value = transform.localScale;
                value -= new Vector3(value.x * 0.01f, value.y * 0.01f, value.z * 0.01f);
                //   Debug.Log(value);
                transform.localScale = value;
            }

            if(RightHand.PalmPosition.x - LeftHand.PalmPosition.x > Palm_Max_Distance)
            {
                LogText.text = "合掌——变大";
                Vector3 value = transform.localScale;
                value += new Vector3(value.x * 0.01f, value.y * 0.01f, value.z * 0.01f);
                //    Debug.Log(value);
                transform.localScale = value;
            }
        }
    }

    public void Zoom()
    {
        Frame frame = provider.CurrentFrame;
        HeZhang(frame);
    }

    public void DisplaySphere()
    {

        Frame frame = provider.CurrentFrame;
        foreach (Hand hand in frame.Hands)
        {
            Hand LeftHand = leftHandModel.GetLeapHand();
            Hand RightHand = rightHandModel.GetLeapHand();
            if (JudgeIndexDetector(LeftHand) && JudgeIndexDetector(RightHand))
            {
                LogText.text = "伸出食指——返回";
                if (Sphere.gameObject.activeSelf)
                {
                    Sphere.gameObject.SetActive(false);
                }
            }
        }
    }

    
}
