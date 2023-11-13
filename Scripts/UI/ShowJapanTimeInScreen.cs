using System.Collections;
using System.Runtime;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// ʵʱ��ʾʱ��
/// </summary>
public class ShowJapanTimeInScreen : MonoBehaviour
{
    /// <summary>
    /// UI��ʱ��������
    /// </summary>
    Text text;

    /// <summary>
    /// ����text��λ��(Start����)
    /// </summary>
    void Start()
    {
        text = this.GetComponent<Text>();
    }

    /// <summary>
    /// ʵʱ����ʱ��(Update����)
    /// </summary>
    void Update()
    {
        text.text = System.DateTime.Now.ToString();
    }
}
