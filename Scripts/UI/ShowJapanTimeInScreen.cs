using System.Collections;
using System.Runtime;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 实时显示时间
/// </summary>
public class ShowJapanTimeInScreen : MonoBehaviour
{
    /// <summary>
    /// UI的时间文字栏
    /// </summary>
    Text text;

    /// <summary>
    /// 设置text的位置(Start启动)
    /// </summary>
    void Start()
    {
        text = this.GetComponent<Text>();
    }

    /// <summary>
    /// 实时更新时间(Update启动)
    /// </summary>
    void Update()
    {
        text.text = System.DateTime.Now.ToString();
    }
}
