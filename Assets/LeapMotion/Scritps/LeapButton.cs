using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LeapButton : baseOnClick
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override void OnEnter()
    {
        transform.GetChild(0).GetComponent<Text>().text = "进入";
    }

    public override void OnStay()
    {
        transform.GetChild(0).GetComponent<Text>().text = "完成";
    }

    public override void OnExit()
    {
        transform.GetChild(0).GetComponent<Text>().text = "按钮";
    }
}
