using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class ScrollViewTest : MonoBehaviour
{
    private ScrollRect scrollRect;

    public float Content_X = 0;

    // Start is called before the first frame update
    void Start()
    {
        scrollRect = this.GetComponent<ScrollRect>();
    }

    // Update is called once per frame
    void Update()
    {
        scrollRect.content.DOAnchorPosX(Content_X, 0.5f);
    }
}
