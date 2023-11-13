using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

/// <summary>
/// ����BGM������toggle��ť
/// </summary>
public class SetMute : MonoBehaviour
{
    /// <summary>
    /// ��������videoplayer
    /// </summary>
    public GameObject vp;

    /// <summary>
    /// �ѷ�������gameobj�У�ʵ���¼�
    /// </summary>
    /// <param name="isMute">�Ƿ�Ϊ����</param>
    public void SetBGMMute(bool isMute)
    {
        if (isMute)
        {
            vp.GetComponent<VideoPlayer>().SetDirectAudioMute(0, true);
        }
        else
        {
            vp.GetComponent<VideoPlayer>().SetDirectAudioMute(0, false);
        }
    }
}
