using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

/// <summary>
/// ��Ҫ��������ѡ���б�����ѡ��
/// </summary>
public class ChooseBackground : MonoBehaviour
{

    /// <summary>
    /// ��ȡ���ű�������Ϸ����
    /// </summary>
    public GameObject background;
    public VideoClip[] vc;
    
    /// <summary>
    /// ͨ��VALUE��ֵ�ı���Ƶ
    /// </summary>
    /// <param name="value">��Ƶ�������������</param>
    public void ChangeBackground(int value)
    {
        background.GetComponentInChildren<VideoPlayer>().clip= vc[value];
        //("�����Ѿ��ı�");
    }
}
