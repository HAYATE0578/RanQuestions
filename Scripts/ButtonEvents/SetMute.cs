using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

/// <summary>
/// 设置BGM静音的toggle按钮
/// </summary>
public class SetMute : MonoBehaviour
{
    /// <summary>
    /// 引擎设置videoplayer
    /// </summary>
    public GameObject vp;

    /// <summary>
    /// 把方法传入gameobj中，实现事件
    /// </summary>
    /// <param name="isMute">是否为静音</param>
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
